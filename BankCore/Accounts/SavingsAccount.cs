using BankCore;

public class SavingsAccount : BankAccount, IAccount
{
    public double InterestRate { get; private set; }
    public SavingsAccount(string accountHolderName, long accountNumber, double interestRate, double initialBalance = 0)
     : base(accountHolderName, accountNumber, initialBalance)
    {
        if (interestRate < 0)
            throw new ArgumentException("Interest rate cannot be negative.");
        if (interestRate > 20)
            throw new ArgumentException("Interest rate cannot exceed 20%.");
        InterestRate = interestRate;
    }

    public void ApplyInterest()
    {
        double interest = Balance * InterestRate / 100;
        if (interest == 0)
        {
            return;
        }
        IncreaseBalance(interest, "Interest Applied");
    }

}