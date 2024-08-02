using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpensesController : ControllerBase
    {
        private readonly CashFlowDbContext _ctx;

        public ExpensesController(CashFlowDbContext context)
        {
            _ctx = context;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredExpenseJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RequestExpenseJson request)
        {
            var registerSucesso = new RegisterExpenseUseCase(_ctx);
            var response = registerSucesso.Execute(request);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAllExpenses()
        {
            var registerService = new GetAllExpenseUseCase(_ctx);
            return Ok(registerService.Execute());
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var expenseService = new GetExpenseByIdUseCase(_ctx);
            return Ok(expenseService.Execute(id));
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var registerService = new DeleteExpenseUseCase(_ctx);
            registerService.Execute(id);

            return Ok("Expense deleted with success!");
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, [FromBody] RequestExpenseJson request)
        {
            var registerService = new UpdateExpenseUseCase(_ctx);
            registerService.Execute(id, request);

            return Ok("Expense update with sucess!");

        }
    }
}
