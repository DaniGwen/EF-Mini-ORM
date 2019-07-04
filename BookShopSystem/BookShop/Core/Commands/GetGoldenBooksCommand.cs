using BookShop.Models;
using BookShop_GoldenBooks_Added.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Models.Enums;

namespace BookShop_GoldenBooks_Added.Core.Commands
{
    public class GetGoldenBooksCommand : ICommand
    {
        private readonly BookShopContext context;

        public GetGoldenBooksCommand(BookShopContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            return GetGoldenBooks(context);
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var goldenBooks = context.Books
                .Where(b => b.EditionType == EditionType.Gold)
                .Where(b => b.Copies < 5000)
                .ToList();

            var listGoldenBooks = new List<Book>();

            var sb = new StringBuilder();

            foreach (var book in goldenBooks.OrderBy(b => b.BookId))
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString();
        }
    }
}
