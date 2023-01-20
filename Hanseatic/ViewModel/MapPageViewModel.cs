using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hanseatic.Managers;
using Hanseatic.Models;

namespace Hanseatic.ViewModel
{
    public partial class MapPageViewModel : ObservableObject
    {
        // Just an object of the current save game
        [ObservableProperty]
        Save currentGame;

        // Formated version of the date
        [ObservableProperty]
        string date;

        // The selected radiobutton. Not used currently, I thinnk.
        [ObservableProperty]
        RadioButton selection;

        public MapPageViewModel()
        {
            // Gets the current save
            LoadSave();
        }

        /// <summary>
        /// Sends a request to the API to get the current save game
        /// And sets the date
        /// </summary>
        /// <returns></returns>
        private async Task LoadSave()
        {
            CurrentGame = await MapManager.GetSaveById(1);

            Date = CurrentGame.Date.ToString("yyyy MMM");
        }

        /// <summary>
        /// Increases the date
        /// </summary>
        public void IncreaseDate()
        {
            CurrentGame.Date.AddMonths(1);

            Date = CurrentGame.Date.ToString("yyyy MMM");
        }

        /// <summary>
        /// Goes to the city
        /// </summary>
        /// <param name="sender"></param>
        [RelayCommand]
        public async void EnterCity(object sender)
        {
            if (sender is RadioButton radioButton)
            {
                if (radioButton.IsChecked)
                {
                    // Go To buypage
                    await Shell.Current.GoToAsync($"{nameof(BuyPage)}?city_name={radioButton.Content}");
                }
            }
        }
    }
}
