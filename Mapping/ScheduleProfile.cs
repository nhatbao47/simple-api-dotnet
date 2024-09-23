using AutoMapper;
using SimpleApi.DTOs;
using SimpleApi.Models;
namespace SimpleApi.Mapping;
public class ScheduleProfile: Profile
{
    public ScheduleProfile()
    {
        CreateMap<Schedule, ScheduleDto>()
            .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.User.FullName));
    }
}