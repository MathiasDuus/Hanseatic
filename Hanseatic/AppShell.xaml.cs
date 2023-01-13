namespace Hanseatic;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();


		Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));
		Routing.RegisterRoute(nameof(BuyPage), typeof(BuyPage));
	}
}
