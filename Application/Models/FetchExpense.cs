using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class FetchExpense
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid UserId { get; set; }
        public string ExpenseType { get; set; }
    }
}
