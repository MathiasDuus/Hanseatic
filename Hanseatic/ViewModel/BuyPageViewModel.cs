using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanseatic.ViewModel;

public partial class BuyPageViewModel : ObservableObject
{
    
    public BuyPageViewModel()
    {

        productsCollection = new ObservableCollection<Products>()
        {
            new Products
            {
                Name = "Beer",
                Town = 62,
                Buy = 50,
                Sell = 47,
                Ship = 0
            },
            new Products
            {
                Name = "Bricks",
                Town = 60,
                Buy = 100,
                Sell = 95,
                Ship = 0
            },
            new Products
            {
                Name = "Cloth",
                Town = 20,
                Buy = 302,
                Sell = 281,
                Ship = 0
            },
            new Products
            {
                Name = "Fish",
                Town = 15,
                Buy = 465,
                Sell = 444,
                Ship = 0
            },
            new Products
            {
                Name = "Grain",
                Town = 6,
                Buy = 237,
                Sell = 166,
                Ship = 0
            },
            new Products
            {
                Name = "Hemp",
                Town = 5,
                Buy = 769,
                Sell = 601,
                Ship = 0
            },
            new Products
            {
                Name = "Honey",
                Town = 24,
                Buy = 130,
                Sell = 123,
                Ship = 0
            },
            new Products
            {
                Name = "Iron Goods",
                Town = 0,
                Buy = 0,
                Sell = 560,
                Ship = 19
            },
            new Products
            {
                Name = "Leather",
                Town = 2,
                Buy = 811,
                Sell = 453,
                Ship = 0
            },
            new Products
            {
                Name = "Meat",
                Town = 0,
                Buy = 0,
                Sell = 2029,
                Ship = 0
            },
            new Products
            {
                Name = "Pig Iron",
                Town = 10,
                Buy = 924,
                Sell = 853,
                Ship = 0
            },
            new Products
            {
                Name = "Pitch",
                Town = 25,
                Buy = 60,
                Sell = 57,
                Ship = 0
            }
        };   
        
    }

    [ObservableProperty]
    ObservableCollection<Products> productsCollection;
}

public class Products
{
    public string Name { get; set; }
    public int Town { get; set; }
    public int Buy { get; set; }
    public int Sell { get; set; }
    public int Ship { get; set; }
}