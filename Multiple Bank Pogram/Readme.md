# Banking System Documentation 💰🏦

This documentation provides an overview of the classes and functionality within the Banking System project. The system includes a set of operations like depositing, withdrawing, and transferring funds between accounts. Below is an explanation of each part of the system.

---

## Overview 📝

This system simulates basic banking operations such as:

- **Account Management**: You can create accounts, check balances, and manage funds.
- **Transactions**: The system handles deposits, withdrawals, and transfers between accounts.
- **Error Handling**: Proper checks are in place to ensure successful transactions and handle failures gracefully. ⚠️

---

## Account Class 💳

### What It Does:
The `Account` class represents a bank account. It holds the balance and account details such as the account holder’s name. You can deposit or withdraw money from the account, and it tracks the balance.

### Main Features:
- **Deposit Funds**: Add money to the account. 💸
- **Withdraw Funds**: Take money from the account (if there are sufficient funds). 💵
- **Check Balance**: View the current balance of the account. 📊

---

## WithdrawTransaction Class 💳💸

### What It Does:
The `WithdrawTransaction` class handles the process of withdrawing funds from an account. It ensures that funds can only be withdrawn if there is enough balance. If the transaction fails, it can be reversed.

### Main Features:
- **Execute Withdrawal**: Takes money out of the account. 🔄
- **Rollback**: If the transaction fails, it can be reversed to restore the account balance. ⏪

---

## DepositTransaction Class 💵💰

### What It Does:
The `DepositTransaction` class is responsible for depositing money into an account. It ensures the deposit amount is valid and performs the deposit operation.

### Main Features:
- **Execute Deposit**: Adds money to the account. ➕
- **Rollback**: If the deposit fails, it can be reversed to remove the added amount. ⏪

---

## TransferTransaction Class 🔁💰

### What It Does:
The `TransferTransaction` class allows money to be transferred between two accounts. It combines a withdrawal from one account and a deposit into another. If any part of the transfer fails, the entire transfer is rolled back.

### Main Features:
- **Execute Transfer**: Moves money from one account to another. 💸➡️💵
- **Rollback**: Reverses the transaction if any part of the transfer fails. ⏪

---

## How It Works 🛠️

The system works by creating accounts and performing transactions:

1. **Account Creation**: First, an account is created with a name and an initial balance. 🆔
2. **Deposits and Withdrawals**: Money can be added or withdrawn using the `DepositTransaction` or `WithdrawTransaction` classes. 💳💰
3. **Transfers**: The `TransferTransaction` class combines a withdrawal from one account and a deposit to another, allowing funds to be transferred between accounts. 🔄
4. **Handle Failures**: If any transaction fails (e.g., insufficient funds), the system will handle it by rolling back the operation to restore the original state. ⚠️❌

---

## Example Workflow 📜

1. **Creating Accounts**: You create two accounts, one for Alice and one for Bob, with initial balances. 🏦
   
2. **Deposit Funds**: Alice deposits money into her account. 💰

3. **Withdraw Funds**: Alice withdraws money from her account. 💵

4. **Transfer Funds**: Alice transfers money to Bob’s account. 💸➡️💵

5. **Handle Failures**: If any transaction fails (e.g., insufficient funds), the system will handle it by rolling back the operation to restore the original state. ⚠️

---

## Conclusion 🎉

This banking system is designed to simulate basic banking transactions such as deposits, withdrawals, and transfers. It ensures secure transactions by checking balances and allowing for the rollback of any failed operations.

The system is built to be flexible, allowing for future expansions such as account types, transaction history, or other banking features. 🚀

---

## Key Features 💡

- **Account Management**: Create accounts and manage balances.
- **Transactions**: Perform deposits, withdrawals, and transfers.
- **Error Handling**: Ensure transactions are safe with rollback functionality.
