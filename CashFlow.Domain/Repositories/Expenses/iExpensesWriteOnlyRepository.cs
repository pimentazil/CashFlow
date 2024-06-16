using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses
{
    public interface IExpensesWriteOnlyRepository
    {
        Task Add(Expense expense);
        /// <summary>
        /// This function return True if the deletion was succesful
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(int id);
    }
}
