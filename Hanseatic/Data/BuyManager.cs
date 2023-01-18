using System.Net.Http.Json;

namespace Hanseatic.Data
{


    class BuyManager
    {
        // TODO: Add fields for BaseAddress, Url, and authorizationKey
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

        public static async Task<IEnumerable<CityProduct>> GetAll()
        {
            // Check for internet, might have to disable, bc emulator
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new List<CityProduct>();

            HttpClient client = await GetClient();

            //List<CityProduct> monkeyList = new List<CityProduct> { };

            //HttpResponseMessage response = null;
            //try
            //{
            //    response = client.GetAsync($"{Url}/city_product").Result;
            //}
            //catch (Exception e)
            //{

            //    System.Diagnostics.Debug.WriteLine(e.Message);
            //}

            //if (response != null && response.IsSuccessStatusCode)
            //{
            //    monkeyList = await response.Content.ReadFromJsonAsync<List<CityProduct>>();
            //}

            return await client.GetFromJsonAsync<IEnumerable<CityProduct>>($"{Url}/city_product");

            //HttpClient client = await GetClient();

            //return await client.GetFromJsonAsync<IEnumerable<CityProduct>>($"{Url}/city_product");
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
    }
}
