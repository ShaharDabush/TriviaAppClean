namespace TriviaAppClean.Views;

public partial class ProfileView : ContentPage
{
	public ProfileView(ProfileViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;

    }
}