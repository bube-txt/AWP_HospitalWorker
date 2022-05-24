using AWPLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AWPApp.Model
{
    partial class TicketHistory
    {
        public string TicketFormatedDate
        {
            get
            {
                if (ConvertationClass.GetFormatedDate(TicketHistoryDate) != null && ConvertationClass.GetFormatedDate(TicketHistoryDate) != "0001-01-01")
                {
                    return ConvertationClass.GetFormatedDate(TicketHistoryDate);
                }
                else
                {
                    return "";
                }
            }
        }
        public string TicketFormatedFinishDate
        {
            get
            {
                if (ConvertationClass.GetFormatedDate((DateTime)TicketHistoryFinishDate) != null && ConvertationClass.GetFormatedDate((DateTime)TicketHistoryFinishDate) != "0001-01-01")
                {
                    return ConvertationClass.GetFormatedDate((DateTime)TicketHistoryFinishDate);
                }
                else
                {
                    return "";
                }
            }
        }
        public string TicketStatusActiveOrWaiting
        {
            get
            {
                if (Status.StatusId == "P")
                {
                    if (TicketHistoryDate.Date > DateTime.Now)
                    {
                        return "Ожидание";
                    }
                    else
                    {
                        return "В процессе";
                    }
                }
                else
                {
                    return Status.StatusName;
                }
            }
        }
    }
}
