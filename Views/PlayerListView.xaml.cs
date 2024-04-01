namespace TriviaAppClean.Views;
using TriviaAppClean.ViewModels;

public partial class PlayerListView : ContentPage
{
	public PlayerListView(PlayerListViewModel vm)
	{
		InitializeComponent();

        this.BindingContext = vm;

    }
}