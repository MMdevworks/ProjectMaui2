using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ProjectMaui2.ViewModels;
using ProjectMaui2.Views;
using ProjectMaui2.Services;

namespace ProjectMaui2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddSingleton<ClientsPage>();
            builder.Services.AddSingleton<ClientsViewModel>();

            builder.Services.AddSingleton<LocalDbService>();
            builder.Services.AddSingleton<ExerciseService>();
#endif

            return builder.Build();
        }
    }
}
