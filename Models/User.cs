using SQLite;

namespace ProjectMaui2.Models
{
    internal class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }

        [MaxLength(30), Unique]
        public string Username { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
