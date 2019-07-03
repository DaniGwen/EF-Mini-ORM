using BookShop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop_AgeRestriction_Added.Core.Contracts
{
    public interface ICommandInterpreter
    {
        string Read(string[] args, BookShopContext context);
    }
}
