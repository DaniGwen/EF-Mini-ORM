using BookShop.Data;
using BookShop_GoldenBooks_Added.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.Core.Commands
{
    public class IncreasePricesCommand : ICommand
    {
        private readonly BookShopContext context;

        public IncreasePricesCommand(BookShopContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            IncreasePrices(context);
            return "";
        }

        public static void IncreasePrices(BookShopContext context)
        {
            int releaseYear = 2010;

            var books = context.Books
                .Where(b => b.ReleaseDate.Year < releaseYear)
                .ToList();

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }
    }
}
