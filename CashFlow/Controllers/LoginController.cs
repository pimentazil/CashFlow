using CashFlow.Application.UseCases.Login;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IDoLoginUseCase _useCase;

        public LoginController(IDoLoginUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] RequestLoginJson request)
        {
            var response = await _useCase.Execute(request);

            return Ok(response);
        }
    }
}
