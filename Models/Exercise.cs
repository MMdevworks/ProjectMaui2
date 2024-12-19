using SQLite;

namespace ProjectMaui2.Models
{
    public class Exercise
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string muscle { get; set; }
        public string equipment { get; set; }
        public string difficulty { get; set; }
        public string instructions { get; set; }
    }
}
