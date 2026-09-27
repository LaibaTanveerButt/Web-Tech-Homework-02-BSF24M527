using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData
{
    public class MemberRepository
    {
        private string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=My Database;Trusted_Connection=True;TrustServerCertificate=True";

        public void AddMember(Member member)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string query = "Insert into Members(MemberId,Name) Values(@MemberId,@Name)";
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlParameter memberidparameter = new SqlParameter("@MemberId", member.MemberId);
            SqlParameter memberNameParameter = new SqlParameter("@Name",member.Name);

            cmd.Parameters.Add(memberidparameter);
            cmd.Parameters.Add(memberNameParameter);
            

            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                Console.WriteLine("Member added successfully....");
            }
            else
            {
                Console.WriteLine("Invalid information:Member not added ");
            }
            conn.Close();

        }

        public List<Member>GetAllMembers()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string query = "Select * from Members";
            SqlCommand cmd = new SqlCommand(query, conn);

            List<Member> new_list = new List<Member>();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Member member = new Member();

                member.MemberId = Convert.ToInt32(dr[0]);
                member.Name = dr[1].ToString();
               
                new_list.Add(member);
            }

            conn.Close();
            return new_list;

        }
    }
}
