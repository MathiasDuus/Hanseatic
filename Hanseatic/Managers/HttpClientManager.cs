namespace Hanseatic.Managers
{
    internal class HttpClientManager
    {
        // The address which we base all calls upon
        static readonly string BaseAddress = "http://10.130.54.29:5000";
        // Adds api to the URI
        public static readonly string Url = $"{BaseAddress}/api";

        // The Http client used for http calls
        static HttpClient client;

        /// <summary>
        /// Get the client and adds header
        /// </summary>
        /// <returns></returns>
        public static async Task<HttpClient> GetClient()
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
    }
}
