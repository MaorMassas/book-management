using System;
using System.Collections.Generic;
using System.Text;

namespace targil_3
{
    class Book
    {
        protected int ID;
        protected string _BookName;
        protected string _AuthurName;
        protected string _Genre;
        public Book(int id, string BookName, string AuthurName, string Genre)
        {
            ID = id;
            _BookName = BookName;
            _AuthurName = AuthurName;
            _Genre = Genre;
        }
        public void SetId(int id)
        {
            ID = id;
        }
        public int getID()
        {
            return ID;
        }
        public void SetBookName(string BookName)
        {
            _BookName = BookName;
        }
        public string getBookName()
        {
            return _BookName;
        }
        public void SetAuthurName(string AuthurName)
        {
            _AuthurName = AuthurName;
        }
        public string getAuthurName()
        {
            return _AuthurName;
        }
        public void SetGenre(string Genre)
        {
            _Genre = Genre;
        }
        public string getGenre()
        {
            return _Genre;
        }
        public virtual void printinfo()
        {
            Console.WriteLine($"Book Name : {_BookName} Author Name: {_AuthurName} Genre: {_Genre}");
        }
    }
}
