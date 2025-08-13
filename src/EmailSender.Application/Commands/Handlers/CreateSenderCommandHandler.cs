using AutoMapper;
using EmailSender.Application.Common;
using EmailSender.Application.Dtos.ViewModels;
using EmailSender.Core.Entities;
using EmailSender.Core.Interfaces.Repositories;
using MediatR;

namespace EmailSender.Application.Commands.Handlers
{
    public class CreateSenderCommandHandler(ISenderRepository repository, IMapper mapper) : IRequestHandler<CreateSenderCommand, Result<IdResponseModel>>
    {
        public async Task<Result<IdResponseModel>> Handle(CreateSenderCommand request, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<Sender>(request);
            if (!entity.IsValid)
                return Result<IdResponseModel>.CreateErrors(entity.Notifications);

            await repository.InsertAsync(entity);
            return Result<IdResponseModel>.CreateSuccess(new(entity.Id));
        }
    }
}
