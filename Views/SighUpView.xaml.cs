using TriviaAppClean.ViewModels;

namespace TriviaAppClean.Views;

public partial class SighUpView : ContentPage
{
	public SighUpView(SighUpViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}