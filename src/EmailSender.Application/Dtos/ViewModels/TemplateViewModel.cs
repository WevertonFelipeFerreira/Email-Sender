namespace EmailSender.Application.Dtos.ViewModels
{
    public class TemplateViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Content { get; set; }
        public string? Subject { get; set; }
        public DateTime CreationDate { get; set; }
        public AttributeViewModel? Attribute { get; set; }
    }
}