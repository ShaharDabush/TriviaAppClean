namespace TriviaAppClean.Views;
using ViewModels;
public partial class PendingQuestionDetailsView : ContentPage
{
	public PendingQuestionDetailsView(PendingQuestionDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}