using System;
using System.Data;
using System.Data.SqlClient;

namespace targil_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder conn_str = new SqlConnectionStringBuilder();
            conn_str.ConnectionString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";

            SqlConnection sqlConnection = new SqlConnection(conn_str.ConnectionString);
            String str_check = @"DROP DATABASE IF EXISTS MAOR";
            SqlCommand myCommand_check = new SqlCommand(str_check, sqlConnection);
            String str = @"CREATE DATABASE MAOR";
            SqlCommand myCommand = new SqlCommand(str, sqlConnection);
            try
            {
                sqlConnection.Open();
                myCommand_check.ExecuteNonQuery();
                myCommand.ExecuteNonQuery();
                Console.WriteLine("DataBase is Created Successfully");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            string connection = @"Server=localhost\SQLEXPRESS;Database=MAOR;Trusted_Connection=True;";
            SqlConnection con = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd.CommandText = "DROP TABLE IF EXISTS Books";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE Books(
                Id_Book VARCHAR(5) NOT NULL PRIMARY KEY,
                Book_Name VARCHAR(50) NOT NULL,
                Authur_Name VARCHAR(30) NOT NULL,
                Genre VARCHAR(30) NOT NULL,
                Type VARCHAR(30) NOT NULL,
                Coppies INT NULL
            )";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Books(Id_Book,book_Name,Authur_Name,Genre,Type,Coppies) VALUES(11,'Harry Potter','JK Rolink','Fantasy','digital',NULL)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Books(Id_Book,book_Name,Authur_Name,Genre,Type,Coppies) VALUES(22,'Lord of Ring','J. R. R. Tolkien','Fantasy','digital',NULL)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Books(Id_Book,book_Name,Authur_Name,Genre,Type,Coppies) VALUES(33,'A Song of Ice and Fire','George R. R. Martin','dantasy','Digital',NULL)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Books(Id_Book,book_Name,Authur_Name,Genre,Type,Coppies) VALUES(44,'Nineteen Eighty-Four','George Orwell','Fiction','digital',NULL)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Books(Id_Book,book_Name,Authur_Name,Genre,Type,coppies) VALUES(1,'Fifty Shades of Grey','E.L James','Romance','physical', 1)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Books(Id_Book,book_Name,Authur_Name,Genre,Type,coppies) VALUES(2,'The Girl On The Train','Paula Hawkins','Thriller','physical', 3)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Books(Id_Book,book_Name,Authur_Name,Genre,Type,coppies) VALUES(3,'Where The Crawdads Sing','Delia Owen','Romance','physical', 1)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Books(Id_Book,book_Name,Authur_Name,Genre,Type,coppies) VALUES(4,'The Help','Kathryn Stocket','The Help','physical', 4)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Books(Id_Book,book_Name,Authur_Name,Genre,Type,coppies) VALUES(5,'Gone Girl','Gillian Flynn','Thriller','physical', 2)";
            cmd.ExecuteNonQuery();

            Console.WriteLine("Table Books created");
            try
            {
                Console.WriteLine("I entered the try clause");

                cmd.CommandText = "INSERT INTO Books(Id_Book,book_Name,Authur_Name,Genre,Type,Coppies) VALUES(55,'Alpha Force','Chris Ryan','Spy','digital',NULL)";
                Console.WriteLine("Maor! The book was added successfully.");
            }
            catch
            {
                Console.WriteLine("Oops! An error occured while trying to add a book. Try again another time.");
            }
            cmd.ExecuteNonQuery();

            cmd.CommandText = "DROP TABLE IF EXISTS Subscriber";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE Subscriber(
                Id_Subscriber VARCHAR(9) NOT NULL PRIMARY KEY,
                First_Name VARCHAR(30) NOT NULL,
                Last_Name VARCHAR(30) NOT NULL,
                Loan_Book_1 VARCHAR(5) NULL FOREIGN KEY REFERENCES Books(Id_Book),
                Loan_Book_2 VARCHAR(5) NULL FOREIGN KEY REFERENCES Books(Id_Book),
                Loan_Book_3 VARCHAR(5) NULL FOREIGN KEY REFERENCES Books(Id_Book)
            )";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Subscriber(Id_Subscriber,First_Name,Last_Name,Loan_Book_1,Loan_Book_2,Loan_Book_3) VALUES('314801887','Shimon','Biton',1,NULL,NULL)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Subscriber(Id_Subscriber,First_Name,Last_Name,Loan_Book_1,Loan_Book_2,Loan_Book_3) VALUES('321654789','Yoram','Cohen',22,NULL,NULL)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Subscriber(Id_Subscriber,First_Name,Last_Name,Loan_Book_1,Loan_Book_2,Loan_Book_3) VALUES('026595321','David','Levi',3,NULL,NULL)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Subscriber(Id_Subscriber,First_Name,Last_Name,Loan_Book_1,Loan_Book_2,Loan_Book_3) VALUES('315900766','Tamir','Hadad',44,NULL,NULL)";
            cmd.ExecuteNonQuery();

            Console.WriteLine("Table Subscriber created");
            try
            {
                Console.WriteLine("I entered the try clause");

                cmd.CommandText = "INSERT INTO Subscriber(Id_Subscriber,First_Name,Last_Name,Loan_Book_1,Loan_Book_2,Loan_Book_3) VALUES('912573435','Hatem','Elmushnino',5,NULL,NULL)";
                Console.WriteLine("Maor! The book was added successfully.");
            }
            catch
            {
                Console.WriteLine("Oops! An error occured while trying to add a subscriber. Try again another time.");
            }
            cmd.ExecuteNonQuery();
            con.Close();
            Library l = new Library();
            while (true)
            {
                Console.WriteLine("Hello!!\n what order would you want to do?");
                Console.WriteLine("1.Inserting a new book into the library database:");
                Console.WriteLine("2.Adding a new subscription to the library:");
                Console.WriteLine("3.Lending a book by subscription according to id:");
                Console.WriteLine("4.Lending a book by subscription according to book name:");
                Console.WriteLine("5.Returning a book by a subscriber:");
                Console.WriteLine("6.Printing book details:");
                Console.WriteLine("7.Printing book details that belong to the subscriber:");
                Console.WriteLine("8.Printing books associated with the genre:");
                Console.WriteLine("9.exit");
                string str1 = Console.ReadLine();
                if (int.TryParse(str1, out int choise))
                {
                    if (choise < 1 || choise > 9)
                    {
                        Console.WriteLine("Invalid input. Please enter a number between 1 and 9.");
                        continue;
                    }
                }
                else { Console.WriteLine("The input must to be a number"); continue; }
                LibraryMethod method = (LibraryMethod)(choise - 1);
                if (method == LibraryMethod.Exit)
                {
                    break;
                }
                l.CallMethod(method);
            }
        }
    }
}
