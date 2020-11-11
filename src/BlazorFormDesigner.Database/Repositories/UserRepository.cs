﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using BlazorFormDesigner.BusinessLogic.Exceptions;
using BlazorFormDesigner.BusinessLogic.Interfaces;
using BlazorFormDesigner.BusinessLogic.Models;
using BlazorFormDesigner.Database.Converters;
using BlazorFormDesigner.Database.Settings;
using MongoDB.Driver;

namespace BlazorFormDesigner.Database.Repositories
{
    public class UserRepository : AppRepository, IUserRepository
    {
        private readonly IMongoCollection<Entities.User> users;

        public UserRepository(DatabaseSettings settings, IMapper mapper) : base(settings, mapper)
        {
            users = database.GetCollection<Entities.User>(settings.UsersCollectionName);
        }

        public async Task<User> GetByUsername(string id)
        {
            var result = await findByUserame(id);
            return result.ToModel(mapper);
        }

        public async Task<User> Create(User user, string password)
        {
            if (await findByUserame(user.Username) != null) throw new DuplicateUsernameException(user.Username);

            var (hash, salt) = GenerateHash(password);

            var entity = user.ToEntity(mapper).WithPassword(hash, salt);
            await users.InsertOneAsync(entity);
            return entity.ToModel(mapper);
        }

        public async Task<User> Update(User user)
        {
            var result = await findByUserame(user.Username);
            if (result == null) throw new InvalidUsernameException();

            var entity = user.ToEntity(mapper).WithPassword(result.Password, result.Salt);
            await users.ReplaceOneAsync(u => u.Username == entity.Username, entity);
            return entity.ToModel(mapper);
        }

        public async Task<User> Delete(string username)
        {
            var entity = await users.FindOneAndDeleteAsync(u => u.Username == username);
            return entity.ToModel(mapper);
        }

        public async Task<User> ChangePassword(string username, string password)
        {
            var user = await users.Find(u => u.Username == username).FirstOrDefaultAsync();
            if (user == null) throw new InvalidUsernameException();

            var (hash, salt) = GenerateHash(password);
            var entity = user.WithPassword(hash, salt);
            await users.ReplaceOneAsync(u => u.Username == entity.Username, entity);

            return user.ToModel(mapper);
        }

        public async Task<User> ValidatePassword(string username, string password)
        {
            var user = await users.Find(u => u.Username == username).FirstOrDefaultAsync();
            if (user == null) throw new InvalidUsernameException();

            if (Hash(password, user.Salt) != user.Password) throw new InvalidPasswordException();

            return user.ToModel(mapper);
        }

        private async Task<Entities.User> findByUserame(string username)
        {
            var result = await users.Find(user => user.Username == username).FirstOrDefaultAsync();
            return result;
        }

        public async Task RegisterAnswer(string username, string formId)
        {
            var user = await users.Find(u => u.Username == username).FirstOrDefaultAsync();
            if (user == null) throw new InvalidUsernameException();
            if (user.AnsweredForms.Contains(formId) || user.DismissedForms.Contains(formId)) throw new FormException("Already answered or dismissed.");
            if (user.StartedForms[formId].AddSeconds(15) < DateTime.Now) throw new FormException("Expired. Too long answer time.");

            user.AnsweredForms.Add(formId);
            await users.ReplaceOneAsync(u => u.Username == user.Username, user);
        }

        public async Task RegisterDismiss(string username, string formId)
        {
            var user = await users.Find(u => u.Username == username).FirstOrDefaultAsync();
            if (user == null) throw new InvalidUsernameException();

            user.DismissedForms.Add(formId);
            await users.ReplaceOneAsync(u => u.Username == user.Username, user);
        }

        public async Task RegisterCreator(string username, string formId)
        {
            var user = await users.Find(u => u.Username == username).FirstOrDefaultAsync();
            if (user == null) throw new InvalidUsernameException();

            user.CreatedForms.Add(formId);
            await users.ReplaceOneAsync(u => u.Username == user.Username, user);
        }

        public async Task RegisterStart(string username, string id, DateTime dateTime)
        {
            var user = await users.Find(u => u.Username == username).FirstOrDefaultAsync();
            if (user == null) throw new InvalidUsernameException();

            user.StartedForms.Add(id, dateTime);
            await users.ReplaceOneAsync(u => u.Username == user.Username, user);
        }
    }
}

