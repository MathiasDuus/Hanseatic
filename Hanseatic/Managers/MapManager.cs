using Hanseatic.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Hanseatic.Managers
{
    internal class MapManager
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
        /// Gets the Save by id of the save
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<Save> GetSaveById(int id)
        {

            // Check for internet, might have to disable, bc emulator
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new Save();

            // Gets the http client used for request
            HttpClient client = await GetClient();

            // Awaits a return from the get request
            return await client.GetFromJsonAsync<Save>($"{Url}/save/{id}");
        }

        public static async Task<Save> UpdateDate(Save save)
        {

            // Check for internet, might have to disable, bc emulator
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new Save();

            // Gets the http client used for request
            HttpClient client = await GetClient();

            // Awaits a return from the get request
            HttpResponseMessage response = await client.PutAsJsonAsync($"{Url}/save/", save);

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Save>(json);
        }
    }
}
