﻿using Microsoft.Extensions.Logging;
using TriviaAppClean.Services;
using TriviaAppClean.ViewModels;
using TriviaAppClean.Views;

namespace TriviaAppClean;

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
			})
            .RegisterDataServices()
            .RegisterPages()
            .RegisterViewModels(); 

#if DEBUG
		builder.Logging.AddDebug();
#endif
		
		return builder.Build();
	}

    public static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<LoginView> ();
        builder.Services.AddSingleton<SighUpView>();
        builder.Services.AddSingleton<GameView>();



        builder.Services.AddTransient<AddQuestionView>();
        builder.Services.AddTransient<CheckDependingQuestionView>();
        builder.Services.AddTransient<GameRegularView>();
        builder.Services.AddTransient<GameWithTimeView>();
        builder.Services.AddTransient<LeaderboardView>();
        builder.Services.AddTransient<PlayerListView>();
        builder.Services.AddTransient<ProfileView>();
        builder.Services.AddTransient<QuestionListView>();


        return builder;
    }

    public static MauiAppBuilder RegisterDataServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<TriviaWebAPIProxy>();
        return builder;
    }
    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<SighUpViewModel>();
        builder.Services.AddSingleton<AddQuestionViewModel>();

        return builder;
    }
}
