using Newtonsoft.Json;
using SQLite;

namespace ProjectMaui2.Models
{
    [Table("client")]
    public class Client
    {
        public Client() 
        {
            Exercises = new List<Exercise>();
        }

        [PrimaryKey, AutoIncrement]
       
        [Column("id")]
        public int Id { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
        
        [Column("mobile")]
        public string Mobile { get; set; }
        
        [Column("email")]
        public string Email { get; set; }
        
        [Column("notes")]
        public string Notes { get; set; }

        [Column("trainer_id")]
        public int TrainerId { get; set; }

        [Ignore]
        public List<Exercise> Exercises { get; set; }

        [Column("exercise_list")]
        public string ExerciseListJson
        {
            get => JsonConvert.SerializeObject(Exercises);
            set => Exercises = string.IsNullOrWhiteSpace(value) ? new List<Exercise>() : JsonConvert.DeserializeObject<List<Exercise>>(value);
        }
    }
}
