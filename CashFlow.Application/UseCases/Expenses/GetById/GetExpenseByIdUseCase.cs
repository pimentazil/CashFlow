using AutoMapper;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;
using CashFlow.Exception;
using CashFlow.Infrastructure.DataAccess;

namespace CashFlow.Application.UseCases.Expenses.GetById
{
    public class GetExpenseByIdUseCase 
    {
        private readonly CashFlowDbContext _ctx;
        public GetExpenseByIdUseCase(CashFlowDbContext context)
        {
            _ctx = context;
        }
        public Expense Execute(int id)
        {
            var expense = _ctx.Expenses.FirstOrDefault(x => x.Id == id);

            if (expense is null)
            {
                throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
            }

            return expense;
        }
    }
}
