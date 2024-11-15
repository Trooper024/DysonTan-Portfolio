
# Account System Documentation

## Overview
This document provides an overview and explanation of the account system implemented in C#. The system consists of multiple classes responsible for handling basic account operations such as deposits, withdrawals, and transactions, including transfers.

---

## File 3 of 6: `Account.cs`

This file defines the `Account` class, which represents a user's bank account.

### Code Overview:

```csharp
using System;

public class Account
{
    private decimal _balance;
    private string _name;

    public Account(string name, decimal balance)
    {
        _name = name;
        _balance = balance;
    }

    // Properties
    public decimal Balance => _balance;
    public string Name => _name;

    public void PrintName()
    {
        Console.WriteLine($"Account name: {_name}");
    }

    public bool Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Please enter a value greater than zero.");
            return false;
        }
        _balance += amount;
        Console.WriteLine($"Deposited: {amount:C}. New Balance: {_balance:C}");
        return true;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Invalid: Withdrawal amount needs to be greater than zero.");
            return false;
        }

        if (amount > _balance)
        {
            Console.WriteLine("Insufficient balance, you don't have that much money!");
            return false;
        }
        _balance -= amount;
        Console.WriteLine($"Withdrawn: {amount:C}. New balance: {_balance:C}");
        return true;
    }

    public void Print()
    {
        PrintName();
        Console.WriteLine($"Balance: {_balance:C}");
    }
}
```

### Explanation:
- The `Account` class represents a user's bank account with a name and balance.
- The `Deposit` method allows adding money to the account if the amount is positive.
- The `Withdraw` method allows withdrawing money from the account if the balance is sufficient and the amount is valid.
- The `Print` method outputs the account name and balance to the console.

---

## File 4 of 6: `WithdrawTransaction.cs`

This file defines the `WithdrawTransaction` class, which represents a transaction to withdraw money from an account.

### Code Overview:

```csharp
using System;

public class WithdrawTransaction
{
    private Account _account;
    private decimal _amount;
    private bool _executed = false;
    private bool _success = false;
    private bool _reversed = false;

    public Account Account => _account;
    public decimal Amount => _amount;
    public bool IsSuccess => _success;
    public bool IsReversed => _reversed;

    public WithdrawTransaction(Account account, decimal amount)
    {
        _account = account;
        _amount = amount;
    }

    public void Execute()
    {
        if (!_executed)
        {
            _executed = true;

            try
            {
                if (_amount > 0)
                {
                    if (_account.Withdraw(_amount))
                    {
                        _success = true;
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

        if (_success)
        {
            if (_account.Deposit(_amount))
            {
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

    public void Print()
    {
        Console.WriteLine($"Withdrawal of {_amount:C} from {_account.Name}'s account");
        Console.WriteLine($"Withdrawal transaction status: {(IsSuccess ? "Success" : "Failed")}");
    }
}
```

### Explanation:
- The `WithdrawTransaction` class handles the withdrawal of money from an account.
- It includes methods for executing the transaction, rolling it back, and printing the transaction status.
- The `Rollback` method attempts to reverse the transaction by depositing the withdrawn amount back into the account.

---

## File 5 of 6: `DepositTransaction.cs`

This file defines the `DepositTransaction` class, which represents a transaction to deposit money into an account.

### Code Overview:

```csharp
using System;

namespace Banksystem
{
    public class DepositTransaction
    {
        private Account _account;
        private decimal _amount;
        private bool _executed = false;
        private bool _success = false;
        private bool _reversed = false;

        public bool IsSuccess => _success;
        public bool IsReversed => _reversed;

        public DepositTransaction(Account account, decimal amount)
        {
            _account = account;
            _amount = amount;
        }

        public void Execute()
        {
            if (_executed)
            {
                throw new InvalidOperationException("Transaction already executed.");
            }

            if (_amount > 0)
            {
                _success = _account.Deposit(_amount);
                _executed = true;
            }
            else
            {
                throw new InvalidOperationException("Invalid deposit amount.");
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

            if (_success)
            {
                if (_account.Withdraw(_amount))
                {
                    _reversed = true;
                }
                else
                {
                    throw new InvalidOperationException("Failed to reverse the deposit.");
                }
            }
            else
            {
                throw new InvalidOperationException("Cannot rollback an unsuccessful deposit.");
            }
        }

        public void Print()
        {
            Console.WriteLine($"Deposit of {_amount:C} to {_account.Name}'s account");
            Console.WriteLine($"Deposit transaction status: {(IsSuccess ? "Success" : "Failed")}");
        }
    }
}
```

### Explanation:
- The `DepositTransaction` class handles the deposit of money into an account.
- Similar to the withdrawal transaction, it provides methods for executing and rolling back the transaction, with the ability to print the transaction status.

---

## File 6 of 6: `TransferTransaction.cs`

This file defines the `TransferTransaction` class, which represents a transaction to transfer money from one account to another.

### Code Overview:

```csharp
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
            _withdraw.Execute();

            if (_withdraw.IsSuccess)
            {
                _deposit.Execute();

                if (!_deposit.IsSuccess)
                {
                    Console.WriteLine("Transfer failed due to deposit failure.");
                    _withdraw.Rollback();
                }
            }
            else
            {
                Console.WriteLine("Transfer failed due to insufficient balance.");
            }

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
            _deposit.Rollback();
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
```

### Explanation:
- The `TransferTransaction` class handles transferring money between two accounts. It executes both a withdrawal and a deposit transaction.
- It provides methods for executing, rolling back, and printing the transaction status.

---

## Conclusion

This system supports basic banking operations with functionality for handling deposits, withdrawals, and transfers. The transactions include mechanisms to execute and reverse actions, ensuring that each operation is safely completed or rolled back.
