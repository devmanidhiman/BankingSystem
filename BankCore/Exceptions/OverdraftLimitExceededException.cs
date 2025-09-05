public class OverdraftLimitExceededException : Exception
{
    public OverdraftLimitExceededException() { }

    public OverdraftLimitExceededException(string message) : base(message) { }

    public OverdraftLimitExceededException(string message, Exception inner)
    : base (message , inner) { }
}