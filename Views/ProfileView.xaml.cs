namespace TriviaAppClean.Views;
using TriviaAppClean.ViewModels;

public partial class ProfileView : ContentPage
{
	public ProfileView(ProfileViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;

    }
}