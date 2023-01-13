using Hanseatic.ViewModel;

namespace Hanseatic;

public partial class BuyPage : ContentPage
{
	public BuyPage(BuyPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}