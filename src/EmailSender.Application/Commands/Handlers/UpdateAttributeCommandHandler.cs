using AutoMapper;
using EmailSender.Application.Common;
using EmailSender.Core.Enums;
using EmailSender.Core.Interfaces.Repositories;
using MediatR;

namespace EmailSender.Application.Commands.Handlers
{
    public class UpdateAttributeCommandHandler(IAttributeRepository attributeRepository, IMapper mapper)
        : IRequestHandler<UpdateAttributeCommand, Result<NoContent>>
    {
        public async Task<Result<NoContent>> Handle(UpdateAttributeCommand request, CancellationToken cancellationToken)
        {
            var attribute = await attributeRepository.FindByIdAsync(request.Id, ["Templates"]);
            if (attribute is null)
                return Result<NoContent>.CreateErrors(EErrorType.NOT_FOUND);

            if (attribute.Templates.Any())
                return Result<NoContent>.CreateErrors(EErrorType.CONFLICT);

            mapper.Map(request, attribute);

            if (!attribute.IsValid)
                return Result<NoContent>.CreateErrors(attribute.Notifications);

            await attributeRepository.UpdateAsync(attribute);
            return Result<NoContent>.CreateSuccess(new());
        }
    }
}
