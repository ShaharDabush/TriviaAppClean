namespace TriviaAppClean.Views;

public partial class PlayerListView : ContentPage
{
	public PlayerListView(PlayerListViewModel vm)
	{
		InitializeComponent();

        this.BindingContext = vm;

    }
}