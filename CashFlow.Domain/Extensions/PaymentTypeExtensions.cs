using CashFlow.Domain.Enums;
using CashFlow.Domain.Reports;

namespace CashFlow.Domain.Extensions
{
    public static class PaymentTypeExtensions
    {
        public static string PaymentTypeToString(this PaymentType paymentType)
        {
            return paymentType switch
            {
                PaymentType.Pix => "Dinheiro",
                PaymentType.CartaoDeCredito => "Cartão de Crédito",
                PaymentType.CartaoDeDebito => "Cartão de Débito",
                PaymentType.Dinheiro => "Transferência Bancária",
                _ => string.Empty
            };
        }
    }
}
