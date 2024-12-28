using Newtonsoft.Json;
using ProjectMaui2.Models;
using SQLite;

namespace ProjectMaui2.Services
{
    public class LocalDbService
    {
        //private const string DB_NAME = "localdb_clients.db3";
        private readonly SQLiteAsyncConnection connection;
        public LocalDbService()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "localdb_clientss.db3");

            connection = new SQLiteAsyncConnection(dbPath);
            Console.WriteLine("===========> Database path: " + dbPath);
            connection.CreateTableAsync<Client>().Wait();
            //joined tables for future scalability
            //connection.CreateTableAsync<Exercise>();
        }
        // todo try catch
        public async Task<List<Client>> GetClients()
        {
            return await connection.Table<Client>().ToListAsync();
        }

        public async Task<Client> GetClientById(int id)
        {
            return await connection.Table<Client>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        //client by trainer id 
        //public Task<List<Client>> GetClientsByTrainerId(int trainerId)
        //{
        //    return Task.Run(() =>
        //    {
        //        return connection.Table<Client>().Where(c => c.TrainerId == trainerId).ToListAsync();
        //    });
        //}

        public async Task CreateClient(Client client)
        {
            await connection.InsertAsync(client);
        }

        public async Task UpdateClient(Client client)
        {
            await connection.UpdateAsync(client);
        }
        public async Task DeleteClient(Client client)
        {
            await connection.DeleteAsync(client);
        }

        public async Task AddExerciseToClientAsync(int clientId, Exercise exercise)
        {
            var client = await GetClientById(clientId);
            if (client == null) return;

            if(client.Exercises == null)
                client.Exercises = new List<Exercise>();

            client.Exercises.Add(exercise);
            await SaveClientExerciseAsync(client);
        }

        public async Task RemoveExerciseFromClientAsync(int clientId, int exerciseId)
        {
            var client = await GetClientById(clientId);
            if (client == null || client.Exercises == null) return;

            client.Exercises.RemoveAll(e => e.Id == exerciseId);
            await SaveClientExerciseAsync(client);
        }

        public async Task SaveClientExerciseAsync(Client client)
        {
            client.ExerciseListJson = JsonConvert.SerializeObject(client.Exercises);
            await connection.UpdateAsync(client);
        }
    }
}
