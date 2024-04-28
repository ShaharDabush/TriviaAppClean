using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
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

        private bool emailExist;
        public bool EmailExist
        {
            get
            {
                return emailExist;
            }
            set
            {
                this.emailExist = value;
                OnPropertyChanged();
            }
        }
        private bool passExist;
        public bool PassExist
        {
            get
            {
                return passExist;
            }
            set
            {
                this.passExist = value;
                OnPropertyChanged();
            }
        }
        private bool nameExist;
        public bool NameExist
        {
            get
            {
                return nameExist;
            }
            set
            {
                this.nameExist = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChangeCommand => new Command(OnChangeCommand);


        async void OnChangeCommand(object param)
        {
            List<User> users = await triviaService.GetAllUsers();
            switch (param)
            {
                case "email"://change email and check if the same one already esit
                    foreach (User user in users)
                    {
                        if (user.Email == this.newEmail)
                        {
                            emailExist = true;
                            await Shell.Current.DisplayAlert("Email", $"Email change failed! A user in the system already uses this mail", "ok");
                            return;
                        }
                    }
                    currentUser.Email = this.newEmail;
                    await Shell.Current.DisplayAlert("Email", $"Email change succeeded! reload the page to loke at your new profile", "ok");
                    break;
                case "pass"://change password
                    currentUser.Password = this.newPass;
                    break;
                case "name":
                    foreach (User user in users)
                    {
                        if (user.Name == this.NewName)
                        {
                            nameExist = true;
                        }
                    }
                    currentUser.Name = this.newName;

                    break;
            }
            await triviaService.UpdateUser(currentUser);

        }

    }
}
