using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Managment.Models
{
    public class ViewmoreModel
    {
        public System.DateTime issue_from { get; set; }
        public System.DateTime issue_to { get; set; }
        public string person_name { get; set; }
        public string book_name { get; set; }
        public Nullable<int> cr_id { get; set; }

        public virtual create_books create_books { get; set; }




    }
}