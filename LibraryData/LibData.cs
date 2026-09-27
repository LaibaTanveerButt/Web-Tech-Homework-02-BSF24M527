using Microsoft.Data.SqlClient;
using System.Net;

namespace LibraryData
{
    public class LibData
    {
        private string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=My Database;Trusted_Connection=True;TrustServerCertificate=True";

        public string IssueBook(int bookId, int memberId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            BookRepository book = new BookRepository();
            int n=book.UpdateBooksStock(bookId);

            
            SqlTransaction tra = conn.BeginTransaction();
           

            string q1 = "Insert into IssuedBooks(BookId,MemberId) Values(@bookId,@memberId)";
            SqlCommand cmd1 = new SqlCommand(q1, conn, tra);

            SqlParameter idparameter = new SqlParameter("@bookId", bookId);
            cmd1.Parameters.Add(idparameter);
            SqlParameter memberidparameter = new SqlParameter("@memberId", memberId);
            cmd1.Parameters.Add(memberidparameter);


            string q2 = "UPDATE Books SET Stock = Stock - 1 WHERE BookId = @bookId AND Stock > 0";
            SqlCommand cmd2 = new SqlCommand(q2, conn,tra);

            SqlParameter idbookparameter = new SqlParameter("@bookId", bookId);
            cmd2.Parameters.Add(idbookparameter);


            if (n != 0)
            {
                try
                {
                    int i = cmd1.ExecuteNonQuery();
                    int j = cmd2.ExecuteNonQuery();

                    int k = i + j;
                    if(k>0)
                    {
                        tra.Commit();
                        conn.Close();
                        return "Transaction successfull";
                    }

                    
                }
                catch (Exception ex)
                {
                    tra.Rollback();
                    conn.Close();
                    Console.WriteLine("SQL Error Details: " + ex.Message); // <-- ADD THIS LINE
                    return "Transaction not successfull";
                }

            }
            conn.Close();
                return "Stock not available.....";
        }

        public string ReturnBook(int issueId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string query = "Select ReturnDate from IssuedBooks where IssuedId=@issuedId";

            SqlCommand cmd = new SqlCommand(query,conn);

            SqlParameter issueidparameter = new SqlParameter("@issuedId", issueId);
            cmd.Parameters.Add(issueidparameter);

            object r= cmd.ExecuteScalar();

            SqlTransaction tra = conn.BeginTransaction();

            string q1 = "Update IssuedBooks set ReturnDate=GetDate() where IssuedId=@issuedId";
            SqlCommand cmd1 = new SqlCommand(q1, conn, tra);
            SqlParameter issidparameter = new SqlParameter("@issuedId",issueId);
            cmd1.Parameters.Add(issidparameter);

            SqlDataReader dr= cmd1.ExecuteReader();
            int bookId = 0;
            while(dr.Read())
            {
                object returnDateObj = dr["ReturnDate"];
                bookId = Convert.ToInt32(dr["BookId"]);
            }
            conn.Close();

            conn.Open();


            string q2 = "Update Books set Stock=Stock+1 where BookId=@bookId";
            SqlCommand cmd2 = new SqlCommand(q2, conn, tra);
            SqlParameter bookidparameter = new SqlParameter("@bookId", bookId);
            cmd2.Parameters.Add(bookidparameter);



            if (r != null)
            {
                try
                {
                    int i = cmd1.ExecuteNonQuery();
                    int j = cmd2.ExecuteNonQuery();

                    if(i+j >0)
                    {
                        tra.Commit();
                        conn.Close();
                        return "Transaction successfull...";
                    }
                   
                }
                
                    catch (Exception ex)
                {
                    tra.Rollback();
                    conn.Close();
                    Console.WriteLine("SQL Error Details: " + ex.Message); 
                    return "Transaction not successfull";
                }

            }

            conn.Close();
            return "Book already returned....";
            
        }
    }
}
