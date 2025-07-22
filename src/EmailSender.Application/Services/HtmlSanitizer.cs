using Ganss.Xss;

namespace EmailSender.Application.Services
{
    public class HtmlSanitizerService
    {
        private readonly HtmlSanitizer _sanitizer;
        public HtmlSanitizerService()
        {
            _sanitizer = new HtmlSanitizer();
        }

        public string Sanitize(string html) => _sanitizer.Sanitize(html);
    }
}
