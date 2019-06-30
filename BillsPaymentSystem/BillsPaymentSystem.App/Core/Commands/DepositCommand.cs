using BillsPaymentSystem.App.Core.Commands.Contracts;
using BillsPaymentSystem.Data;
using BillsPaymentSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillsPaymentSystem.App.Core.Commands
{
    public class DepositCommand : ICommand
    {
        private readonly BillsPaymentSystemContext context;

        public DepositCommand(BillsPaymentSystemContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            int userId = int.Parse(args[0]);
            decimal depositAmount = decimal.Parse(args[1]);
            PaymentType cardType = (PaymentType)int.Parse(args[2]);

            return Deposit(userId, depositAmount, cardType);
        }

        private string Deposit(int userId, decimal depositAmount, PaymentType cardType)
        {
            var userPaymentMethods = context.PaymentMethods
                .Where(pm => pm.UserId == userId)
                .ToList();

            string result = null;
            decimal totalAmount = 0;

            foreach (var card in userPaymentMethods)
            {
                if (cardType == PaymentType.BankAccount || card.Type == PaymentType.BankAccount)
                {
                    var bankAccount = context.BankAccounts
                        .FirstOrDefault(b => b.BankAccountId == card.BankAccountId);

                    totalAmount = bankAccount.Balance += depositAmount;

                    context.SaveChanges();
                    return result = $"Deposited {depositAmount} \n Balance -> {totalAmount}";
                }
                else if(cardType == PaymentType.CreditCard || card.Type == PaymentType.CreditCard)
                {
                    var creditCard = context.CreditCards.
                        FirstOrDefault(cc => cc.CreditCardId == card.CreditCardId);

                    totalAmount = creditCard.MoneyOwed += depositAmount;

                    context.SaveChanges();
                    return result = $"Deposited {depositAmount} \n Balance -> {totalAmount}";
                }
                else
                {
                    return "No bank cards found!";
                }
            }

            if (result == null)
            {
                return "No CreditCards or BankAccounts found!";
            }

            return result;
        }
    }
}
