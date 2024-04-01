using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaAppClean.ViewModels
{
    public class QuestionDetailsViewModel:ViewModelBase
    {
        private string qText;
        public string QText 
        { 
            get { return qText; }
            set 
            { 
                qText = value;
                OnPropertyChanged();
            }
        }
        private string correctAnswer;
        public string CorrectAnswer
        {
            get { return correctAnswer; }
            set
            {
                correctAnswer = value;
                OnPropertyChanged();
            }
            }
        private string bad1;
        public string Bad1
        {
            get { return bad1; }
            set
            {
                bad1 = value;
                OnPropertyChanged();
            }
        }
        private string bad2;
        public string Bad2
        {
            get { return bad2; }
            set
            {
                bad2 = value;
                OnPropertyChanged();
            }
        }
        private string bad3;
        public string Bad3
        {
            get { return bad3; }
            set
            {
                bad3 = value;
                OnPropertyChanged();
            }
        }
        private int userId;
        public int UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                OnPropertyChanged();
            }
        }
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }
        private int status;
        public int Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }
    }
}
