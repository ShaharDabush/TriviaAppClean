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
        public PlayerListViewModel()
        {
            _proxy = new TriviaWebAPIProxy();
            GetUsersAsync();
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
        public ICommand SingleSelectCommand => new Command(OnSingleSelectUser);
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

        public async void GetUsersAsync()
        {
            inServerCall = true;
            List<User> us = await _proxy.GetAllUsers();
            Users = new ObservableCollection<User>(us);
            inServerCall = false;
        }
        public ICommand SortCommand => new Command(Sort);
        public void Sort()
        {
            GetUsersAsync();
            List<User> temp = Users.Where(u => u.Name.Contains(Name)).ToList();
            Users = new ObservableCollection<User>(temp);
        }
        public ICommand ClearSortCommand => new Command(GetUsersAsync);

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
        public ICommand ResetScoreCommand => new Command<User>(ResetScore);
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
    }
}
