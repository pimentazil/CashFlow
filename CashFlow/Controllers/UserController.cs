using CashFlow.Application.UseCases.Login;
using CashFlow.Application.UseCases.Users;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly RegisterUserUseCase _useCase;

        public UserController(RegisterUserUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Register([FromBody] RequestRegisterUserJson request)
        {
            var sucesso = await _useCase.Execute(request);

            return Ok(sucesso);
        }
    }
}
