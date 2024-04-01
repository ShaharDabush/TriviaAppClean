namespace TriviaAppClean.Views;
using TriviaAppClean.ViewModels;

public partial class GameView : ContentPage
{
	public GameView(GameViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}