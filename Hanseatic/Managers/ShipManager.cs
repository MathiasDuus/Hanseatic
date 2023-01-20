using Hanseatic.Models;
using System.Text;

namespace Hanseatic.Managers
{
    class ShipManager
    {
        // The url to call the API
        static readonly string Url = HttpClientManager.Url;

        /// <summary>
        /// Send a POST request to create a new ship
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static async Task<Ship> Post(string Name)
        {
            // Gets the client
            HttpClient client = await HttpClientManager.GetClient();

            // Creates the ship
            Ship ship = new()
            {
                Name = Name,
                Coin = 500,
                SaveId = 1
            };

            // Converts the ship to a json string
            string ShipJson = Newtonsoft.Json.JsonConvert.SerializeObject(ship);

            // Creates the body to be sent
            StringContent data = new(ShipJson, Encoding.UTF8, "application/json");

            // The response from the API
            HttpResponseMessage response = await client.PostAsync($"{Url}/ship", data);

            // Converts the response to a string
            string result = await response.Content.ReadAsStringAsync();

            return ship;
        }
    }
}
