namespace TriviaAppClean.Views;

public partial class CheckDependingQuestionView : ContentPage
{
	public CheckDependingQuestionView(CheckDependingQuestionView vm)
	{
		InitializeComponent();

        this.BindingContext = vm;

    }
}