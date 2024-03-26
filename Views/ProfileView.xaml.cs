namespace TriviaAppClean.Views;

public partial class ProfileView : ContentPage
{
	public ProfileView(ProfileView vm)
	{
		InitializeComponent();

        this.BindingContext = vm;

    }
}