using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        //LoadCityProduct();
        //LoadShipProduct();
        LoadProducts();
    }

    private async Task LoadProducts()
    {
        // Get ship id
        int shipId = 3;

        // Used to endure that "CityName" is set
        await Task.Delay(200);

        // Call api to get the id of the city
        int cityId = await BuyManager.GetCityIdByName(CityName);

        // Gets all the products from the API
        var product = await BuyManager.GetAllByCityId(cityId);

        // Gets all the products from the API
        var shipProducts = await BuyManager.GetAllByShipId(shipId);

        // Creates a new collection in the observed collection
        ProductsCollection = new ObservableCollection<CityProduct>();

        // Loops over the respone from the API
        foreach (CityProduct prod in product)
        {
            // Gets all the info on the product from the API
            Product prodType = await BuyManager.GetProductById(prod.ProductID);

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

            foreach (ShipProduct shipProduct in shipProducts)
            {
                prod.ShipProductAmount = 0;
                if (shipProduct.ProductTypeID == prod.ProductID)
                {
                    prod.ShipProductAmount = shipProduct.Amount;
                }
            }

            // Adds the product to the collection
            ProductsCollection.Add(prod);
        }
    }

    [RelayCommand]
    public async void Buy(int cityProductId)
    {
        int shipId = 3;

        // Get the product from the api
        CityProduct product = productsCollection[cityProductId];

        // Get the ship from the api
        Ship ship = await BuyManager.GetShipById(shipId);

        int Coins = ship.Coin - product.BuyPrice;

        var newShip = new Ship
        {
            Id = shipId,
            Name = ship.Name,
            Coin = Coins,
            SaveId = ship.SaveId
        };

        await BuyManager.PutShip(newShip);
    }

}
