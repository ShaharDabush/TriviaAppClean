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

namespace TriviaAppClean.ViewModels
{
    public class GameRegularViewModel : ViewModelBase
    {
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

        public GameRegularViewModel()
        {
            _proxy = new TriviaWebAPIProxy();
            GetTheQuestion();
        }
        public ICommand AnswerCommand1 => new Command(Choice1);
        public ICommand AnswerCommand2 => new Command(Choice2);
        public ICommand AnswerCommand3 => new Command(Choice3);
        public ICommand AnswerCommand4 => new Command(Choice4);


        public void Choice1()
        {
            AgainNewQuestion(answer1);
        }
        public void Choice2()
        {
            AgainNewQuestion(answer2);
        }
        public void Choice3()
        {
            AgainNewQuestion(answer3);
        }
        public void Choice4()
        {
            AgainNewQuestion(answer4);
        }
        public async void AgainNewQuestion(string answer)
        {
            
            if(answer == randomQuestion.CorrectAnswer)
            {
                currentUser.Score =+ 10;
                await Application.Current.MainPage.DisplayAlert("Correct!", "Your score:" + CurrentUser.Score, "ok");
            }
           RandomQuestion = await _proxy.GetRandomQuestion();
            GetRandomAnswers();
            
 
        }
        public async void GetTheQuestion()
        {

            RandomQuestion = await _proxy.GetRandomQuestion();
            GetRandomAnswers();
        }

        Random rnd = new Random();
        public void GetRandomAnswers()
        {
            int num = rnd.Next(1, 5);
            if(num == 1)
            {
                answer1 = RandomQuestion.CorrectAnswer;
                answer2 = RandomQuestion.Bad1;
                answer3 = RandomQuestion.Bad2;
                answer4 = RandomQuestion.Bad3;
            }
            else if(num == 2)
            {
                answer1 = RandomQuestion.Bad3;
                answer2 = RandomQuestion.CorrectAnswer;
                answer3 = RandomQuestion.Bad1;
                answer4 = RandomQuestion.Bad2;
            }
            else if (num == 3)
            {
                answer1 = RandomQuestion.Bad2;
                answer2 = RandomQuestion.Bad3;
                answer3 = RandomQuestion.CorrectAnswer;
                answer4 = RandomQuestion.Bad1;
            }
            else
            {
                answer1 = RandomQuestion.Bad1;
                answer2 = RandomQuestion.Bad2;
                answer3 = RandomQuestion.Bad3;
                answer4 = RandomQuestion.CorrectAnswer;
            }
        }
    }
}
