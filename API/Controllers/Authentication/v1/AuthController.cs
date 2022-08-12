using Caffe.Application.Common.Interfaces.Base;
using Caffe.Application.Common.Models.Dtos.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caffe.API.Controllers.Authentication.v1
{
    public class AuthController : VersionController
    {
        private readonly ISerilogService _serilog;

        public AuthController(ISerilogService serilog)
        {
            _serilog = serilog ?? throw new ArgumentNullException(nameof(serilog));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost(nameof(RegisterUserReq))]
        [ApiVersion("1.0")]
        public async Task<ActionResult<RegisterUserRes>> RegisterUser([FromForm] RegisterUserReq req)
        {
            try
            {
                _serilog.Info("RegisterUser", null);
                return await Task.FromResult(new RegisterUserRes());
            }
            catch (Exception ex)
            {
                _serilog.Error("RegisterUser", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}