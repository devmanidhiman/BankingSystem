using BankCore;

public class Bank
{

    private readonly Dictionary<long, IAccount> _accounts = new();
    private long _nextAccountNumber = 1000001;
    private int _nextCustomerId = 1;
    private readonly List<Customer> _customer = [];

    public Customer CreateCustomer(string name)
    {
        var customer = new Customer(name, _nextCustomerId++);
        _customer.Add(customer);
        return customer;
    }

    public CheckingAccount CreateCheckingAccount(Customer customer, double initialBalance, double overdraftLimit)
    {
        var account = new CheckingAccount(customer.Name, _nextAccountNumber++, initialBalance, overdraftLimit);
        AddAccount(account);
        customer.AddAccount(account);
        return account;
    }

    public SavingsAccount CreateSavingsAccount(Customer customer, double interestRate, double initialBalance = 0.0)
    {
        var account = new SavingsAccount(customer.Name, _nextAccountNumber++, interestRate, initialBalance);
        AddAccount(account);
        return account;
    }

    public void AddAccount(IAccount account)
    {
        if (_accounts.ContainsKey(account.AccountNumber))
        {
            throw new ArgumentException("An account with number already exists.");
        }

        _accounts[account.AccountNumber] = account;
    }

    public IAccount FindAccount(long accountNumber)
    {
        if (_accounts.TryGetValue(accountNumber, out var account))
        {
            return account;
        }
        throw new KeyNotFoundException($"No Account Number with this number: {accountNumber}.");
    }
    public void ListAccounts()
    {
        if (_accounts.Count == 0)
        {
            Console.WriteLine("No accounts registered in the bank.");
        }
        Console.WriteLine("List of Accounts.");
        foreach (var acc in _accounts.Values)
        {
            Console.WriteLine($"Account Number: {acc.AccountNumber}, Holder: {acc.AccountHolderName}, Balance: {acc.GetBalance():C}");
        }

    }

    public IAccount? GetAccount(long accountNumber)
    {
        return _accounts.TryGetValue(accountNumber, out var account) ? account : null;
    }
}