﻿using TriviaAppClean.Models;
using TriviaAppClean.Views;
using TriviaAppClean.ViewModels;

namespace TriviaAppClean;

public partial class App : Application
{
	//Use this class to store global application data that should be accessible throughout the entire app!
	public User LoggedInUser { get; set; }
	public App(LoginView v)
	{
		LoggedInUser = null;
		InitializeComponent();

		MainPage = new NavigationPage(v);
    }
}
