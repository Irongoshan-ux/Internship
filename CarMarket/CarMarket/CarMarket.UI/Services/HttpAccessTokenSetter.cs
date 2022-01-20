using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarMarket.UI.Services
{
    public class HttpAccessTokenSetter
    {
        public HttpClient HttpClient { get; set; }
        private readonly IAccessTokenProvider _tokenProvider;

        public HttpAccessTokenSetter(IAccessTokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        public async Task AddAccessTokenAsync()
        {
            if (HttpClient.DefaultRequestHeaders.Contains("Authorization"))
                return;

            var tokenResult = await _tokenProvider.RequestAccessToken(new AccessTokenRequestOptions());

            if (tokenResult.TryGetToken(out var token))
            {
                HttpClient.DefaultRequestHeaders.Add("Authorization",
                    $"Bearer {token.Value}");
            }
        }
    }
}
