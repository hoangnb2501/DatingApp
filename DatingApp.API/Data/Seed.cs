using System.Text;
using System.Security.Cryptography;
using System.Text.Json;
using DatingApp.API.Entities;
using Microsoft.EntityFrameworkCore;
using DatingApp.API.Helpers;

namespace DatingApp.API.Data
{
    public class Seed
    {
        public static async Task SeedUser(DatingAppDbContext context)
        {
            if (await context.Users.AnyAsync())
                return;

            var userData = await File.ReadAllBytesAsync("Data/UserSeedData.json");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            options.Converters.Add(new DateOnlyJsonConverter());

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);

            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();
                user.Username = user.Username.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("1234"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}