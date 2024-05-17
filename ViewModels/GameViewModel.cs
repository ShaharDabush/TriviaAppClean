using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Views;
namespace TriviaAppClean.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        GameRegularView gameRegularView;
        public ICommand PlayCommand => new Command(GoPlay);

        public GameViewModel(GameRegularView gameRegularView)
        {
            this.gameRegularView = gameRegularView;
        }

        private async void GoPlay()
        {
            await App.Current.MainPage.Navigation.PushAsync(gameRegularView);
        }
    }
}
