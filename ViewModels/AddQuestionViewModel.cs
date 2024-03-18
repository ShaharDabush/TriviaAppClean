using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaAppClean.ViewModels
{
    internal class AddQuestionViewModel:ViewModelBase
    {
        public string flyoutTitleName = "Locked";

        public string FlyoutTitleName
        {
            get { return this.flyoutTitleName; }
            set
            {
                this.flyoutTitleName = value;
                OnPropertyChanged();
            }
        }

        public string flyoutIcon = "lock_icon.png";

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
        public AddQuestionViewModel()
        {
            SwichRanks = new Command(JustChecking);
            //JustChecking();
        }

        public void JustChecking()
        {
            if(this.flyoutTitleName == "Locked")
            {
                this.flyoutTitleName = " Add Question";
                this.flyoutIcon = "game_icon.png";
                this.IsFlyoutEnabled = false;
            }
            else
            {
                this.flyoutTitleName = "Locked";
                this.flyoutIcon = "lock_icon.png";
                this.IsFlyoutEnabled = true;
            }
        }
    }
}
