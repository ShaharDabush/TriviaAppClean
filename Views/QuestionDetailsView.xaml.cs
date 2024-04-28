namespace TriviaAppClean.Views;
using ViewModels;

public partial class QuestionDetailsView : ContentPage
{
	public QuestionDetailsView(QuestionDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}