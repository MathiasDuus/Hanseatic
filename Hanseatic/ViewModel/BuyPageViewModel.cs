using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hanseatic.Managers;
using Hanseatic.Models;
using System.Collections.ObjectModel;

namespace Hanseatic.ViewModel;

// Two parameters send via the shell
[QueryProperty("CityName", "city_name")]
[QueryProperty("Ship", "ship")]
public partial class BuyPageViewModel : ObservableObject
{
    // The current name of the city
    [ObservableProperty]
    string cityName;

    // The current ship
    [ObservableProperty]
    Ship ship;

    // A collection of the products in the city
    [ObservableProperty]
    ObservableCollection<CityProduct> productsCollection;

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

        // Substract amount from city product
        cityProduct.ActualAmount -= 1;

        // Add product to ship product
        cityProduct.ShipProductAmount += 1;

        // Calculates the price at which it should be sold
        cityProduct.SellPrice = UpdateSellPrice(cityProduct);

        // If the sell price is below 0, it should just be 0
        if (cityProduct.SellPrice < 0)
        {
            cityProduct.SellPrice = 0;
        }

        // The price which the player pays to get a product
        cityProduct.BuyPrice = cityProduct.SellPrice + 3;

        // Updates the amount in ship product
        ShipProduct shipProduct = new ShipProduct
        {
            Id = cityProduct.ShipProductId,
            ShipId = ship.Id,
            ProductTypeId = cityProduct.ProductID,
            Amount = cityProduct.ShipProductAmount
        };

        // Update Ship Product
        await BuyManager.PutShipProduct(shipProduct);

        // Update ship
        Ship = await BuyManager.PutShip(Ship);

        // Update city product
        await BuyManager.PutCityProduct(cityProduct);
    }

    [RelayCommand]
    public async void Sell(CityProduct cityProduct)
    {
        // Check if ship has enough product to sell
        if (cityProduct.ShipProductAmount == 0)
        {
            return;
        }

        // Substract price from coins
        Ship.Coin += cityProduct.SellPrice;

        // Increase amount in city product
        cityProduct.ActualAmount += 1;

        // Substract in ship product
        cityProduct.ShipProductAmount -= 1;

        // Calculates the price at which it should be sold
        cityProduct.SellPrice = UpdateSellPrice(cityProduct);

        // If the sell price is below 0, it should just be 0
        if (cityProduct.SellPrice < 0)
        {
            cityProduct.SellPrice = 0;
        }

        // The price which the player pays to get a product
        cityProduct.BuyPrice = cityProduct.SellPrice + 3;

        // Updates the amount in ship product
        ShipProduct shipProduct = new ShipProduct
        {
            Id = cityProduct.ShipProductId,
            ShipId = ship.Id,
            ProductTypeId = cityProduct.ProductID,
            Amount = cityProduct.ShipProductAmount
        };

        // Update Ship Product
        await BuyManager.PutShipProduct(shipProduct);

        // Update ship
        Ship = await BuyManager.PutShip(Ship);

        //Update city product
        await BuyManager.PutCityProduct(cityProduct);
    }

    [RelayCommand]
    private async Task LoadProducts()
    {
        // Call api to get the id of the city
        int cityId = await BuyManager.GetCityIdByName(CityName);

        // Gets all the products from the API
        var product = await BuyManager.GetAllByCityId(cityId);

        // Gets all the products from the API
        var shipProducts = await BuyManager.GetAllByShipId(Ship.Id);

        // Creates a new collection in the observed collection
        ProductsCollection = new ObservableCollection<CityProduct>();

        // Loops over the respone from the API
        foreach (CityProduct prod in product)
        {
            // Gets all the info on the product from the API
            Product prodType = await BuyManager.GetProductById(prod.ProductID);

            // Calculates the price at which it should be sold
            prod.SellPrice = UpdateSellPrice(prod);

            // If the sell price is below 0, it should just be 0
            if (prod.SellPrice < 0)
            {
                prod.SellPrice = 0;
            }

            // The price which the player pays to get a product
            prod.BuyPrice = prod.SellPrice + 3;

            // Sets the product name
            prod.Product = prodType.Name;

            // Gets the shipProduct that matches the product ID
            ShipProduct shipProduct = shipProducts.First(s => s.ProductTypeId == prod.ProductID);

            // Sets the product amount
            prod.ShipProductAmount = shipProduct.Amount;

            // adds ship product id
            prod.ShipProductId = shipProduct.Id;

            // Adds the product to the collection
            ProductsCollection.Add(prod);
        }
    }

    /// <summary>
    /// Update the sell price for the CityProduct
    /// </summary>
    /// <param name="prod"></param>
    /// <returns></returns>
    private int UpdateSellPrice(CityProduct prod)
    {
        double firstPart = Math.Pow(2 * (Convert.ToDouble(prod.ActualAmount) / Convert.ToDouble(prod.DesiredAmount)) - 1, 3);

        // Calculates the price at which it should be sold
        if (firstPart < 2)
        {
            prod.SellPrice = Convert.ToInt32(firstPart * (-30) + prod.BasePrice);
        }
        else
        {
            prod.SellPrice = 0;
        }
        return prod.SellPrice;
    }

    [RelayCommand]
    public async void ClearProducts()
    {
        ProductsCollection.Clear();
    }

}
