using System.Security.Principal;
using Caffe.Application.Common.Interfaces.Authentication;
using Caffe.Application.Common.Interfaces.Base;
using Caffe.Application.Common.Interfaces.Presistence;
using Caffe.Application.Common.Models.Dtos.Auth;
using Caffe.Domain.Entities.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caffe.API.Controllers.Authentication.v1
{
    public class AuthController : BaseApiController
    {
        private readonly ISerilogService _serilog;
        private readonly IAuthService _authService;
        public AuthController(ISerilogService serilog, IAuthService authService, IIdentityService identityService, IDateTime date, IJwtTokenGenerator jwt)
        {
            _serilog = serilog ?? throw new ArgumentNullException(nameof(serilog));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost(nameof(RegisterUserReq))]
        public async Task<ActionResult<RegisterUserRes>> RegisterUser([FromForm] RegisterUserReq req)
        {
            try
            {
                _serilog.Info("Registering User ...", null);
                //create application user type from req

                var result = await _authService.RegisterUser(req);

                _serilog.Info("user registered successfully", null);

                return Ok(result);

            }
            catch (Exception ex)
            {
                _serilog.Error("RegisterUser", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}