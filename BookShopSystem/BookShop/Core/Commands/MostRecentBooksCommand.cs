using BookShop.Data;
using BookShop_GoldenBooks_Added.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.Core.Commands
{
    public class MostRecentBooksCommand : ICommand
    {
        private readonly BookShopContext context;

        public MostRecentBooksCommand(BookShopContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            return GetMostRecentBooks(context);
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categories = context.Categories
                .OrderBy(c => c.Name)
                .Select(x => new
                {
                    CategorName = x.Name,
                    books = x.CategoryBooks.OrderByDescending(c => c.Book.ReleaseDate)
                    .Select(y => new
                    {
                        BookTitle = y.Book.Title,
                        BookYear = y.Book.ReleaseDate.Year
                    })
                    .Take(3)
                })
            .ToList();

            var sb = new StringBuilder();

            foreach (var category in categories)
            {
                sb.AppendLine($"--{category.CategorName}");

                foreach (var book in category.books)
                {
                    sb.AppendLine($"{book.BookTitle} ({book.BookYear})");
                }   
            }
            return sb.ToString();
        }
    }
}
