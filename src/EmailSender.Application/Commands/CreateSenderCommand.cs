using EmailSender.Application.Common;
using EmailSender.Application.Dtos.ViewModels;
using MediatR;

namespace EmailSender.Application.Commands
{
    public class CreateSenderCommand : IRequest<Result<IdResponseModel>>
    {
        public string? Name { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public bool UseStartTls { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? FromName { get; set; }
        public string? FromEmail { get; set; }
    }
}
