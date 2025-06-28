using AutoMapper;
using EmailSender.Application.Common;
using EmailSender.Application.Dtos.ViewModels;
using EmailSender.Core.Entities;
using EmailSender.Core.Interfaces.Repositories;
using MediatR;

namespace EmailSender.Application.Commands.Handlers
{
    public class CreateTemplateCommandHandler(ITemplateRepository repository, IMapper mapper)
        : IRequestHandler<CreateTemplateCommand, Result<IdModel>>
    {
        public async Task<Result<IdModel>> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<Template>(request);
            if (!entity.IsValid)
                return Result<IdModel>.CreateErrors(entity.Notifications);

            await repository.CreateAsync(entity);
            return Result<IdModel>.CreateSuccess(new IdModel(entity.Id));
        }
    }
}