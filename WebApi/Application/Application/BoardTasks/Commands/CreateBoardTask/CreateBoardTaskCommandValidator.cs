using FluentValidation;
using MyTaskBoard.Domain.Entities;

namespace MyTaskBoard.Application.Application.BoardTasks.Commands.CreateBoardTask;

public class CreateBoardTaskCommandValidator : AbstractValidator<CreateBoardTaskCommand>
{
    public CreateBoardTaskCommandValidator()
    {
        RuleFor(boardTask => boardTask.Name)
            .NotEmpty().WithMessage("Board task name is required")
            .MaximumLength(255).WithMessage("Board task name is too long (255 max)");

        RuleFor(boardTask => boardTask.Description)
            .MaximumLength(1500).WithMessage("Board task description is too long (1500 max)");   
    }
}
