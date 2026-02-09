using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;

namespace FdjUnited.Api.Exchange
{
    [ExcludeFromCodeCoverage]
    public static class LoggingConfiguration
    {
        public static LoggerConfiguration ConfigureLogger()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning);

            if (environment?.ToLower() != "local")
            {
                loggerConfiguration
                    .WriteTo.Console(new CustomElasticsearchJsonFormatter());
            }
            else
            {
                loggerConfiguration
                    .WriteTo.Console()
                    .WriteTo.File(new CustomElasticsearchJsonFormatter(), "logs/log.txt", rollingInterval: RollingInterval.Day);
            }

            return loggerConfiguration;
        }

        private sealed class CustomElasticsearchJsonFormatter : ElasticsearchJsonFormatter
        {
            protected override void WriteTimestamp(DateTimeOffset timestamp, ref string delim, TextWriter output)
            {
                // default field name @timestamp clashes with Fluentd
                WriteJsonProperty("source-timestamp", timestamp, ref delim, output);
            }
        }
    }
}
