using System.Text.Json.Serialization;
using DatingApp.API.Extensions;
using DatingApp.API.Helpers;

namespace DatingApp.API.Entities
{
    public class AppUser
    {
        private int id;
        private string username;
        private byte[] passwordHash;
        private byte[] passwordSalt;
        private DateOnly dateOfBirth;
        private string knownAs;
        private DateTime createdAt = DateTime.UtcNow;
        private DateTime lastActiveAt = DateTime.UtcNow;
        private string gender;
        private string introduction;
        private string lookingFor;
        private string interests;
        private string city;
        private string country;
        private List<Photo> photos = new();

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public byte[] PasswordHash { get => passwordHash; set => passwordHash = value; }
        public byte[] PasswordSalt { get => passwordSalt; set => passwordSalt = value; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public string KnownAs { get => knownAs; set => knownAs = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public DateTime LastActiveAt { get => lastActiveAt; set => lastActiveAt = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Introduction { get => introduction; set => introduction = value; }
        public string LookingFor { get => lookingFor; set => lookingFor = value; }
        public string Interests { get => interests; set => interests = value; }
        public string City { get => city; set => city = value; }
        public string Country { get => country; set => country = value; }
        public List<Photo> Photos { get => photos; set => photos = value; }

        // public int GetAge()
        // {
        //     return DateOfBirth.CalculateAge();
        // }
    }
}