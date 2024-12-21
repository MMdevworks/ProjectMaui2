using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using ProjectMaui2.Services;
using ProjectMaui2.Models;

namespace ProjectMaui2.ViewModels 
{ 
    public partial class ClientsViewModel : BaseViewModel
    {
        private readonly LocalDbService localDbService;

        [ObservableProperty]
        private int clientId;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string mobile;

        [ObservableProperty]
        private string notes;

        [ObservableProperty]
        private bool isFormVisible;

        [ObservableProperty]
        private bool isAddBtnVisible = true;

        [ObservableProperty]
        private bool isEdit;

        [ObservableProperty]
        private Client selectedClient;

        public ObservableCollection<Client> Clients { get; } = new();

        public ICommand EditClientCommand => new RelayCommand<Client>(EditClient);
        public ICommand DeleteClientCommand => new RelayCommand<Client>(async (client) => await DeleteClient(client));

        public ICommand ClientTappedCommand => new AsyncRelayCommand<Client>(OnClientTapped);
        public ClientsViewModel(LocalDbService localService)
        {
            Title = "Clients";
            this.localDbService = localService;
            Task.Run(LoadClientsAsync);
        }

        [RelayCommand]
        private void AddClient()
        {
            IsFormVisible = true;
            IsAddBtnVisible = false;
            IsEdit = false;
            Name = string.Empty;
            Email = string.Empty;
            Mobile = string.Empty;
            Notes = string.Empty;
        }

        [RelayCommand]
        private async Task LoadClientsAsync()
        {
            var clients = await localDbService.GetClients();
            Clients.Clear();
            foreach (var client in clients)
            {
                Clients.Add(client);
            }
        }

        [RelayCommand]
        private async Task SaveClientAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                if (IsEdit) //edit
                {
                    await localDbService.UpdateClient(new Client
                    {
                        Id = selectedClient.Id,
                        Name = Name,
                        Email = Email,
                        Mobile = Mobile,
                        Notes = Notes,
                        //change list later
                        //Exercises = new List<Exercise>()
                    });
                }
                else //add
                {
                    await localDbService.CreateClient(new Client
                    {
                        Name = Name,
                        Email = Email,
                        Mobile = Mobile,
                        Notes = Notes,
                        //Exercises = new List<Exercise>()
                    });
                }

                // hide form and reload
                IsFormVisible = false;
                IsAddBtnVisible = true;
                await LoadClientsAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OnClientTapped(Client client)
        {
            Debug.WriteLine("=========> Tap event triggered");
            try
            {
                if (client != null)
                {
                    Debug.WriteLine($"==============>  On Tapped go to details page for: {client.Name} {client.Id}");
                    await Shell.Current.GoToAsync($"///ClientDetailsPage?clientId={client.Id}");
                    await Task.CompletedTask;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=========> Error navigating: {ex.Message}");
            }
        }

        private void EditClient(Client client)
        {
            if (client != null)
            {
                Debug.WriteLine($"===========================================================================================>  Editing client: {client.Name}");
                selectedClient = client;
                IsFormVisible = true;
                IsAddBtnVisible = false;
                IsEdit = true;
                Name = client.Name;
                Email = client.Email;
                Mobile = client.Mobile;
                Notes = client.Notes;
            }
        }

        private async Task DeleteClient(Client client)
        {
            if (client != null)
            {
                Debug.WriteLine($"===================>  Deleting client: {client.Name}");
                bool userConfirm = await App.Current.MainPage.DisplayAlert(
                    "Delete Client", $"Are you sure you want to delete {client.Name}?",
                    "Yes",
                    "No");

                if (userConfirm)
                {
                    await localDbService.DeleteClient(client);

                    Clients.Remove(client);
                }
            }
        }
    }
}
