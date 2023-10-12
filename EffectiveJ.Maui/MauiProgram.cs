using Microsoft.Extensions.Logging;
using EffectiveJ.Maui.Services;
using EffectiveJ.Maui.Views;

namespace EffectiveJ.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });



#if DEBUG
            builder.Logging.AddDebug();
#endif


            builder.Services.AddSingleton<CountdownTimerService>();
            builder.Services.AddSingleton<TaskRecordingService>();
            
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<MainPage>();

            return builder.Build();
        }
    }
}