﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaAppClean.Services;
using TriviaAppClean.Models;
using TriviaAppClean.Views;
//using static Android.Content.ClipData;

namespace TriviaAppClean.ViewModels
{
    internal class LeaderboardViewModle:ViewModelBase
    {
        private TriviaWebAPIProxy triviaService;
        private Task<List<User>> leaderboardUsers;
        public LeaderboardViewModle(TriviaWebAPIProxy triviaService)
        {
           this.triviaService = triviaService;
            this.LeaderboardUsers = GetListAsync();
           //LeaderboardUsers = triviaService.GetAllUsers();
           // var OrderedUsers = LeaderboardUsers.toList().OrderBy(x => x.Age).ThenBy(x => x.Salary).ToList();
        }
        public Task<List<User>> LeaderboardUsers
        {
            get { return this.leaderboardUsers; }
            set
            {
                this.leaderboardUsers = value;
                OnPropertyChanged();
            }
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
        private string questionAdded;
        public string QuestionAdded
        {
            get { return this.questionAdded; }
            set
            {
                this.questionAdded = value;
                OnPropertyChanged();
            }
        }
        public async Task<List<User>> GetListAsync()
        {
            List<User> list = await Task.Run(() => triviaService.GetAllUsers());
            var OrderedUsers = list.OrderBy(x => x.Questions).ThenBy(x => x.Questions).ToList();
            return OrderedUsers;
        }



    }
}
