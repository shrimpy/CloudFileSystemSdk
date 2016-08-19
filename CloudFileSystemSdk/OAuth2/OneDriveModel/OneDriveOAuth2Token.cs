using System;

namespace CloudFileSystemSdk.OAuth2.OneDriveModel
{
    public class OneDriveOAuth2Token : IOAuth2Data
    {
        public string token_type { get; set; }
        public long expires_in { get; set; }
        public string scope { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string authentication_token { get; set; }
        public string user_id { get; set; }

        public OAuth2Data ConvertToOAuth2Data()
        {
            return new OAuth2Data
            {
                AccessToken = this.access_token,
                RefreshToken = this.refresh_token,
                ExpiresAtUtc = DateTime.UtcNow.AddSeconds(-1 * (this.expires_in - 60 /*one mintues offset to cover latecy that cause by network and others*/))
            };
        }
    }
}
