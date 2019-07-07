using BookShop.Data;
using BookShop_GoldenBooks_Added.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.Core.Commands
{
    public class CountBooksCommand : ICommand
    {
        private readonly BookShopContext context;

        public CountBooksCommand(BookShopContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            int titleLongerThan = int.Parse(args[0]);

            return CountBooks(context, titleLongerThan).ToString();
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            int books = context.Books
                .Where(book => book.Title.Length > lengthCheck)
                .Count();

            return books;
        }
    }
}
