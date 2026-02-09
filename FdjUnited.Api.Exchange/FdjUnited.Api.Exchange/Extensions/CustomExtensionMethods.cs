using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FdjUnited.Api.Exchange.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services
                .AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy",
                                      builder => builder
                                                 .SetIsOriginAllowed(host => true)
                                                 .AllowAnyMethod()
                                                 .AllowAnyHeader()
                                                 .AllowCredentials());
                });

            return services;
        }

        public static IServiceCollection AddCustomHealthChecks(
            this IServiceCollection services)
        {
            var hcBuilder = services.AddHealthChecks();
            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

            return services;
        }
    }
}
