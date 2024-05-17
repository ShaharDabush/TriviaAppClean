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
        public ICommand DeleteCommand => new Command<AmericanQuestion>(RemoveQuestion);

        public void RemoveQuestion(AmericanQuestion americanQuestion)
        {
            if (Questions.Contains(americanQuestion))
            {
                Questions.Remove(americanQuestion);
            }
        }
       
        public async void GetQuestionsAsync()
        {
            List<AmericanQuestion> qs = await _proxy.GetAllQuestions();
            Questions = new ObservableCollection<AmericanQuestion>(qs);
        }
        public ICommand SortCommand => new Command(Sort);
        public void Sort()
        {
            GetQuestionsAsync();
            ObservableCollection<AmericanQuestion> temp = new ObservableCollection<AmericanQuestion>();
            foreach (AmericanQuestion question in Questions)
            {
                if (question.QText.Contains(query))
                {
                    temp.Add(question);
                }
            }
            Questions = temp;
        }
        public ICommand ClearSortCommand => new Command(GetQuestionsAsync);


    }
}