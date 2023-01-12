using Android.Database;
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
    /*
    public productsViewModel()
    {
        
        products = new ObservableCollection<object>
        {
            new products
            {
                Name = "Beer",
                Town = 62,
                Buy = 50,
                Sell = 47,
                Ship = 0
            }
        };   
        
    }
    */

    [ObservableProperty]
    ObservableCollection<Object> products;
}
