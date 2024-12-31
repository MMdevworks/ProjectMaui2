using CommunityToolkit.Mvvm.ComponentModel;
using ProjectMaui2.ViewModels;
using ProjectMaui2.Services;
using ProjectMaui2.Models;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ProjectMaui2.ViewModels
{
    [QueryProperty(nameof(ClientId), "clientId")]
    public partial class ClientDetailsViewModel : BaseViewModel
    {
        private readonly LocalDbService localDbService;
        private readonly ExerciseService exerciseService;
        private readonly IConnectivity connectivity;

        public ObservableCollection<Exercise> Exercises { get; } = new();

        public ObservableCollection<Exercise> ClientExercises { get; } = new();


        [ObservableProperty]
        private Client client;

        private int clientId;

        public int ClientId
        {
            get => clientId;
            set
            {
                clientId = value;
                Task.Run(() => LoadClientDetailsAsync());
            }
        }
        
        [ObservableProperty]
        private bool isClientExerciseListVisible = true;

        [ObservableProperty]
        private string muscle;
        public List<string> MuscleList { get; } = new List<string>
        {
            "abdominals", "abductors", "adductors", "biceps", "calves", "chest",
            "forearms", "glutes", "hamstrings", "lats", "lower_back", "middle_back",
            "neck", "quadriceps", "traps", "triceps"
        };

        public List<string> FormattedMuscleList => MuscleList.Select(m => m.Replace('_', ' ').ToUpper()).ToList();

        public ClientDetailsViewModel(LocalDbService localDbService, ExerciseService exerciseService, IConnectivity connectivity)
        {
            this.localDbService = localDbService;
            this.exerciseService = exerciseService;
            this.connectivity = connectivity;
        }

        public async Task LoadClientDetailsAsync()
        {
            try
            {
                if (clientId != 0)
                {
                    Client = await localDbService.GetClientById(clientId);
                    ClientExercises.Clear();
                    if (Client.Exercises != null)
                    {
                        foreach (var exercise in Client.Exercises) { ClientExercises.Add(exercise); }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading client details: {ex.Message}");
            }
        }

        partial void OnMuscleChanged(string value)
        {
            LoadExercisesCommand.Execute(null);
        }

        [RelayCommand]
        async Task LoadExercisesAsync()
        {
            if (IsBusy || string.IsNullOrWhiteSpace(Muscle)) return;
            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Internet issue", "Check your internet connection", "Ok");
                    return;
                }
                IsBusy = true;
                Exercises.Clear();
                IsClientExerciseListVisible = false;

                string formattedMuscle = Muscle.Replace(" ", "_").ToLower();
                var exercises = await exerciseService.GetExercise(formattedMuscle);
                foreach (var exercise in exercises)
                    Exercises.Add(exercise);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", "Unable to fetch data", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task AddExerciseToClientAsync(Exercise exercise)
        {
            //Debug.WriteLine($"Client is null: {Client == null}");
            //Debug.WriteLine($"Exercise is null: {exercise == null}");
            //Debug.WriteLine($"Client.Exercises is null: {Client?.Exercises == null}");
            if (Client == null || exercise == null) return;

            if (Client.Exercises == null)
            {
                Debug.WriteLine("Initializing Exercises list");
                Client.Exercises = new List<Exercise>();
            }

            Debug.WriteLine("Before adding exercise to client:");
            Client.Exercises.Add(exercise);
            ClientExercises.Add(exercise);
            Debug.WriteLine("After adding exercise to client:");

            await localDbService.UpdateClient(Client);

            // Reset picker and clear the exercises
            Muscle = null;
            Exercises.Clear();
            IsClientExerciseListVisible = true;

            // Refresh display of client ex
            ClientExercises.Clear();
            foreach (var ex in Client.Exercises)
            {
                ClientExercises.Add(ex);
            }

            await Shell.Current.DisplayAlert("Success", "Exercise added to client.", "OK");
        }

        //TODO: Fix delete
        [RelayCommand]
        async Task DeleteExerciseFromClientAsync(Exercise exercise)
        {
            Debug.WriteLine("=====> DELETE FROM CLIENT CLICKED");
            if (Client == null || exercise == null) return;

            var exerciseToRemove = Client.Exercises.FirstOrDefault(e => e.Id == exercise.Id);
            if (exerciseToRemove != null)
            {
                Client.Exercises.Remove(exerciseToRemove);
                ClientExercises.Remove(exerciseToRemove);
            }

            await localDbService.UpdateClient(Client);

            ClientExercises.Clear();
            foreach (var ex in Client.Exercises)
            {
                ClientExercises.Add(ex);
            }

            await Shell.Current.DisplayAlert("Success", "Exercise deleted from client.", "OK");
        }

        //[RelayCommand]
        //async Task GoToDetailsAsync(Exercise exercise)
        //{
        //    if (exercise == null) return;

        //    await Shell.Current.GoToAsync($"{nameof(ExerciseDetailsPage)}", true,
        //        new Dictionary<string, object> { { "Exercise", exercise } });
        //}
    }
}


