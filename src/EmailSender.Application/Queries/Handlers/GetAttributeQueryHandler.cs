using AutoMapper;
using EmailSender.Application.Common;
using EmailSender.Application.Dtos.ViewModels;
using EmailSender.Application.Extensions;
using EmailSender.Core.Interfaces.Repositories;
using MediatR;

namespace EmailSender.Application.Queries.Handlers
{
    public class GetAttributeQueryHandler(IAttributeRepository repository, IMapper mapper) : IRequestHandler<GetAttributeQuery, Result<Paged<AttributeViewModel>>>
    {
        public async Task<Result<Paged<AttributeViewModel>>> Handle(GetAttributeQuery request, CancellationToken cancellationToken)
        {
            var page = request.Page;
            var pageSize = request.PageSize;

            var result = await repository.PaginatedSearchAsync(page, pageSize, request.SearchQuery());
            var entites = result.Items;
            var totalCount = result.TotalItems;

            if (!entites.Any())
                return Result<Paged<AttributeViewModel>>.CreateSuccess(new Paged<AttributeViewModel>([], totalCount, page, pageSize));

            var mapped = mapper.Map<IEnumerable<AttributeViewModel>>(entites);
            var pagedResult = new Paged<AttributeViewModel>(mapped, totalCount, page, pageSize);

            return Result<Paged<AttributeViewModel>>.CreateSuccess(pagedResult);
        }
    }
}
