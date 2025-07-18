using EmailSender.Application.Commands;

namespace EmailSender.Application.Dtos.ViewModels
{
    public class AttributeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public IEnumerable<FieldModel> Fields { get; set; }
    }
}