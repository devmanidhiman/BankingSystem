public interface IAccount
{
    string AccountHolderName { get; }
    long AccountNumber { get; }
    double GetBalance();
    double WithdrawFund(double amount);
    double DepositFund(double amount);

}