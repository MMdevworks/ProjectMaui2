using ProjectMaui2.ViewModels;

namespace ProjectMaui2.Views;

public partial class ClientDetailsPage : ContentPage
{
	public ClientDetailsPage(ClientDetailsViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
    //protected override void OnAppearing()
    //{
    //    base.OnAppearing();

    //    // clear any exercises when page appears
    //    if (BindingContext is ExerciseViewModel viewModel)
    //    {
    //        viewModel.Exercises.Clear();
    //        viewModel.Muscle = null;
    //    }
    //}
}