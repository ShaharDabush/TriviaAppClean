using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;

namespace TriviaAppClean.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
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

      //  public ICommand ChangeCommand => new Command(OnChangeCommand);




    }
}
