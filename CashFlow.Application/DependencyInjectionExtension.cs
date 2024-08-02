using AutoMapper;
using CashFlow.Application.AutoMapper;
using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using CashFlow.Application.UseCases.Expenses.Reports.Pdf;
using CashFlow.Application.UseCases.Login;
using CashFlow.Application.UseCases.Users;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapping));

            AddUseCases(services);
        }

        private static void AddUseCases(this IServiceCollection services)
        {
            // Registre outros casos de uso conforme necessário
            // services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
            // services.AddScoped<IGetAllExpenseUseCase, GetAllExpenseUseCase>();
            // services.AddScoped<IGetExpenseByIdUseCase, GetExpenseByIdUseCase>();
            // services.AddScoped<IDeleteExpenseUseCase, DeleteExpenseUseCase>();
            services.AddScoped<IGenerateExpensesReportExcelUseCase, GenerateExpensesReportExcelUseCase>();
            services.AddScoped<IGenerateExpensesReportPdfUseCase, GenerateExpensesReportPdfUseCase>();
            services.AddScoped<RegisterUserUseCase>();
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        }
    }
}
