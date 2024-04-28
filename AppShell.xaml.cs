using TriviaAppClean.Views;
using System.Windows.Input;
using System;

namespace TriviaAppClean;

public partial class AppShell : Shell
{
    public AppShell()
	{
		this.BindingContext = this;
		InitializeComponent();
		RegisterRoutes();
    }

    private void RegisterRoutes()
	{
        Routing.RegisterRoute("connectingToServer", typeof(ConnectingToServerView));
    }
}
