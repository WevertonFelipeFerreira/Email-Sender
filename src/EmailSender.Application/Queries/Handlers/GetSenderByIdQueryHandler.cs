using AutoMapper;
using EmailSender.Application.Common;
using EmailSender.Application.Dtos.ViewModels;
using EmailSender.Core.Enums;
using EmailSender.Core.Interfaces.Repositories;
using MediatR;

namespace EmailSender.Application.Queries.Handlers
{
    public class GetSenderByIdQueryHandler(ISenderRepository repository, IMapper mapper)
        : IRequestHandler<GetSenderByIdQuery, Result<SenderViewModel>>
    {
        public async Task<Result<SenderViewModel>> Handle(GetSenderByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await repository.FindByIdAsync(request.Id);
            if (entity == null)
                return Result<SenderViewModel>.CreateErrors(EErrorType.NOT_FOUND);

            var responseVm = mapper.Map<SenderViewModel>(entity);
            return Result<SenderViewModel>.CreateSuccess(responseVm);
        }
    }
}