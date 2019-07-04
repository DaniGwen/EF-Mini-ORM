using BookShop.Models;
using BookShop_GoldenBooks_Added.Core.Contracts;
using System.Linq;
using System.Text;

namespace BookShop_GetBookByPrice.Core.Commands
{
    public class GetBooksByPriceCommand : ICommand
    {
        private readonly BookShopContext context;

        public GetBooksByPriceCommand(BookShopContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            return GetBooksByPrice(context);
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Price > 40)
                .ToList();

            var sb = new StringBuilder();

            foreach (var book in books.OrderByDescending(b => b.Price))
            {
                sb.AppendLine($"{book.Title} - {book.Price}");
            }
            return sb.ToString();
        }
    }
}
