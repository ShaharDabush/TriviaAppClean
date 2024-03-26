namespace TriviaAppClean.Views;

public partial class LeaderboardView : ContentPage
{
	public LeaderboardView(LeaderboardView vm)
	{
		InitializeComponent();

        this.BindingContext = vm;

    }
}