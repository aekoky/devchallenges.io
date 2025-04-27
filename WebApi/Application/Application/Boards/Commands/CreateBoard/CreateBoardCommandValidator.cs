using FluentValidation;

namespace MyTaskBoard.Application.Application.Boards.Commands.CreateBoard;

public class CreateBoardCommandValidator : AbstractValidator<CreateBoardCommand>
{
    public CreateBoardCommandValidator()
    {
        RuleFor(board => board.Name)
            .NotEmpty().WithMessage("Board name is required")
            .MaximumLength(255).WithMessage("Board name is too long (255 max)");

        RuleFor(board => board.Description)
            .MaximumLength(1500).WithMessage("Board description is too long (1500 max)");
    }
}
