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
    
    private void RegisterRoutes()
	{
        Routing.RegisterRoute("connectingToServer", typeof(ConnectingToServerView));
        Routing.RegisterRoute("QuestionDetailsView", typeof(QuestionDetailsView));
    }
}
