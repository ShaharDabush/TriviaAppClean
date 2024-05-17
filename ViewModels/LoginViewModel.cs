using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
using TriviaAppClean.Views;

namespace TriviaAppClean.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        private TriviaWebAPIProxy triviaService;
        private SignUpView signupView;
        public LoginViewModel(TriviaWebAPIProxy service,SignUpView signUp) 
        {
            InServerCall = false;
            this.signupView = signUp;
            this.triviaService = service;
            this.LoginCommand = new Command(OnLogin);
            this.SignUpCommand = new Command(GoToSignUp);
        }

        public ICommand LoginCommand { get; set; }
        public Command SignUpCommand{ protected set; get; }
        private async void OnLogin()
        {
            //Choose the way you want to blobk the page while indicating a server call
            InServerCall=true;
            //await Shell.Current.GoToAsync("connectingToServer");
            User? u  = await this.triviaService.LoginAsync(mail, pass);
            //await Shell.Current.Navigation.PopModalAsync();
            InServerCall = false;

            //Set the application logged in user to be whatever user returned (null or real user)
            ((App)Application.Current).LoggedInUser = u;
            if (u == null)
            {
                
                await Application.Current.MainPage.DisplayAlert("Login", "Login Failed!", "ok");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login", $"Login Succeed!", "ok");
                u = null;
                Mail = "";
                Pass = "";


                Application.Current.MainPage = new AppShell(new ShellViewModel());

            }
        }
        private async void GoToSignUp()
        {
            
            await App.Current.MainPage.Navigation.PushAsync(signupView);
        }
        private string pass;
        public string Pass
        {
            get { return pass; } set
            {
                pass = value;
                OnPropertyChanged("Pass");
            }
        }
        private string mail;
        public string Mail
        {
            get { return mail; } set
            {
                mail = value;
                OnPropertyChanged("Mail");
            }
        }
        private bool inServerCall;
        public bool InServerCall
        {
            get
            {
                return this.inServerCall;
            }
            set
            {
                this.inServerCall = value;
                OnPropertyChanged("NotInServerCall");
                OnPropertyChanged("InServerCall");
            }
        }

        public bool NotInServerCall
        {
            get
            {
                return !this.InServerCall;
            }
        }
    }
}
