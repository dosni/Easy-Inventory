using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EntityStock
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }      
        public int? ContactId { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public bool IsPosted { get; set; }
        public DateTime CreatedAt { get; set; }

        //public ProductSku ProductSku { get; set; }
        //public Store Store { get; set; }
        public Contact Contact { get; set; }
        public ICollection<TransactionLine> LineTransactions { get; set; }

    }
}
