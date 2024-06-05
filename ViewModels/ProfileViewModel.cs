using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
using System.Text.RegularExpressions;

namespace TriviaAppClean.ViewModels
{
    public class ProfileViewModel : ViewModelBase
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
            this.ShowPasswordError = (Password == null) || Password.Length < 8 || !Password.Any(x => char.IsLetter(x)); // need to inclode one letter  
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
                ValidateEmail();
                OnPropertyChanged("Email");
            }
        }

        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }
        private void ValidateEmail()
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(Email);
            if (match.Success)
            {
                ShowEmailError = false;
            }
            else
            {
                ShowEmailError = true;
            }

        }
        #endregion
        
        //This function validate the entire form upon submit!
        private bool ValidateForm()
        {
            //Validate all fields first
            ValidatePassword();
            ValidateEmail();
            ValidateName();


            //check if any validation failed
            if (ShowPasswordError || ShowEmailError || ShowNameError)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
        #region attributes and properties
        private TriviaWebAPIProxy triviaService;
        public ProfileViewModel(TriviaWebAPIProxy service)
        {
            this.triviaService = service;
            this.NameError = "This is must";
            this.ShowNameError = false;
            this.PasswordError = "The password must include at least 8 digits and with at least 1 letter";
            this.ShowPasswordError = false;
            this.EmailError = "The email was not found";
            this.ShowEmailError = false;
            IsVisible = false;
            IsEnabled = true;
        }

        private User currentUser;
        public User CurrentUser
        {
            get
            {
                return ((App)Application.Current).LoggedInUser;
            }
            set
            {
                this.currentUser = value;
                OnPropertyChanged();
            }
        }

        private string newEmail;
        public string NewEmail
        {
            get
            {
                return newEmail;
            }
            set
            {
                this.newEmail = value;
                OnPropertyChanged();
            }
        }
        private string newPass;
        public string NewPass
        {
            get
            {
                return newPass;
            }
            set
            {
                this.newPass = value;
                OnPropertyChanged();
            }
        }
        private string newName;
        public string NewName
        {
            get
            {
                return newName;
            }
            set
            {
                this.newName = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }
        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }
        private bool isEnabled;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }

        }
        #endregion

        //constractor
        //initialize the service
        public ProfileViewModel(TriviaWebAPIProxy service)
        {
            this.triviaService = service;
        }

        #region changeUserDetails
        //on pressing change on any of the proparties in view
        public ICommand ChangeCommand => new Command(OnChangeCommand);
        #region validations
        private bool ValidateEmail(string Email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(Email);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private bool ValidatePassword(string pass)
        {
            return pass.Length > 8 && pass.Any(x => char.IsLetter(x)) ; // need to inclode one letter
        }
        #endregion

        //on ChangeCommand and getting with param (command parameter)
        //changes properties and update the DB
        async void OnChangeCommand(object param)
        {
            IsVisible = true;
            IsEnabled = false;
            
            List<User> users = await triviaService.GetAllUsers();
            switch (param)
            {
                case "email"://change email and check if the same one already esit
                    foreach (User user in users)
                    {
                        if (!ValidateEmail(newEmail))
                        {
                            await Shell.Current.DisplayAlert("Email", $"Email change failed! there is a problem with the email", "ok");
                            return;
                        }
                        if (user.Email == this.newEmail)
                        {
                            await Shell.Current.DisplayAlert("Email", $"Email change failed! A user in the system already uses this mail", "ok");
                            return;
                        }
                    }
                    CurrentUser.Email = this.newEmail;
                    await Shell.Current.DisplayAlert("Email", $"Email change succeeded! reload the page to loke at your new profile", "ok");
                    break;

                case "pass"://change password
                    if (!ValidatePassword(newPass))
                    {
                        await Shell.Current.DisplayAlert("Password", $"Password change failed! the password must be 8 letters log and include at least 1 letter", "ok");
                        return;
                    }
                    CurrentUser.Password = this.newPass;
                    break;
                case "name"://change name
                    foreach (User user in users)
                    {
                        if (NewName == null)
                        {
                            await Shell.Current.DisplayAlert("Name", $"Name change failed! the name cannot be empty", "ok");
                            return;
                        }
                        if (user.Name == this.NewName)
                        {
                            await Shell.Current.DisplayAlert("Name", $"Name change failed! The name already exist please chose a different name", "ok");
                            return;
                        }
                    }
                    CurrentUser.Name = this.newName;
                    break;

            }
            
            NewEmail = "";
            NewName = "";
            NewPass = "";
            inServerCall = true;
            //update the DB
            bool b = await triviaService.UpdateUser(CurrentUser);
            inServerCall = false;
            if (!b)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Try again later", "ok");
            }
            else
            {
                await Shell.Current.DisplayAlert("UpdateUser", $"Update seccesful! please open the profile page again to see your new details!", "ok");
                CurrentUser = (((App)Application.Current).LoggedInUser);
                IsVisible = false;
                IsEnabled = true;
            }

        }
        #endregion

    }
}
