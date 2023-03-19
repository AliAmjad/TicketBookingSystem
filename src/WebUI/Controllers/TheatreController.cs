using CleanArchitecture.Application.Theatre.Commands.CreateTheatre;
using CleanArchitecture.Application.Theatre.Commands.DeleteTheatre;
using CleanArchitecture.Application.Theatre.Commands.UpdateTheatre;
using CleanArchitecture.Application.Theatre.Queries.Dto;
using CleanArchitecture.Application.Theatre.Queries.GetTheatres;
using CleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;
using CleanArchitecture.Application.TodoItems.Commands.DeleteTodoItem;
using CleanArchitecture.Application.TodoLists.Commands.UpdateTodoList;
using CleanArchitecture.Application.TodoLists.Queries.GetTodos;
using CleanArchitecture.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;
public class TheatreController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TheatreDto>>> Get()
    {
        return await Mediator.Send(new GetTheatresQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TheatreDto>> GetById(int id)
    {
        return await Mediator.Send(new GetTheatreQuery() { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateTheatreCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, UpdateTheatreCommand command)
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
        await Mediator.Send(new DeleteTheatreCommand(id));

        return NoContent();
    }

}
