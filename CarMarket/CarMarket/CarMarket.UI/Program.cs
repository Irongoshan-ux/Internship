using CarMarket.UI.Services;
using CarMarket.UI.Services.Car;
using CarMarket.UI.Services.User;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CarMarket.UI
{
    public class Program
    {
        private static readonly string API_BASE_ADDRESS = "https://localhost:10001";

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped<HttpAccessTokenSetter>();

            builder.Services.AddHttpClient("API", client =>
            {
                client.BaseAddress = new Uri(API_BASE_ADDRESS);
            });

            builder.Services.AddHttpClient<IHttpCarService, HttpCarService>(client =>
            {
                client.BaseAddress = new Uri(API_BASE_ADDRESS);
            });

            builder.Services.AddHttpClient<IHttpUserService, HttpUserService>(client =>
            {
                client.BaseAddress = new Uri(API_BASE_ADDRESS);
            });

            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                builder.Configuration.Bind("Local", options.ProviderOptions);
            });

            builder.Services.AddOidcAuthentication(options =>
            {
                options.ProviderOptions.Authority = API_BASE_ADDRESS;
                options.ProviderOptions.ClientId = "client_blazor";
                options.ProviderOptions.DefaultScopes.Add("profile");
                options.ProviderOptions.DefaultScopes.Add("email");
                options.ProviderOptions.DefaultScopes.Add("openid");
                options.ProviderOptions.ResponseType = "code";

                options.UserOptions.NameClaim = "sub";
            });

            await builder.Build().RunAsync();
        }
    }
}