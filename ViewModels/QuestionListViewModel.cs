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
                    { "selectedMonkey",SelectedQuestion }
                };
                await Shell.Current.GoToAsync($"monkeyDetails", navParam);
                SelectedQuestion = null;
            }
        }
    }
}
