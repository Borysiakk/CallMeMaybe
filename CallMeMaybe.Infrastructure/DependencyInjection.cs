using System;
using System.Text;
using System.Threading.Tasks;
using CallMeMaybe.Domain.Entities;
using CallMeMaybe.Infrastructure.Interface;
using CallMeMaybe.Infrastructure.Services;
using CallMeMaybe.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CallMeMaybe.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddSingleton<ITokenService, JwtTokenService>();
            services.AddScoped<IFriendService, FriendService>();
            services.AddScoped<ISessionService, SessionService>();
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
            
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    LifetimeValidator = (before, expires, token, parameters) =>
                    {
                        if (expires != null)
                        {
                            return expires > DateTime.UtcNow;
                        }
                        return false;
                    },
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                };

                //  x.Events = new JwtBearerEvents
                //  {
                //      OnMessageReceived = context =>
                //      {
                //          var accessToken = context.Request.Query["access_token"];
                //          var path = context.HttpContext.Request.Path;
                //          if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/hubs" + Routes.SignalR.Connection)))
                //          {
                //              context.Token = accessToken;
                //          }
                //          return Task.CompletedTask;
                //      }
                // };

            });
            
            return services;
        }
    }
}