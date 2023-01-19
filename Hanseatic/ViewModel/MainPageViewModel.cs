using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hanseatic.Data;
using Hanseatic.Model;

namespace Hanseatic.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        string text;

        [RelayCommand]
        async Task SubmitName()
        {
            if (string.IsNullOrWhiteSpace(Text))
                return;


            // Make Post request to server, to store the name, and then go to the next page.


            _ = new Ship
            {
                Name = Text
            };

            await ShipManager.Post(Text);

            await Shell.Current.GoToAsync(nameof(MapPage));
        }
    }
}
