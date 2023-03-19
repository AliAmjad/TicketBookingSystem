using CleanArchitecture.Application.Movie.Commands.CreateMovie;
using CleanArchitecture.Application.Movie.Commands.DeleteMovie;
using CleanArchitecture.Application.Movie.Commands.UpdateMovie;
using CleanArchitecture.Application.Movie.Queries.Dto;
using CleanArchitecture.Application.Movie.Queries.GetMovies;
using CleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;
using CleanArchitecture.Application.TodoItems.Commands.DeleteTodoItem;
using CleanArchitecture.Application.TodoLists.Commands.UpdateTodoList;
using CleanArchitecture.Application.TodoLists.Queries.GetTodos;
using CleanArchitecture.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

public class MovieController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<MovieDto>>> Get()
    {
        return await Mediator.Send(new GetMoviesQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetById(int id)
    {
        return await Mediator.Send(new GetMovieQuery() { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateMovieCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, UpdateMovieCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteMovieCommand(id));

        return NoContent();
    }

}
