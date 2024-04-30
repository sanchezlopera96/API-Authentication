using Authentication.Domain.Model;
using Authentication.Infrastructure.Data.SqlDB.Entities;
using AutoMapper;

namespace Authentication.Infrastructure.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserEntity>()
                .ForMember(dest => dest.IdUsuario, opt => opt.MapFrom(s => s.UserId))
                .ForMember(dest => dest.IdentificacionUsuario, opt => opt.MapFrom(s => s.UserIdentification))
                .ForMember(dest => dest.ContrasenaUsuario, opt => opt.MapFrom(s => s.UserPassword))
                .ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(s => s.UserName))
                .ForMember(dest => dest.CorreoUsuario, opt => opt.MapFrom(s => s.UserEmail))
                .ForMember(dest => dest.TipoUsuario, opt => opt.MapFrom(s => s.UserType))
                .ForMember(dest => dest.EstadoUsuario, opt => opt.MapFrom(s => s.UserState))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(s => s.CreationDate))
                .ForMember(dest => dest.FechaModificacion, opt => opt.MapFrom(s => s.ModificationDate))
                .ReverseMap();
        }
    }
}