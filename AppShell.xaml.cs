using TriviaAppClean.Views;
using System.Windows.Input;
using System;
using TriviaAppClean.ViewModels;
namespace TriviaAppClean;

public partial class AppShell : Shell
{
    public AppShell(ShellViewModel vm)
	{
		
		InitializeComponent();
        this.BindingContext = vm;
        RegisterRoutes();
    }

    //all the Routing to different pages (via shell) like from QuestionList to QuestionDetailsView 
    private void RegisterRoutes()
	{
        Routing.RegisterRoute("connectingToServer", typeof(ConnectingToServerView));
        Routing.RegisterRoute("QuestionDetailsView", typeof(QuestionDetailsView));
        Routing.RegisterRoute("PendingQuestionDetailsView", typeof(PendingQuestionDetailsView));
        Routing.RegisterRoute("PlayerDetailsView", typeof(PlayerDetailsView));
        Routing.RegisterRoute("AddQuestionView", typeof(AddQuestionView));


    }
}
