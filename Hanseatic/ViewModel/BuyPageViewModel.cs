using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hanseatic.Managers;
using Hanseatic.Models;
using System.Collections.ObjectModel;

namespace Hanseatic.ViewModel;

// A parameter send via the shell
[QueryProperty("CityName", "city_name")]
[QueryProperty("Ship", "ship")]
public partial class BuyPageViewModel : ObservableObject
{
    // The current name of the city
    [ObservableProperty]
    string cityName;

    [ObservableProperty]
    Ship ship;

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
        int shipId = 1;

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
    public async void Buy(CityProduct cityProduct)
    {
        // Check if ship has enough coin to buy
        if (Ship.Coin < cityProduct.BuyPrice)
        {
            return;
        }

        // Substract price from coins
        Ship.Coin -= cityProduct.BuyPrice;

        // Update ship
        Ship = await BuyManager.PutShip(Ship);

        // Substract amount from city product
        cityProduct.ActualAmount -= 1;


        var lil = new CityProduct
        {
            Id = cityProduct.Id,
            CityId = cityProduct.CityId,
            SaveId = cityProduct.SaveId,
            ProductID = cityProduct.ProductID,
            DesiredAmount = cityProduct.DesiredAmount,
            ActualAmount = cityProduct.ActualAmount,
            BasePrice = cityProduct.BasePrice,
            MinAmountFluctation = cityProduct.MinAmountFluctation,
            MaxAmountFluctation = cityProduct.MaxAmountFluctation
        };

        //Update city product
        cityProduct = await BuyManager.PutCityProduct(lil);
        ProductsCollection = new ObservableCollection<CityProduct>(ProductsCollection);

    }

    [RelayCommand]
    public async void Sell(CityProduct cityProduct)
    {
        // Get the city product from the api
        // CityProduct cityProductAPI = await BuyManager.GetCityProductById(cityProduct.Id);

        // Get the city from product collection
        // CityProduct cityProductAPI = productsCollection[cityProduct.Id];
        CityProduct cityProductAPI = (CityProduct)productsCollection.Single(i => i.Id == cityProduct.Id);

        // Check if ship has enough product to sell
        if (cityProduct.ShipProductAmount == 0)
        {
            return;
        }

        // Substract price from coins
        Ship.Coin += cityProduct.SellPrice;

        // Update ship
        Ship = await BuyManager.PutShip(Ship);

        // Increase amount in city product
        cityProductAPI.ActualAmount += 1;

        var lil = new CityProductAPI
        {
            Id = cityProductAPI.Id,
            CityId = cityProductAPI.CityId,
            SaveId = cityProductAPI.SaveId,
            ProductId = cityProductAPI.ProductID,
            DesiredAmount = cityProductAPI.DesiredAmount,
            ActualAmount = cityProductAPI.ActualAmount,
            BasePrice = cityProductAPI.BasePrice,
            MinAmountFluctation = cityProductAPI.MinAmountFluctation,
            MaxAmountFluctation = cityProductAPI.MaxAmountFluctation
        };

        //Update city product
        await BuyManager.PutCityProduct(lil);
    }

}
