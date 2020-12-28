using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CallMeMaybe.SignalR
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSignalCore (this IServiceCollection services)
        {
            services.AddSignalR();

            return services;
        }

        public static IApplicationBuilder UseSignalR(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseEndpoints(a => a.MapHub<CommunicationHub>(""));
            return app;
        }
    }
}