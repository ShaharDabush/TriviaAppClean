using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;

namespace TriviaAppClean.ViewModels
{
    public class ShellViewModel
    {
        public ShellViewModel() 
        {
            this.LogoutCommand = new Command(OnLogout);
        }
        public ICommand LogoutCommand { get; set; }

        private bool isMaster;
        private bool isAdmin;
        public bool IsMaster
        {
            get
            {
                if ((App)Application.Current == null)
                    return true;
                else if (((App)Application.Current).LoggedInUser.Rank > 0)
                    return true; 
                else
                    return false;
            }
        }
        public bool IsAdmin
        {
            get
            {
                if ((App)Application.Current == null)
                    return true;
                else if (((App)Application.Current).LoggedInUser.Rank > 1)
                    return true;
                else
                    return false;
            }
        }

        public async void OnLogout()
        {
            ((App)Application.Current).LoggedInUser = null;
            ((App)Application.Current).MainPage = ((App)Application.Current).Login;
        }
    }
}
