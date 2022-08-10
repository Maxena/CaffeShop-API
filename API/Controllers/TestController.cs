using Caffe.Application.Common.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caffe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IJwtTokenGenerator _jwt;

        public TestController(IJwtTokenGenerator jwt)
        {
            _jwt = jwt;
        }


        [HttpGet]
        public async Task<ActionResult<string>> TestJwtToken()
        {
            return Ok("");
        } 
    }
}
