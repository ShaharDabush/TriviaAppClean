using TriviaAppClean.ViewModels;
namespace TriviaAppClean.Views;

public partial class CheckPendingQuestionView : ContentPage
{
	public CheckPendingQuestionView(CheckPendingQuestionViewModel vm)
	{
		InitializeComponent();

        this.BindingContext = vm;

    }
}