﻿using BlazorFormDesigner.BusinessLogic.Models;
using System;
using System.Threading.Tasks;

namespace BlazorFormDesigner.BusinessLogic.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByUsername(string id);
        Task<User> Create(User user, string password);
        Task<User> Update(User user);
        Task<User> Delete(string username);
        Task<User> ChangePassword(string username, string password);
        Task<User> ValidatePassword(string username, string password);
        Task RegisterAnswer(string username, string formId);
        Task RegisterDismiss(string username, string formId);
        Task RegisterCreator(string username, string formId);
        Task RegisterStart(string username, string id, DateTime dateTime);
    }
}
