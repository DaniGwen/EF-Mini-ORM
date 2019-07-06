using BookShop.Data;
using BookShop_GoldenBooks_Added.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.Core.Commands
{
    public class AuthorSearchCommand : ICommand
    {
        private readonly BookShopContext context;

        public AuthorSearchCommand(BookShopContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            var endingString = args[0].ToLower();

            return GetAuthorNamesEndingIn(context, endingString);
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName
                })
                .OrderBy(a => a.FirstName)
                .ToList();

            var sb = new StringBuilder();

            foreach (var author in authors)
            {
                sb.AppendLine(author.FirstName + " " + author.LastName);
            }

            return sb.ToString();
        }
    }
}
