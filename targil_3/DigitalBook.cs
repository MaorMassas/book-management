using System;
using System.Collections.Generic;
using System.Text;

namespace targil_3
{
    class DigitalBook : Book
    {
        public DigitalBook(int id, string BookName, string AuthurName, string Genre) : base(id, BookName, AuthurName, Genre)
        {

        }
        public override void printinfo()
        {
            Console.WriteLine($"Book Name : {_BookName} Author Name: {_AuthurName} Genre: {_Genre} is digital book");
        }
    }
}
