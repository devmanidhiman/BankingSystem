using BankCore;
namespace TestBankAccount
{
    public class TestBankAccount : BankAccount
    {

        public TestBankAccount(): base("test User", 9999999999, 0.0) { }

        public override double DepositFund(double amount) => base.DepositFund(amount);
        public override double WithdrawFund(double amount) => base.WithdrawFund(amount);
        [Fact]
        public void DepositFund_WithValidAmount_UpdatesBalance()
        {
            // Arrange, Act, Assert
            var account = new TestBankAccount();
            var result = account.DepositFund(100.00);

            Assert.Equal(100, result);
            Assert.Equal(100.00, account.Balance);

        }

        [Fact]
        public void WithdrawFund_WithSufficientBalance_UpdatesBalance()
        {
            var account = new TestBankAccount();
            account.DepositFund(200);
            var result = account.WithdrawFund(50);

            Assert.Equal(150, result);
            Assert.Equal(150, account.Balance);

        }

        [Fact]
        public void WithdrawFund_WithInsufficientBalance_ThrowsException()
        {
            var account = new TestBankAccount();
            account.DepositFund(30.0);

            var ex = Assert.Throws<InsufficientFundsException>(() => account.WithdrawFund(50.0));
            Assert.Equal("Insufficient balance for withdrawal.", ex.Message);

            Assert.Equal(30.0, account.Balance); // Balance should remain unchanged
            Assert.DoesNotContain(account.TransactionHistory, t => t.Type == TransactionType.Withdraw);
        }

        [Theory]
        [InlineData(-10.0)]
        [InlineData(0.0)]
        public void DepositFund_WithInvalidAmount_ThrowsException(double invalidAmount)
        {
            var account = new TestBankAccount();

            Assert.Throws<InvalidTransactionException>(() => account.DepositFund(invalidAmount));
        }

        [Theory]
        [InlineData(-20.0)]
        [InlineData(0.0)]
        public void WithdrawFund_WithInvalidAmount_ThrowsException(double invalidAmount)
        {
            var account = new TestBankAccount();

            Assert.Throws<InvalidTransactionException>(() => account.WithdrawFund(invalidAmount));
        }
    }
}
