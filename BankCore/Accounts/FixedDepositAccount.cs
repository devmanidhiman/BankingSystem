using BankCore;

public class FixedDepositAccount : BankAccount
{
    public int TermInMonths { get; }
    public double InterestRate { get; }
    public DateTime MaturityDate { get; }

    private bool _isMatured = false;

    public FixedDepositAccount(string accountHolderName, long accountNumber, double principal, int termInMonths, double interestRate, DateTime maturityDate)
    : base(accountHolderName, accountNumber, principal)
    {

        if (maturityDate < DateTime.Now)
        {
            throw new ArgumentException("Maturity date should not be in the past.");
        }
        MaturityDate = maturityDate;
        TermInMonths = termInMonths;
        InterestRate = interestRate;
    }

    public override double WithdrawFund(double amount)
    {
        if (!IsMature())
        {
            AddTransaction(0.0, TransactionType.Withdraw, "Withdrawal blocked: account not matured");
            throw new InvalidTransactionException("Cannot withdraw before maturity.");
        }
        ValidateAndWithdraw(amount, "Fixed Deposit withdrawal");
        return Balance;

    }

    public override double DepositFund(double amount)
    {
        throw new InvalidOperationException("Deposits are not allowed after creation.");
    }

    public void Mature()
    {
        if (_isMatured) return;

        double interest = Balance * InterestRate / 100;
        IncreaseBalance(interest, "Fixed Deposit Maturity");
        _isMatured = true;
    }

    public void CheckAndMature()
    {
        if (IsMature()) Mature();
    }
    public bool IsMature() => DateTime.Now >= MaturityDate.AddDays(1);
    
    public override string ToString()
    {
        return $"FD Account #{AccountNumber} | Holder: {AccountHolderName} | Balance: {Balance:C} | Matures: {MaturityDate:d}";
    }
}