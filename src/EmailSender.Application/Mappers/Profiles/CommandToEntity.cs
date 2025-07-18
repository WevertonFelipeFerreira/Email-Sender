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
                .ForMember(dest => dest.Html, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.Attribute, opt => opt.Ignore())
                .AfterMap((src, dest) => dest.Validate());
            #endregion

            #region Attribute
            CreateMap<CreateAttributeCommand, AttributeEntity>()
                .AfterMap((src, dest) => dest.Validate());

            CreateMap<UpdateAttributeCommand, AttributeEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Validate();
                    dest.SetModifiedDate();
                });

            CreateMap<FieldModel, Field>();
            #endregion
        }
    }
}