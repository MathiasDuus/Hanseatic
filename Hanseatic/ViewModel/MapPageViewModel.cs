using CommunityToolkit.Mvvm.ComponentModel;
using Hanseatic.Data;

namespace Hanseatic.ViewModel
{
    public partial class MapPageViewModel : ObservableObject
    {
        [ObservableProperty]
        Save currentGame;

        [ObservableProperty]
        string date;

        [ObservableProperty]
        RadioButton selection;

        public MapPageViewModel()
        {
            LoadSave();
        }

        private async Task LoadSave()
        {
            CurrentGame = await MapManager.GetById(1);

            Date = CurrentGame.Date.ToString("yyyy MMM");
        }


        public void IncreaseDate()
        {
            CurrentGame.Date.AddMonths(1);

            Date = CurrentGame.Date.ToString("yyyy MMM");
        }
    }
}
