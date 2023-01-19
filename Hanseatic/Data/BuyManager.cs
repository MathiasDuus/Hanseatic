﻿using System.Net.Http.Json;

namespace Hanseatic.Data
{
    class BuyManager
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

        public static async Task<IEnumerable<CityProduct>> GetAll()
        {
            // Check for internet, might have to disable, bc emulator
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new List<CityProduct>();

            HttpClient client = await GetClient();

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

        public static async Task<int> GetCityIdByName(string name)
        {
            // Check for internet, might have to disable, bc emulator
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return -1;

            HttpClient client = await GetClient();

            City respone = await client.GetFromJsonAsync<City>($"{Url}/city/GetByName/{name}");

            return respone.Id;
        }

        public static async Task<IEnumerable<CityProduct>> GetAllByCityId(int id)
        {
            // Check for internet, might have to disable, bc emulator
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new List<CityProduct>();

            HttpClient client = await GetClient();


            return await client.GetFromJsonAsync<IEnumerable<CityProduct>>($"{Url}/city_product/GetByCityId/{id}");
        }

        public static async Task<Product> GetProductById(int id)
        {
            // Check for internet, might have to disable, bc emulator
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new Product();

            HttpClient client = await GetClient();


            return await client.GetFromJsonAsync<Product>($"{Url}/product_type/{id}");
        }
    }
}
