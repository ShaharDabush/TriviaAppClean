namespace TriviaAppClean.Views;

public partial class QuestionListView : ContentPage
{
	public QuestionListView(QuestionListViewModel vm)
	{
		InitializeComponent();

        this.BindingContext = vm;

    }
}