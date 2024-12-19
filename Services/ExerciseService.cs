
using System.Net.Http.Json;
using ProjectMaui2;
using ProjectMaui2.Models;

namespace ProjectMaui2.Services
{
    public class ExerciseService
    {
        HttpClient httpClient;

        public ExerciseService()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(Env.API_BASE_URL)
            };
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", Env.API_KEY);
        }

        public async Task<List<Exercise>> GetExercisesByMuscleAsync(string muscle) 
        { 
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) 
                throw new InvalidOperationException("No internet connection."); 

            var response = await httpClient.GetFromJsonAsync<List<Exercise>>($"exercises?muscle={muscle}"); 
            return response ?? new List<Exercise>(); 
        }
        
        public async Task<Exercise> GetExerciseByIdAsync(int exerciseId) 
        { 
            var response = await httpClient.GetFromJsonAsync<Exercise>($"exercises/{exerciseId}"); 
            return response; 
        }

        //public async Task<List<Exercise>> GetExercise(string muscle)
        //{
        //    if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        //        throw new InvalidOperationException("No internet connection.");

        //    var response = await httpClient.GetFromJsonAsync<List<Exercise>>($"exercises?muscle={muscle}");
        //    return response ?? new List<Exercise>();
        //}
    }
}
