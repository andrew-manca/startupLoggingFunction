﻿using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;

[assembly: FunctionsStartup(typeof(StartupLog.Startup))]

namespace StartupLog
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            try
            {
                ConfigureServices(builder.Services).BuildServiceProvider(true);

                throw new Exception("TEST EXCEPTION!!!");
            }
            catch (Exception ex)
            {
                var config = new TelemetryConfiguration(Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY"));
                var client = new TelemetryClient(config);
                client.TrackException(ex);
                client.Flush();
                throw;
            }

            //builder.Services.AddSingleton<IMyLoggerProvider, MyLoggerProvider>();
        }

        private IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMyLoggerProvider, MyLoggerProvider>();

            return services;
        }
    }
}