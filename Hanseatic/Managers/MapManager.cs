using Hanseatic.Models;
using System.Net.Http.Json;

namespace Hanseatic.Managers
{
    internal class MapManager
    {
        // The URL used to contact the API
        static readonly string Url = HttpClientManager.Url;

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
            HttpClient client = await HttpClientManager.GetClient();

            // Awaits a return from the get request
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
