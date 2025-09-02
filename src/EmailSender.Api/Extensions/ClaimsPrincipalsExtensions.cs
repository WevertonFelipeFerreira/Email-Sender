using System.Security.Claims;

namespace EmailSender.Api.Extensions
{
    public static class ClaimsPrincipalsExtensions
    {
        public static Guid? GetUserId(this ClaimsPrincipal claims) 
        {
            var parseSuccess = Guid.TryParse(claims.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId);
            return parseSuccess ? userId : null;
        }
    }
}
