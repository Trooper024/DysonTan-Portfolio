using Banksystem;
using System;

public enum MenuOption
{
    Withdraw = 1,
    Deposit = 2,
    Transfer = 3,
    Print = 4,
    AddAccount = 5, // New option for adding an account
    Quit = 6,
}



public class BankSystem
{
    private static Bank bank; // Create an instance of the Bank class

    public BankSystem()
    {
        // Initialize the bank
        bank = new Bank();
    }

    public static void Main()
    {
        //This is the test accounts that will be used to test if the transfer method works by initializing these
        Account sourceAccount = new Account("Savings Account", 1000); // Example initial balance
        Account destinationAccount = new Account("Spending Account", 500); // Example initial balance

        // Create an instance of the BankSystem class
        BankSystem bankSystem = new BankSystem();

        bank.AddAccount(sourceAccount);
        bank.AddAccount(destinationAccount);

        // Get an account by name
        Account retrievedAccount = bank.GetAccount("Savings Account");
        if (retrievedAccount != null)
        {
            Console.WriteLine($"Found account: {retrievedAccount.Name}, Balance: {retrievedAccount.Balance:C}");
        }
        else
        {
            Console.WriteLine("Account not found.");
        }

        // User interaction loop
        while (true)
        {
            MenuOption choice = bankSystem.ReadUserOption();

            switch (choice)
            {
                case MenuOption.Withdraw:
                    bankSystem.DoWithdraw(bank);
                    break;

                case MenuOption.Deposit:
                    bankSystem.DoDeposit(bank);
                    break;

                case MenuOption.Transfer:
                    bankSystem.DoTransfer(bank);
                    break;

                case MenuOption.Print:
                    bankSystem.DoPrint(bank);
                    break;

                case MenuOption.AddAccount:
                    bankSystem.DoAddAccount(); // Call a new method for adding an account
                    break;


                case MenuOption.Quit:
                    Console.WriteLine("Goodbye!");
                    return; // Exit the program

                default:
                    Console.WriteLine("Error: Invalid choice. Please try again.");
                    break;
            }
        }
    }

    public MenuOption ReadUserOption()
    {
        int userChoice;

        do
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Withdraw");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Transfer");
            Console.WriteLine("4. Print");
            Console.WriteLine("5. Add new account"); // New option
            Console.WriteLine("6. Quit");

            userChoice = ReadInteger("Enter your choice (1-6): "); // Adjust the range

            if (Enum.IsDefined(typeof(MenuOption), userChoice))
            {
                return (MenuOption)userChoice;
            }
            else
            {
                Console.WriteLine("Error: Invalid choice. Please enter a valid option (1-6)."); // Adjust the range
            }
        } while (true);
    }

    public void DoAddAccount()
    {
        string accountName = ReadAccountName("Enter the name of the new account: ");
        decimal startingBalance = ReadDecimal("Enter the starting balance: ");

        Account newAccount = new Account(accountName, startingBalance);

        bank.AddAccount(newAccount);

        Console.WriteLine($"Account '{newAccount.Name}' with starting balance {newAccount.Balance:C} added successfully.");
    }

    private static Account FindAccount(Bank bank)
    {
        while (true)
        {
            string accountName = ReadAccountName("Enter the name of the account: ");
            Account foundAccount = bank.GetAccount(accountName);

            if (foundAccount != null)
            {
                return foundAccount;
            }
            else
            {
                Console.WriteLine("Account not found. Please enter a valid account name.");
            }
        }
    }


    public void DoTransfer(Bank bank)
    {
        // Use the FindAccount method to find the source account for transfer
        Account sourceAccount = FindAccount(bank);

        // Use the FindAccount method to find the destination account for transfer
        Account destinationAccount = FindAccount(bank);

        decimal transferAmount = ReadDecimal("Enter the amount to transfer: ");

        if (transferAmount > 0 && sourceAccount != null && destinationAccount != null)
        {
            TransferTransaction transferTransaction = new TransferTransaction(sourceAccount, destinationAccount, transferAmount);

            try
            {
                transferTransaction.Execute();  // Execute the transfer
                transferTransaction.Print();    // Print transaction details
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Transfer failed: " + ex.Message);
                if (!transferTransaction.IsSuccess) // Check if the transfer was successful before attempting rollback
                {
                    Console.WriteLine("Do you want to rollback the transaction? (Y/N)");
                    if (Console.ReadLine().ToUpper() == "Y")
                    {
                        transferTransaction.Rollback();
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid transfer amount or accounts. Please enter a valid positive amount and ensure both accounts exist.");
        }
    }

    public void DoWithdraw(Bank bank)
    {
        // Use the FindAccount method to find the account for withdrawal
        Account account = FindAccount(bank);

        decimal withdrawalAmount = ReadDecimal("Enter the amount to withdraw: ");

        if (withdrawalAmount > 0)
        {
            WithdrawTransaction withdrawTransaction = new WithdrawTransaction(account, withdrawalAmount);

            try
            {
                withdrawTransaction.Execute();  // Execute the withdrawal
                withdrawTransaction.Print();    // Print transaction details
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Withdrawal failed: " + ex.Message);
                if (!withdrawTransaction.IsReversed)
                {
                    Console.WriteLine("Do you want to rollback the transaction? (Y/N)");
                    if (Console.ReadLine().ToUpper() == "Y")
                    {
                        try
                        {
                            withdrawTransaction.Rollback();  // Attempt to rollback the transaction
                            Console.WriteLine("Transaction rolled back successfully.");
                        }
                        catch (InvalidOperationException rollbackEx)
                        {
                            Console.WriteLine("Rollback failed: " + rollbackEx.Message);
                        }
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid withdrawal amount. Please enter a valid positive amount.");
        }
    }
    public void DoDeposit(Bank bank)
    {
        // Use the FindAccount method to find the account for deposit
        Account account = FindAccount(bank);

        decimal depositAmount = ReadDecimal("Enter the amount to deposit: ");

        if (depositAmount > 0)
        {
            DepositTransaction depositTransaction = new DepositTransaction(account, depositAmount);

            try
            {
                depositTransaction.Execute();  // Execute the deposit
                depositTransaction.Print();    // Print transaction details
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Deposit failed: " + ex.Message);
                if (!depositTransaction.IsReversed)
                {
                    Console.WriteLine("Do you want to rollback the transaction? (Y/N)");
                    if (Console.ReadLine().ToUpper() == "Y")
                    {
                        depositTransaction.Rollback();
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid deposit amount. Please enter a valid positive amount.");
        }
    }

    public void DoPrint(Bank bank)
    {
        // Use the FindAccount method to find the account to print
        Account account = FindAccount(bank);

        if (account != null)
        {
            account.Print();
        }
        else
        {
            Console.WriteLine("Account not found. Please enter a valid account name.");
        }
    }

    public decimal ReadDecimal(string message)
    {
        decimal result;

        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();

            if (decimal.TryParse(input, out result) && result >= 0)
            {
                break; // Successfully parsed the input as a positive decimal, exit the loop
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid positive decimal number.");
            }
        }

        return result;
    }

    public int ReadInteger(string message)
    {
        int result;

        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();

            if (int.TryParse(input, out result))
            {
                break; // Successfully parsed the input as an integer, exit the loop
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }

        return result;
    }

    public static string ReadAccountName(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }

}
