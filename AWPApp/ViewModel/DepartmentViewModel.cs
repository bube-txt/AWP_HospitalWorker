using AWPApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWPApp.ViewModel
{
    class DepartmentViewModel
    {
        static Core db = new Core();
        public static int GetDepartmentIdByName(string name)
        {
            List<Department> departments = db.context.Department.Where(x => x.DepartmentName.Contains(name)).ToList();
            int matches = departments.Count();

            Department department = new Department();
            if (matches > 0)
            {
                department = departments.FirstOrDefault();
                return department.DepartmentId;
            }
            return -1;
        }
    }
}
