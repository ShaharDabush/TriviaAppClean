using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaAppClean.Views;
using TriviaAppClean.ViewModels;
using TriviaAppClean.Services;
using TriviaAppClean.Models;
using System.Windows.Input;
using Microsoft.Maui.Layouts;

namespace TriviaAppClean.ViewModels
{
    public class GameRegularViewModel : ViewModelBase
    {
        #region properties
        public static Random rnd = new Random();
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

        private List<AmericanQuestion> questionList;
        public List<AmericanQuestion> QuestionList
        {
            get
            {
                return questionList;
            }
            set
            {
                this.questionList = value;
                OnPropertyChanged();
            }
        }

        private string qtext;
        public string Qtext
        {
            get { return qtext; }
            set
            {
                qtext = value;
                OnPropertyChanged("Qtext");
            }
        }
        private string answer1;
        public string Answer1
        {
            get { return answer1; }
            set
            {
                answer1 = value;
                OnPropertyChanged("Answer1");
            }
        }

        private string answer2;
        public string Answer2
        {
            get { return answer2; }
            set
            {
                answer2 = value;
                OnPropertyChanged("Answer2");
            }
        }
        private string answer3;
        public string Answer3
        {
            get { return answer3; }
            set
            {
                answer3 = value;
                OnPropertyChanged("Answer3");
            }
        }
        private string answer4;
        public string Answer4
        {
            get { return answer4; }
            set
            {
                answer4 = value;
                OnPropertyChanged("Answer4");
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
                OnPropertyChanged();
            }
        }


        private AmericanQuestion randomQuestion;
        public AmericanQuestion RandomQuestion
        {
            get { return randomQuestion; }
            set
            {
                randomQuestion = value;
                OnPropertyChanged("RandomQuestion");
            }
        }


        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }
        private bool isVisible2;
        public bool IsVisible2
        {
            get { return isVisible2; }
            set
            {
                isVisible2 = value;
                OnPropertyChanged("IsVisible2");
            }
        }
        #endregion
        public GameRegularViewModel()
        {
            _proxy = new TriviaWebAPIProxy();
            GetQuestionList();
            IsVisible = true;
            IsVisible2 = false;

        }
        // Commend for evrey answer 
        public ICommand AnswerCommand1 => new Command(Choice1);
        public ICommand AnswerCommand2 => new Command(Choice2);
        public ICommand AnswerCommand3 => new Command(Choice3);
        public ICommand AnswerCommand4 => new Command(Choice4);


        public void Choice1()
        {
            ChangeIsVisible();
            AgainNewQuestion(answer1);
        }
        public void Choice2()
        {
            ChangeIsVisible();
            AgainNewQuestion(answer2);
        }
        public void Choice3()
        {
            ChangeIsVisible();
            AgainNewQuestion(answer3);
        }
        public void Choice4()
        {
            ChangeIsVisible();
            AgainNewQuestion(answer4);
        }
        public async void AgainNewQuestion(string answer)
        {
           
            
            if(answer == randomQuestion.CorrectAnswer)
            {
                inServerCall = true;

                ((App)Application.Current).LoggedInUser.Score += 10;
                inServerCall = false;
                inServerCall = true;

                await _proxy.UpdateUser(CurrentUser);
                inServerCall = false;

                // refresh the score
                await Application.Current.MainPage.DisplayAlert("Correct!", "Your score:" + CurrentUser.Score, "ok");

                if (((App)Application.Current).LoggedInUser.Score >= 100)
                {
                    await Application.Current.MainPage.DisplayAlert("congratulations!", "for reaching 100 points you can add a Question!", "ok");
                    

                    await Shell.Current.GoToAsync($"AddQuestionView");


                }
            }
            else
            {
                if (((App)Application.Current).LoggedInUser.Score < 5)
                {
                    ((App)Application.Current).LoggedInUser.Score = 0;
                }
                else
                {
                    ((App)Application.Current).LoggedInUser.Score += -5;
                }
                inServerCall = true;

                await _proxy.UpdateUser(CurrentUser);
                inServerCall = false;

                await Application.Current.MainPage.DisplayAlert("Wrong!", "Your score:" + CurrentUser.Score, "ok");
            }
            ChangeIsVisible();
            GetTheQuestion();
            

        }
        public async void GetQuestionList()
        {

            List<AmericanQuestion> QuestionListTemp = await _proxy.GetAllQuestions();
            QuestionList = new List<AmericanQuestion>(QuestionListTemp);
            foreach (AmericanQuestion item in QuestionListTemp)
            {
                if (item.Status != 1)
                {
                    QuestionList.Remove(item);
                }
            }
            GetTheQuestion();
        }

        public async void GetTheQuestion()
        {
            if (QuestionList != null)
            {
                int num = rnd.Next(QuestionList.Count);

                RandomQuestion = QuestionList[num];
                Qtext = RandomQuestion.QText;
            }
            else
            {
                RandomQuestion = await _proxy.GetRandomQuestion();
                Qtext = RandomQuestion.QText;
            }
            
            GetRandomAnswers();
        }
        public void ChangeIsVisible()
        {
            if(IsVisible)
            {
                IsVisible = false;
                IsVisible2 = true;
            }
            else { IsVisible = true; IsVisible2 = false; }
        }

        public void GetRandomAnswers() // method which randomize the four answers
        {
            isVisible = true;
            int num = rnd.Next(1, 5);
            if(num == 1)
            {
                Answer1 = RandomQuestion.CorrectAnswer;
                Answer2 = RandomQuestion.Bad1;
                Answer3 = RandomQuestion.Bad2;
                Answer4 = RandomQuestion.Bad3;
            }
            else if(num == 2)
            {
                Answer1 = RandomQuestion.Bad3;
                Answer2 = RandomQuestion.CorrectAnswer;
                Answer3 = RandomQuestion.Bad1;
                Answer4 = RandomQuestion.Bad2;
            }
            else if (num == 3)
            {
                Answer1 = RandomQuestion.Bad2;
                Answer2 = RandomQuestion.Bad3;
                Answer3 = RandomQuestion.CorrectAnswer;
                Answer4 = RandomQuestion.Bad1;
            }
            else
            {
                Answer1 = RandomQuestion.Bad1;
                Answer2 = RandomQuestion.Bad2;
                Answer3 = RandomQuestion.Bad3;
                Answer4 = RandomQuestion.CorrectAnswer;
            }
            
        }
    }
}
