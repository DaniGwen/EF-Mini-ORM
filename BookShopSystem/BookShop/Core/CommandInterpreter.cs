using BookShop.Models;
using BookShop_AgeRestriction_Added.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BookShop_AgeRestriction_Added.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private const string suffix = "Command";

        public string Read(string[] args, BookShopContext context)
        {
            string command = args[0];
            string[] commandArgs = args.Skip(1).ToArray();

            var type = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == command + suffix);

            if (type == null)
            {
                throw new ArgumentNullException("No such command!");
            }

            var typeInstance = Activator.CreateInstance(type, context);

            var result = ((ICommand)typeInstance).Execute(commandArgs);

            return result;
        }
    }
}
