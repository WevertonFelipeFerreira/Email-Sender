using AutoMapper;
using EmailSender.Application.Common;
using EmailSender.Application.Dtos.ViewModels;
using EmailSender.Core.Entities;
using EmailSender.Core.Interfaces.Repositories;
using MediatR;

namespace EmailSender.Application.Commands.Handlers
{
    internal class CreateAttributeCommandHandler(IAttributeRepository repository, IMapper mapper) 
        : IRequestHandler<CreateAttributeCommand, Result<IdResponseModel>>
    {
        public async Task<Result<IdResponseModel>> Handle(CreateAttributeCommand request, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<AttributeEntity>(request);
            if(!entity.IsValid)
                return Result<IdResponseModel>.CreateErrors(entity.Notifications);

            await repository.InsertAsync(entity);
            return Result<IdResponseModel>.CreateSuccess(new IdResponseModel(entity.Id));
        }
    }
}
