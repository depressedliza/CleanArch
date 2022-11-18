// using Application.Users.Command;
// using Application.Users.Queries;
// using Domain.Entities;
// using MediatR;
// using Microsoft.AspNetCore.Mvc;
//
// namespace WebApi.Controllers;
//
// [ApiController]
// [Route("api/[controller]")]
// public class UserController : ControllerBase
// {
//     private readonly IMediator _mediator;
//
//     public UserController(IMediator mediator)
//     {
//         _mediator = mediator;
//     }
//
//     // map dto to query?
//     [HttpPost("create")]
//     public Task<Guid> Create(CreateCommand.Request request)
//     {
//         return _mediator.Send(request);
//     }
//
//     [HttpGet("all")]
//     public Task<IEnumerable<User>> GetAll()
//     {
//         return _mediator.Send(new GetAllQuery.Request());
//     }
// }
