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
    [QueryProperty(nameof(CurrentQuestion), "selectedQuestion")]
    public class PendingQuestionDetailsViewModel:ViewModelBase
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
            }
        }
        public PendingQuestionDetailsViewModel()
        {
            service = new TriviaWebAPIProxy();
        }
        public ICommand ApproveCommand => new Command(ApproveQuestion);
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
        public ICommand DismissCommand => new Command(DismissQuestion);
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
    }
}
