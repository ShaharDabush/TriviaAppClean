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
        #region attributes and proparties
        private TriviaWebAPIProxy triviaService;
        public ProfileViewModel(TriviaWebAPIProxy service)
        {
            this.triviaService = service;
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
        #endregion

        #region changeUserDetails
        //on pressig
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
        async void OnChangeCommand(object param)
        {
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
            }

        }
        #endregion

    }
}
