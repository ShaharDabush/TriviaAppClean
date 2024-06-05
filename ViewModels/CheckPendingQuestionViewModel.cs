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
    public class CheckPendingQuestionViewModel : ViewModelBase
    {
        #region Attributes and properties
        private TriviaWebAPIProxy _proxy;
        //while we commune with the database this is true
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
        private ObservableCollection<AmericanQuestion> questions;
        public ObservableCollection<AmericanQuestion> Questions
        {
            get { return questions; }
            set
            {
                questions = value;
                OnPropertyChanged();
            }
        }
        public CheckPendingQuestionViewModel()
        {
            _proxy = new TriviaWebAPIProxy();
            GetQuestionsAsync();
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
        #endregion

        //command to check when a question on the list is selected
        public ICommand SingleSelectCommand => new Command(OnSingleSelectQuestion);

        //activated by the command SingleSelectCommand
        //this method send the user with shell to the PendingQuestionDetailsView of the selected question
        async void OnSingleSelectQuestion()
        {
            if (SelectedQuestion != null)
            {
                var navParam = new Dictionary<string, object>()
                {
                    { "selectedQuestion",SelectedQuestion }
                };
                await Shell.Current.GoToAsync($"PendingQuestionDetailsView", navParam);
                SelectedQuestion = null;
            }
        }

        //this method activated on opening the page and loads all of the questions that are pending (with Questionstatus equal to 0)
        //to the property Questions
        public async void GetQuestionsAsync()
        {
            inServerCall = true;
            List<AmericanQuestion> qs = await _proxy.GetAllQuestions();
            qs = qs.Where(q => q.Status == 0).ToList();
            Questions = new ObservableCollection<AmericanQuestion>(qs);
            inServerCall = false;
        }
        
        
    }
}
