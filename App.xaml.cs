using TriviaAppClean.Models;
using TriviaAppClean.Views;
using TriviaAppClean.ViewModels;
using System.Windows.Input;

namespace TriviaAppClean;
public partial class App : Application
{
	public LoginView Login;
    //Use this class to store global application data that should be accessible throughout the entire app!
    public User LoggedInUser { get; set; }
	public App(LoginView v)
	{
		LoggedInUser = null;
		InitializeComponent();
		Login = v;
        this.LogoutCommand = new Command(Logout);
        MainPage = new NavigationPage(v);
    }
    public ICommand LogoutCommand { get; set; }
    private void Logout()
    {
        MainPage = new NavigationPage(Login);
    }
}
