
using Kata.Models;

namespace Kata.Services
{
    public interface IAccountService
    {
        decimal GetAccountValueForDate(DateTime targetDate, Account account);
    }
}
