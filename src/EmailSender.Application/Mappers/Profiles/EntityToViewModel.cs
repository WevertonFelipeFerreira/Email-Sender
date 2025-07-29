using AutoMapper;
using EmailSender.Application.Commands;
using EmailSender.Application.Dtos.ViewModels;
using EmailSender.Core.Entities;
using EmailSender.Core.Entities.Aggregates;

namespace EmailSender.Application.Mappers.Profiles
{
    public class EntityToViewModel : Profile
    {
        public EntityToViewModel()
        {
            #region Attribute
            CreateMap<AttributeEntity, AttributeViewModel>()
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreatedAt.DateTime));
            CreateMap<Field, FieldModel>();
            #endregion

            #region Template
            CreateMap<Template, TemplateViewModel>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Html))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreatedAt.DateTime));
            #endregion
        }
    }
}
