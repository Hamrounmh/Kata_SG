using System.Globalization;

namespace Kata.Models
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Category { get; set; }

        public static Transaction FromCsv(string line)
        {
            string[] values = line.Split(';');
            return new Transaction
            {
                Date = DateTime.ParseExact(values[0], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Amount = decimal.Parse(values[1], CultureInfo.InvariantCulture),
                Currency = values[2],
                Category = values[3]
            };
        }
        
    }
}
