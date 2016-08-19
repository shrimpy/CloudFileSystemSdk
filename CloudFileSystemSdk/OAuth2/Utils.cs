using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CloudFileSystemSdk.OAuth2
{
    public class Utils
    {
        public const string JsonMediaType = "application/json";

        public const string FormUrlEncodedMediaType = "application/x-www-form-urlencoded";

        public static HttpClient CreateHttpClient(string accessToken = null)
        {
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 1024 * 1024 * 10;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonMediaType));
            if (!String.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            }
            return client;
        }

        public static async Task<T> ProcessResponse<T>(HttpResponseMessage response)
        {
            string content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException(response.StatusCode + " : " + content);

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
