using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EntityStock
{
    public class ApplicationActivity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
