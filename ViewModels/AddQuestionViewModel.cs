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

namespace TriviaAppClean.ViewModels
{
    public class AddQuestionViewModel:ViewModelBase
    {
        #region Attributes and properties
        private TriviaWebAPIProxy _proxy;
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
        private AmericanQuestion addedQuestion;
        public AmericanQuestion AddedQuestion
        {
            get { return addedQuestion; }
            set
            {
                addedQuestion = value;
                OnPropertyChanged("AddedQuestion");
            }
        }
        //while we commune with the DB this is true
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

        //command when submitting the new question
        public ICommand SubmitCommand { get; set; }

        
        public AddQuestionViewModel() 
        {
            _proxy = new TriviaWebAPIProxy();
            addedQuestion = new AmericanQuestion();
            SubmitCommand = new Command(OnSubmitting);
        }


        //activated by command SubmitCommand
        //This method checks if any field is null ==>
        //send the new question to the DataBase ==>
        //increase the score of the user
        async void OnSubmitting()
        {
            if (AddedQuestion != null)
            {
                if (AddedQuestion.QText ==null||AddedQuestion.CorrectAnswer==null|| AddedQuestion.Bad1 == null || AddedQuestion.Bad2 == null || AddedQuestion.Bad3 == null)
                {
                    return;
                }
                AddedQuestion.UserId = ((App)Application.Current).LoggedInUser.Id;
                AddedQuestion.Status = 0;
                CurrentUser.Score = 0;
                inServerCall = true;

                await _proxy.UpdateUser(CurrentUser);
                inServerCall = false;
                await _proxy.PostNewQuestion(AddedQuestion);
                AddedQuestion = null;

                await Shell.Current.DisplayAlert("UpdateUser", $"your question has successfuly been added!", "ok");

                if (((App)Application.Current).LoggedInUser.Questions.Count >= 10 && ((App)Application.Current).LoggedInUser.Rank == 0)
                {
                    await Application.Current.MainPage.DisplayAlert("congratulations!", "for adding 10 questions you are promoted to master rank!", "ok");
                    ((App)Application.Current).LoggedInUser.Rank += 1;
                    inServerCall = true;

                    await _proxy.UpdateUser(CurrentUser);
                    inServerCall = false;

                }

                Application.Current.MainPage = new AppShell(new ShellViewModel());


            }

        }
    }
}