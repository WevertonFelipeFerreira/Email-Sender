using EmailSender.Application.Common;
using EmailSender.Application.Dtos.ViewModels;
using MediatR;

namespace EmailSender.Application.Queries
{
    public class GetAttributeQuery : IRequest<Result<Paged<AttributeViewModel>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Name { get; set; }

        public GetAttributeQuery(int page = 1, int pageSize = 10, string? name = null)
        {
            Page = page;
            PageSize = pageSize;
            Name = name;
        }
    }
}