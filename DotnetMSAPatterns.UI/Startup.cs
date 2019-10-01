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
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10)
            }));


            services.AddHttpClient("CircuitBreaker", client =>
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/");
            })
            .ConfigurePrimaryHttpMessageHandler<WebAssemblyHttpMessageHandler>()
            .AddTransientHttpErrorPolicy(
                builder => builder.CircuitBreakerAsync(2, TimeSpan.FromSeconds(5)));
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
