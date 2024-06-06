using AutoMapper;
using WebApi.Data.Entities;
using WebApi.Models;
using WebApi.Dtos.In;
using WebApi.Dtos.Out;

namespace WebApi.Dtos
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<AlbumAddDTO, Album>()
                .ForMember(x => x.Created, opt => opt.MapFrom((_, _, _, context) => (DateTime)context.Items["creationTime"]))
                .ForMember(x => x.Updated, opt => opt.MapFrom((_, _, _, context) => (DateTime)context.Items["creationTime"]));
            CreateMap<AlbumUpdateDTO, Album>()
                .ForMember(x => x.Updated, opt => opt.MapFrom((_, _, _, context) => (DateTime)context.Items["creationTime"]))
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.UserId, opt => opt.Ignore());
            CreateMap<PhotoAddDTO, Photo>()
                .ForMember(x => x.Created, opt => opt.MapFrom((_, _, _, context) => (DateTime)context.Items["creationTime"]));
            CreateMap<PhotoUpdateDTO, Photo>()
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<LoginReqDto, User>();
            CreateMap<Album, AlbumDTO>();
            CreateMap<Photo, PhotoDTO>()
                .ForMember(x => x.Likes, opt => opt.MapFrom(x => x.Likes.Count()))
                .ForMember(x => x.IsLiked, opt => opt.MapFrom((photo, photoDTO, _, context) =>
                    photo.Likes.Any(like => like.UserId == (int)context.Items["userId"])));

        }
    }
}
