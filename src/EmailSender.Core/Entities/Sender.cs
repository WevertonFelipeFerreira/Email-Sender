using EmailSender.Core.Entities.Common;

namespace EmailSender.Core.Entities
{
    public class Sender : EntityBase
    {
        public Sender()
        {

        }
        public Sender(string name, string host, int port, bool useSsl, bool useStartTls, string username, string hashPassword, string fromName, string fromEmail)
        {
            Name = name;
            Host = host;
            Port = port;
            UseSsl = useSsl;
            UseStartTls = useStartTls;
            Username = username;
            HashPassword = hashPassword;
            FromName = fromName;
            FromEmail = fromEmail;
        }

        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public bool UseStartTls { get; set; }
        public string Username { get; set; }
        public string HashPassword { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }

        public override void Validate()
        {

        }
    }
}
