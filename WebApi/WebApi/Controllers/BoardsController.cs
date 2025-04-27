using MyTaskBoard.Application.Application.Boards.Commands.DeleteBoard;
using MyTaskBoard.Application.Application.Boards.Commands.CreateBoard;
using MyTaskBoard.Application.Application.Boards.Commands.UpdateBoard;
using MyTaskBoard.Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using MyTaskBoard.Application.Application.Boards.Queries.GetBoardById;
using MyTaskBoard.Application.Application.Boards.Queries.GetBoard;

namespace MyTaskBoard.WebApi.Controllers;

public class BoardsController : ApiControllerBase
{

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateBoardCommand command, CancellationToken cancellationToken)
    {
        return await Mediator.Send(command, cancellationToken);
    }

    [HttpGet]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<BoardDto>> GetBoard(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetBoardQuery(), cancellationToken);
    }

    [HttpGet("{id}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<BoardDto>> GetBoardById(int id, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetBoardByIdQuery(id), cancellationToken);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, UpdateBoardCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command, cancellationToken);

        return NoContent();
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteBoardCommand(id), cancellationToken);

        return NoContent();
    }
}
