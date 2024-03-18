namespace TriviaAppClean.Views;

public partial class AddQuestionView : ContentPage
{
	public AddQuestionView(AddQuestionView vm)
	{
		InitializeComponent();

		this.BindingContext = vm;
	}
}