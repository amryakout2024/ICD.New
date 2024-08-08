using CommunityToolkit.Maui;
using ICD.Models;
using ICD.ViewModels;
using ICD.Views;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace ICD
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<DataContext>();

            builder.Services.AddSingleton<HomeVM>()
                            .AddSingleton<HomePage>();

            builder.Services.AddSingleton<LoadingVM>()
                            .AddSingleton<LoadingPage>();

            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<AppShellVM>();

            return builder.Build();
        }
    }
}
