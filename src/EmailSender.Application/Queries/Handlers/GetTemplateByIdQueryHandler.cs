using AutoMapper;
using EmailSender.Application.Common;
using EmailSender.Application.Dtos.ViewModels;
using EmailSender.Core.Enums;
using EmailSender.Core.Interfaces.Repositories;
using MediatR;

namespace EmailSender.Application.Queries.Handlers
{
    public class GetTemplateByIdQueryHandler(ITemplateRepository repository, IMapper mapper) : IRequestHandler<GetTemplateByIdQuery, Result<TemplateViewModel>>
    {
        public async Task<Result<TemplateViewModel>> Handle(GetTemplateByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await repository.FindByIdAsync(request.Id, ["Attribute"]);
            if (entity == null)
                return Result<TemplateViewModel>.CreateErrors(EErrorType.NOT_FOUND);

            var responseVm = mapper.Map<TemplateViewModel>(entity);
            return Result<TemplateViewModel>.CreateSuccess(responseVm);
        }
    }
}
