using Hanseatic.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Json;
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

        /// <summary>
        /// Send a POST request to create all products for the ship with 0 in the amount
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static async Task<List<ShipProduct>> PostShipProduct(int id)
        {

            var products = await GetAllProducts();
            // Gets the client
            HttpClient client = await GetClient();

            // Creates the ship
            List<ShipProduct> shipProducts = new();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            foreach (Product product in products)
            {
                shipProducts.Add(new ShipProduct
                {
                    ProductTypeId = product.Id,
                    ShipId = id,
                    Amount = 0
                });
                //stopWatch.Start();
                // Converts the ship to a json string
                //string ShipJson = JsonConvert.SerializeObject(new ShipProduct
                //{
                //    ProductTypeId = product.Id,
                //    ShipId = id,
                //    Amount = 0
                //});

                //// Creates the body to be sent
                //StringContent data = new(ShipJson, Encoding.UTF8, "application/json");

                //// The response from the API
                //HttpResponseMessage response = await client.PostAsync($"{Url}/ship_product", data);

                //if (response.IsSuccessStatusCode)
                //{
                //    string result = await response.Content.ReadAsStringAsync();

                //    shipProducts.Add(JsonConvert.DeserializeObject<ShipProduct>(result));
                //}
                //else
                //{
                //    return new List<ShipProduct>();
                //}

                //stopWatch.Stop();
                //// Get the elapsed time as a TimeSpan value.
                //TimeSpan ts = stopWatch.Elapsed;

                //// Format and display the TimeSpan value.
                //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                //     ts.Hours, ts.Minutes, ts.Seconds,
                //     ts.Milliseconds / 10);
                //Console.WriteLine("RunTime " + elapsedTime);
            }
            //ship_product_list
            // Converts the ship to a json string
            string ShipJson = JsonConvert.SerializeObject(shipProducts);

            // Creates the body to be sent
            StringContent data = new(ShipJson, Encoding.UTF8, "application/json");

            // The response from the API
            HttpResponseMessage response = await client.PostAsync($"{Url}/ship_product/ship_product_list", data);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();

                shipProducts.Add(JsonConvert.DeserializeObject<ShipProduct>(result));
            }
            else
            {
                return new List<ShipProduct>();
            }

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                 ts.Hours, ts.Minutes, ts.Seconds,
                 ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);

            return shipProducts;
        }

        /// <summary>
        /// Gets all products
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<Product>> GetAllProducts()
        {

            // Check for internet, might have to disable, bc emulator
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new List<Product>();

            // Gets the http client used for request
            HttpClient client = await GetClient();

            // Awaits a return from the get request
            return await client.GetFromJsonAsync<IEnumerable<Product>>($"{Url}/product_type");
        }

        /// <summary>
        /// Gets the Ship by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<Ship> GetShip(int id)
        {

            // Check for internet, might have to disable, bc emulator
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new Ship();

            // Gets the http client used for request
            HttpClient client = await GetClient();

            // Awaits a return from the get request
            return await client.GetFromJsonAsync<Ship>($"{Url}/ship/{id}");
        }
    }
}
