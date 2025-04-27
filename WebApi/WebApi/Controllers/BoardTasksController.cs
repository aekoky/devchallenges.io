
using Microsoft.AspNetCore.Mvc;
using MyTaskBoard.Application.Application.BoardTasks.Commands.CreateBoardTask;
using MyTaskBoard.Application.Application.BoardTasks.Commands.DeleteBoardTask;
using MyTaskBoard.Application.Application.BoardTasks.Commands.UpdateBoardTask;
using MyTaskBoard.WebApi.Controllers;

namespace MyTaskBoardTask.WebApi.Controllers;

public class BoardTasksController : ApiControllerBase
{

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateBoardTaskCommand command, CancellationToken cancellationToken)
    {
        return await Mediator.Send(command, cancellationToken);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, UpdateBoardTaskCommand command, CancellationToken cancellationToken)
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
        await Mediator.Send(new DeleteBoardTaskCommand(id), cancellationToken);

        return NoContent();
    }
}
