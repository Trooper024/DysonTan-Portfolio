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
