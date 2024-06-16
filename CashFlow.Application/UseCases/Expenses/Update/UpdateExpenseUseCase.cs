using AutoMapper;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CashFlow.Infrastructure.DataAccess;

namespace CashFlow.Application.UseCases.Expenses.Update
{
    public class UpdateExpenseUseCase
    {
        private readonly CashFlowDbContext _ctx;

        public UpdateExpenseUseCase(CashFlowDbContext context)
        {
            _ctx = context;
        }
        public bool Execute(int id, RequestExpenseJson request)
        {
            Validate(request);

            var expense = _ctx.Expenses.Find(id);

            if (expense is null)
            {
                throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
            }

            expense.Title = request.Title;
            expense.Description = request.Description;
            expense.Date = request.Date;
            expense.Amount = request.Amount;
            expense.PaymentType = (Domain.Enums.PaymentType)request.PaymentType;

            _ctx.SaveChanges();
            return true;
        }

        private void Validate(RequestExpenseJson request)
        {
            var validator = new ExpenseValidator();

            var result = validator.Validate(request);   

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList(); 

                throw new ErrorOrValidationException(errorMessages);
            }
        }
    }
}
