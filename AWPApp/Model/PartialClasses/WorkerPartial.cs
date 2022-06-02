using AWPApp.Assets.Access;
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
        public bool MedicalPersonal
        {
            get
            {
                if ((int)Roles.Doctor <= Job.JobAccessLevel)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
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
                return ConvertationClass.GetFormatedTime((TimeSpan)WorkerScheduleStart);
            }
        }
        public string WorkerEndTime
        {
            get
            {
                return ConvertationClass.GetFormatedTime((TimeSpan)WorkerScheduleEnd);
            }
        }

        public bool GetTempertureListAccess
        {
            get
            {
                if (Job.JobAccessLevel == (int)Roles.System)
                {
                    return true;
                }
                else if (Job.JobAccessLevel == (int)Roles.MainDoctor)
                {
                    return true;
                }
                else 
                if (Job.JobAccessLevel == (int)Roles.Doctor)
                {
                    return true;
                }
                else if (Job.JobAccessLevel == (int)Roles.Administrator)
                {
                    return false;
                }
                else if (Job.JobAccessLevel == (int)Roles.Archivist)
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool GetDailyMapAccess
        {
            get
            {
                if (Job.JobAccessLevel == (int)Roles.System)
                {
                    return true;
                }
                else if (Job.JobAccessLevel == (int)Roles.MainDoctor)
                {
                    return true;
                }
                else 
                if (Job.JobAccessLevel == (int)Roles.Doctor)
                {
                    return true;
                }
                else if (Job.JobAccessLevel == (int)Roles.Administrator)
                {
                    return false;
                }
                else if (Job.JobAccessLevel == (int)Roles.Archivist)
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool GetEditAccess
        {
            get
            {
                if (Job.JobAccessLevel == (int)Roles.System)
                {
                    return true;
                }
                else if (Job.JobAccessLevel == (int)Roles.MainDoctor)
                {
                    return true;
                }
                else 
                if (Job.JobAccessLevel == (int)Roles.Doctor)
                {
                    return true;
                }
                else if (Job.JobAccessLevel == (int)Roles.Administrator)
                {
                    return false;
                }
                else if (Job.JobAccessLevel == (int)Roles.Archivist)
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool GetSendToArchiveAccess
        {
            get
            {
                if (Job.JobAccessLevel == (int)Roles.System)
                {
                    return true;
                }
                else if (Job.JobAccessLevel == (int)Roles.MainDoctor)
                {
                    return false;
                }
                else 
                if (Job.JobAccessLevel == (int)Roles.Doctor)
                {
                    return false;
                }
                else if (Job.JobAccessLevel == (int)Roles.Administrator)
                {
                    return false;
                }
                else if (Job.JobAccessLevel == (int)Roles.Archivist)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
