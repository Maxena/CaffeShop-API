using Caffe.Application.Common.Interfaces.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Caffe.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    private readonly ICurrentUser _currentUser;
    private ISender _mediator = null!;

    public BaseApiController(ICurrentUser currentUser)
        => _currentUser = currentUser;

    public BaseApiController()
    {

    }
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    //get user id from Icurrent user that been injected
    protected string UserId => _currentUser.UserId!;
    protected string UserName => _currentUser.UserName;
    protected string[] Roles => _currentUser.Roles;

}