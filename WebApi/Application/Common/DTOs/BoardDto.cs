using MyTaskBoard.Application.Common.Mappings;
using MyTaskBoard.Domain.Entities;

namespace MyTaskBoard.Application.Common.DTOs;

public class BoardDto : IMapFrom<Board>
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public List<BoardTaskDto> Tasks { get; set; } = [];
}