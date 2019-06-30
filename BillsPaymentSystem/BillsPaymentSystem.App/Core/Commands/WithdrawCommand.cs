using BillsPaymentSystem.App.Core.Commands.Contracts;
using BillsPaymentSystem.Data;
using BillsPaymentSystem.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BillsPaymentSystem.App.Core.Commands
{
    public class WithdrawCommand : ICommand
    {
        internal BillsPaymentSystemContext context;
        private object bank;

        public WithdrawCommand(BillsPaymentSystemContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            int userId = int.Parse(args[0]);
            decimal amount = decimal.Parse(args[1]);

            return Withdraw(userId, amount);
        }

        internal string Withdraw(int userId, decimal amount)
        {
            decimal totalBalanceBankAccount = 0;
            decimal totalBalanceCreditCard = 0;
            string result = null;

            var paymentMethods = context.PaymentMethods
                .Where(pm => pm.UserId == userId)
                .Include("BankAccount")
                .Include("CreditCard")
                .ToList();

            foreach (var paymentMethod in paymentMethods)
            {
                if (paymentMethod.Type == PaymentType.BankAccount)
                {

                    decimal balanceBank = paymentMethod.BankAccount.Balance;

                    if (balanceBank < amount)
                    {
                        continue;
                    }
                    else
                    {
                        paymentMethod.BankAccount.Balance -= amount;
                        context.SaveChanges();

                        totalBalanceBankAccount = paymentMethod.BankAccount.Balance;

                        result = $"BankAccount -> {totalBalanceBankAccount}";

                        break;
                    }
                }

                else if (paymentMethod.Type == PaymentType.CreditCard)
                {
                    var balanceCredit = paymentMethod.CreditCard.MoneyOwed;

                    var limitLeft = paymentMethod.CreditCard.LimitLeft;

                    if (balanceCredit < amount && limitLeft < amount)
                    {
                        continue;
                    }
                    else
                    {
                        paymentMethod.CreditCard.MoneyOwed -= amount;
                        context.SaveChanges();

                        totalBalanceCreditCard = paymentMethod.CreditCard.MoneyOwed;

                        result = $"BankAccount -> {totalBalanceCreditCard}";

                        break;
                    }
                }
            }

            if (result == null)
            {
                return "Insufficient funds!";
            }
            return result;
        }
    }
}
