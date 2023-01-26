using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hanseatic.Managers;
using Hanseatic.Models;

namespace Hanseatic.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        // The name of the ship
        [ObservableProperty]
        string shipName;

        /// <summary>
        /// Command to submit the name entered.
        /// And goes to the mapPage
        /// </summary>
        /// <returns>Nothing</returns>
        [RelayCommand]
        async Task SubmitName()
        {
            if (string.IsNullOrWhiteSpace(ShipName))
                return;

            Ship postShip = await ShipManager.Post(ShipName);

            await ShipManager.PostShipProduct(postShip.Id);

            var ship = new Dictionary<string, object>
            {
                {"ship", postShip }
            };

            // Goes to the map
            await Shell.Current.GoToAsync($"{nameof(MapPage)}", ship);
        }
    }
}
