using EmailSender.Application.Common;
using EmailSender.Core.Enums;
using EmailSender.Core.Interfaces.Repositories;
using MediatR;

namespace EmailSender.Application.Commands.Handlers
{
    public class DeleteAttributeCommandHandler(IAttributeRepository attributeRepository) : IRequestHandler<DeleteAttributeCommand, Result<NoContent>>
    {
        public async Task<Result<NoContent>> Handle(DeleteAttributeCommand request, CancellationToken cancellationToken)
        {
            var entity = await attributeRepository.SearchAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            if (entity is null)
                return Result<NoContent>.CreateErrors(EErrorType.NOT_FOUND);

            entity.SetDeletedDate();
            await attributeRepository.UpdateAsync(entity);

            return Result<NoContent>.CreateSuccess(new());
        }
    }
}