using CommunityToolkit.Maui;
using ICD.Models;
using ICD.ViewModels;
using ICD.Views;
using UraniumUI;
using UraniumUI.Material;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using ZXing.Net.Maui.Controls;
using Plugin.AdMob;
using Plugin.AdMob.Configuration;
//using Plugin.MauiMTAdmob;

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
                .UseUraniumUI()
                .UseAdMob()
                .UseUraniumUIMaterial()
                .UseBarcodeReader()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
            AdConfig.UseTestAdUnitIds = true;

#endif

            builder.Services.AddSingleton<DataContext>();

            builder.Services.AddSingleton<HomeVM>()
                            .AddSingleton<HomePage>();

            builder.Services.AddSingleton<DrugDetailVM>()
                            .AddSingleton<DrugDetailPage>();

            builder.Services.AddSingleton<AboutVM>()
                            .AddSingleton<AboutPage>();
            return builder.Build();
        }
    }
}
