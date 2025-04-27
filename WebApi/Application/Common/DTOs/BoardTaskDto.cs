using MyTaskBoard.Application.Common.Mappings;
using MyTaskBoard.Domain.Entities;
using MyTaskBoard.Domain.Enums;

namespace MyTaskBoard.Application.Common.DTOs;

public class BoardTaskDto : IMapFrom<BoardTask>
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public BoardTaskIcon Icon { get; set; }

    public BoardTaskStatus Status { get; set; }

    public int BoardId { get; set; }

    public BoardDto? Board { get; set; }

}