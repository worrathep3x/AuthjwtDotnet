using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserAuthenApi.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private ISender _mediator;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8601 // Possible null reference assignment.
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
#pragma warning restore CS8601 // Possible null reference assignment.
    }
}
