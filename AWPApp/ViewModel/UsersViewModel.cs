using AWPApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AWPApp.ViewModel
{
    public class UsersViewModel
    {
        static Core db = new Core();
        /// <summary>
        /// Проверка введёных данных пользователя с данными из БД
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>Верно или нет</returns>
        public static bool AuthUser(string login, string password) {

            int matches = db.context.Users.Where(x => x.Login == login && x.Password == password).Count();

            if (matches > 0)
            {
                App.ActiveUser = db.context.Users.Where(x => x.Login == login).FirstOrDefault();
                Users user = App.ActiveUser;
                App.ActiveWorker = db.context.Worker.Where(x => x.WorkerUserId == user.UserId).FirstOrDefault();

                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
