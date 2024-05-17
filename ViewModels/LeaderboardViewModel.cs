using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaAppClean.Services;
using TriviaAppClean.Models;
using TriviaAppClean.Views;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
//using static Android.Content.ClipData;

namespace TriviaAppClean.ViewModels
{
    public class LeaderboardViewModel:ViewModelBase
    {
        private TriviaWebAPIProxy triviaService;
        private ObservableCollection<User> leaderboardUsers;
        public ObservableCollection<User> LeaderboardUsers
        {
            get { return this.leaderboardUsers; }
            set
            {
                this.leaderboardUsers = value;
                OnPropertyChanged();

            }
        }
       
        public LeaderboardViewModel(TriviaWebAPIProxy triviaService)
        {
           this.triviaService = triviaService;
           GetListAsync();
          
           //LeaderboardUsers = triviaService.GetAllUsers();
           // var OrderedUsers = LeaderboardUsers.toList().OrderBy(x => x.Age).ThenBy(x => x.Salary).ToList();
        }
        

        private string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                OnPropertyChanged();
            }
        }
        private string questionsAdded;
        public string QuestionsAdded
        {
            get { return questionsAdded; }
            set
            {
                this.questionsAdded = value;
                OnPropertyChanged();
            }
        }
        public async void GetListAsync()
        {
            List<User> list = await triviaService.GetAllUsers();
            list = list.OrderByDescending(x => x.Questions.Count).ToList();
            LeaderboardUsers = new ObservableCollection<User>(list);

        }
      

    }
}
