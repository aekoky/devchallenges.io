using FluentValidation;

namespace MyTaskBoard.Application.Application.BoardTasks.Commands.UpdateBoardTask;

public class UpdateBoardTaskCommandValidator : AbstractValidator<UpdateBoardTaskCommand>
{
    public UpdateBoardTaskCommandValidator()
    {
        RuleFor(boardTask => boardTask.Name)
          .MaximumLength(255).WithMessage("Board task name is too long (255 max)");

        RuleFor(boardTask => boardTask.Description)
            .MaximumLength(1500).WithMessage("Board task description is too long (1500 max)");
    }
}
