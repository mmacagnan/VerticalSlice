using MediatR;
using Microsoft.AspNetCore.Mvc;
using VerticalSlice.Extensions;

namespace VerticalSlice.Abstraction.Controller;

[ApiController]
[Route("api/[controller]")]
[ApiConventionType(typeof(DefaultApiConventions))]
[Produces("application/json")]
public abstract class AbstractBaseController
    : ControllerBase
{
    protected readonly IMediator _mediator;

    protected AbstractBaseController(IMediator mediator)
    {
        _mediator = mediator;
    }


    protected async Task<ActionResult<T>> StandardResponse<T>(IRequest<T> rq)
        => this.OkUnit(await _mediator.Send(rq), "");
}