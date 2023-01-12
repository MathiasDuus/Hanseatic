using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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


            _ = new Player
            {
                Name = Text
            };

            await Shell.Current.GoToAsync(nameof(MapPage));
        }
    }
}
