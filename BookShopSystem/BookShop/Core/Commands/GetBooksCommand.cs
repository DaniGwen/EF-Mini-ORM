using BookShop.Models;
using BookShop.Models.Enums;
using BookShop_AgeRestriction_Added.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop_AgeRestriction_Added.Core.Commands
{
    public class GetBooksCommand : ICommand
    {
        private readonly BookShopContext context;

        public GetBooksCommand(BookShopContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            var ageRestrictionCommand = args[0].ToLower();

            return GetBooksByAgeRestriction(context, ageRestrictionCommand);
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            string firstLetter = command.Substring(0, 1).ToUpper();
            command = firstLetter + command.Substring(1);

            var ageRestrictionCommand = (AgeRestriction)Enum.Parse(typeof(AgeRestriction), command);

            var books = context.Books
                .Where(b => b.AgeRestriction == ageRestrictionCommand)
                .ToList();

            var sb = new StringBuilder();

            foreach (var book in books.OrderBy(b=>b.Title))
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString();
        }
    }
}
