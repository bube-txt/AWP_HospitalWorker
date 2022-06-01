using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWPApp.Model
{
    public partial class Users
    {
        Core db = new Core();
        public Worker GetWorker
        {
            get
            {
                return db.context.Worker.Where(x => x.WorkerUserId == UserId).FirstOrDefault();
            }
        }
        public string GetSpecial
        {
            get
            {
                return db.context.Worker.Where(x => x.WorkerUserId == UserId).FirstOrDefault().Special.SpecialName;
            }
        }
        public int GetSpecialId
        {
            get
            {
                return db.context.Worker.Where(x => x.WorkerUserId == UserId).FirstOrDefault().Special.SpecialId;
            }
        }
        public string GetWorkerFullName
        {
            get
            {
                return db.context.Worker.Where(x => x.WorkerUserId == UserId).FirstOrDefault().FullName;
            }
        }
    }
}
