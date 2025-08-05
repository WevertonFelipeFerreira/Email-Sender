using EmailSender.Application.Common;
using EmailSender.Core.Enums;
using EmailSender.Core.Interfaces.Repositories;
using MediatR;

namespace EmailSender.Application.Commands.Handlers
{
    public class DeleteTemplateCommandHandler(ITemplateRepository templateRepository) : IRequestHandler<DeleteTemplateCommand, Result<NoContent>>
    {
        public async Task<Result<NoContent>> Handle(DeleteTemplateCommand request, CancellationToken cancellationToken)
        {
            var entity = await templateRepository.FindByIdAsync(request.Id);
            if (entity is null)
                return Result<NoContent>.CreateErrors(EErrorType.NOT_FOUND);

            entity.SetDeletedDate();
            await templateRepository.UpdateAsync(entity);

            return Result<NoContent>.CreateSuccess(new());
        }
    }
}
