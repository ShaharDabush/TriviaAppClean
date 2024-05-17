using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaAppClean.Models;

namespace TriviaAppClean.ViewModels
{
    [QueryProperty(nameof(CurrentQuestion), "selctedQuestion")]
    public class QuestionDetailsViewModel:ViewModelBase
    {
        private AmericanQuestion currentQuestion;
        public AmericanQuestion CurrentQuestion
        {
            get { return currentQuestion; }
            set
            {
                currentQuestion = value;
                OnPropertyChanged();
            }
        }
        //private string qText;
        //public string QText 
        //{ 
        //    get { return qText; }
        //    set 
        //    { 
        //        qText = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private string correctAnswer;
        //public string CorrectAnswer
        //{
        //    get { return correctAnswer+"(correct)"; }
        //    set
        //    {
        //        correctAnswer = value;
        //        OnPropertyChanged();
        //    }
        //    }
        //private string bad1;
        //public string Bad1
        //{
        //    get { return bad1+"(wrong)"; }
        //    set
        //    {
        //        bad1 = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private string bad2;
        //public string Bad2
        //{
        //    get { return bad2 + "(wrong)"; }
        //    set
        //    {
        //        bad2 = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private string bad3;
        //public string Bad3
        //{
        //    get { return bad3 + "(wrong)"; }
        //    set
        //    {
        //        bad3 = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private int userId;
        //public string UserId
        //{
        //    get { return "User ID:"+userId; }
        //    set
        //    {
        //        userId = int.Parse(value);
        //        OnPropertyChanged();
        //    }
        //}
        //private int id;
        //public string Id
        //{
        //    get { return "Question ID:"+id; }
        //    set
        //    {
        //        id = int.Parse(value);
        //        OnPropertyChanged();
        //    }
        //}
    }
}
