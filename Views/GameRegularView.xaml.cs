namespace TriviaAppClean.Views;

public partial class GameRegularView : ContentPage
{
	public GameRegularView(GameRegularViewModel vm)
	{
		InitializeComponent();

        this.BindingContext = vm;

    }
}