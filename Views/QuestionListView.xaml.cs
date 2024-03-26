namespace TriviaAppClean.Views;

public partial class QuestionListView : ContentPage
{
	public QuestionListView(QuestionListView vm)
	{
		InitializeComponent();

        this.BindingContext = vm;

    }
}