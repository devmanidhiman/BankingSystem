using BankCore;

public class CheckingAccount : BankAccount, IAccount
{
    public double OverdraftLimit { get; set; }

    public CheckingAccount(string accountHolderName, long accountNumber, double initialBalance, double overdraftLimit)
    : base(accountHolderName, accountNumber, initialBalance)
    {
        if (overdraftLimit < 0)
        {
            throw new OverdraftLimitExceededException("Overdraft limit cannot be negative.");
        }
        OverdraftLimit = overdraftLimit;
    }

    public override double WithdrawFund(double amount)
    {
        if (amount <= 0)
        {
            throw new InvalidTransactionException("Withdrawal amount must be positive.");
        }

        if (amount > Balance + OverdraftLimit)
        {
            throw new OverdraftLimitExceededException("Withdrawal exceeds overdraft limit.");
        }

        WithdrawWithOverdraft(amount, "Checking Account withdrawal");
        return Balance;
    }

    private void WithdrawWithOverdraft(double amount, string description)
    {
        double originalBalance = Balance;
        if (amount <= originalBalance)
        {
            ValidateAndWithdraw(amount, description);
        }
        else
        {
            ValidateAndWithdraw(originalBalance, description);
            double overdraftUsed = amount - originalBalance;
            DecreaseBalance(overdraftUsed, $"Overdraft Withdrawal. (Limit: {OverdraftLimit})");
        }
    }

    public override double DepositFund(double amount)
    {
        return base.DepositFund(amount);
    }

}