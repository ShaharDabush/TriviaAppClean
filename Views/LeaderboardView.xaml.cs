using TriviaAppClean.ViewModels;

namespace TriviaAppClean.Views;

public partial class LeaderboardView : ContentPage
{
	public LeaderboardView(LeaderboardViewModel vm)
	{
		InitializeComponent();

        this.BindingContext = vm;

    }
}