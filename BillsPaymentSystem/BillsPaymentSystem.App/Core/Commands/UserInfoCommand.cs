using BillsPaymentSystem.App.Core.Commands.Contracts;
using BillsPaymentSystem.Data;
using BillsPaymentSystem.Models;
using BillsPaymentSystem.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BillsPaymentSystem.App.Core.Commands
{
    public class UserInfoCommand : ICommand
    {
        private readonly BillsPaymentSystemContext context;

        public UserInfoCommand(BillsPaymentSystemContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            int userId = int.Parse(args[0]);

            var user = this.context
                .Users
                .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                throw new ArgumentNullException($"User with Id {userId} not found!");
            }

            var paymentMethods = context.PaymentMethods
                .Where(pm => pm.UserId == user.UserId)
                .ToList();

            var bankAccounts = context.BankAccounts
                .Join(paymentMethods, (b => b.BankAccountId), (p => p.BankAccountId),
                (b, p) => new
                {
                    b.BankAccountId,
                    b.Balance,
                    b.BankName,
                    b.SwiftCode
                })
                .ToList();

            var creditCards = context.CreditCards.Join(paymentMethods,
                (cc => cc.CreditCardId),
                (pm => pm.CreditCardId),
                (cc, pm) => new
                {
                    cc.CreditCardId,
                    cc.Limit,
                    cc.MoneyOwed,
                    cc.LimitLeft,
                    cc.ExpirationDate
                })
           .ToList();

            var sb = new StringBuilder();

            sb.AppendLine($"User: {user.FirstName} {user.LastName}");
            sb.AppendLine("Bank Accounts:");

            foreach (var account in bankAccounts)
            {
                sb.AppendLine($"-- ID: {account.BankAccountId}");
                sb.AppendLine($"--- Balance: {account.Balance}");
                sb.AppendLine($"--- Bank: {account.BankName}");
                sb.AppendLine($"--- SWIFT: {account.SwiftCode}");
            }

            sb.AppendLine("Credit cards:");

            foreach (var creditCard in creditCards)
            {
                sb.AppendLine($"-- ID: {creditCard.CreditCardId}");
                sb.AppendLine($"--- Limit: {creditCard.Limit}");
                sb.AppendLine($"-- Money Owed: {creditCard.MoneyOwed}");
                sb.AppendLine($"-- Limit Left: {creditCard.LimitLeft}");
                sb.AppendLine($"-- Expiration Date: {creditCard.ExpirationDate}");
            }
            return sb.ToString();
        }
    }
}
