using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Update
{
    public interface IUpdateExpenseUseCase
    {
        Task<ResponseExpenseJson> Execute(int id);
    }
}
