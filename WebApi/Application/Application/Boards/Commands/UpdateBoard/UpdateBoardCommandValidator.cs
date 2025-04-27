using FluentValidation;

namespace MyTaskBoard.Application.Application.Boards.Commands.UpdateBoard;

public class UpdateBoardCommandValidator : AbstractValidator<UpdateBoardCommand>
{
    public UpdateBoardCommandValidator()
    {
        RuleFor(board => board.Name)
            .MaximumLength(255).WithMessage("Board name is too long (255 max)");

        RuleFor(board => board.Description)
            .MaximumLength(1500).WithMessage("Board description is too long (1500 max)");
    }
}
