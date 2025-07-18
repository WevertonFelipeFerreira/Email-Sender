using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EmailSender.Api.Extensions
{
    public static class ModelStateExtensions
    {
        public static IEnumerable<Notification> ToNotifications(this ModelStateDictionary modelState)
        {
            return modelState
                .Where(kvp => kvp.Value!.Errors.Any())
                .SelectMany(kvp =>
                    kvp.Value!.Errors.Select(error =>
                        new Notification
                        {
                            Key = kvp.Key,
                            Message = error.ErrorMessage
                        }));
        }
    }
}
