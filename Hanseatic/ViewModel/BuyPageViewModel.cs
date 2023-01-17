using CommunityToolkit.Mvvm.ComponentModel;
using Hanseatic.Data;
using System.Collections.ObjectModel;

namespace Hanseatic.ViewModel;

[QueryProperty("CityName", "city_name")]
public partial class BuyPageViewModel : ObservableObject
{
    [ObservableProperty]
    string cityName;


    [ObservableProperty]
    ObservableCollection<CityProduct> productsCollection;

    public BuyPageViewModel()
    {
        LoadCityProduct();
    }

    private async Task LoadCityProduct()
    {
        var prod = await BuyManager.GetAll();
        ProductsCollection = new ObservableCollection<CityProduct>();

        foreach (var product in prod)
        {
            ProductsCollection.Add(product);
        }
    }
}
