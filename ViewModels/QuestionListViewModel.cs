using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;

namespace TriviaAppClean.ViewModels
{
    [QueryProperty(nameof(selectedQuestion), "selctedQuestion")]
    public class QuestionListViewModel : ViewModelBase
    {
        private List<AmericanQuestion> questions;
        public List<AmericanQuestion> Questions
        {
            get { return questions; }
            set { questions = value;
            OnPropertyChanged();
            }
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
    }
}
