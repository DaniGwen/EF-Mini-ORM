using BookShop.Data;
using BookShop_GoldenBooks_Added.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.Core.Commands
{
    public class BookSearchByAuthorCommand : ICommand
    {
        private readonly BookShopContext context;

        public BookSearchByAuthorCommand(BookShopContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            var input = args[0].ToLower();

            return GetBooksByAuthor(context, input);
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(book => book.Author.LastName.StartsWith(input))
                .Select(x => new
                {
                    x.BookId,
                    x.Title,
                    x.Author.FirstName,
                    x.Author.LastName
                })
                .OrderBy(book => book.BookId)
                .ToList();

            var sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.FirstName} {book.LastName})");
            }

            return sb.ToString();
        }
    }
}
