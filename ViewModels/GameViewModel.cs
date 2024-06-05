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
        //on pressing the play button
        public ICommand PlayCommand => new Command(GoPlay);

        //send you to GameRegularView (via shell)
        private async void GoPlay()
        {
            await App.Current.MainPage.Navigation.PushAsync(new GameRegularView(new GameRegularViewModel()));
        }
    }
}
