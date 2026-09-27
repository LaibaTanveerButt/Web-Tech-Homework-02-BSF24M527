using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace LibraryData
{
    public class BookRepository
    {
        private string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=My Database;Trusted_Connection=True;TrustServerCertificate=True";

        public void AddBook(Book book)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string query = "Insert into Books(BookId,Title,Author,Stock) Values(@BookId,@Title,@Author,@Stock)";
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlParameter bookidparameter = new SqlParameter("@BookId", book.BookId);
            SqlParameter booktitleParameter = new SqlParameter("@Title", book.Title);
            SqlParameter bookauthroParameter = new SqlParameter("@Author", book.Author);
            SqlParameter bookstockParameter = new SqlParameter("@Stock", book.Stock);

            cmd.Parameters.Add(bookidparameter);
            cmd.Parameters.Add(booktitleParameter);
            cmd.Parameters.Add(bookauthroParameter);
            cmd.Parameters.Add(bookstockParameter);


            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                Console.WriteLine("Book added successfully...");
            }
            else
            {
                Console.WriteLine("Invalid credentials: book not added successfully");
            }
        }

        public List<Book> GetAllBooks()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string query = "Select * from Books";
            SqlCommand cmd = new SqlCommand(query, conn);

            List<Book> new_list = new List<Book>();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Book book = new Book();

                book.BookId = Convert.ToInt32(dr[0]);
                book.Title = dr[1].ToString();
                book.Author = dr[2].ToString();
                book.Stock = Convert.ToInt32(dr[3]);

                new_list.Add(book);
            }

            conn.Close();
            return new_list;

        }

        public int UpdateBooksStock(int bookid)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string query = "Select Stock From Books where BookId=@bookid";
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlParameter bookidparameter = new SqlParameter("@bookid", bookid);

            cmd.Parameters.Add(bookidparameter);

            object r = cmd.ExecuteScalar();
            if (r != null)
            {
                int result = Convert.ToInt32(r);
                return result;
            }
            return 0;

        }

        public void DeleteBook(int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string query = "Delete from Books where BookId=@id";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlParameter idparameter = new SqlParameter("@id", id);
            cmd.Parameters.Add(idparameter);

            int i = cmd.ExecuteNonQuery();

            if (i > 0)
            {
                Console.WriteLine("Book deleted successfully....");
            }
            else
            {
                Console.WriteLine("Invalid id: Book is not deletd...");
            }

        }
    }
}
