using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.API.Entities
{
    [Table("Photos")]
    public class Photo
    {
        private int id;
        private string url;
        private bool isMain;
        private string publicId;
        private int appUserId;
        private AppUser appUser;

        public int Id { get => id; set => id = value; }
        public string Url { get => url; set => url = value; }
        public bool IsMain { get => isMain; set => isMain = value; }
        public string PublicId { get => publicId; set => publicId = value; }
        public int AppUserId { get => appUserId; set => appUserId = value; }
        public AppUser AppUser { get => appUser; set => appUser = value; }
    }
}