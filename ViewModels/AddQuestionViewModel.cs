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
    internal class AddQuestionViewModel:ViewModelBase
    {
        public string flyoutTitleName;

        public string FlyoutTitleName
        {
            get { return this.flyoutTitleName; }
            set
            {
                this.flyoutTitleName = value;
                OnPropertyChanged();
            }
        }

        public string flyoutIcon;

        public string FlyoutIcon
        {
            get { return this.flyoutIcon; }
            set
            {
                this.flyoutIcon = value;
                OnPropertyChanged();
            }
        }
        public bool isFlyoutEnabled;

        public bool IsFlyoutEnabled
        {
            get { return this.isFlyoutEnabled; }
            set
            {
                this.isFlyoutEnabled = value;
                OnPropertyChanged();
            }
        }
        public Command SwichRanks { get; set; }
        User u;
        
        public AddQuestionViewModel()
        {
            u = ((App)Application.Current).LoggedInUser;
            if (u.Rank == 1)
            {
                this.FlyoutTitleName = "Locked";
                this.FlyoutIcon = "lock_icon.png";
                this.IsFlyoutEnabled = false;
            }
            else
            {
                this.flyoutTitleName = " Add Question";
                this.flyoutIcon = "question_mark_icon.png";
                this.IsFlyoutEnabled = true;
            }

            SwichRanks = new Command(JustChecking);
            //JustChecking();
        }

        public void JustChecking()
        {
            if(this.flyoutTitleName == "Locked")
            {
                this.flyoutTitleName = " Add Question";
                this.flyoutIcon = "question_mark_icon.png";
                this.IsFlyoutEnabled = true;
            }
            else
            {
                this.flyoutTitleName = "Locked";
                this.flyoutIcon = "lock_icon.png";
                this.IsFlyoutEnabled = false;
            }
        }
    }
}
