using System;
using Newtonsoft.Json;
using JWT.Algorithms;
using JWT.Builder;
using BlazorFormDesigner.BusinessLogic.Models;

namespace LoginApp.WebApi.Services
{
    public static class TokenService
    {
        private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

        public static string GenerateToken(string username, int expireMinutes = 60)
        {
            var token = new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(Secret)
                .AddClaim("exp", DateTimeOffset.UtcNow.AddMinutes(expireMinutes).ToUnixTimeSeconds())
                .AddClaim("username", username)
                .Encode();

            return token;
        }

        public static User DecodeToken(string token)
        {
            try
            {
                string json = new JwtBuilder()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret(Secret)
                    .MustVerifySignature()
                    .Decode(token);
                return JsonConvert.DeserializeObject<User>(json);
            }
            catch (JWT.Exceptions.TokenExpiredException)
            {
                //throw new TokenException(token, TokenExceptionType.TokenExpired);
            }
            catch (Exception)
            {
                //throw new TokenException(token, TokenExceptionType.UnableToDecode);
            }

            return null;
        }
    }
}
