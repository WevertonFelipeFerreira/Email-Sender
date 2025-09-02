using EmailSender.Application.Queries;
using EmailSender.Core.Entities;
using System.Linq.Expressions;

namespace EmailSender.Application.Extensions
{
    public static class EntityExtensions
    {
        public static Expression<Func<AttributeEntity, bool>> SearchQuery(this GetAttributeQuery query)
        {
            return e => (string.IsNullOrEmpty(query.Name) || e.Name.Contains(query.Name)) &&
                        e.UserId.Equals(query.UserId);
        }
    }
}