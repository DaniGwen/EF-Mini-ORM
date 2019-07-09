using BookShop.Data;
using BookShop_GoldenBooks_Added.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Core.Commands
{
    public class ProfitByCategoryCommand : ICommand
    {
        private readonly BookShopContext context;

        public ProfitByCategoryCommand(BookShopContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            return GetTotalProfitByCategory(context);
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categories = context.Categories
                .Select(x => new
                {
                    CategoryName = x.Name,
                    TotalProfit = x.CategoryBooks.Sum(s => s.Book.Price * s.Book.Copies)
                }) 
                .OrderByDescending(x=>x.TotalProfit)
                .ThenBy(x=>x.CategoryName)
                .ToList();

            var sb = new StringBuilder();

            foreach (var category in categories)
            {
                sb.AppendLine($"{category.CategoryName} ${category.TotalProfit:F2}");
            }

            return sb.ToString();
        }
    }
}
