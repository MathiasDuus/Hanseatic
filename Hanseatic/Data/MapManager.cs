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

        public static async Task<IEnumerable<Save>> GetAll()
        {
            throw new NotImplementedException();
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
