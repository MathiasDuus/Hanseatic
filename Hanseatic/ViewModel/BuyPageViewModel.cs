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
        // TODO: maybe use on page load
        await Task.Delay(200);

        int cityId = await BuyManager.GetCityIdByName(CityName);

        var product = await BuyManager.GetAllByCityId(cityId);

        ProductsCollection = new ObservableCollection<CityProduct>();

        foreach (var prod in product)
        {
            Product prodType = await BuyManager.GetProductById(prod.Id);

            prod.SellPrice = -30 * (2 * (prod.ActualAmount / prod.DesiredAmount) - 1) ^ 3 + prod.BasePrice;
            if (prod.SellPrice < 0)
            {
                prod.SellPrice = 0;
            }

            prod.BuyPrice = prod.SellPrice + 3;

            prod.Product = prodType.Name;
            ProductsCollection.Add(prod);
        }
    }

}
