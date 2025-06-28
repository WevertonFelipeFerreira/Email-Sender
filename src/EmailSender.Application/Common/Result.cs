using EmailSender.Core.Enums;
using Flunt.Notifications;

namespace EmailSender.Application.Common
{
    public sealed class Result<T> : Notifiable<Notification> where T : class
    {
        public T? Value { get; private set; }
        public EErrorType ErrorType { get; private set; }

        private Result(T value) => Value = value;
        private Result(EErrorType errorType) => ErrorType = errorType;

        public static Result<T> CreateSuccess(T value) => new(value);
        public static Result<T> CreateErrors(EErrorType errorType) => new(errorType);
        public static Result<T> CreateErrors(string key, string message)
        {
            var notification = new Notification(key, message);
            var result = Result<T>.CreateErrors(EErrorType.NOTIFICATION_ERROR);
            result.AddNotification(notification);
            return result;
        }
        public static Result<T> CreateErrors(IEnumerable<Notification> notifications)
        {
            var result = Result<T>.CreateErrors(EErrorType.NOTIFICATION_ERROR);
            result.AddNotifications((IList<Notification>)notifications.ToList());
            return result;
        }
    }
}
