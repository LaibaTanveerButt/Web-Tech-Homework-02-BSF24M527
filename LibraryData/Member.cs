using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData
{
    public class Member
    {
        public int MemberId { get; set; }
        public string ?Name { get; set; }


        public Member()
        {

        }
        public Member(int id,string name)
        {
            this.MemberId = id;
            this.Name = name;
        }

    }
}
