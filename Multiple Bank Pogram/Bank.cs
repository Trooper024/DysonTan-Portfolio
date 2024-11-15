using Banksystem;
using System;
using System.Collections.Generic;

// Define the Bank class to manage a collection of bank accounts
public class Bank
{
    // Create a List to store affiliated bank accounts
    private List<Account> accounts;

    // Constructor to initialize the bank with an empty list of accounts
    public Bank()
    {
        accounts = new List<Account>();
    }

    // Method to add an account to the list of affiliated bank accounts
    public void AddAccount(Account account)
    {
        accounts.Add(account);
    }

    // Method to get an account by name
    public Account GetAccount(string name)
    {
        // Loop through the list of accounts to find a matching name
        foreach (Account account in accounts)
        {
            if (account.Name == name)
            {
                return account; // Return the matching account
            }
        }

        return null; // Return null if no account with the given name is found
    }

    // Method to execute a deposit transaction
    public void ExecuteTransaction(DepositTransaction transaction)
    {
        // Call the Execute method of the deposit transaction
        transaction.Execute();
    }

    // Method to execute a withdrawal transaction
    public void ExecuteTransaction(WithdrawTransaction transaction)
    {
        // Call the Execute method of the withdrawal transaction
        transaction.Execute();
    }

    // Method to execute a transfer transaction
    public void ExecuteTransaction(TransferTransaction transaction)
    {
        // Call the Execute method of the transfer transaction
        transaction.Execute();
    }
}
