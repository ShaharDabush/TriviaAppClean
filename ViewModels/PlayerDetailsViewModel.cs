﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Views;
using TriviaAppClean.Models;
using TriviaAppClean.ViewModels;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    [QueryProperty(nameof(CurrentUser), "selectedUser")]
    public class PlayerDetailsViewModel: ViewModelBase
    {
        TriviaWebAPIProxy service;
        private AmericanQuestion currentUser;
        public AmericanQuestion CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                OnPropertyChanged();
                //UpdateStatus();
            }
        }
        public PlayerDetailsViewModel()
        {
            service = new TriviaWebAPIProxy();

        }
        //public ICommand UpdateCommand => new Command(UpdateUser);
        //public async void UpdateUser()
        //{
        //    inServerCall = true;
        //    bool b = await service.UpdateUser(CurrentUser);
        //    inServerCall = false;
        //    if (!b)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Update", "Update Failed!", "ok");
        //    }
        //    else
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Update", $"Update Succeed!", "ok");
        //    }
        //}
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
        //private string status;
        //public string Status
        //{
        //    get { return status; }
        //    set
        //    {
        //        status = value;
        //        OnPropertyChanged();
        //    }
        //}
        //public void UpdateStatus()
        //{
        //    switch (CurrentQuestion.Status)
        //    {
        //        case 0:
        //            status = "Pending";
        //            break;
        //        case 1:
        //            status = "Approved";
        //            break;
        //        case 2:
        //            status = "Dismissed";
        //            break;
        //    }
        //}
    }
}
