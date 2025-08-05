using AutoMapper;
using EmailSender.Application.Common;
using EmailSender.Core.Enums;
using EmailSender.Core.Interfaces.Repositories;
using MediatR;

namespace EmailSender.Application.Commands.Handlers
{
    public class UpdateAttributeCommandHandler(IAttributeRepository attributeRepository)
        : IRequestHandler<UpdateAttributeCommand, Result<NoContent>>
    {
        public async Task<Result<NoContent>> Handle(UpdateAttributeCommand request, CancellationToken cancellationToken)
        {
            var attribute = await attributeRepository.FindByIdAsync(request.Id, ["Templates"]);
            if (attribute is null)
                return Result<NoContent>.CreateErrors(EErrorType.NOT_FOUND);

            attribute.Update(request.Name);
            if (!attribute.IsValid)
                return Result<NoContent>.CreateErrors(attribute.Notifications);

            await attributeRepository.UpdateAsync(attribute);
            return Result<NoContent>.CreateSuccess(new());
        }
    }
}
