using BookShop.Data;
using BookShop_GoldenBooks_Added.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.Core.Commands
{
    public class BookSearchCommand : ICommand
    {
        private readonly BookShopContext context;

        public BookSearchCommand(BookShopContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            var input = args[0].ToLower();

            return GetBookTitlesContaining(context, input);
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(book => book.Title.Contains(input))
                .Select(book => book.Title)
                .OrderBy(book => book)
                .ToList();

            var sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine(book);
            }

            return sb.ToString();
        }
    }
}
