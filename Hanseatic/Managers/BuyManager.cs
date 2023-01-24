using Hanseatic.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Hanseatic.Managers
{
    class BuyManager
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

        public static async Task<IEnumerable<CityProduct>> GetAll()
        {
            // Check for internet, might have to disable, bc emulator
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new List<CityProduct>();

            // Gets the client used to make http requests
            HttpClient client = await GetClient();

            // Waits for the request to finish
            return await client.GetFromJsonAsync<IEnumerable<CityProduct>>($"{Url}/city_product");
        }

        public static async Task<CityProduct> Add(string cityProductName, string supplier, string cityProductType)
        {
            throw new NotImplementedException();
        }

        public static async Task Update(CityProduct cityProduct)
        {
            throw new NotImplementedException();
        }

        public static async Task Delete(string cityProductID)
        {
            throw new NotImplementedException();
        }

        public static async Task<Ship> PutShip(Ship ship)
        {
            // Check for internet, might have to disable, bc emulator
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new Ship();

            // Gets the client used to make http requests
            HttpClient client = await GetClient();

            // Returns a list of products in the city
            var response = await client.PutAsJsonAsync<Ship>($"{Url}/ship/", ship);

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Ship>(json);
        }

        public static async Task<CityProduct> PutCityProduct(CityProduct cityProduct)
        {
            // Check for internet, might have to disable, bc emulator
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new CityProduct();

            // Gets the client used to make http requests
            HttpClient client = await GetClient();

            // Returns a list of products in the city
            var response = await client.PutAsJsonAsync<CityProduct>($"{Url}/city_product/", cityProduct);

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CityProduct>(json);
        }

        /// <summary>
        /// Gets the city ID via the name of the city
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<int> GetCityIdByName(string name)
        {
            // Check for internet, might have to disable, bc emulator
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return -1;

            // Gets the client used to make http requests
            HttpClient client = await GetClient();

            // Gets the city from the API
            City respone = await client.GetFromJsonAsync<City>($"{Url}/city/GetByName/{name}");

            // Returns only the ID
            return respone.Id;
        }

        /// <summary>
        /// Gets all the products in the city via city ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<CityProduct>> GetAllByCityId(int id)
        {
            // Check for internet, might have to disable, bc emulator
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new List<CityProduct>();

            // Gets the client used to make http requests
            HttpClient client = await GetClient();

            // Returns a list of products in the city
            return await client.GetFromJsonAsync<IEnumerable<CityProduct>>($"{Url}/city_product/GetByCityId/{id}");
        }

        /// <summary>
        /// Gets the product via Product ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<Product> GetProductById(int id)
        {
            // Check for internet, might have to disable, bc emulator
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new Product();

            // Gets the client used to make http requests
            HttpClient client = await GetClient();

            // Returns the product
            return await client.GetFromJsonAsync<Product>($"{Url}/product_type/{id}");
        }

        public static async Task<Ship> GetShipById(int id)
        {
            // Check for internet, might have to disable, bc emulator
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new Ship();

            // Gets the client used to make http requests
            HttpClient client = await GetClient();

            // Returns the product
            return await client.GetFromJsonAsync<Ship>($"{Url}/ship/{id}");
        }

        public static async Task<CityProduct> GetCityProductById(int id)
        {
            // Check for internet, might have to disable, bc emulator
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new CityProduct();

            // Gets the client used to make http requests
            HttpClient client = await GetClient();

            // Returns the product
            return await client.GetFromJsonAsync<CityProduct>($"{Url}/city_product/{id}");
        }

        public static async Task<IEnumerable<ShipProduct>> GetAllByShipId(int id)
        {
            // Check for internet, might have to disable, bc emulator
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new List<ShipProduct>();

            // Gets the client used to make http requests
            HttpClient client = await GetClient();

            // Returns a list of products in the ship
            return await client.GetFromJsonAsync<IEnumerable<ShipProduct>>($"{Url}/ship_product/GetByShipId/{id}");
        }
    }
}
