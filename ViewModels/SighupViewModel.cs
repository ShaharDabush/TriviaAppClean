using System;

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
using TriviaAppClean.ViewModels;


namespace TriviaAppClean.ViewModels
{
    public class SighUpViewModel: ViewModelBase
    {
        #region FormValidation
        #region שם
        private bool showNameError;

        public bool ShowNameError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }

        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                ValidateName();
                OnPropertyChanged("Name");
            }
        }

        private string nameError;

        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }
        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(Name);
        }
        #endregion
        #region password
        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }

        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                ValidatePassword();
                OnPropertyChanged("Password");
            }
        }

        private string passwordError;

        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }
        private void ValidatePassword()
        {
            this.ShowPasswordError = Password == null || Password.Length < 8 ; // need to inclode one letter  
        }
        #endregion
        #region Email
        private bool showEmailError;

        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidatePassword();
                OnPropertyChanged("Email");
            }
        }

        private string emaildError;

        public string EmailError
        {
            get => EmailError;
            set
            {
                EmailError = value;
                OnPropertyChanged("EmailError");
            }
        }
        private void ValidateEmail()
        {

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(Email);
            if (match.Success)
                ShowEmailError = true;
        }
        #endregion
        #region Private policy
        private bool showCheckBoxError;

        public bool ShowCheckBoxError
        {
            get => showCheckBoxError;
            set
            {
                showCheckBoxError = value;
                OnPropertyChanged("ShowCheckBoxError");
            }
        }

        private bool checkBox;

        public bool CheckBox
        {
            get => CheckBox;
            set
            {
                checkBox = value;
                ValidatePassword();
                OnPropertyChanged("checkBox");
            }
        }

        private string CheckBoxdError;

        public string CheckBoxError
        {
            get => CheckBoxError;
            set
            {
                CheckBoxError = value;
                OnPropertyChanged("CheckBoxError");
            }
        }
        private void ValidateCheckBox()
        {
            if(CheckBox == true)
            {
                ShowEmailError = true;
            }
           
        }
        #endregion
        public Command SaveDataCommand { protected set; get; }
        private TriviaWebAPIProxy triviaService;
        public SighupViewModel(TriviaWebAPIProxy service)
        {
            this.NameError = "This is must";
            this.ShowNameError = false;
            this.PasswordError = "The password must include at least 8 digits and with at least 1 letter";
            this.ShowPasswordError = false;
            this.EmailError = "The email was not found";
            this.ShowEmailError = false;
            this.CheckBoxError = "Please confirm our private policy";
            this.ShowCheckBoxError = false;
            this.SaveDataCommand = new Command(() => SaveData());
            this.triviaService = service;
            ;
        }
        //This function validate the entire form upon submit!
        private bool ValidateForm()
        {
            //Validate all fields first
            ValidatePassword();
            ValidateEmail();
            ValidateName();
            ValidateCheckBox();

            //check if any validation failed
            if (ShowPasswordError || ShowEmailError || ShowCheckBoxError||
                ShowNameError)
                return false;
            return true;
        }
       
   
        private async void SaveData()
        {
            if (ValidateForm())
            {
                User u = new User();
                u.Email = this.Email;
                u.Password = this.Password;
                u.Name = this.Name;
                u.Rank = 1;
                u.Score = 0;
                if (await this.triviaService.RegisterUser(u))
                {
                    await App.Current.MainPage.DisplayAlert("Save deta", "your deta is saved", "אישור", FlowDirection.RightToLeft);
                    await App.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Save deta", "there is a problem with your deta", "אישור", FlowDirection.RightToLeft);
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Save deta", "there is a problem with your deta", "אישור", FlowDirection.RightToLeft);
            }
                
        }
        #endregion
    }
}
