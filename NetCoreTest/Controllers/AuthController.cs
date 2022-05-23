using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreTest.Entities;
using NetCoreTest.ReqModels;
using NetCoreTest.Services;

namespace NetCoreTest.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailSenderService _mailSenderService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService,
                              IEmailSenderService mailSenderService,
                              IMapper mapper)
        {
            _authService = authService;
            _mailSenderService = mailSenderService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterReqModel reqModel)
        {
            var user = _mapper.Map<User>(reqModel);
            user = await _authService.RegisterAsync(user, reqModel.Password);
            await _mailSenderService.SendVeiryAccountEmailAsync(user.Email, user.VerifyEmailToken);
            return Ok();
        }

        [HttpPost("verify-account/{token}")]
        public async Task<IActionResult> VerifyAccount(string token)
        {
            await _authService.VerifyAccountAsync(token);
            return Ok();
        }
    }
}