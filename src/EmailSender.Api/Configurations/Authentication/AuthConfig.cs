using EmailSender.Api.ProblemDetail;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using System.Text.Json;

namespace EmailSender.Api.Configurations.Authentication
{
    public static class AuthConfig
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration cfg) 
        {
            var authority = cfg["Authentication:Schemes:Bearer:Authority"];
            var validAudiences = cfg["Authentication:Schemes:Bearer:ValidAudiences:0"];
            var validIssuer = cfg["Authentication:Schemes:Bearer:ValidIssuer"];

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = authority;
                    options.Audience = validAudiences;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = validIssuer,
                        ValidateAudience = true,
                        ValidateLifetime = true

                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            context.HandleResponse();
                            var problem = ApiError.CreateProblem(context.HttpContext, HttpStatusCode.Unauthorized, "Unauthorized", "Invalid or expired token.");

                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/problem+json";
                            await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
                        },
                        OnForbidden = async context =>
                        {
                            var problem = ApiError.CreateProblem(context.HttpContext, HttpStatusCode.Forbidden, "Forbidden", "You do not have permission to access the requested resource.");

                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/problem+json";
                            await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
                        }
                    };
                });
            services.AddAuthorization();

            return services;
        }
    }
}
