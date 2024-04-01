namespace TriviaAppClean.Views;
using TriviaAppClean.ViewModels;

public partial class GameWithTimeView : ContentPage
{
	public GameWithTimeView(GameWithTimeViewModel vm)
	{
		InitializeComponent();

        this.BindingContext = vm;

    }
}