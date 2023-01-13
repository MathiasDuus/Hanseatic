using Hanseatic.ViewModel;

namespace Hanseatic;

public partial class MapPage : ContentPage
{
	public MapPage(MapPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

}

