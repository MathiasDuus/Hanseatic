using Hanseatic.Models;
using System.Net.Http.Json;

namespace Hanseatic.Managers
{
    class BuyManager
    {
        // The url used to make calls to API
        static readonly string Url = HttpClientManager.Url;

        public static async Task<IEnumerable<CityProduct>> GetAll()
        {
            // Check for internet, might have to disable, bc emulator
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new List<CityProduct>();

            // Gets the client used to make http requests
            HttpClient client = await HttpClientManager.GetClient();

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
            HttpClient client = await HttpClientManager.GetClient();

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
            HttpClient client = await HttpClientManager.GetClient();

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
            HttpClient client = await HttpClientManager.GetClient();

            // Returns the product
            return await client.GetFromJsonAsync<Product>($"{Url}/product_type/{id}");
        }
    }
}
