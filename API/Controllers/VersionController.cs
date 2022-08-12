using Caffe.Application.Common.Interfaces.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Caffe.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    public class VersionController : BaseApiController
    {

    }
}
