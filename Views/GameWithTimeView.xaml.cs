namespace TriviaAppClean.Views;

public partial class GameWithTimeView : ContentPage
{
	public GameWithTimeView(GameWithTimeViewModel vm)
	{
		InitializeComponent();

        this.BindingContext = vm;

    }
}