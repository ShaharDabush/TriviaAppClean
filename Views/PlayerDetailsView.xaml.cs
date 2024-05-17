using TriviaAppClean.ViewModels;

namespace TriviaAppClean.Views;

public partial class PlayerDetailsView : ContentPage
{
	public PlayerDetailsView(PlayerDetailsViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
    }
}