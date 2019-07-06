using BookShop.Data;
using BookShop_GoldenBooks_Added.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BookShop.Core.Commands
{
    public class GetBooksReleasedBeforeCommand : ICommand
    {
        private readonly BookShopContext context;

        public GetBooksReleasedBeforeCommand(BookShopContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            string date = args[0];
            return GetBooksReleasedBefore(context, date);
        }

        private static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var dateTime = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books
                .Where(b => b.ReleaseDate < dateTime)
                .OrderByDescending(b=>b.ReleaseDate)
                .ToList();

            var sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - {book.Price}");
            }
            
            return sb.ToString();
        }
    }
}
