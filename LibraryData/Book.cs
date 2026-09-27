using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData
{
    public class Book
    {
        public int BookId { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int Stock { get; set; }

        public Book()
        {

        }
        public Book(int id,string title,string author,int stock)
        {
            this.BookId = id;
            this.Title = title;
            this.Author = author;
            this.Stock = stock;
        }

    }
}
