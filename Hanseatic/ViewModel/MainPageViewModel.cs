using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hanseatic.Managers;
using Hanseatic.Models;

namespace Hanseatic.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        // The text in the input
        [ObservableProperty]
        string text;

        /// <summary>
        /// Command to submit the name entered.
        /// And goes to the mapPage
        /// </summary>
        /// <returns>Nothing</returns>
        [RelayCommand]
        async Task SubmitName()
        {
            if (string.IsNullOrWhiteSpace(Text))
                return;

            _ = new Ship
            {
                Name = Text
            };

            var ship = await ShipManager.Post(Text);

            Console.WriteLine(ship);

            await Shell.Current.GoToAsync(nameof(MapPage));
        }
    }
}
