namespace TriviaAppClean.Views;

public partial class GameWithTimeView : ContentPage
{
	public GameWithTimeView(GameWithTimeView vm)
	{
		InitializeComponent();

        this.BindingContext = vm;

    }
}