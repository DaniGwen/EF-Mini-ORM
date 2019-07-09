using BookShop.Data;
using BookShop_GoldenBooks_Added.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BookShop.Core.Commands
{
    public class TotalBookCopiesCommand : ICommand
    {
        private readonly BookShopContext context;

        public TotalBookCopiesCommand(BookShopContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            return CountCopiesByAuthor(context);
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors
                .Select(x => new
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    TotalCopies = x.Books.Sum(c => c.Copies)
                })
                .OrderByDescending(x => x.TotalCopies)
                .ToList();


            var sb = new StringBuilder();

            foreach (var author in authors)
            {
                sb.AppendLine($"{author.FirstName} {author.LastName} - {author.TotalCopies}");
            }

            return sb.ToString();
        }
    }
}
