using BillsPaymentSystem.Data;
using BillsPaymentSystem.Models;
using BillsPaymentSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsPaymentSystem.App
{
    public class DbInitializer
    {
        public static void Seed(BillsPaymentSystemContext context)
        {
            SeedUsers(context);
            SeedCreditCards(context);
            SeedBankAccounds(context);
            SeedPaymentMethod(context);
        }

        private static void SeedPaymentMethod(BillsPaymentSystemContext context)
        {
            var paymentMethods = new List<PaymentMethod>();

            for (int i = 0; i < 4; i++)
            {
                var paymentMethod = new PaymentMethod
                {
                    UserId = new Random().Next(1, 8),
                    Type = (PaymentType)new Random().Next(0, 2)
                };

                if (paymentMethod.Type == PaymentType.BankAccount)
                {
                    paymentMethod.BankAccountId = i + 1;
                    paymentMethod.CreditCardId = null;
                }
                else if (paymentMethod.Type == PaymentType.CreditCard)
                {
                    paymentMethod.CreditCardId = i + 1;
                    paymentMethod.BankAccountId = null;
                }

                if (!IsValid(paymentMethod))
                {
                    continue;
                }

                //var user = context.Users.Find(paymentMethod.UserId);
                //var creditCard = context.Users.Find(paymentMethod.CreditCardId);
                //var bankAccount = context.Users.Find(paymentMethod.BankAccountId);

                paymentMethods.Add(paymentMethod);
            }

            context.PaymentMethods.AddRange(paymentMethods);
            context.SaveChanges();
        }

        private static void SeedBankAccounds(BillsPaymentSystemContext context)
        {
            var accounts = new List<BankAccount>();

            for (int i = 0; i < 8; i++)
            {
                var bankAccount = new BankAccount
                {
                    Balance = new Random().Next(-1000, 3000),
                    BankName = "Bank" + i,
                    SwiftCode = "SWIFT" + 234 + i
                };

                if (!IsValid(bankAccount))
                {
                    continue;
                }

                accounts.Add(bankAccount);
            }

            context.BankAccounts.AddRange(accounts);
            context.SaveChanges();
        }

        private static void SeedCreditCards(BillsPaymentSystemContext context)
        {
            var creditCards = new List<CreditCard>();

            for (int i = 0; i < 8; i++)
            {
                var creditcard = new CreditCard
                {
                    Limit = new Random().Next(-20000, 25000),
                    MoneyOwed = new Random().Next(-20000, 25000),
                    ExpirationDate = DateTime.Now.AddDays(new Random().Next(-200, 200))
                };

                if (!IsValid(creditcard))
                {
                    continue;
                }

                creditCards.Add(creditcard);
            }

            context.CreditCards.AddRange(creditCards);
            context.SaveChanges();
        }

        private static void SeedUsers(BillsPaymentSystemContext context)
        {
            string[] firstNames = { "Denko", "tonko", "Canko", "Bonio", "Gunio", "Tunio", " ", null };
            string[] lastNames = { "Dekov", "Tonkov", "Cankov", "Boniov", "guniov", "Tuniov", " ", null };
            string[] email = { "dkv@mail.com", "tonko@mail.com", "canko@mail.com", "Bonio@mail.com", "gunio@mail.com", "Tunio@mail.com", "ERR", null };
            string[] password = { "dkv@mails335om", "tonk435ail.com", "canko657ail.com", "Bonio@8789.com", "gunio98om", "Tun909897ail.com", "ERR", null };

            var users = new List<User>();

            for (int i = 0; i < firstNames.Length; i++)
            {
                var user = new User
                {
                    FirstName = firstNames[i],
                    LastName = lastNames[i],
                    Email = email[i],
                    Password = password[i]
                };

                if (!IsValid(user))
                {
                    continue;
                }

                users.Add(user);
            }

            context.Users.AddRange(users);
            context.SaveChanges();
        }

        private static bool IsValid(object entity)
        {
            var validationContext = new ValidationContext(entity);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entity, validationContext, validationResults, true);

            return isValid;
        }
    }
}
