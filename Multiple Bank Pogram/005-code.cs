using Banksystem;
using System;

public class TransferTransaction
{
    private bool _executed = false;
    private bool _reversed = false;
    private decimal _amount;
    private Account _fromAccount;
    private Account _toAccount;

    private DepositTransaction _deposit;
    private WithdrawTransaction _withdraw;

    public bool IsSuccess => _deposit.IsSuccess && _withdraw.IsSuccess;
    public bool IsReversed => _reversed;

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount)
    {
        _fromAccount = fromAccount;
        _toAccount = toAccount;
        _amount = amount;

        // Initialize constituent transactions in the constructor
        _withdraw = new WithdrawTransaction(fromAccount, amount);
        _deposit = new DepositTransaction(toAccount, amount);
    }

    public void Execute()
    {
        if (_executed)
        {
            throw new InvalidOperationException("Transaction already executed.");
        }

        try
        {
            // Execute the withdrawal transaction
            _withdraw.Execute();

            if (_withdraw.IsSuccess)
            {
                // Execute the deposit transaction
                _deposit.Execute();

                if (!_deposit.IsSuccess)
                {
                    Console.WriteLine("Transfer failed due to deposit failure.");
                    // If deposit fails, rollback the withdrawal
                    _withdraw.Rollback();
                }
            }
            else
            {
                Console.WriteLine("Transfer failed due to insufficient balance.");
            }

            // If both transactions were successful, mark the transfer as executed
            if (_withdraw.IsSuccess && _deposit.IsSuccess)
            {
                _executed = true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during transfer: {ex.Message}");
        }
    }

    public void Rollback()
    {
        if (!_executed)
        {
            throw new InvalidOperationException("Transaction not executed yet.");
        }

        if (_reversed)
        {
            throw new InvalidOperationException("Transaction already reversed.");
        }

        try
        {
            // Rollback the deposit transaction
            _deposit.Rollback();

            // Rollback the withdrawal transaction
            _withdraw.Rollback();

            _reversed = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during rollback: {ex.Message}");
        }
    }

    public void Print()
    {
        Console.WriteLine($"Transfer transaction status: {(IsSuccess ? "Success" : "Failed")}");

        if (IsSuccess)
        {
            Console.WriteLine($"Transferred {_amount:C} from {_fromAccount.Name}'s account to {_toAccount.Name}'s account");
        }
    }
}
