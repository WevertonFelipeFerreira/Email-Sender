using AutoMapper;
using EmailSender.Application.Common;
using EmailSender.Application.Dtos.ViewModels;
using EmailSender.Core.Enums;
using EmailSender.Core.Interfaces.Repositories;
using MediatR;

namespace EmailSender.Application.Queries.Handlers
{
    public class GetAttributeByIdQueryHandler(IAttributeRepository repository, IMapper mapper)
        : IRequestHandler<GetAttributeByIdQuery, Result<AttributeViewModel>>
    {
        public async Task<Result<AttributeViewModel>> Handle(GetAttributeByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await repository.FindByIdAsync(request.Id);
            if (entity == null)
                return Result<AttributeViewModel>.CreateErrors(EErrorType.NOT_FOUND);

            var responseVm = mapper.Map<AttributeViewModel>(entity);
            return Result<AttributeViewModel>.CreateSuccess(responseVm);
        }
    }
}