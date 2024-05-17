using Microsoft.Extensions.Logging;
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
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<GameView>();


        builder.Services.AddTransient<LoginView>();
        builder.Services.AddTransient<SignUpView>();
        builder.Services.AddTransient<AddQuestionView>();
        builder.Services.AddTransient<CheckPendingQuestionView>();
        builder.Services.AddTransient<GameRegularView>();
        builder.Services.AddTransient<GameWithTimeView>();
        builder.Services.AddTransient<LeaderboardView>();
        builder.Services.AddTransient<PlayerListView>();
        builder.Services.AddTransient<ProfileView>();
        builder.Services.AddTransient<QuestionListView>();
        builder.Services.AddTransient<QuestionDetailsView>();

        return builder;
    }

    public static MauiAppBuilder RegisterDataServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<TriviaWebAPIProxy>();
        return builder;
    }
    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<ShellViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<SignUpViewModel>();
        builder.Services.AddSingleton<AddQuestionViewModel>();
        builder.Services.AddSingleton<CheckPendingQuestionViewModel>();
        builder.Services.AddSingleton<GameViewModel>();
        builder.Services.AddSingleton<GameRegularViewModel>();
        builder.Services.AddSingleton<GameWithTimeViewModel>();
        builder.Services.AddSingleton<LeaderboardViewModel>();
        builder.Services.AddSingleton<PlayerListViewModel>();
        builder.Services.AddSingleton<ProfileViewModel>();
        builder.Services.AddSingleton<QuestionListViewModel>();
        builder.Services.AddSingleton<QuestionDetailsViewModel>();


        return builder;
    }
}
