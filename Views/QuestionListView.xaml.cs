namespace TriviaAppClean.Views;
using TriviaAppClean.ViewModels;

public partial class QuestionListView : ContentPage
{
	public QuestionListView(QuestionListViewModel vm)
	{
		InitializeComponent();

        this.BindingContext = vm;

    }
}