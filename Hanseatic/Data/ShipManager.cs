using Hanseatic.Model;
using System.Text;

namespace Hanseatic.Data
{
    class ShipManager
    {
        static readonly string BaseAddress = "http://10.130.54.29:5000";
        static readonly string Url = $"{BaseAddress}/api";

        static HttpClient client;

        private static async Task<HttpClient> GetClient()
        {
            if (client is not null)
                return client;


            client = new HttpClient();

            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public static async Task<Ship> Post(string Name)
        {

            HttpClient client = await GetClient();

            var ship = new Ship
            {
                Name = Name,
                Coin = 500,
                SaveId = 1
            };

            var ShipJson = Newtonsoft.Json.JsonConvert.SerializeObject(ship);

            StringContent data = new StringContent(ShipJson, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{Url}/ship", data);

            string result = await response.Content.ReadAsStringAsync();

            //return await client.PostAsync($"{Url}/ship", data);

            //(Url, new StringContent(ShipJson, Encoding.UTF8, "application/json"));
            return ship;
        }
    }
}
