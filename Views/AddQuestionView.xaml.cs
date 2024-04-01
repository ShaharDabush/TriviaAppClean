namespace TriviaAppClean.Views;

public partial class AddQuestionView : ContentPage
{
	public AddQuestionView(AddQuestionViewModel vm)
	{
		InitializeComponent();

		this.BindingContext = vm;
	}
}