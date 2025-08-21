using EmailSender.Core.Entities.Common;
using Flunt.Validations;

namespace EmailSender.Core.Entities
{
    public class Sender : EntityBase
    {
        public Sender()
        {

        }
        public Sender(string name, string host, int port, bool useSsl, bool useStartTls, string username, string fromName, string fromEmail, string encryptedPassword)
        {
            Name = name;
            Host = host;
            Port = port;
            UseSsl = useSsl;
            UseStartTls = useStartTls;
            Username = username;
            FromName = fromName;
            FromEmail = fromEmail;
            EncryptedPassword = encryptedPassword;
        }

        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public bool UseStartTls { get; set; }
        public string Username { get; set; }
        public string EncryptedPassword { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract<Template>()
                .Requires()
                .IsNotNullOrEmpty(Username, nameof(Username), "Invalid username.")
                .IsNotNullOrEmpty(EncryptedPassword, "Password", "Invalid password.")
                .IsBetween(Port, 1, 65535, nameof(Port), "Port must be between 1 - 65535.")
                .Matches(Host, @"^(([a-zA-Z0-9](-?[a-zA-Z0-9])*)\.)+[a-zA-Z]{2,}$", nameof(Host), "Invalid host.")
                .Matches(FromEmail, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", nameof(FromEmail), "Invalid email."));
        }

        public void SetPassword(string encryptedPassword)
            => EncryptedPassword = encryptedPassword;
    }
}
