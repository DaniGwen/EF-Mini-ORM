using BookShop.Models;
using BookShop_GoldenBooks_Added.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop_GoldenBooks_Added.Core.Commands
{
    public class NotReleasedInCommand : ICommand
    {
        private readonly BookShopContext context;

        public NotReleasedInCommand(BookShopContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            var year = int.Parse(args[0]);

            return GetBooksNotReleasedIn(context, year);
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Year != year)
                .OrderBy(b=>b.BookId)
                .ToList();

            var sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString();
        }
    }
}
