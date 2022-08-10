using System.Globalization;
using Caffe.Application.Common.Interfaces.Authentication;
using Caffe.Application.Common.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace Caffe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IJwtTokenGenerator _jwt;
        //private readonly ISerilogService _log;
        private readonly ILogger _logger;
        private readonly ISerilogService _serilog;
        public TestController(IJwtTokenGenerator jwt, ILogger logger, ISerilogService serilog)
        {
            _jwt = jwt ?? throw new ArgumentNullException(nameof(jwt));
            _logger = logger;
            _serilog = serilog ?? throw new ArgumentNullException(nameof(serilog));
            //_log = log ?? throw new ArgumentNullException(nameof(log));
        }


        [HttpGet]
        public async Task<ActionResult<string>> TestJwtToken()
        {
            _serilog.Error("Test Serilog Service 😁", null);
            _logger.Information("TestJwtToken😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂");
            _logger.Error("Test error with serilog");
            var dayOfWeek = new PersianCalendar().GetDayOfWeek(DateTime.Now);

            return Ok($"logged ? 😊 {dayOfWeek}");
        }
    }
}
