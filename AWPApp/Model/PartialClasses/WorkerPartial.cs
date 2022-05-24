using AWPLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AWPApp.Model
{
    public partial class Worker
    {
        public string FullName
        {
            get
            {
                return WorkerLastName + " " + WorkerFirstName + " " + WorkerPatronicName;
            }
        }
        public string WorkerJobName
        {
            get
            {
                return Job.JobName;
            }
        }
        public string WorkerStartTime
        {
            get
            {
                return ConvertationClass.GetFormatedTimeSpan((TimeSpan)WorkerScheduleStart);
            }
        }
        public string WorkerEndTime
        {
            get
            {
                return ConvertationClass.GetFormatedTimeSpan((TimeSpan)WorkerScheduleEnd);
            }
        }

    }
}
