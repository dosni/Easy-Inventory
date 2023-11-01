using DataLayer.EntityStock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.TransactionServices
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        [Required(ErrorMessage = "Transaction Type is required")]
        public string TransactionType { get; set; }
        [Required(ErrorMessage = "Date is required")]

        public DateTime TransactionDate { get; set; }
        public int? ContactId { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public bool IsPosted { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
