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

    //Properties
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

        _balance += amount; //add and assigns the new amount to the balance
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