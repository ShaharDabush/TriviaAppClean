using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
using TriviaAppClean.ViewModels;
using TriviaAppClean.Views;

namespace TriviaAppClean.ViewModels
{
    public class PlayerListViewModel : ViewModelBase
    {
        #region attributs and properties
        private TriviaWebAPIProxy _proxy;
        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged();
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }
        private User selectedUser;
        public User SelectedUser
        {
            get { return selectedUser; }
            set { selectedUser = value; OnPropertyChanged(); }
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

        //constrator
        //initialize the service and get all users for the list
        public PlayerListViewModel()
        {
            _proxy = new TriviaWebAPIProxy();
            GetUsersAsync();
        }

        #region commands
        //on selecting a player
        public ICommand SingleSelectCommand => new Command(OnSingleSelectUser);

        //on searching a player by name (in view)
        public ICommand SortCommand => new Command(Sort);

        //on pressing the button to clear the sort
        public ICommand ClearSortCommand => new Command(GetUsersAsync);

        //on swiping left in the view
        public ICommand ResetScoreCommand => new Command<User>(ResetScore);

        #endregion

        #region methods    
        //on SingleSelectCommand
        //send you to PlayerDetailsView sending it the data of the player
        async void OnSingleSelectUser()
        {
            if (SelectedUser != null)
            {
                var navParam = new Dictionary<string, object>()
                {
                    { "selectedUser",SelectedUser }
                };
                await Shell.Current.GoToAsync($"PlayerDetailsView", navParam);
                SelectedUser = null;
            }
        }

        //on starting the page
        //get all the players for the list
        public async void GetUsersAsync()
        {
            inServerCall = true;
            List<User> us = await _proxy.GetAllUsers();
            Users = new ObservableCollection<User>(us);
            inServerCall = false;
        }



        //on SortCommand change the list and leave only the users that contain the given string
        public void Sort()
        {
            GetUsersAsync();
            List<User> temp = Users.Where(u => u.Name.Contains(Name)).ToList();
            Users = new ObservableCollection<User>(temp);
        }

        //on ResetScoreCommand 
        //reset the score of a user to 0 and updates the DB
        public async void ResetScore(User currentUser)
        {
            currentUser.Score = 0;
            inServerCall = true;
            bool b = await _proxy.UpdateUser(currentUser);
            inServerCall = false;
            if (!b)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Try again later", "ok");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Succses", "Score Reset", "ok");
            }
        }
        #endregion
    }
}
