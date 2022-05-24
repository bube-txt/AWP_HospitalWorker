using AWPApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWPApp.ViewModel
{
    class DoctorsViewModel
    {
        static Core db = new Core();
        public static string getName(int id)
        {
            int matches = db.context.Worker.Where(x => x.WorkerId == id).Count();
            if (matches < 0)
            {
                return "";
            }
            string firstName = db.context.Worker.Where(x => x.WorkerId == id).FirstOrDefault().WorkerFirstName;
            string lastName = db.context.Worker.Where(x => x.WorkerId == id).FirstOrDefault().WorkerLastName;
            return firstName + " " + lastName;
        }
        public static int getId(string fullName)
        {
            string[] name = fullName.Trim().Split(' ');
            if (name.Length == 1 | name.Length == 2)
            {
                return -1;
            }
            int matches = 0;
            int choice = 0;
            if (!string.IsNullOrEmpty(name[0]) & !string.IsNullOrEmpty(name[1]))
            {
                matches = db.context.Worker.Where(x => x.WorkerFirstName == name[0] & x.WorkerLastName == name[1]).Count();

                choice = 1;
                if (matches < 1)
                {
                    choice = 2;
                    matches = db.context.Worker.Where(x => x.WorkerFirstName == name[1] & x.WorkerLastName == name[0]).Count();
                }
            }
            else if (!string.IsNullOrEmpty(name[0]))
            {
                matches = db.context.Worker.Where(x => x.WorkerFirstName == name[0]).Count();

                choice = 3;
                if (matches < 1)
                {
                    choice = 4;
                    matches = db.context.Worker.Where(x => x.WorkerLastName == name[0]).Count();
                }
            }

            if (choice == 1)
            {
                return Convert.ToInt32(db.context.Worker.Where(x => x.WorkerFirstName == name[0] & x.WorkerLastName == name[1]).FirstOrDefault());
            }
            else if (choice == 2)
            {
                return Convert.ToInt32(db.context.Worker.Where(x => x.WorkerFirstName == name[1] & x.WorkerLastName == name[0]).FirstOrDefault());
            }
            else if (choice == 3)
            {
                return Convert.ToInt32(db.context.Worker.Where(x => x.WorkerFirstName == name[0]).FirstOrDefault());
            }
            else if (choice == 4)
            {
                return Convert.ToInt32(db.context.Worker.Where(x => x.WorkerLastName == name[0]).FirstOrDefault());
            }
            else
            {
                return -1;
            }

        }
    }
}
