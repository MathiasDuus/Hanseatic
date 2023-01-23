using Hanseatic.Models;
using Newtonsoft.Json;
using System.Text;

namespace Hanseatic.Managers
{
    class ShipManager
    {
        // The address which we base all calls upon
        static readonly string BaseAddress = "http://10.130.54.29:5000";
        // Adds api to the URI
        private static readonly string Url = $"{BaseAddress}/api";

        // The Http client used for http calls
        static HttpClient client;

        /// <summary>
        /// Get the client and adds header
        /// </summary>
        /// <returns></returns>
        private static async Task<HttpClient> GetClient()
        {
            // If there already is a client, do not make a new one
            if (client is not null)
                return client;

            // Creates a new client
            client = new HttpClient();

            // Sets a request header, so the respne is in json
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            // Returns the client
            return client;
        }

        /// <summary>
        /// Send a POST request to create a new ship
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static async Task<Ship> Post(string Name)
        {
            // Gets the client
            HttpClient client = await GetClient();

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

            return JsonConvert.DeserializeObject<Ship>(result); ;
        }
    }
}
