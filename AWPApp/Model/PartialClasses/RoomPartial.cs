using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWPApp.Model
{
    partial class Room
    {
        public string GetRoomNumberAndDepartmentName
        {
            get
            {
                return RoomNumber + " (" + Department.DepartmentName + ")";
            }
        }
    }
}
