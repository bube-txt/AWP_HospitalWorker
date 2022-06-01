using AWPApp.Model;
using AWPLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AWPApp.View.Pages.Personal.MainPageFramePages
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        Core db = new Core();
        List<Users> UsersArray;
        List<Users> delUsersArray = new List<Users>();
        CheckClass cc = new CheckClass();
        Users editUsers = new Users();
        Users selectedUsers = new Users();
        Users selectedUsersView = new Users();
        List<Worker> specials = new List<Worker>();
        public UsersPage()
        {
            InitializeComponent();

            Update();


            specials = db.context.Worker.Where(x => x.Special.SpecialName != "Система").ToList();

            ComboBoxSearchFilterWorker.ItemsSource = specials;
            ComboBoxSearchFilterWorker.SelectedValuePath = "WorkerId";
            ComboBoxSearchFilterWorker.DisplayMemberPath = "FullName";

            ComboBoxAddWorker.ItemsSource = specials;
            ComboBoxAddWorker.SelectedValuePath = "WorkerId";
            ComboBoxAddWorker.DisplayMemberPath = "FullName";

            ComboBoxEditWorker.ItemsSource = specials;
            ComboBoxEditWorker.SelectedValuePath = "WorkerId";
            ComboBoxEditWorker.DisplayMemberPath = "FullName";
        }
        public void Update()
        {
            UsersArray = db.context.Users.ToList();
            UsersTable.ItemsSource = UsersArray;
        }

        #region [ события ]

        #region { Пациент }

        #region Поиск

        /// <summary>
        /// Лайв обновление таблицы при изменении
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxSearchFilterFullName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        /// <summary>
        /// Очистка активных фильтров поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonFilterClear_Click(object sender, RoutedEventArgs e)
        {
            TextBoxSearchFilterLogin.Text = "";
            ComboBoxSearchFilterWorker.SelectedIndex = -1;

            Update();
        }

        #endregion

        #region Добавление

        /// <summary>
        /// Добавление отделений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Add();
        }

        /// <summary>
        /// Очистка активных фильтров добавления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAddClear_Click(object sender, RoutedEventArgs e)
        {
            TextBoxAddLogin.Text = "";
            TextBoxAddPassword.Text = "";
            ComboBoxAddWorker.SelectedIndex = -1;

            Update();
        }

        #endregion

        #region Удаление

        /// <summary>
        /// Удаление палат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        /// <summary>
        /// Добавление в список на удаление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkedBox = (CheckBox)sender;
            Users newUsers = checkedBox.DataContext as Users;

            delUsersArray.Add(newUsers);
        }

        /// <summary>
        /// Удаление из списка на удаление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkedBox = (CheckBox)sender;
            Users newUsers = checkedBox.DataContext as Users;

            delUsersArray.Remove(newUsers);
        }


        #endregion

        #region Изменение

        /// <summary>
        /// Редактировать выбранное отделение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Button ButtonClicked = (Button)sender;
            editUsers = ButtonClicked.DataContext as Users;

            TabControlUsers.SelectedIndex = 2;
            EditGet();
        }

        /// <summary>
        /// Подтвердить изменения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEditConfirm_Click(object sender, RoutedEventArgs e)
        {
            EditPatientConfirm();
        }

        #endregion

        #endregion

        #endregion [ события ]

        #region [ классы ]

        #region { Пациент }

        #region Поиск



        /// <summary>
        /// Просмотр отделений
        /// </summary>
        private void Search()
        {
            UsersArray = db.context.Users.ToList();
            UsersTable.ItemsSource = UsersArray;

            string loginFilter = TextBoxSearchFilterLogin.Text;
            string loginCased = "";
            bool loginChecked = false;

            int specialFilter = ComboBoxSearchFilterWorker.SelectedValue == null ? -1 : (int)ComboBoxSearchFilterWorker.SelectedValue;
            bool specialChecked = false;

            // Существует ли имя
            if (loginFilter != "")
            {
                loginCased = loginFilter.ToLower().Trim();
                loginChecked = true;
            }

            // Выбрана ли специальность
            if (specialFilter > 0)
            {
                specialChecked = true;
            }



            // Фильтры поиска
            if (loginChecked)
            {
                UsersArray = UsersArray.Where(x => x.Login.ToLower().Contains(loginCased)).ToList();
            }
            if (specialChecked)
            {
                UsersArray = UsersArray.Where(x => x.GetSpecialId == specialFilter).ToList();
            }

            // Вывод
            UsersTable.ItemsSource = UsersArray;
        }

        #endregion Поиск

        #region Добавление

        /// <summary>
        /// Добавить отделение
        /// </summary>
        private void Add()
        {
            Update();

            string firstNameFilter = TextBoxAddLogin.Text;
            string firstNameCased = "";
            bool firstNameChecked = false;

            string lastNameFilter = TextBoxAddPassword.Text;
            string lastNameCased = "";
            bool lastNameChecked = false;

            var specialFilter = ComboBoxAddWorker.SelectedValue;
            bool specialChecked = false;

            // Существует ли имя
            if (firstNameFilter != "")
            {
                firstNameCased = firstNameFilter.Trim();
                firstNameChecked = true;
            }

            // Существует ли фамилия
            if (lastNameFilter != "")
            {
                lastNameCased = lastNameFilter.Trim();
                lastNameChecked = true;
            }

            // Выбрана ли специальность
            if (!(specialFilter is null))
            {
                specialChecked = true;
            }



            if (firstNameChecked &&
                lastNameChecked &&
                specialChecked
                )
            {

                Users newUsers = new Users
                {
                    Login = firstNameCased,
                    Password = lastNameCased,
                    UserJobId = db.context.Worker.Where(x => x.WorkerId == (int)ComboBoxAddWorker.SelectedValue).FirstOrDefault().WorkerJobId,
                };

                // добавляем комнату в БД
                db.context.Users.Add(newUsers);
                db.context.SaveChanges();

                newUsers = db.context.Users.Where(x => x.Login == firstNameCased && x.Password == lastNameCased).FirstOrDefault();

                db.context.Worker.Where(x => x.WorkerId == (int)specialFilter).FirstOrDefault().WorkerUserId = newUsers.UserId;

                db.context.SaveChanges();

                // обновление таблицы
                Update();
            }
        }

        #endregion Добавление

        #region Удаление

        /// <summary>
        /// Удаление отделений
        /// </summary>
        private void Delete()
        {
            // Добавить столбик с CheckBox`ами
            // Добавить проверки дат в коде через DateCheck()
            int count = delUsersArray.Count;
            if (count > 0)
            {
                // удаление выбранных пациентов
                foreach (Users Users in delUsersArray)
                {
                    db.context.Users.Remove(Users);
                    try
                    {
                        db.context.Worker.Where(x => x.WorkerId == Users.GetWorker.WorkerId).FirstOrDefault().WorkerUserId = null;
                    }
                    catch{}
                }
                db.context.SaveChanges();
            }
            delUsersArray.Clear();
            Update();
        }

        #endregion Удаление

        #region Изменение

        /// <summary>
        /// Редактировать отделение
        /// </summary>
        private void EditGet()
        {
            TextBoxEditLogin.Text = editUsers.Login;

            TextBoxEditPassword.Text = editUsers.Password;

            ComboBoxEditWorker.SelectedValue = editUsers.GetWorker.WorkerId;
        }

        /// <summary>
        /// Подтверждение об изменении получено
        /// </summary>
        private void EditPatientConfirm()
        {
            if (ComboBoxEditWorker.SelectedValue is null)
            {
                return;
            }
            Users editedUsers = new Users()
            {
                Login = TextBoxEditLogin.Text,
                Password = TextBoxEditPassword.Text,
                UserJobId = editUsers.GetWorker.WorkerJobId
            };

            Users selectedUsers = db.context.Users.Where(x => x.UserId == editUsers.UserId).FirstOrDefault();

            selectedUsers.Login = editedUsers.Login;
            selectedUsers.Password = editedUsers.Password;
            selectedUsers.UserJobId = editedUsers.UserJobId;

            db.context.Worker.Where(x => x.WorkerId == (int)ComboBoxEditWorker.SelectedValue).FirstOrDefault().WorkerUserId = selectedUsers.UserId;

            db.context.SaveChanges();


            Update();

            
        }

        /// <summary>
        /// Очистка полей изменений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEditClear_Click(object sender, RoutedEventArgs e)
        {
            TextBoxEditLogin.Text = "";
            TextBoxEditPassword.Text = "";
            ComboBoxEditWorker.SelectedIndex = -1;
        }

        #endregion Изменение

        #region Общее

        #endregion Общее

        #endregion { Пациент }

        #endregion [ классы ]

        private void TextBoxSearchFilterTimeStart_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void TextBoxSearchFilterTimeEnd_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void ComboBoxSearchFilterSpecial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

    }

}

