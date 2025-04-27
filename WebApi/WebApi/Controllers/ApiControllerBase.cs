using MyTaskBoard.WebApi.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MyTaskBoard.WebApi.Controllers;

[ApiController]
[ApiExceptionFilter]
[Route("[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
