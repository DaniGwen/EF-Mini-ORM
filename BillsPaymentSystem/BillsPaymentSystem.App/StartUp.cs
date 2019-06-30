using BillsPaymentSystem.App.Core;
using BillsPaymentSystem.App.Core.Contracts;
using BillsPaymentSystem.Data;
using System;
using System.Data.SqlClient;

namespace BillsPaymentSystem.App
{
    class StartUp
    {
        static void Main()
        {
            ICommandInterpreter commandInterpreter = new CommandInterpreter();

            IEngine engine = new Engine(commandInterpreter);
            engine.Run();

            //using (BillsPaymentSystemContext context = new BillsPaymentSystemContext())
            //{
            //    DbInitializer.Seed(context);
            //}

            //using (BillsPaymentSystemContext context = new BillsPaymentSystemContext())
            //{
            //    using (SqlConnection connection = new SqlConnection(ConnectionConfig.ConnectionString))
            //    {
            //        connection.Open();

            //        PrintUsersFromDB(connection);

            //        Console.WriteLine();

            //        PrintCrediCardsFromDB(connection);

            //        Console.WriteLine();

            //        PrintBankAccountsFromDB(connection);
            //    }
            //}
        }

        private static void PrintBankAccountsFromDB(SqlConnection connection)
        {
            string commandString = "SELECT * FROM BankAccounts";
            SqlCommand command = new SqlCommand(commandString, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{int.Parse(reader[0].ToString())} {reader[1].ToString()} {reader[2].ToString()}  ");
                }
            }
        }

        private static void PrintCrediCardsFromDB(SqlConnection connection)
        {
            string commandString = "SELECT * FROM CreditCards";
            SqlCommand command = new SqlCommand(commandString, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{int.Parse(reader[0].ToString())} {reader[1].ToString()} {reader[2].ToString()}  ");
                }
            }
        }

        private static void PrintUsersFromDB(SqlConnection connection)
        {
            string commandString = "SELECT * FROM Users";
            SqlCommand command = new SqlCommand(commandString, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var userId = int.Parse(reader[0].ToString());
                    var email = reader[1].ToString();
                    var firstName = reader[2].ToString();
                    var lastName = reader[3].ToString();
                    var password = reader[4].ToString();

                    Console.WriteLine($"{userId} {email} {firstName} {lastName} {password}");
                }
            }
        }
    }
}
