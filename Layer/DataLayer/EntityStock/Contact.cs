using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EntityStock
{
    public class Contact
    {
        public int ContactId { get; set; }
      
        public string type { get; set; }
     
        public string Name { get; set;}
        public string Address { get; set;}  
        public string Phone { get; set;} 
        public string Description { get; set;}

        public ICollection<Transaction> Transactions { get; set; }
    }

}
