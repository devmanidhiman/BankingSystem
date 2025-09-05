using BankCore;

public class Customer
{
    public string Name { get; }
    public long CustomerId { get; }
    private readonly List<BankAccount> _accounts = [];
    public IReadOnlyList<BankAccount> Accounts => _accounts.AsReadOnly();

    public Customer(string name, long customerId)
    {
        Name = name;
        CustomerId = customerId;
    }

    public void AddAccount(BankAccount account)
    {
        if (account.AccountHolderName != Name)
        {
            throw new InvalidOperationException("Account holder name must match customer name.");
        }
        _accounts.Add(account);
    }

    public BankAccount? GetAccountByNumber(long accountNumber)
    {
        return _accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
    }

}