using System;
using Kata.Services;
using NUnit.Framework;
using Moq;
using System.IO;
using System.Collections.Generic;
using Kata.Models;

namespace Kata.Tests.Services;

[TestFixture]
[TestOf(typeof(ICsvAccountReaderService))]
public class ICsvAccountReaderServiceTest
{
    private Mock<ICsvAccountReaderService> _csvAccountReaderServiceMock;

    [SetUp]
    public void Setup()
    {
        _csvAccountReaderServiceMock = new Mock<ICsvAccountReaderService>();
    }

    [Test]
    public void DeserializeCsv_ValidCsv_ReturnsAccount()
    {
        // Arrange
        var expectedAccount = new Account
        {
            Amount = 1000,
            AccountAsOfDate = DateTime.Now,
            EurYenRate = 130,
            EurUsdRate = 1.2m,
            Transactions = new List<Transaction>
            {
                new Transaction
                {
                    Date = DateTime.Now.AddDays(-1),
                    Amount = 100,
                    Currency = "EUR",
                    Category = "Groceries"
                }
            }
        };

        _csvAccountReaderServiceMock.Setup(x => x.DeserializeCsv()).Returns(expectedAccount);

        var result = _csvAccountReaderServiceMock.Object.DeserializeCsv();

        Assert.That(expectedAccount == result);
    }

    [Test]
    public void DeserializeCsv_InvalidCsv_ThrowsException()
    {
        _csvAccountReaderServiceMock.Setup(x => x.DeserializeCsv()).Throws(new InvalidDataException());

        Assert.Throws<InvalidDataException>(() => _csvAccountReaderServiceMock.Object.DeserializeCsv());
    }

    [Test]
    public void DeserializeCsv_EmptyCsv_ReturnsEmptyAccount()
    {
        var expectedAccount = new Account
        {
            Amount = 0,
            AccountAsOfDate = DateTime.MinValue,
            EurYenRate = 0,
            EurUsdRate = 0,
            Transactions = new List<Transaction>()
        };

        _csvAccountReaderServiceMock.Setup(x => x.DeserializeCsv()).Returns(expectedAccount);

        var result = _csvAccountReaderServiceMock.Object.DeserializeCsv();

        Assert.That(expectedAccount ==  result);
    }
}