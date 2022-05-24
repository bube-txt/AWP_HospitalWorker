using AWPLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWPApp.Model
{
    public partial class Ticket
    {
        public string TicketFormatedDate
        {
            get
            {
                return ConvertationClass.GetFormatedDate(TicketDate);
            }
        }
    }
}
