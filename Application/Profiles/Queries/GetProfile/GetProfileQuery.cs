using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Profiles.Queries.GetProfile;

public class GetProfileQuery
{
    public class Request : IRequest<ProfileDto>
    {
        public Guid UserId { get; set; }
    }

    public class GetByUserHandle : IRequestHandler<Request, ProfileDto>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;
        
        public GetByUserHandle(IProfileRepository profileRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        public async Task<ProfileDto> Handle(Request request, CancellationToken cancellationToken)
        {
            var profile = await _profileRepository.GetByUserId(request.UserId);
            return profile is null ? new ProfileDto() : _mapper.Map<ProfileDto>(profile);
        }
    }
}
