using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using Polly;
using Microsoft.AspNetCore.Blazor.Http;

namespace DotnetMSAPatterns.UI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<WebAssemblyHttpMessageHandler>();

            services.AddHttpClient("Retry", client =>
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/");                
            })
            .ConfigurePrimaryHttpMessageHandler<WebAssemblyHttpMessageHandler>()
            .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(3),
                TimeSpan.FromSeconds(5)
            }));


            services.AddHttpClient("CircuitBreaker", client =>
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/");
            })
            .ConfigurePrimaryHttpMessageHandler<WebAssemblyHttpMessageHandler>()
            .AddTransientHttpErrorPolicy(
                builder => builder.CircuitBreakerAsync(2, TimeSpan.FromSeconds(5)));





            services.AddHttpClient("RetryIstio", client =>
            {
                client.BaseAddress = new Uri("http://52.186.13.229/api/");
            })
            .ConfigurePrimaryHttpMessageHandler<WebAssemblyHttpMessageHandler>();



            services.AddHttpClient("CircuitBreakerIstio", client =>
            {
                client.BaseAddress = new Uri("http://52.186.13.229/api/");
            })
            .ConfigurePrimaryHttpMessageHandler<WebAssemblyHttpMessageHandler>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
