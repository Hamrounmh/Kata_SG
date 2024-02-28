namespace Kata.Models
{
    public class Account
    {
        public decimal Amount { get; set; }
        public DateTime AccountAsOfDate { get; set; }
        public decimal EurYenRate { get; set; }
        public decimal EurUsdRate { get; set; }
        public List<Transaction> Transactions { get; set; }
       
        //TODO:  to add a sortedTransaction to easyly manipulates transactions
        private SortedList<DateTime, Transaction> _sortedTransactions;
    }
}
