using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Profiles.Command.UpdateProfile;

public class UpdateProfileCommand
{
    public class Request : IRequest
    {
        public string NickName { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }

    public class UpdateHandler : IRequestHandler<Request, Unit>
    {
        private readonly IProfileRepository _profileRepository;

        public UpdateHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            var profile = await _profileRepository.GetByUserId(request.UserId);
            
            if (profile is null)
            {
                var newProfile = new Profile { NickName = request.NickName };
                await _profileRepository.InsertAsync(newProfile);
            }
            else
            {
                profile.NickName = request.NickName;
            }

            await _profileRepository.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}
