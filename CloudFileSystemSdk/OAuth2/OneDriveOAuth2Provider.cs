﻿using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CloudFileSystemSdk.OAuth2.OneDriveModel;

namespace CloudFileSystemSdk.OAuth2
{
    public class OneDriveOAuth2Provider : IOAuth2Provider
    {
        public string GetAuthUrl(string clientId, string redirectUri, string responseType = "code", string[] scopes = null, string state = null)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("https://login.live.com/oauth20_authorize.srf");
            strb.AppendFormat("?client_id={0}", WebUtility.UrlEncode(clientId));
            strb.AppendFormat("&response_type={0}", responseType);

            if (!string.IsNullOrEmpty(redirectUri))
                strb.AppendFormat("&redirect_uri={0}", WebUtility.UrlEncode(redirectUri));

            if (scopes != null)
                strb.AppendFormat("&scope={0}", WebUtility.UrlEncode(string.Join(" ", scopes)));

            if (state != null)
                strb.AppendFormat("&state={0}", WebUtility.UrlEncode(state));

            return strb.ToString();
        }

        public async Task<OAuth2Data> GetAuthData(string clientId, string clientSecret, string callbackUri, string grantType = "authorization_code") where T : IOAuth2Data
        {
            var queryStrings = HttpUtility.ParseQueryString(callbackUri);
            var message = queryStrings["error_description"] ?? queryStrings["error"];
            if (!string.IsNullOrEmpty(message))
                throw new Exception(message);

            var code = queryStrings["code"];
            if (String.IsNullOrEmpty(code))
                throw new ArgumentException("Missing code query string.");

            var redirectUri = new Uri(callbackUri);
            redirectUri = new Uri(redirectUri, redirectUri.AbsolutePath);

            var strb = new StringBuilder();
            strb.AppendFormat("client_id={0}", WebUtility.UrlEncode(clientId));
            strb.AppendFormat("&client_secret={0}", WebUtility.UrlEncode(clientSecret));
            strb.AppendFormat("&redirect_uri={0}", WebUtility.UrlEncode(redirectUri.AbsoluteUri));
            strb.AppendFormat("&code={0}", WebUtility.UrlEncode(code));
            strb.Append("&grant_type=authorization_code");

            var content = new StringContent(strb.ToString(), Encoding.UTF8, Utils.FormUrlEncodedMediaType);
            using (var client = Utils.CreateHttpClient())
            using (var response = await client.PostAsync("https://login.live.com/oauth20_token.srf", content))
            {
                var result = await Utils.ProcessResponse<OneDriveOAuth2Token>(response);
                return result.ConvertToOAuth2Data();
            }
        }
    }
}
