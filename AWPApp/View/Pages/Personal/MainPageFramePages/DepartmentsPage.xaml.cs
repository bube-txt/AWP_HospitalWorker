using AWPApp.Model;
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

namespace AWPApp.View.Pages.Doctor.mpf
{
    /// <summary>
    /// Логика взаимодействия для DepartmentPage.xaml
    /// </summary>
    public partial class DepartmentsPage : Page
    {
        #region [ объявление переменных ]
        Core db = new Core();
        List<Department> departmentArray;
        List<Department> delRoomArray = new List<Department>();
        Department editDepartment = new Department();
        // bool doctorNameChecked = false;

        #endregion
        public DepartmentsPage()
        {
            InitializeComponent();

            // таблица
            departmentArray = db.context.Department.ToList();
            DepartmentsTable.ItemsSource = departmentArray;
        }

        #region [ события ]

        #region Поиск

        /// <summary>
        /// Лайв обновление таблицы при изменении
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxSearchFilterDepartmentName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        /// <summary>
        /// Очистка активных фильтров поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClearFilter_Click(object sender, RoutedEventArgs e)
        {
            TextBoxSearchFilterDepartmentName.Text = "";

            UpdateTable();
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
        private void ButtonFilterClear_Click(object sender, RoutedEventArgs e)
        {
            TextBoxAddDepartmentName.Text = "";

            UpdateTable();
        }

        #endregion

        #region Изменение

        /// <summary>
        /// Изменить выбранное отделение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Button ButtonClicked = (Button)sender;
            editDepartment = ButtonClicked.DataContext as Department;

            TabControlDepartment.SelectedIndex = 2;

            EditGet();
        }

        /// <summary>
        /// Подтвердить изменения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEditConfirm_Click(object sender, RoutedEventArgs e)
        {
            EditDepartmentConfirm();
        }

        #endregion

        #endregion

        #region [ классы ]

        #region Поиск

        /// <summary>
        /// Просмотр отделений
        /// </summary>
        private void Search()
        {
            departmentArray = db.context.Department.ToList();

            string departmentNameFilter = TextBoxSearchFilterDepartmentName.Text;
            string departmentNameCased = "";
            bool departmentNameChecked = false;

            // Существует ли номер комнаты
            if (departmentNameFilter != "")
            {
                departmentNameCased = departmentNameFilter.Trim();
                departmentNameChecked = true;
            }

            // Фильтры поиска
            if (departmentNameChecked)
            {
                departmentArray = departmentArray.Where(x => x.DepartmentName.ToLower().Contains(TextBoxSearchFilterDepartmentName.Text.ToLower())).ToList();
            }

            // Вывод
            DepartmentsTable.ItemsSource = departmentArray;
        }

        #endregion

        #region Добавление

        /// <summary>
        /// Добавить отделение
        /// </summary>
        private void Add()
        {

            string departmentNameFilter = TextBoxAddDepartmentName.Text;
            string departmentNameCased = "";
            bool departmentNameChecked = false;

            // Существует ли название комнаты
            if (departmentNameFilter != "")
            {
                departmentNameCased = departmentNameFilter.Trim();
                departmentNameChecked = true;
            }


            if (departmentNameChecked)
            {

                Department newDepartment = new Department();

                newDepartment = new Department
                {
                    DepartmentName = departmentNameCased
                };

                // добавляем комнату в БД
                db.context.Department.Add(newDepartment);
                db.context.SaveChanges();

                // обновление таблицы
                UpdateTable();
            }

        }

        #endregion

        #region Удаление

        /*
        /// <summary>
        /// Удаление отделений
        /// </summary>
        private void Delete()
        {
            int count = delRoomArray.Count;
            if (count > 0)
            {
                // удаление выбранных отделений
                foreach (Department department in delRoomArray)
                {
                    db.context.Department.Remove(department);
                }
                db.context.SaveChanges();
            }

            UpdateTable();
        }
        */

        #endregion

        #region Изменение

        /// <summary>
        /// Редактировать отделение
        /// </summary>
        private void EditGet()
        {
            TextBoxEditDepartmentName.Text = editDepartment.DepartmentName + "";
        }

        /// <summary>
        /// Подтверждение об изменении получено
        /// </summary>
        private void EditDepartmentConfirm()
        {
            Department editedDepartment = new Department()
            {
                DepartmentName = TextBoxEditDepartmentName.Text.Trim()
            };

            Department selectedDepartment = db.context.Department.Where(x => x.DepartmentId == editDepartment.DepartmentId).FirstOrDefault();

            selectedDepartment.DepartmentName = editedDepartment.DepartmentName;

            db.context.SaveChanges();


            UpdateTable();
        }

        /// <summary>
        /// Очистка полей изменений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEditClear_Click(object sender, RoutedEventArgs e)
        {
            TextBoxEditDepartmentName.Text = "";
        }

        #endregion

        #region Общее

        /// <summary>
        /// Обновить таблицу
        /// </summary>
        private void UpdateTable()
        {
            departmentArray = db.context.Department.ToList();
            DepartmentsTable.ItemsSource = departmentArray;
        }

        #endregion

        #endregion

    }
}
