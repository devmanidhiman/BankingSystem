namespace BankCore
{
    public abstract class BankAccount :IAccount
    {
        public string AccountHolderName { get; private set; }
        public long AccountNumber { get; }
        public double Balance { get; private set; }

        // Keep transaction history immutable from outside
        private List<Transaction> _transactionHistory = [];
        public IReadOnlyList<Transaction> TransactionHistory => _transactionHistory.AsReadOnly();
        protected BankAccount(string accountHolderName, long accountNumber ,double initialBalance = 0.0)
        {
            if (string.IsNullOrWhiteSpace(accountHolderName))
            {
                throw new ArgumentException("Account holder name cannot be empty.");
            }

            if (initialBalance < 0)
            {
                throw new ArgumentException("Initial Balance cannot be negative");
            }

            AccountHolderName = accountHolderName;
            Balance = initialBalance;
            AccountNumber = accountNumber;
            _transactionHistory = [];

            if (initialBalance > 0)
            {
                AddTransaction(initialBalance, TransactionType.Deposit, "Initial Deposit");
            }

        }

        public virtual double GetBalance() => Balance;

        public virtual double DepositFund(double amount)
        {
            if (amount <= 0)
            {
                throw new InvalidTransactionException("Deposit amount must be greater than 0.");
            }

            IncreaseBalance(amount, "Deposit");
            return Balance;
        }

        public virtual double WithdrawFund(double amount)
        {
            ValidateAndWithdraw(amount, "Normal withdrawal.");
            return Balance;
        }

        /// <summary>
        /// Core withdrawal validation — subclasses can call this safely.
        /// </summary>
        protected virtual void ValidateAndWithdraw(double amount, string description)
        {
            if (amount <= 0)
            {
                throw new InvalidTransactionException("Withdrawal amount must be positive.");
            }
            if (amount > Balance)
            {
                throw new InsufficientFundsException("Insufficient balance for withdrawal.");
            }
            DecreaseBalance(amount, description);
        }

        /// <summary>
        /// Centralized balance increase with transaction logging.
        /// </summary>
        protected void IncreaseBalance(double amount, string description)
        {
            Balance += amount;
            AddTransaction(amount, TransactionType.Deposit, description);
        }

        // <summary>
        /// Centralized balance decrease with transaction logging.
        /// </summary>
        protected void DecreaseBalance(double amount, string description)
        {
            Balance -= amount;
            AddTransaction(amount, TransactionType.Withdraw, description);
        }

        public void AddTransaction(double amount, TransactionType type, string description)
        {
            _transactionHistory.Add(new Transaction
            {
                Timestamp = DateTime.Now,
                Type = type,
                Amount = amount,
                BalanceAfterTransaction = Balance,
                Description = description
            });
        }

        public void PrintTransactionHistory()
        {
            if (_transactionHistory.Count == 0)
            {
                Console.WriteLine("No transactions yet.");
                return;
            }

            Console.WriteLine("Transaction History: ");
            foreach (var transaction in _transactionHistory)
            {
                Console.WriteLine(transaction);
            }

        }
    }
}