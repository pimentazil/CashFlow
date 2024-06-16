using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CashFlow.Infrastructure.DataAccess;

namespace CashFlow.Application.UseCases.Expenses.Delete
{
    public class DeleteExpenseUseCase
    {
        private readonly CashFlowDbContext _ctx;

        public DeleteExpenseUseCase(CashFlowDbContext context)
        {
            _ctx = context;
        }

        public bool Execute(int id)
        {
            var register = _ctx.Expenses.Find(id);

            if (register is null)
            {
                throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
            }
            _ctx.Expenses.Remove(register);
            _ctx.SaveChanges();
            return true;
        }
    }
}
