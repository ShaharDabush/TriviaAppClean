namespace TriviaAppClean.Views;

public partial class GameRegularView : ContentPage
{
	public GameRegularView(GameRegularView vm)
	{
		InitializeComponent();

        this.BindingContext = vm;

    }
}