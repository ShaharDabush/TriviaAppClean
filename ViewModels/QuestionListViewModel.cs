using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{    
    public class QuestionListViewModel : ViewModelBase
    {
        #region attributes and proparties
        private TriviaWebAPIProxy _proxy;
        private ObservableCollection<AmericanQuestion> questions;

        public ObservableCollection<AmericanQuestion> Questions
        {
            get { return questions; }
            set { questions = value;
            OnPropertyChanged();
            }
        }

        private string query;
        public string Query
        {
            get { return query; }
            set { query = value; OnPropertyChanged(); }
        }
        private AmericanQuestion selectedQuestion;
        public AmericanQuestion SelectedQuestion
        {
            get { return selectedQuestion; }
            set { selectedQuestion = value; OnPropertyChanged(); }
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

        //constractor
        //initialise the service and the list of questions
        public QuestionListViewModel()
        {            
            _proxy = new TriviaWebAPIProxy();
            GetQuestionsAsync();
        }

        #region commands
        //on filtering the list
        public ICommand SortCommand => new Command(Sort);

        //on selecting a question
        public ICommand SingleSelectCommand => new Command(OnSingleSelectQuestion);

        //on presing to clear the sort/filter
        public ICommand ClearSortCommand => new Command(GetQuestionsAsync);

        //on swiping left
        public ICommand DismissCommand => new Command<AmericanQuestion>(DismissQuestion);


        #endregion

        #region methods
        //activated with SingleSelectCommand
        //send to QuestionDetailsView of the current question
        async void OnSingleSelectQuestion()
        {
            if (SelectedQuestion != null)
            {
                var navParam = new Dictionary<string, object>()
                {
                    { "selectedQuestion",SelectedQuestion }
                };
                await Shell.Current.GoToAsync($"QuestionDetailsView", navParam);
                SelectedQuestion = null;
            }
        }       
       
        //on constractor and on clearing sort
        //initilize list of questions
        public async void GetQuestionsAsync()
        {
            inServerCall = true;
            List<AmericanQuestion> qs = await _proxy.GetAllQuestions();
            Questions = new ObservableCollection<AmericanQuestion>(qs);
            inServerCall = false;
        }

        //on SortCommand
        //sort questions by a string they contain
        public void Sort()
        {
            GetQuestionsAsync();
            List<AmericanQuestion> temp = Questions.Where(q => q.QText.Contains(query)).ToList();            
            Questions = new ObservableCollection<AmericanQuestion>(temp);
        }

        //on DismissCommand
        //change status of question to not approved (question status 2) 
        public async void DismissQuestion(AmericanQuestion currentQuestion)
        {
            currentQuestion.Status = 2;
            inServerCall = true;
            bool b = await _proxy.UpdateQuestion(currentQuestion);
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

        #endregion
    }
}