namespace TriviaAppClean.Views;

public partial class PlayerListView : ContentPage
{
	public PlayerListView(PlayerListView vm)
	{
		InitializeComponent();

        this.BindingContext = vm;

    }
}