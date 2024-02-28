using System;
using System.Collections.Generic;
using Kata.Models;
using Kata.Services;
using NUnit.Framework;

namespace Kata.Tests.Services;

[TestFixture]
[TestOf(typeof(IAccountService))]
public class AccountServiceTest
{

   [Test]
    public void GetAccountValueForDate_ReturnsCorrectValue()
    {
        // Arrange
        var targetDate = new DateTime(2021, 12, 31);
        var account = new Account
        {
            Amount = 1000,
            AccountAsOfDate = new DateTime(2022, 1, 1),
            EurYenRate = 100,
            EurUsdRate = 1.2m,
            Transactions = new List<Transaction>
            {
                new Transaction
                {
                    Date = new DateTime(2021, 12, 30), Amount = 500, Currency = "USD", Category = "Expense"
                },
                new Transaction
                {
                    Date = new DateTime(2021, 12, 31), Amount = 200, Currency = "EUR", Category = "Income"
                },
                new Transaction
                {
                    Date = new DateTime(2021, 12, 31), Amount = 300, Currency = "USD", Category = "Buy"
                }
            }
        };
        var accountService = new AccountService();
        
        var result = accountService.GetAccountValueForDate(targetDate, account);
        Assert.That((1000 + 200 + (300*1.2m) ) ==  result);
    }
    [Test]
    public void GetAccountValueForDate_ReturnsZero_WhenNoTransactionsAfterTargetDate()
    {
        var targetDate = new DateTime(2022, 01, 02);
        var account = new Account
        {
            Amount = 1000,
            AccountAsOfDate = new DateTime(2022, 1, 1),
            EurYenRate = 100,
            EurUsdRate = 1.2m,
            Transactions = new List<Transaction>
            {
                new Transaction
                {
                    Date = new DateTime(2021, 12, 30), Amount = 500, Currency = "USD", Category = "Expense"
                },
                new Transaction
                {
                    Date = new DateTime(2021, 12, 31), Amount = 200, Currency = "EUR", Category = "Income"
                },
                new Transaction
                {
                    Date = new DateTime(2021, 12, 31), Amount = 300, Currency = "USD", Category = "Buy"
                }
            }
        };
        var accountService = new AccountService();

        var result = accountService.GetAccountValueForDate(targetDate, account);

        Assert.That(result == 1000);
    }
}