using ProjectMaui2.ViewModels;
namespace ProjectMaui2.Views;

public partial class ClientsPage : ContentPage
{
	public ClientsPage(ClientsViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}