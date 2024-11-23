using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AppUser, MemberDto>()
        .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
        .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
        CreateMap<Photo, PhotoDto>();
        CreateMap<MemberUpdateDto, AppUser>();
        CreateMap<RegisterDto, AppUser>();
        CreateMap<Message, MessageDto>()
        .ForMember(d => d.SenderPhotUrl, o => o.MapFrom(o => o.Sender.Photos.FirstOrDefault(s => s.IsMain).Url))
        .ForMember(d => d.RecipientPhotoUrl, o => o.MapFrom(o => o.Recipient.Photos.FirstOrDefault(s => s.IsMain).Url));

    }
}
