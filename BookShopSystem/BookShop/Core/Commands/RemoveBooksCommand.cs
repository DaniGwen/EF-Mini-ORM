using BookShop.Data;
using BookShop_GoldenBooks_Added.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.Core.Commands
{
    public class RemoveBooksCommand : ICommand
    {
        private readonly BookShopContext context;

        public RemoveBooksCommand(BookShopContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            return RemoveBooks(context).ToString();
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 4200)
                .ToList();

            int counter = 0;

            foreach (var book in books)
            {
                context.Remove(book);

                counter += 1;
            }

            context.SaveChanges();

            return counter;
        }
    }
}
