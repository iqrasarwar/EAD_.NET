namespace ATMBussinessObjects
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountNum { get; set; }
        public string TransactionType { get; set; }
        public int ToAccount { get; set; }
        public int Amount { get; set; }
        public string Date { get; set; }

    }
}
