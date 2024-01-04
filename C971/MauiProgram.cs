﻿using C971.Data;
using C971.Views;
using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;

namespace C971
{
    public enum Category { Objective, Practical}

    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseLocalNotification()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif       

            return builder.Build();
        }
    }

}