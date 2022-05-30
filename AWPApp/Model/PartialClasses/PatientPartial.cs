using AWPLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWPApp.Model
{
    public partial class Patient
    {
        public string FullName
        {
            get
            {
                return PatientLastName + " " + PatientFirstName + " " + PatientPatronicName;
            }
        }
        public string GetDoctorName
        {
            get
            {
                if (Ticket.Where(x => x.TicketPatientId == PatientId).OrderBy(x => x.TicketDate).FirstOrDefault() is null)
                {
                    return "";
                }
                else
                {
                    return Ticket.Where(x => x.TicketPatientId == PatientId).OrderBy(x => x.TicketDate).FirstOrDefault().Worker.FullName;
                }                
            }
        }
        public int GetDoctorId
        {
            get
            {
                if (Ticket.Where(x => x.TicketPatientId == PatientId).OrderBy(x => x.TicketDate).FirstOrDefault() is null)
                {
                    return -1;
                }
                else
                {
                    return Ticket.Where(x => x.TicketPatientId == PatientId).OrderBy(x => x.TicketDate).FirstOrDefault().Worker.WorkerId;
                }                
            }
        }
        public string FullNameAndBirthday
        {
            get
            {
                return PatientLastName + " " + PatientFirstName + " " + PatientPatronicName + " (" + PatientFormatedDate + ")";
            }
        }
        public string PatientFormatedDate
        {
            get
            {
                return ConvertationClass.GetFormatedDate(PatientDate);
            }
        }
        public string PatientFormatedTime
        {
            get
            {
                return ConvertationClass.GetFormatedTime(PatientDate);
            }
        }
        public string PatientFormatedDateTime
        {
            get
            {
                return ConvertationClass.GetFormatedDateTime(PatientDate);
            }
        }
        public string PatientGetRoomNumberAndDoctorFullName
        {
            get
            {
                Ticket getTicket = Ticket.Where(x => x.Patient.PatientId == PatientId).FirstOrDefault();
                return getTicket.Room.RoomNumber + " (" + getTicket.Worker.FullName + ")";
            }
        }
    }
}
