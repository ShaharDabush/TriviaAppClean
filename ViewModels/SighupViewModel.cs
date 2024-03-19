using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TriviaAppClean.ViewModels
{
    internal class SighupViewModel: ViewModelBase
    {
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
            this.ShowPasswordError = Password == null || Password.Length < 8 ;
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
            this.ShowEmailError = Email == null || email.IndexOf("@") == 1 ;
        }
        #endregion
    }
}
