using System;
using System.Security.Cryptography;
using AutoMapper;
using MongoDB.Driver;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using BlazorFormDesigner.Database.Settings;

namespace BlazorFormDesigner.Database.Repositories
{
    public class AppRepository
    {
        protected readonly DatabaseSettings settings;
        protected readonly IMapper mapper;
        protected readonly IMongoDatabase database;

        public AppRepository(DatabaseSettings settings, IMapper mapper)
        {
            this.mapper = mapper;
            this.settings = settings;

            var client = new MongoClient(settings.ConnectionString);

            database = client.GetDatabase(settings.DatabaseName);
        }

        protected (string hash, byte[] salt) GenerateHash(string password)
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create()) { rng.GetBytes(salt); }

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8));

            return (hashed, salt);
        }

        protected string Hash(string password, byte[] salt)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8));

            return hashed;
        }
    }

    public static class Filters
    {
        public static IFindFluent<T, T> FindAll<T>(this IMongoCollection<T> collection)
        {
            return collection.Find(entity => true);
        }
    }
}

