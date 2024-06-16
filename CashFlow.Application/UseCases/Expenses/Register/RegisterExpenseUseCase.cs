using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;
using CashFlow.Infrastructure.DataAccess;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase 
    {

        private readonly CashFlowDbContext _ctx;

        public RegisterExpenseUseCase(CashFlowDbContext context)
        {
            _ctx = context;
        }
        public ResponseRegisteredExpenseJson Execute(RequestExpenseJson request)
        {
            Validate(request);

            var expense = new Expense
            {
                Title = request.Title,
                Description = request.Description,
                Date = request.Date,
                Amount = request.Amount,
                PaymentType = (Domain.Enums.PaymentType)request.PaymentType,
            };

            _ctx.Expenses.Add(expense);
            _ctx.SaveChanges();

            var response = new ResponseRegisteredExpenseJson
            {
                Title = expense.Title,
            };

            return response;
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
