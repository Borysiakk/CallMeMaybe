using System;
using CallMeMaybe.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace CallMeMaybe.SignalR
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSignalRHubs (this IServiceCollection services)
        {
            services.AddSignalR().AddMessagePackProtocol();
            return services;
        }
        
        public static IApplicationBuilder UseSignalR(this IApplicationBuilder app)
        {
            app.UseEndpoints(a => a.MapHub<CommunicationServerHubs>("/CommunicationServerHubs"));
            return app;
        }
    }
}