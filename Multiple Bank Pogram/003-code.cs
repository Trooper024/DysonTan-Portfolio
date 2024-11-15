using System;

public class WithdrawTransaction
{
    private Account _account;    // Refers to the account from which money is to be withdrawn
    private decimal _amount;     // Specifies the amount of money to withdraw
    private bool _executed = false; // Indicates if the transaction has been executed
    private bool _success = false;  // Indicates if the transaction was successful
    private bool _reversed = false; // Indicates if the transaction has been reversed

    public Account Account => _account; // Property to access the associated account
    public decimal Amount => _amount;    // Property to access the withdrawal amount
    public bool IsSuccess => _success;   // Property to check if the withdrawal was successful
    public bool IsReversed => _reversed; // Property to check if the transaction was reversed

    // Constructor
    public WithdrawTransaction(Account account, decimal amount)
    {
        _account = account;
        _amount = amount;
    }

    // Execute method to perform the withdrawal
    public void Execute()
    {
        if (!_executed)
        {
            _executed = true;

            try
            {
                if (_amount > 0)
                {
                    if (_account.Withdraw(_amount)) // Attempt to withdraw the specified amount
                    {
                        _success = true; // Mark the transaction as successful
                    }
                    else
                    {
                        Console.WriteLine("Withdrawal failed. Insufficient balance.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid withdrawal amount.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during withdrawal: {ex.Message}");
            }
        }
        else
        {
            throw new InvalidOperationException("Transaction already executed.");
        }
    }

    // Rollback method (for illustrative purposes)
    public void Rollback()
    {
        // Check if the transaction has not been executed
        if (!_executed)
        {
            throw new InvalidOperationException("Transaction not executed yet.");
        }

        // Check if the transaction has already been reversed
        if (_reversed)
        {
            throw new InvalidOperationException("Transaction already reversed.");
        }

        // Check if the transaction was successful
        if (_success)
        {
            if (_account.Deposit(_amount)) // Attempt to deposit the withdrawn amount back
            {
                // Mark the transaction as successfully reversed
                _reversed = true;
            }
            else
            {
                Console.WriteLine("Failed to reverse the withdrawal.");
            }
        }
        else
        {
            throw new InvalidOperationException("Transaction was not successful.");
        }
    }

    // Print method to display transaction details
    public void Print()
    {
        Console.WriteLine($"Withdrawal of {_amount:C} from {_account.Name}'s account");
        Console.WriteLine($"Withdrawal transaction status: {(IsSuccess ? "Success" : "Failed")}");
    }
}
