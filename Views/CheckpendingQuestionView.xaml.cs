using TriviaAppClean.ViewModels;
namespace TriviaAppClean.Views;

public partial class CheckpendingQuestionView : ContentPage
{
	public CheckpendingQuestionView(CheckpendingQuestionViewModel vm)
	{
		InitializeComponent();

        this.BindingContext = vm;

    }
}