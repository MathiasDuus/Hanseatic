using System.Net.Http.Json;

namespace Hanseatic.Data
{
    internal class MapManager
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

        public static async Task<Save> GetById(int id)
        {

            // Check for internet, might have to disable, bc emulator
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new Save();

            HttpClient client = await GetClient();

            return await client.GetFromJsonAsync<Save>($"{Url}/save/{id}");
        }

        public static async Task UpdateDate(Save save)
        {
            throw new NotImplementedException();
        }

        public static async Task Delete(string saveID)
        {
            throw new NotImplementedException();
        }

    }
}
