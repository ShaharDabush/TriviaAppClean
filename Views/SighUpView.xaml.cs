using TriviaAppClean.ViewModels;

namespace TriviaAppClean.Views;

public partial class SighUpView : ContentPage
{
	public SighUpView(SignUpViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}