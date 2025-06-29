using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmailSender.Api.ProblemDetail
{
    public sealed class ApiError
    {
        private const string DocumentationUrl = "https://github.com/WevertonFelipeFerreira/Email-Sender";

        public static ProblemDetails CreateValidationProblem(HttpContext context, IEnumerable<Notification> notifications)
            => CreateProblem(context, HttpStatusCode.BadRequest, "Bad Request", "Error with the request fields.", notifications);

        public static ProblemDetails CreateProblem(HttpContext context, HttpStatusCode status, string title, string detail, IEnumerable<Notification> notifications)
        {
            var problemDetail = CreateBaseProblem(context, status, title, detail);
            problemDetail.Extensions["errors"] = MapNotifications(notifications);
            return problemDetail;
        }

        public static ProblemDetails CreateProblem(HttpContext context, HttpStatusCode status, string title, string detail)
            => CreateBaseProblem(context, status, title, detail);

        private static ProblemDetails CreateBaseProblem(HttpContext context, HttpStatusCode status, string title, string detail)
        {
            var problemDetail = new ProblemDetails()
            {
                Type = DocumentationUrl,
                Title = title,
                Status = (int)status,
                Detail = detail,
                Instance = context.Request.Path
            };
            problemDetail.Extensions["traceId"] = context.TraceIdentifier;
            context.Response.ContentType = "application/problem+json";
            return problemDetail;
        }

        private static IEnumerable<FieldError> MapNotifications(IEnumerable<Notification> notifications)
        {
            return notifications
                .GroupBy(n => n.Key)
                .Select(g => new FieldError(
                    field: g.Key,
                    errors: g.Select(n => n.Message).ToArray()
                ));
        }
    }
}