using CleanArchitecture.Application.Movie.Queries.Dto;
using CleanArchitecture.Application.Movie.Queries.GetMovies;
using CleanArchitecture.Application.Security.Dto;
using CleanArchitecture.Application.Security.Queries;
using CleanArchitecture.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;
public class AuthController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<UserInfoDto>> Get(string id)
    {
        return await Mediator.Send(new GetUserInfoQuery() { UserId = id });
    }
}
