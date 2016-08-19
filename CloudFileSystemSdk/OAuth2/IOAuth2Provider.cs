using System.Threading.Tasks;

namespace CloudFileSystemSdk.OAuth2
{
    interface IOAuth2Provider
    {
        string GetAuthUrl(string clientId, string redirectUri, string responseType, string[] scopes = null, string state = null);
        Task<OAuth2Data> GetAuthData(string clientId, string clientSecret, string callbackUri, string grantType);
    }
}
