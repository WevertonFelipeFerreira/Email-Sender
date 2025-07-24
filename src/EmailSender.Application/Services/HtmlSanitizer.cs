using Ganss.Xss;

namespace EmailSender.Application.Services
{
    public class HtmlSanitizerService
    {
        private readonly HtmlSanitizer _sanitizer;
        public HtmlSanitizerService()
        {
            _sanitizer = new HtmlSanitizer();

            #region Filters
            _sanitizer.FilterUrl += (s, e) =>
            {
                if (e.OriginalUrl.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase))
                    e.SanitizedUrl = null;
            };
            #endregion

            #region Allowed Tags
            _sanitizer.AllowedTags.Add("style");
            _sanitizer.AllowedAttributes.Add("style");
            _sanitizer.AllowedAttributes.Add("class");
            _sanitizer.AllowedAttributes.Add("id");
            _sanitizer.AllowedTags.Add("svg");
            #endregion
        }

        public string Sanitize(string html) => _sanitizer.Sanitize(html);
    }
}
