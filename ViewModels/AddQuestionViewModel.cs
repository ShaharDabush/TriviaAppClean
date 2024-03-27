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
        private TriviaWebAPIProxy _proxy;
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

        public ICommand SubmitCommand => new Command(OnSubmitting);

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

                await _proxy.PostNewQuestion(AddedQuestion);
            }                AddedQuestion.UserId = ((App)Application.Current).LoggedInUser.Id;
            //need to exit to another page here
        }
    }
}
