using System;

namespace CloudFileSystemSdk.OAuth2
{
    public class OAuth2Data
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAtUtc { get; set; }
    }
}
