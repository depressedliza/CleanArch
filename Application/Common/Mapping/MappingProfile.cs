
using Application.Profiles.Queries;
using Domain.Entities;

namespace Application.Common.Mapping;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<Profile, ProfileDto>();
    }
}
