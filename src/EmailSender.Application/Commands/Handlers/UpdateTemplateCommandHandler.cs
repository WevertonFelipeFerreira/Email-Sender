using AutoMapper;
using EmailSender.Application.Common;
using EmailSender.Application.Services;
using EmailSender.Core.Entities;
using EmailSender.Core.Enums;
using EmailSender.Core.Interfaces.Repositories;
using MediatR;

namespace EmailSender.Application.Commands.Handlers
{
    public class UpdateTemplateCommandHandler(IAttributeRepository attributeRepository, ITemplateRepository templateRepository, IMapper mapper) : IRequestHandler<UpdateTemplateCommand, Result<NoContent>>
    {
        public async Task<Result<NoContent>> Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
        {
            var entity = await templateRepository.FindByIdAsync(request.Id);
            if(entity == null)
                return Result<NoContent>.CreateErrors(EErrorType.NOT_FOUND);

            AttributeEntity? attribute = null;
            if (request.AttributeId is not null)
            {
                attribute = await attributeRepository.FindByIdAsync(request.AttributeId.Value);
                if (attribute is null)
                    return Result<NoContent>.CreateErrors(nameof(request.AttributeId), "Invalid Attribute Id.");
            }

            mapper.Map(request, entity);
            if (attribute is not null)
                entity.ValidateAttributes(attribute.Fields);

            entity.Validate();
            if (!entity.IsValid)
                return Result<NoContent>.CreateErrors(entity.Notifications);

            var sanitizer = new HtmlSanitizerService();
            entity.SanitizeHtml(sanitizer.Sanitize);

            await templateRepository.UpdateAsync(entity);
            return Result<NoContent>.CreateSuccess(new());
        }
    }
}
