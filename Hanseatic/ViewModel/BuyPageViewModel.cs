using CommunityToolkit.Mvvm.ComponentModel;
using Hanseatic.Managers;
using Hanseatic.Models;
using System.Collections.ObjectModel;

namespace Hanseatic.ViewModel;

// A parameter send via the shell
[QueryProperty("CityName", "city_name")]
public partial class BuyPageViewModel : ObservableObject
{
    // The current name of the city
    [ObservableProperty]
    string cityName;

    // A collection of the products in the city
    [ObservableProperty]
    ObservableCollection<CityProduct> productsCollection;

    public BuyPageViewModel()
    {
        // Get all the products in the city
        LoadCityProduct();
    }

    /// <summary>
    /// Calls the api to get all the products in the city and 
    /// adds them to the observed product collection
    /// </summary>
    /// <returns></returns>
    private async Task LoadCityProduct()
    {
        // TODO: maybe use on page load

        // Used to endure that "CityName" is set
        await Task.Delay(200);

        // Call api to get the id of the city
        int cityId = await BuyManager.GetCityIdByName(CityName);

        // Gets alle the products from the API
        IEnumerable<CityProduct> product = await BuyManager.GetAllByCityId(cityId);

        // Creates a new collection in the observed collection
        ProductsCollection = new ObservableCollection<CityProduct>();

        // Loops over the respone from the API
        foreach (CityProduct prod in product)
        {
            // Gets all the info on the product from the API
            Product prodType = await BuyManager.GetProductById(prod.Id);

            // Calculates the price at which it should be sold
            prod.SellPrice = -30 * (2 * (prod.ActualAmount / prod.DesiredAmount) - 1) ^ 3 + prod.BasePrice;

            // If the sell price is below 0, it should just be 0
            if (prod.SellPrice < 0)
            {
                prod.SellPrice = 0;
            }

            // The price which the player pays to get a product
            prod.BuyPrice = prod.SellPrice + 3;

            // Sets the product name
            prod.Product = prodType.Name;

            // Adds the product to the collection
            ProductsCollection.Add(prod);
        }
    }

}
