﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    [QueryProperty(nameof(CurrentQuestion), "selectedQuestion")]
    public class QuestionDetailsViewModel:ViewModelBase
    {
        TriviaWebAPIProxy service;
        private AmericanQuestion currentQuestion;
        public AmericanQuestion CurrentQuestion
        {
            get { return currentQuestion; }
            set
            {
                currentQuestion = value;
                OnPropertyChanged();
                UpdateStatus();
            }
        }
        public QuestionDetailsViewModel()
        {
            service = new TriviaWebAPIProxy();
           
        }
        public ICommand UpdateCommand => new Command(UpdateQuestion);
        public async void UpdateQuestion()
        {
            inServerCall = true;
          bool b = await service.UpdateQuestion(CurrentQuestion);
            inServerCall = false;
            if (!b)
            {
                await Application.Current.MainPage.DisplayAlert("Update", "Update Failed!", "ok");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Update", $"Update Succeed!", "ok");               
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
        private string status;
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }
        public void UpdateStatus()
        {
            switch (CurrentQuestion.Status)
            {
                case 0:
                    status = "Pending";
                    break;
                case 1:
                    status = "Approved";
                    break;
                case 2:
                    status = "Dismissed";
                    break;
            }
        }
        //private string qText;
        //public string QText 
        //{ 
        //    get { return qText; }
        //    set 
        //    { 
        //        qText = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private string correctAnswer;
        //public string CorrectAnswer
        //{
        //    get { return correctAnswer+"(correct)"; }
        //    set
        //    {
        //        correctAnswer = value;
        //        OnPropertyChanged();
        //    }
        //    }
        //private string bad1;
        //public string Bad1
        //{
        //    get { return bad1+"(wrong)"; }
        //    set
        //    {
        //        bad1 = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private string bad2;
        //public string Bad2
        //{
        //    get { return bad2 + "(wrong)"; }
        //    set
        //    {
        //        bad2 = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private string bad3;
        //public string Bad3
        //{
        //    get { return bad3 + "(wrong)"; }
        //    set
        //    {
        //        bad3 = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private int userId;
        //public string UserId
        //{
        //    get { return "User ID:"+userId; }
        //    set
        //    {
        //        userId = int.Parse(value);
        //        OnPropertyChanged();
        //    }
        //}

    }
}
