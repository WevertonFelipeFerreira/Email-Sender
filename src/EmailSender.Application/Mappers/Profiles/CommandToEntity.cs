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
                .ForMember(dest => dest.Attribute, opt => opt.Ignore());

            CreateMap<UpdateTemplateCommand, Template>()
                .ForMember(dest => dest.Html, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.Attribute, opt => opt.Ignore())
                .AfterMap((src, dest) => dest.SetModifiedDate());
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

            #region Sender
            CreateMap<CreateSenderCommand, Sender>()
                .ForMember(dest => dest.HashPassword, opt => opt.MapFrom(src => src.Password)) // Use resolver encrypting password
                .AfterMap((src, dest) => dest.Validate());
            #endregion
        }
    }
}