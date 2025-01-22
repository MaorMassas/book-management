using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;

namespace targil_3
{
    enum LibraryMethod
    {
        AddBook,
        AddSub,
        TakeBookById,
        TakeBookByName,
        ReturnBook,
        PrintBook,
        PrintBookBySubscriber,
        PrintGenre,
        Exit
    }

    class Library
    {
        public Library() { }
        public void CallMethod(LibraryMethod method)
        {
            var cs = @"Server=localhost\SQLEXPRESS;Database=MAOR;Trusted_Connection=True;";
            SqlConnection con = new SqlConnection(cs);
            switch (method)
            {
                case LibraryMethod.AddBook:
                    con.Open();
                    AddBook(con);
                    con.Close();
                    break;
                case LibraryMethod.AddSub:
                    con.Open();
                    AddSub(con);
                    con.Close();
                    break;
                case LibraryMethod.TakeBookById:
                    con.Open();
                    TakeBookById(con);
                    con.Close();
                    break;
                case LibraryMethod.TakeBookByName:
                    con.Open();
                    TakeBookByName(con);
                    con.Close();
                    break;
                case LibraryMethod.ReturnBook:
                    con.Open();
                    ReturnBook(con);
                    con.Close();
                    break;
                case LibraryMethod.PrintBook:
                    con.Open();
                    PrintBook(con);
                    con.Close();
                    break;
                case LibraryMethod.PrintBookBySubscriber:
                    con.Open();
                    PrintBookBySubscriber(con);
                    con.Close();
                    break;
                case LibraryMethod.PrintGenre:
                    con.Open();
                    PrintGenre(con);
                    con.Close();
                    break;
                case LibraryMethod.Exit:
                    break;
            }
        }
        public void AddBook(SqlConnection con)
        {
            while (true)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                Console.WriteLine("please enter name book , auothur name and ganre(enter after each and don't forget a capital first letter )");
                string name = Console.ReadLine();
                string auothur = Console.ReadLine();
                string ganre = Console.ReadLine();
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(auothur) || string.IsNullOrEmpty(ganre))
                {
                    Console.WriteLine("your input was empty");
                    continue;
                }
                else if (int.TryParse(auothur, out int result_a) || int.TryParse(ganre, out int result_b))
                {
                    Console.WriteLine("The author and the ganre can not contain numbers!");
                    continue;
                }
                Console.WriteLine("what the type of the book?(digital/physical)");
                string type = Console.ReadLine().ToLower();
                if (type != "digital" && type != "physical")
                {
                    Console.WriteLine("wrong input! please enter digital or physical");
                    continue;
                }
                Console.WriteLine("please enter the id of the book");
                string str = Console.ReadLine();
                if (str.Length > 5)
                {
                    Console.WriteLine("book id need to contain at most 5 numbers");
                    continue;
                }
                if (int.TryParse(str, out int id))
                {
                    try
                    {
                        Console.WriteLine("I entered the try clause");
                        cmd.CommandText = "SELECT * FROM Books WHERE Id_Book = '" + id + "'";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            //Checks if records in the table have been read
                            if (reader.HasRows)
                            {
                                reader.Close();
                                cmd.CommandText = "SELECT Type FROM Books WHERE Id_Book = '" + id + "'";
                                var typecheck = cmd.ExecuteScalar().ToString();
                                if (typecheck == "physical")
                                {
                                    cmd.CommandText = "UPDATE Books SET Coppies = Coppies + 1 WHERE Id_Book = '" + id + "'";
                                    cmd.ExecuteNonQuery();
                                    Console.WriteLine("copies add!");
                                    break;
                                }
                                Console.WriteLine("Book already exists!");
                                break;
                            }
                        }
                        if (type == "digital")
                        {
                            cmd.CommandText = "INSERT INTO Books(Id_Book, book_Name, Authur_Name, Genre, Type, Coppies) " +
                                "VALUES('" + id + "','" + name + "','" + auothur + "','" + ganre + "','digital',NULL)";
                        }
                        else
                        {
                            cmd.CommandText = "INSERT INTO Books(Id_Book, book_Name, Authur_Name, Genre, Type, Coppies) " +
                                "VALUES('" + id + "','" + name + "','" + auothur + "','" + ganre + "','physical',1)";
                        }
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("The book was added successfully");
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred while trying to add the book. Please try again later.");
                        Console.WriteLine("Error details: " + ex.Message);
                    }
                }
                else { Console.WriteLine("the ID must to be numbers"); continue; }
            }
        }

        public void AddSub(SqlConnection con)
        {
            while (true)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                Console.WriteLine("Please enter the first name and family name");
                string FName = Console.ReadLine();
                string LName = Console.ReadLine();
                if (string.IsNullOrEmpty(FName) || string.IsNullOrEmpty(LName))
                {
                    Console.WriteLine("Your input was empty");
                    continue;
                }
                else if (int.TryParse(FName, out int a) || int.TryParse(LName, out int b))
                {
                    Console.WriteLine("The names cannot contain numbers!");
                    continue;
                }
                Console.WriteLine("Please enter the ID of the subscriber");
                string str = Console.ReadLine();
                if (str.Length != 9)
                {
                    Console.WriteLine("The subscriber ID needs to contain 9 numbers");
                    continue;
                }
                if (int.TryParse(str, out int id))
                {
                    try
                    {
                        Console.WriteLine("I entered the try clause");
                        //This query checks if the ID exists in the table
                        cmd.CommandText = "SELECT COUNT(*) FROM Subscriber WHERE Id_Subscriber = '" + id + "'";
                        int count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            Console.WriteLine("Subscriber already exists");
                            break;
                        }
                        cmd.CommandText = "INSERT INTO Subscriber(Id_Subscriber, First_Name, Last_Name, Loan_Book_1, Loan_Book_2, Loan_Book_3) VALUES('" + id + "','" + FName + "','" + LName + "',NULL,NULL,NULL)";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Subscriber was added successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Oops! An error occurred while trying to add a subscriber. Try again another time.");
                        Console.WriteLine("Error details: " + ex.Message);
                        continue;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("The ID must be numbers");
                    continue;
                }
            }
        }
        public void TakeBookById(SqlConnection con)
        {
            while (true)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                Console.WriteLine("please enter the subscriber ID");
                string str_sub = Console.ReadLine();
                if (string.IsNullOrEmpty(str_sub))
                {
                    Console.WriteLine("the input was empty");
                    continue;
                }
                if (str_sub.Length != 9)
                {
                    Console.WriteLine("subscriber id need to contain 9 numbers");
                    continue;
                }
                if (int.TryParse(str_sub, out int id_sub))
                {
                    Console.WriteLine("please enter the book ID");
                    string str_book = Console.ReadLine();
                    if (string.IsNullOrEmpty(str_book))
                    {
                        Console.WriteLine("the input was empty");
                        continue;
                    }
                    if (str_book.Length > 5)
                    {
                        Console.WriteLine("book id need to contain at most 5 numbers");
                        continue;
                    }
                    if (int.TryParse(str_book, out int id_book))
                    {
                        try
                        {
                            Console.WriteLine("I entered the try clause");
                            cmd.CommandText = "SELECT * FROM Subscriber WHERE Id_Subscriber = '" + id_sub + "'";
                            var subscriber = cmd.ExecuteScalar();
                            if (subscriber == null)
                            {
                                Console.WriteLine("Subscriber does not exist.");
                                continue;
                            }
                            cmd.CommandText = "SELECT * FROM Books WHERE Id_Book = '" + id_book + "'";
                            var book = cmd.ExecuteScalar();
                            if (book == null)
                            {
                                Console.WriteLine("book does not exist.");
                                continue;
                            }
                            cmd.CommandText = "SELECT Type FROM Books WHERE Id_Book = '" + id_book + "'";
                            var type = cmd.ExecuteScalar().ToString();
                            int cell = 1;
                            bool findcell = false;
                            while (findcell == false)
                            {
                                cmd.CommandText = "SELECT Loan_Book_" + cell + " FROM Subscriber WHERE Id_Subscriber = '" + id_sub + "'";
                                var loanBook = cmd.ExecuteScalar();
                                //check if the box is empty.
                                if (loanBook == DBNull.Value)
                                {
                                    if (type == "physical")
                                    {
                                        findcell = true;
                                        cmd.CommandText = "UPDATE Subscriber SET Loan_Book_" + cell + " = '" + id_book + "' WHERE Id_Subscriber = '" + id_sub + "'";
                                        cmd.ExecuteNonQuery();
                                        Console.WriteLine("Maor! The book was added successfully.");
                                        cmd.CommandText = "SELECT Coppies FROM Books WHERE Id_Book = '" + id_book + "'";
                                        var copies = int.Parse(cmd.ExecuteScalar().ToString());
                                        if (copies > 0)
                                        {
                                            cmd.CommandText = "UPDATE Books SET Coppies = Coppies - 1 WHERE Id_Book = '" + id_book + "'";
                                            cmd.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            Console.WriteLine("No copies available");
                                            break;
                                        }
                                    }
                                    else if (type == "digital")
                                    {
                                        findcell = true;
                                        cmd.CommandText = "UPDATE Subscriber SET Loan_Book_" + cell + " = '" + id_book + "' WHERE Id_Subscriber = '" + id_sub + "'";
                                        cmd.ExecuteNonQuery();
                                        Console.WriteLine("Maor! The book was added successfully.");
                                    }
                                    
                                }
                                cell++;
                                if (cell > 3)
                                {
                                    Console.WriteLine("you have now three book! subscriber reached limit");
                                    break;
                                }
                            }
                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine("An error occurred while trying to update the database. Please try again.");
                            Console.WriteLine("Error details: " + ex.Message);
                        }
                        break;
                    }
                    else { Console.WriteLine("the ID must to be numbers"); continue; }
                }
                else { Console.WriteLine("the ID must to be numbers"); continue; }
            }

        }
        public void TakeBookByName(SqlConnection con)
        {
            while (true)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                Console.WriteLine("please enter the subscriber ID");
                string str_sub = Console.ReadLine();
                if (string.IsNullOrEmpty(str_sub))
                {
                    Console.WriteLine("the input was empty");
                    continue;
                }
                if (str_sub.Length != 9)
                {
                    Console.WriteLine("subscriber id needs to contain 9 numbers");
                    continue;
                }
                if (int.TryParse(str_sub, out int id_sub))
                {
                    Console.WriteLine("please enter the book name");
                    string book_name = Console.ReadLine();
                    if (string.IsNullOrEmpty(book_name))
                    {
                        Console.WriteLine("the input was empty");
                        continue;
                    }
                    try
                    { 
                        Console.WriteLine("I entered the try clause");
                        cmd.CommandText = "SELECT * FROM Subscriber WHERE Id_Subscriber = '" + id_sub + "'";
                        var subscriber = cmd.ExecuteScalar();
                        if (subscriber == null)
                        {
                            Console.WriteLine("Subscriber does not exist.");
                            continue;
                        }
                        cmd.CommandText = "SELECT Id_Book, Book_Name,Authur_Name,Genre, Type,Coppies FROM Books WHERE Book_Name='" + book_name + "'";
                        //Inserting all the selected books into a list
                        SqlDataReader reader = cmd.ExecuteReader();
                        List<Book> matchingBooks = new List<Book>();
                        while (reader.Read())
                        {
                            string s_id = reader.GetString(0);
                            int id = int.Parse(s_id);
                            string name = reader.GetString(1);
                            string authur = reader.GetString(2);
                            string ganre = reader.GetString(3);
                            string isDigital = reader.GetString(4);
                            int coppies = 0;
                            if (!Convert.IsDBNull(reader[5]))
                            {
                                coppies = Convert.ToInt32(reader[5]);
                            }

                            if (isDigital == "digital")
                            {
                                matchingBooks.Add(new DigitalBook(id, name, authur, ganre));
                            }
                            else
                            {
                                matchingBooks.Add(new PhysicalBook(id, name, authur, ganre, coppies));
                            }
                        }
                        reader.Close();

                        if (matchingBooks.Count == 0)
                        {
                            Console.WriteLine("book does not exist");
                            continue;
                        }
                        //Showing all the books in the list to the user and choosing a book
                        Console.WriteLine("Please select a book by entering its number:");
                        for (int i = 0; i < matchingBooks.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}."); matchingBooks[i].printinfo();
                        }
                        string selection = Console.ReadLine();
                        if (int.TryParse(selection, out int index) && index > 0 && index <= matchingBooks.Count)
                        {
                            Book b = matchingBooks[index - 1];
                            int book_id = b.getID();
                            cmd.CommandText = "SELECT Type FROM Books WHERE Id_Book = '" + book_id + "'";
                            var type = cmd.ExecuteScalar().ToString();
                            int cell = 1;
                            bool findCell = false;
                            while (!findCell)
                            {
                                cmd.CommandText = "SELECT Loan_Book_" + cell + " FROM Subscriber WHERE Id_Subscriber = '" + id_sub + "'";
                                var loanBook = cmd.ExecuteScalar();
                                if (loanBook == DBNull.Value)
                                {
                                    if (type == "physical")
                                    {
                                        findCell = true;
                                        cmd.CommandText = "UPDATE Subscriber SET Loan_Book_" + cell + " = '" + book_id + "' WHERE Id_Subscriber = '" + id_sub + "'";
                                        cmd.ExecuteNonQuery();
                                        Console.WriteLine("Maor! The book was added successfully.");
                                        cmd.CommandText = "SELECT Coppies FROM Books WHERE Id_Book = '" + book_id + "'";
                                        var copies = int.Parse(cmd.ExecuteScalar().ToString());
                                        if (copies > 0)
                                        {
                                            cmd.CommandText = "UPDATE Books SET Coppies = Coppies - 1 WHERE Id_Book = '" + book_id + "'";
                                            cmd.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            Console.WriteLine("No copies available");
                                            break;
                                        }
                                    }
                                    else if (type == "digital")
                                    {
                                        findCell = true;
                                        cmd.CommandText = "UPDATE Subscriber SET Loan_Book_" + cell + " = '" + book_id + "' WHERE Id_Subscriber = '" + id_sub + "'";
                                        cmd.ExecuteNonQuery();
                                        Console.WriteLine("Maor! The book was added successfully.");
                                        break;
                                    }

                                }
                                cell++;
                                if (cell > 3)
                                {
                                    Console.WriteLine("subscriber reached limit");
                                    break;
                                }
                            }
                        }
                        else { Console.WriteLine("Must contain numbers and be greater than 0!"); continue; }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Oops! An error occured while trying to add a book. Try again another time.");
                        Console.WriteLine("Error details: " + ex.Message);
                        continue;
                    }
                    break;
                }
                else { Console.WriteLine("the ID must to be numbers"); continue; }
            }

        }
        public void ReturnBook(SqlConnection con)
        {
            while (true)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                Console.WriteLine("please enter the subscriber ID");
                string str_sub = Console.ReadLine();
                if (string.IsNullOrEmpty(str_sub))
                {
                    Console.WriteLine("the input was empty");
                    continue;
                }
                if (str_sub.Length != 9)
                {
                    Console.WriteLine("subscriber id need to contain 9 numbers");
                    continue;
                }
                if (int.TryParse(str_sub, out int id_sub))
                {
                    //Checking by reading from the database if the subscription exists
                    cmd.CommandText = "SELECT * FROM Subscriber WHERE Id_Subscriber = '" + id_sub + "'";
                    SqlDataReader reader = cmd.ExecuteReader();
                    //hash checks whether the call returned a number greater than 0 or not
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("subscriber does not exist");
                        reader.Close();
                        continue;
                    }
                    reader.Close();
                    Console.WriteLine("please enter the book ID");
                    string str_book = Console.ReadLine();
                    if (string.IsNullOrEmpty(str_book))
                    {
                        Console.WriteLine("the input was empty");
                        continue;
                    }
                    if (str_book.Length > 5)
                    {
                        Console.WriteLine("book id need to contain at most 5 numbers");
                        continue;
                    }
                    if (int.TryParse(str_book, out int id_book))
                    {
                        try
                        {
                            Console.WriteLine("I entered the try clause");
                            cmd.CommandText = "SELECT * FROM Books WHERE Id_Book = '" + id_book + "'";
                            reader = cmd.ExecuteReader();
                            //hash checks whether the call returned a number greater than 0 or not
                            if (!reader.HasRows)
                            {
                                Console.WriteLine("book does not exist");
                                reader.Close();
                                continue;
                            }
                            reader.Close();
                            cmd.CommandText = "SELECT Type FROM Books WHERE Id_Book = '" + id_book + "'";
                            var type = cmd.ExecuteScalar().ToString();
                            int Cell = 1;
                            bool findcell = false;
                            while (Cell <= 3 && findcell == false)
                            {
                                cmd.CommandText = "SELECT Loan_Book_" + Cell + " FROM Subscriber WHERE Id_Subscriber = '" + id_sub + "'";
                                var loanBook = cmd.ExecuteScalar();
                                if (loanBook != DBNull.Value)
                                {
                                    if (int.Parse((string)loanBook) == id_book)
                                    {
                                        findcell = true;
                                        cmd.CommandText = "UPDATE Subscriber SET Loan_Book_" + Cell + " = NULL WHERE Id_Subscriber = '" + id_sub + "'";
                                        cmd.ExecuteNonQuery();
                                        Console.WriteLine("Maor! The book was returned successfully.");
                                        if (type == "physical")
                                        {
                                            cmd.CommandText = "UPDATE Books SET Coppies = Coppies + 1 WHERE Id_Book = '" + id_book + "'";
                                            cmd.ExecuteNonQuery();
                                        }
                                        break;
                                    }
                                }
                                Cell++;
                            }
                            if (!findcell)
                            {
                                Console.WriteLine("book does not exist");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Oops! An error occured while trying to add a book. Try again another time.");
                            Console.WriteLine("Error details: " + ex.Message);
                            continue;
                        }
                    }
                    else { Console.WriteLine("the ID must to be numbers"); continue; }
                }
                else { Console.WriteLine("the ID must to be numbers"); continue; }
                break;
            }
        }
        public void PrintBook(SqlConnection con)
        {
            while (true)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                Console.WriteLine("please enter the book id");
                string str = Console.ReadLine();
                if (string.IsNullOrEmpty(str))
                {
                    Console.WriteLine("the input was empty");
                    continue;
                }
                if (str.Length > 5)
                {
                    Console.WriteLine("book id need to contain at most 5 numbers");
                    continue;
                }
                if (int.TryParse(str, out int id_book))
                {
                    try
                    {
                        Console.WriteLine("I entered the try clause");
                        //Searching for the book, building an object with the values obtained from the database and activating the printing method of the book
                        cmd.CommandText = $"SELECT Id_Book, Book_Name,Authur_Name,Genre, Type ,Coppies FROM Books WHERE Id_Book='{id_book}'";
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string s_id = reader.GetString(0);
                            int id = int.Parse(s_id);
                            string name = reader.GetString(1);
                            string authur = reader.GetString(2);
                            string ganre = reader.GetString(3);
                            string isDigital = reader.GetString(4);
                            int coppies = 0;
                            //Checking if the cell is empty to know if it is physical or digital so that Ricker won't try to read a cell and destroy it
                            if (!Convert.IsDBNull(reader[5]))
                            {
                                coppies = Convert.ToInt32(reader[5]);
                            }

                            if (isDigital == "digital")
                            {
                                DigitalBook d = new DigitalBook(id, name, authur, ganre);
                                d.printinfo();
                                break;
                            }
                            else
                            {
                                PhysicalBook p = new PhysicalBook(id, name, authur, ganre, coppies);
                                p.printinfo();
                                break;
                            }
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Oops! An error occured while trying to add a book. Try again another time.");
                        Console.WriteLine("Error details: " + ex.Message);
                        continue;
                    }
                    break;
                }
                else { Console.WriteLine("the id must to be from numbers"); continue; }
            }
        }
        public void PrintBookBySubscriber(SqlConnection con)
        {
            while (true)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                Console.WriteLine("Please enter the subscriber id");
                string str = Console.ReadLine();
                if (string.IsNullOrEmpty(str))
                {
                    Console.WriteLine("The input was empty");
                    continue;
                }
                if (str.Length != 9)
                {
                    Console.WriteLine("The id must contain 9 digits");
                    continue;
                }
                List<int> myid = new List<int>();
                if (int.TryParse(str, out int id_sub))
                {
                    try
                    {
                        int transaction = 0;
                        for (int Cell = 1; Cell <= 3; Cell++)
                        {
                            cmd.CommandText = "SELECT Loan_Book_" + Cell + " FROM Subscriber WHERE Id_Subscriber = '" + id_sub + "'";
                            var loanBook = cmd.ExecuteScalar();
                            if (loanBook != DBNull.Value)
                            {
                                myid.Add(int.Parse((string)loanBook));
                                transaction++;
                            }
                        }
                        if (transaction == 0)
                        {
                            Console.WriteLine("There are no book in loan!");
                        }
                        foreach (int i in myid)
                        {
                            cmd.CommandText = $"SELECT Id_Book, Book_Name,Authur_Name,Genre, Type,Coppies FROM Books WHERE Id_Book='{i}'";
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                string s_id = reader.GetString(0);
                                int id = int.Parse(s_id);
                                string name = reader.GetString(1);
                                string authur = reader.GetString(2);
                                string ganre = reader.GetString(3);
                                string isDigital = reader.GetString(4);
                                int coppies = 0;
                                if (!Convert.IsDBNull(reader[5]))
                                {
                                    coppies = Convert.ToInt32(reader[5]);
                                }

                                if (isDigital == "digital")
                                {
                                    DigitalBook d = new DigitalBook(id, name, authur, ganre);
                                    d.printinfo();
                                }
                                else
                                {
                                    PhysicalBook p = new PhysicalBook(id, name, authur, ganre, coppies);
                                    p.printinfo();
                                }
                            }
                            reader.Close();
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Oops! An error occured while trying to add a book. Try again another time.");
                        Console.WriteLine("Error details: " + ex.Message);
                        continue;
                    }
                }
                else { Console.WriteLine("the id must be from numbers"); continue; }
            }
        }
        public void PrintGenre(SqlConnection con)
        {
            while (true)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                Console.WriteLine("please enter the the ganre");
                string ganre = Console.ReadLine();
                if (string.IsNullOrEmpty(ganre))
                {
                    Console.WriteLine("the input was empty");
                    continue;
                }
                try
                {
                    cmd.CommandText = $"SELECT Id_Book, Book_Name,Authur_Name,Genre, Type,Coppies FROM Books WHERE Genre='{ganre}'";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Book> my_books = new List<Book>();
                    while (reader.Read())
                    {
                        string s_id = reader.GetString(0);
                        int id = int.Parse(s_id);
                        string name = reader.GetString(1);
                        string authur = reader.GetString(2);
                        string Ganre = reader.GetString(3);
                        string isDigital = reader.GetString(4);
                        int coppies = 0;
                        if (!Convert.IsDBNull(reader[5]))
                        {
                            coppies = Convert.ToInt32(reader[5]);
                        }

                        if (isDigital == "digital")
                        {
                            my_books.Add(new DigitalBook(id, name, authur, Ganre));
                        }
                        else
                        {
                            my_books.Add(new PhysicalBook(id, name, authur, Ganre, coppies));
                        }
                    }
                    reader.Close();
                    string genreRegex = $"\\b{ganre}\\b";
                    foreach (var item in my_books)
                    {
                        if (Regex.IsMatch(item.getGenre(), genreRegex, RegexOptions.IgnoreCase))
                        {

                            item.printinfo();
                        }
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Oops! An error occured while trying to add a book. Try again another time.");
                    Console.WriteLine("Error details: " + ex.Message);
                    continue;
                }
            }
        }
        
    }
}

