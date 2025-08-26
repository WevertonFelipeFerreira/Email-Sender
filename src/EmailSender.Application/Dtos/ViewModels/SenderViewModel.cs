namespace EmailSender.Application.Dtos.ViewModels
{
    public class SenderViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public bool UseStartTls { get; set; }
        public string? Username { get; set; }
        public string? FromName { get; set; }
        public string? FromEmail { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
