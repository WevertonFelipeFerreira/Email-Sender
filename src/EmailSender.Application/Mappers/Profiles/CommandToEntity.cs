using AutoMapper;
using EmailSender.Application.Commands;
using EmailSender.Core.Entities;

namespace EmailSender.Application.Mappers.Profiles
{
    public class CommandToEntity : Profile
    {
        public CommandToEntity()
        {
            CreateMap<CreateTemplateCommand, Template>()
                .ForMember(dest => dest.Html, opt => opt.MapFrom(src=> src.Content))
                .AfterMap((x,y) => y.Validate());
        }
    }
}
