using AutoMapper;
using EmailSender.Application.Common;
using EmailSender.Application.Dtos.ViewModels;
using EmailSender.Application.Services;
using EmailSender.Core.Entities;
using EmailSender.Core.Interfaces.Repositories;
using MediatR;

namespace EmailSender.Application.Commands.Handlers
{
    public class CreateTemplateCommandHandler(ITemplateRepository templateRepository, IAttributeRepository attributeRepository, IMapper mapper)
        : IRequestHandler<CreateTemplateCommand, Result<IdResponseModel>>
    {
        public async Task<Result<IdResponseModel>> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
        {
            AttributeEntity? attribute = null;
            if (request.AttributeId is not null)
            {
                attribute = await attributeRepository.FindByIdAsync(request.AttributeId.Value);
                if (attribute is null)
                    return Result<IdResponseModel>.CreateErrors(nameof(request.AttributeId), "Invalid Attribute Id.");
            }

            var entity = mapper.Map<Template>(request);
            if (attribute is not null)
                entity.ValidateAttributes(attribute.Fields);

            entity.Validate();
            if (!entity.IsValid)
                return Result<IdResponseModel>.CreateErrors(entity.Notifications);

            var sanitizer = new HtmlSanitizerService();
            entity.SanitizeHtml(sanitizer.Sanitize);

            await templateRepository.InsertAsync(entity);
            return Result<IdResponseModel>.CreateSuccess(new IdResponseModel(entity.Id));
        }
    }
}