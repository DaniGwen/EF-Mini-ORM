using BookShop.Models;
using BookShop_AgeRestriction_Added.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop_AgeRestriction_Added.Core
{
    public class Engine : IEngine
    {
        private readonly ICommandInterpreter commandInterpreter;

        public Engine(ICommandInterpreter commandInterpreter)
        {
            this.commandInterpreter = commandInterpreter;
        }

        public void Run()
        {
            while (true)
            {
                var inputArgs = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                using (BookShopContext context = new BookShopContext())
                {
                    var result = commandInterpreter.Read(inputArgs, context);

                    Console.WriteLine(result);
                }
            }
        }
    }
}
