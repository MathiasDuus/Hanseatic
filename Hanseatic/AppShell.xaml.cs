namespace Hanseatic;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// Registering of routes
		Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));
		Routing.RegisterRoute(nameof(BuyPage), typeof(BuyPage));
	}
}
