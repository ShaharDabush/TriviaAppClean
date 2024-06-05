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
    //gets the question from pending question ViewModel
    [QueryProperty(nameof(CurrentQuestion), "selectedQuestion")]
    public class PendingQuestionDetailsViewModel:ViewModelBase
    {
        #region attributes and proprties
        TriviaWebAPIProxy service;
        private AmericanQuestion currentQuestion;
        public AmericanQuestion CurrentQuestion
        {
            get { return currentQuestion; }
            set
            {
                currentQuestion = value;
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
        #endregion

        //constactor
        //initialize the service
        public PendingQuestionDetailsViewModel()
        {
            service = new TriviaWebAPIProxy();
        }

        //command on acceping the question
        public ICommand ApproveCommand => new Command(ApproveQuestion);

        //method
        //activated by ApproveCommand
        //chenges the status of the question to approved (change question status to 1)
        public async void ApproveQuestion()
        {
            CurrentQuestion.Status = 1;
            inServerCall = true;
            bool b = await service.UpdateQuestion(CurrentQuestion);
            inServerCall = false;
            if (!b)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Try again later", "ok");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Approved", "Question approved!", "ok");
            }
        }

        //command to disaprove the question
        public ICommand DismissCommand => new Command(DismissQuestion);

        //method
        //activated by DismissCommand
        //chenges the status of the question to Not Approved (Question status to 2) 
        public async void DismissQuestion()
        {
            CurrentQuestion.Status = 2;
            inServerCall = true;
            bool b = await service.UpdateQuestion(CurrentQuestion);
            inServerCall = false;
            if (!b)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Try again later", "ok");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Dismissed", "Question dismissed", "ok");
            }
        }
        
    }
}
