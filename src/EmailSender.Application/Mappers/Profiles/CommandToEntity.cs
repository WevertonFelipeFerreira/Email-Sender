using AutoMapper;
using EmailSender.Application.Commands;
using EmailSender.Core.Entities;
using EmailSender.Core.Entities.Aggregates;

namespace EmailSender.Application.Mappers.Profiles
{
    public class CommandToEntity : Profile
    {
        public CommandToEntity()
        {
            #region Template
            CreateMap<CreateTemplateCommand, Template>()
                .ForMember(dest => dest.Html, opt => opt.MapFrom(src=> src.Content))
                .AfterMap((x,y) => y.Validate());
            #endregion

            #region Attribute
            CreateMap<CreateAttributeCommand, AttributeEntity>();
            CreateMap<FieldModel, Field>();
            #endregion
        }
    }
}
