using AutoMapper;
using EmailSender.Application.Commands;
using EmailSender.Application.Services.Interfaces;
using EmailSender.Core.Entities;

namespace EmailSender.Application.Mappers.Resolvers
{
    public class EncryptPasswordResolver : IValueResolver<CreateSenderCommand, Sender, string>
    {
        private readonly IPasswordProtector _passwordProtector;
        public EncryptPasswordResolver(IPasswordProtector passwordProtector)
        {
            _passwordProtector = passwordProtector;
        }
        public string Resolve(CreateSenderCommand source, Sender destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Password))
                return string.Empty;

            return _passwordProtector.Encrypt(source.Password);
        }
    }
}
