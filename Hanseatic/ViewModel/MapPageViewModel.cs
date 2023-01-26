using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hanseatic.Managers;
using Hanseatic.Models;

namespace Hanseatic.ViewModel
{

    // A parameter send via the shell
    [QueryProperty("Ship", "ship")]
    public partial class MapPageViewModel : ObservableObject
    {
        // Just an object of the current save game
        [ObservableProperty]
        Save currentGame;

        // The ship
        [ObservableProperty]
        Ship ship;

        // Formated version of the date
        [ObservableProperty]
        string date;

        // The selected radiobutton. Not used currently, I think.
        [ObservableProperty]
        RadioButton selection;

        public MapPageViewModel()
        {
            // Gets the current save
            Load();
        }

        /// <summary>
        /// Sends a request to the API to get the current save game
        /// And sets the date
        /// </summary>
        /// <returns></returns>
        private async Task Load()
        {
            CurrentGame = await MapManager.GetSaveById(1);

            Date = CurrentGame.Date.ToString("yyyy MMM");
        }

        /// <summary>
        /// Increases the date
        /// </summary>
        public async Task IncreaseDate()
        {
            CurrentGame.Date = CurrentGame.Date.AddMonths(1);

            CurrentGame = await MapManager.UpdateDate(CurrentGame);

            Date = CurrentGame.Date.ToString("yyyy MMM");
        }

        /// <summary>
        /// Goes to the city
        /// </summary>
        /// <param name="city_name"></param>
        [RelayCommand]
        public async void EnterCity(string city_name)
        {
            await IncreaseDate();
            var parameter = new Dictionary<string, object>
            {
                {
                    "city_name", city_name
                },
                {
                    "ship", Ship
                }

            };
            // Go To buypage
            await Shell.Current.GoToAsync($"{nameof(BuyPage)}", parameter);
        }

        [RelayCommand]
        public async Task GetShip()
        {
            Ship = await ShipManager.GetShip(Ship.Id);
        }
    }
}
