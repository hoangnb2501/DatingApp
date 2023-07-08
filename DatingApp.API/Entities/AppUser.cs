namespace DatingApp.API.Entities
{
    public class AppUser
    {
        private int id;
        private string username;
        private byte[] passwordHash;
        private byte[] passwordSalt;

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public byte[] PasswordHash { get => passwordHash; set => passwordHash = value; }
        public byte[] PasswordSalt { get => passwordSalt; set => passwordSalt = value; }
    }
}