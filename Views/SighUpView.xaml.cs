namespace TriviaAppClean.Views;

public partial class SighUpView : ContentPage
{
	public SighUpView(SighUpView vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}