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
        private TriviaWebAPIProxy _proxy;
        private ObservableCollection<AmericanQuestion> questions;
        public ObservableCollection<AmericanQuestion> Questions
        {
            get { return questions; }
            set { questions = value;
            OnPropertyChanged();
            }
        }
        public QuestionListViewModel()
        {            
            _proxy = new TriviaWebAPIProxy();
            GetQuestionsAsync();
        }
        private string query;
        public string Query
        {
            get { return query; }
            set {  query = value; OnPropertyChanged(); }
        }
        private AmericanQuestion selectedQuestion;
        public AmericanQuestion SelectedQuestion
        {
            get { return selectedQuestion; }
            set { selectedQuestion = value; OnPropertyChanged(); }
        }
        public ICommand SingleSelectCommand => new Command(OnSingleSelectQuestion);
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
       
        public async void GetQuestionsAsync()
        {
            inServerCall = true;
            List<AmericanQuestion> qs = await _proxy.GetAllQuestions();
            Questions = new ObservableCollection<AmericanQuestion>(qs);
            inServerCall = false;
        }
        public ICommand SortCommand => new Command(Sort);
        public void Sort()
        {
            GetQuestionsAsync();
            List<AmericanQuestion> temp = Questions.Where(q => q.QText.Contains(query)).ToList();            
            Questions = new ObservableCollection<AmericanQuestion>(temp);
        }
        public ICommand ClearSortCommand => new Command(GetQuestionsAsync);

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
        public ICommand DismissCommand => new Command(DismissQuestion);
        public async void DismissQuestion()
        {
            SelectedQuestion.Status = 2;
            inServerCall = true;
            bool b = await _proxy.UpdateQuestion(SelectedQuestion);
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