using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectMaui2.Models;
using SQLite;

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
            Console.WriteLine("======================================================================> Database path users: " + dbPath);
            dbconnection = new SQLiteConnection(dbPath);
            dbconnection.CreateTable<User>();

            //AddTestUser();
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
