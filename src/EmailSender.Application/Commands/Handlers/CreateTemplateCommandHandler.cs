using AutoMapper;
using EmailSender.Application.Dtos.ViewModels;
using EmailSender.Core.Entities;
using EmailSender.Core.Interfaces.Repositories;
using MediatR;

namespace EmailSender.Application.Commands.Handlers
{
    public class CreateTemplateCommandHandler(ITemplateRepository repository, IMapper mapper) 
        : IRequestHandler<CreateTemplateCommand, IdModel>
    {
        public async Task<IdModel> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<Template>(request);
            await repository.CreateAsync(entity);
            return new IdModel(entity.Id);
        }
    }
}