using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Infrastructure.DataAccess;

namespace CashFlow.Application.UseCases.Expenses.GetAll
{
    public class GetAllExpenseUseCase 
    {
        private readonly CashFlowDbContext _ctx;
        public GetAllExpenseUseCase(CashFlowDbContext context)
        {
            _ctx = context;
        }
        public List<ResponseShortExpenseJson> Execute()
        {
            var expenses = _ctx.Expenses.ToList();
            return expenses.Select(item => new ResponseShortExpenseJson
            {
                Id = item.Id,
                Title = item.Title,
                Date = item.Date,
                Amount = item.Amount
            }).ToList();
        }
    }
}

