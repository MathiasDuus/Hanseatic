using Hanseatic.ViewModel;

namespace Hanseatic;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}

