using BookShop.Data;
using BookShop.Models;
using BookShop_GoldenBooks_Added.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace p5_BookShop__BookTitlesByCategory.Core.Commands
{
    public class BooksByCategoryCommand : ICommand
    {
        private readonly BookShopContext context;

        public BooksByCategoryCommand(BookShopContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            return GetBooksByCategory(context, args);
        }

        public static string GetBooksByCategory(BookShopContext context, string[] input)
        {
            var sb = new StringBuilder();
            var listOfBooks = new List<string>();

            foreach (var category in input)
            {
                category.ToLower();

                var books = context.Books
                   .Where(b => b.BookCategories.Any(c => c.Category.Name.ToLower() == category))
                   .Select(b => b.Title)
                   .ToList();

                listOfBooks.AddRange(books);
            }

            foreach (var book in listOfBooks.OrderBy(b => b))
            {
                sb.AppendLine(book);
            }

            return sb.ToString();
        }
    }
}
