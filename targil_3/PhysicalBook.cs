using System;
using System.Collections.Generic;
using System.Text;

namespace targil_3
{
    class PhysicalBook : Book
    {
        protected int _coppies;
        public PhysicalBook(int id, string BookName, string AuthurName, string Genre, int coppies) : base(id, BookName, AuthurName, Genre)
        {
            _coppies = coppies;
        }
        public void setCoppies(int coppies)
        {
            _coppies = coppies;
        }
        public int getcoppies()
        {
            return _coppies;
        }
        public override void printinfo()
        {
            Console.WriteLine($"Book Name : {_BookName} Author Name: {_AuthurName} Genre: {_Genre}  coppies: {_coppies} is physical book");
        }
    }
}

