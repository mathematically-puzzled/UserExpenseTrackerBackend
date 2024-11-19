using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Expenses
    {
        public int Id { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }
        public string ExpenseType { get; set; }
        public DateTime ExpenseDate { get; set; }

        public User User { get; set; }
    }
}
