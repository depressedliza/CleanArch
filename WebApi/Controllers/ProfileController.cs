using Application.Profiles.Command.UpdateProfile;
using Application.Profiles.Queries;
using Application.Profiles.Queries.GetProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Authorize]
public class ProfilesController : BaseController
{
    private readonly IMediator _mediator;

    public ProfilesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public Task<ProfileDto> GetByUserId()
    {
        var id = GetUserId();
        var request = new GetProfileQuery.Request { UserId = id };
        
        return _mediator.Send(request);
    }

    [HttpPut]
    public Task Update(UpdateProfileCommand.Request request)
    {
        var id = GetUserId();
        request.UserId = id;
        
        return _mediator.Send(request);
    }
}
