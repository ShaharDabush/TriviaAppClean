using TriviaAppClean.ViewModels;
namespace TriviaAppClean.Views;

public partial class CheckDependingQuestionView : ContentPage
{
	public CheckDependingQuestionView(CheckDependingQuestionViewModel vm)
	{
		InitializeComponent();

        this.BindingContext = vm;

    }
}