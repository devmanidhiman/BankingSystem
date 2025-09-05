public class Transaction
{
    public DateTime Timestamp { get; set; }
    public TransactionType Type { get; set; }
    public double Amount { get; set; }
    public double BalanceAfterTransaction { get; set; }
    public string Description { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{Timestamp:G} | {Type} | Amount: {Amount:C} | Balance: {BalanceAfterTransaction:C}";
    }

}