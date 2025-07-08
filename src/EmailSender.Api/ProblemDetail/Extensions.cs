using EmailSender.Api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Api.ProblemDetail
{
    public static class Extensions
    {
        public static IServiceCollection AddCustomModelStateValidation(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetailsError = ApiError.CreateValidationProblem(context.HttpContext, context.ModelState.ToNotifications());

                    return new BadRequestObjectResult(problemDetailsError);
                };
            });

            return services;
        }
    }
}
