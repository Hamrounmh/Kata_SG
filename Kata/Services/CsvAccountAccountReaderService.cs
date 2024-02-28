using Kata.Models;
using System.Formats.Asn1;
using System.Globalization;
using CsvHelper;

namespace Kata.Services;

class CsvAccountAccountReaderService : ICsvAccountReaderService
{
    private readonly string _filePath;
    public CsvAccountAccountReaderService(string filePath)
    {
        this._filePath = filePath;
    }
    
    public Account DeserializeCsv()
    {
        using (var reader = new StreamReader(_filePath))
        {
            var account = new Account
            {
                Transactions = new List<Transaction>()
            };
            if (!reader.EndOfStream)
            {
                var accountInfo = reader.ReadLine().Split(':');
                account.Amount = decimal.Parse(accountInfo[1].Split(' ')[1].Trim());
                account.AccountAsOfDate = DateTime.ParseExact(accountInfo[0].Split(' ')[2], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (!reader.EndOfStream)
            {
                var eurYenRate = reader.ReadLine().Split(':');
                account.EurYenRate = decimal.Parse(eurYenRate[1].Trim());
            }
            if (!reader.EndOfStream)
            {
                var eurUsdRate = reader.ReadLine().Split(':');
                account.EurUsdRate = decimal.Parse(eurUsdRate[1].Trim());
            }
            // Skip header line
            if (!reader.EndOfStream)
            {
                reader.ReadLine();
            }
            // Parse transactions
            while (!reader.EndOfStream)
            {
                account.Transactions.Add(Transaction.FromCsv(reader.ReadLine()));
            }
            return account;
        }
    }
   
}