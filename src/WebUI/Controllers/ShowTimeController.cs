using CleanArchitecture.Application.Movie.Commands.UpdateMovie;
using CleanArchitecture.Application.Movie.Queries.Dto;
using CleanArchitecture.Application.Movie.Queries.GetMovies;
using CleanArchitecture.Application.ShowTime.Commands.CreateShowTime;
using CleanArchitecture.Application.ShowTime.Commands.UpdateShowTime;
using CleanArchitecture.Application.ShowTime.Queries.Dto;
using CleanArchitecture.Application.ShowTime.Queries.GetShowTimes;
using CleanArchitecture.Application.Theatre.Commands.CreateTheatre;
using CleanArchitecture.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;
public class ShowTimeController : ApiControllerBase
{

    [HttpGet]
    public async Task<ActionResult<List<ShowTimeDto>>> Get()
    {
        return await Mediator.Send(new GetShowTimesQuery());
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateShowTimeCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, UpdateShowTimeCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }
}
