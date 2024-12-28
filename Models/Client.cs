using Newtonsoft.Json;
using SQLite;

namespace ProjectMaui2.Models
{
    //[Table("client")]
    public class Client
    {
        public Client() 
        {
            Exercises = new List<Exercise>();
        }

        [PrimaryKey, AutoIncrement]
       
        public int Id { get; set; }      

        public string Name { get; set; }
        
        public string Mobile { get; set; }
        
        public string Email { get; set; }
        
        public string Notes { get; set; }

        public int TrainerId { get; set; }

        [Ignore]
        public List<Exercise> Exercises { get; set; }

        [Column("ExerciseList")]
        public string ExerciseListJson
        {
            get => JsonConvert.SerializeObject(Exercises);
            set => Exercises = string.IsNullOrWhiteSpace(value) ? new List<Exercise>() : JsonConvert.DeserializeObject<List<Exercise>>(value);
        }
    }
}
