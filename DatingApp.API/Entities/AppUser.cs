namespace DatingApp.API.Entities
{
    public class AppUser
    {
        private int id;
        private string username;

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
    }
}