using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using CashFlow.Application.UseCases.Expenses.Reports.Pdf;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpGet("excel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetExcel(
            [FromServices] IGenerateExpensesReportExcelUseCase useCase,
            [FromHeader] string month)
        {
            if (!DateOnly.TryParseExact(month, "MM/yyyy", out DateOnly dateOnly))
            {
                return BadRequest("Invalid date format. Expected format is MM/yyyy.");
            }

            byte[] file = await useCase.Execute(dateOnly);

            if (file.Length > 0)
                return File(file, MediaTypeNames.Application.Octet, "report.xlsx");

            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> GetPdf(
            [FromServices] IGenerateExpensesReportPdfUseCase useCase,
            [FromQuery] DateOnly month)
        {
            byte[] file = await useCase.Execute(month);

            if (file.Length > 0)
                return File(file, MediaTypeNames.Application.Pdf, "report.pdf");

            return NoContent();
        }
    }
}
