using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaAppClean.Models;

namespace TriviaAppClean.ViewModels
{
    [QueryProperty(nameof(selctedQuestion), "selctedQuestion")]
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
        private AmericanQuestion selctedQuestion;
        public AmericanQuestion SelectedQuestion
        {
            get { return selctedQuestion; }
            set { selctedQuestion = value; OnPropertyChanged(); }
        }

    }
}
