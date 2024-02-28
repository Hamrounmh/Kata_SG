
using Kata.Models;

namespace Kata.Services;

public class AccountService : IAccountService
{
    public decimal GetAccountValueForDate(DateTime targetDate, Account account)
    {
        decimal accountValue = account.Amount;

        foreach (var transaction in account.Transactions)
        {
            if (transaction.Date >= targetDate)
            {
                decimal transactionValue = transaction.Amount;
                if (transaction.Currency == "JPY")
                {
                    transactionValue *= account.EurYenRate;
                }
                else if (transaction.Currency == "USD")
                {
                    transactionValue *= account.EurUsdRate;
                }
                accountValue += transactionValue;
            }
        }
        return accountValue;
    }
}