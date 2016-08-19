using System.Threading.Tasks;

namespace CloudFileSystemSdk.OAuth2
{
    interface IOAuth2Provider
    {
        string GetAuthUrl(string responseType, string[] scopes = null, string state = null);
        Task<OAuth2Data> GetAuthData(string callbackUri, string grantType);
    }
}
