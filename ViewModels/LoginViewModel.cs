using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectMaui2.Models;
using SQLite;
using System.Diagnostics;

namespace ProjectMaui2.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly SQLiteConnection dbconnection;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string errorMessage;

        // create the folder path to store data (depending on device)
        public LoginViewModel()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "localdb_users.db3");
            dbconnection = new SQLiteConnection(dbPath);
            dbconnection.CreateTable<User>();

            //AddTestUser();
            //AddTestClient();
        }

        #region Test User
        //private void AddTestUser()
        //{
        //    var hashedPassword = BCrypt.Net.BCrypt.HashPassword("password");
        //    var testUser = new User
        //    {
        //        Username = "tester",
        //        Password = hashedPassword
        //    };
        //    dbconnection.Insert(testUser);
        //}

        //private void AddTestClient()
        //{
        //    var hashedPassword = BCrypt.Net.BCrypt.HashPassword("password");
        //    var testUser = new User
        //    {
        //        Username = "clienttester",
        //        Password = hashedPassword,
        //        Role = "client"
        //    };
        //    dbconnection.Insert(testUser);
        //    int trainerId = 0;
        //    var client = new Client
        //    {
        //        Name = "Test Client",
        //        Mobile = "111-222-3333",
        //        Email = "test@email.com",
        //        Notes = "Testing",
        //        TrainerId = trainerId,
        //        Exercises = new List<Exercise>()
        //    };
        //    dbconnection.Insert(client);
        //}
        #endregion
        #region multiuser login
        //private async Task OnLoginAsync()
        //{

        //    if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        //    {
        //        ErrorMessage = "Please enter a username and password.";
        //        return;
        //    }

        //    var user = dbconnection.Table<User>().FirstOrDefault(u => u.Username == Username);
        //    if (user != null && BCrypt.Net.BCrypt.Verify(Password, user.Password))
        //    {

        //        ErrorMessage = string.Empty;
        //        if (user.Role == "client")
        //        {
        //            await Shell.Current.GoToAsync("///ClientDashboardPage");
        //        }
        //        else
        //        {
        //            await Shell.Current.GoToAsync("///ClientsPage");
        //        }
        //    }
        //    else
        //    {
        //        ErrorMessage = "Invalid username or password.";
        //    }
        //} 
        #endregion

        [RelayCommand]
        private async Task OnLoginAsync()
        {

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Please enter a username and password.";
                return;
            }

            var user = dbconnection.Table<User>().FirstOrDefault(u => u.Username == Username);
            if (user != null && BCrypt.Net.BCrypt.Verify(Password, user.Password))
            {

                ErrorMessage = string.Empty;
                await Shell.Current.GoToAsync("///ClientsPage");
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
            }
        }

        [RelayCommand]
        private async Task OnRegisterAsync()
        {
            await Shell.Current.GoToAsync("/RegistrationPage");
        }
    }
}
