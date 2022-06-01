using AWPApp.Model;
using AWPApp.ViewModel;
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
using AWPLibrary.Classes;
using System.Data.SqlClient;
using AWPApp.Assets.Access;

namespace AWPApp.View.Pages.Doctor.mpf
{
    /// <summary>
    /// Логика взаимодействия для RoomPage.xaml
    /// </summary>
    public partial class RoomsPage : Page
    {
        #region [ объявление переменных ]
        Core db = new Core();
        List<Room> roomsArray;
        List<Room> delRoomArray = new List<Room>();
        Room editRoom = new Room();
        bool doctorNameChecked = false;

        #endregion

        public RoomsPage()
        {
            InitializeComponent();

            // таблица
            roomsArray = db.context.Room.ToList();
            RoomsTable.ItemsSource = roomsArray;

            // поиск
            ComboBoxDoctorName.ItemsSource = db.context.Worker.Where(x => x.Job.JobAccessLevel != (int)Roles.System).ToList();
            ComboBoxDoctorName.DisplayMemberPath = "FullName";
            ComboBoxDoctorName.SelectedValuePath = "WorkerId";

            ComboBoxDepartmentName.ItemsSource = db.context.Department.ToList();
            ComboBoxDepartmentName.DisplayMemberPath = "DepartmentName";
            ComboBoxDepartmentName.SelectedValuePath = "DepartmentId";

            // добавление
            ComboBoxDepartmentNameAdd.ItemsSource = db.context.Department.ToList();
            ComboBoxDepartmentNameAdd.DisplayMemberPath = "DepartmentName";
            ComboBoxDepartmentNameAdd.SelectedValuePath = "DepartmentId";

            ComboBoxDoctorNameAdd.ItemsSource = db.context.Worker.Where(x => x.Job.JobAccessLevel != (int)Roles.System).ToList();
            ComboBoxDoctorNameAdd.DisplayMemberPath = "FullName";
            ComboBoxDoctorNameAdd.SelectedValuePath = "WorkerId";

            // редактирование
            ComboBoxDepartmentNameEdit.ItemsSource = db.context.Department.ToList();
            ComboBoxDepartmentNameEdit.DisplayMemberPath = "DepartmentName";
            ComboBoxDepartmentNameEdit.SelectedValuePath = "DepartmentId";

            ComboBoxDoctorNameEdit.ItemsSource = db.context.Worker.Where(x => x.Job.JobAccessLevel != (int)Roles.System).ToList();
            ComboBoxDoctorNameEdit.DisplayMemberPath = "FullName";
            ComboBoxDoctorNameEdit.SelectedValuePath = "WorkerId";


            Search();
        }

        #region [ события ]

        #region Поиск

        /// <summary>
        /// Лайв обновление таблицы при изменении
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxDepartmentName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        /// <summary>
        /// Лайв обновление таблицы при изменении
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxDoctorName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        /// <summary>
        /// Лайв обновление таблицы при изменении
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxSearchFilterRoomNumber_TextChanged(object sender, TextChangedEventArgs e)
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
            TextBoxSearchFilterRoomNumber.Text = "";
            ComboBoxDepartmentName.SelectedIndex = -1;
            ComboBoxDoctorName.SelectedIndex = -1;

            UpdateTable();
        }

        #endregion

        #region Добавление

        /// <summary>
        /// Добавление палат
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
            TextBoxAddRoomNumber.Text = "";
            ComboBoxDepartmentNameAdd.SelectedIndex = -1;
            ComboBoxDoctorNameAdd.SelectedIndex = -1;

            UpdateTable();
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
            Room newRoom = checkedBox.DataContext as Room;

            delRoomArray.Add(newRoom);
        }

        /// <summary>
        /// Удаление из списка на удаление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkedBox = (CheckBox)sender;
            Room newRoom = checkedBox.DataContext as Room;

            delRoomArray.Remove(newRoom);
        }


        #endregion

        #region Изменение

        /// <summary>
        /// Редактировать выбранную палату
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Button ButtonClicked = (Button)sender;
            editRoom = ButtonClicked.DataContext as Room;

            TabControlRooms.SelectedIndex = 2;

            EditGet();
        }

        /// <summary>
        /// Подтвердить изменения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEditConfirm_Click(object sender, RoutedEventArgs e)
        {
            EditRoomConfirm();
        }

        #endregion

        #endregion

        #region [ классы ]

        #region Поиск

        /// <summary>
        /// Просмотр палат
        /// </summary>
        private void Search(){
            roomsArray = db.context.Room.ToList();

            string roomNumberFilter = TextBoxSearchFilterRoomNumber.Text;
            int roomNumberCased = -1;
            bool roomNumberChecked = false;

            string departmentNameFilter = ComboBoxDepartmentName.Text;
            bool departmentChecked = false;

            string doctorNameFilter = ComboBoxDoctorName.Text;




            // Существует ли номер комнаты
            if (roomNumberFilter != "")
            {
                Int32.TryParse(roomNumberFilter, out roomNumberCased);
                if (Int32.TryParse(roomNumberFilter, out roomNumberCased))
                {
                    roomNumberChecked = true;
                }
            }

            // Существует ли отделение
            if (departmentNameFilter != String.Empty || ComboBoxDepartmentName.SelectedItem != null)
            {
               // departmentNameCased = ComboBoxDepartmentName.Text;
                departmentChecked = true;
            }

            // Существует ли имя доктора
            if (doctorNameFilter != String.Empty || ComboBoxDoctorName.SelectedItem!=null)
            {
               // doctorNameCased = ComboBoxDoctorName.Text;
                doctorNameChecked = true;
            }



            // Фильтры поиска
            if (roomNumberChecked)
            {
                roomsArray = roomsArray.Where(x => x.RoomNumber == roomNumberCased).ToList();
            }
            if (departmentChecked)
            {
                roomsArray = roomsArray.Where(x => x.Department.DepartmentId == Convert.ToInt32(ComboBoxDepartmentName.SelectedValue)).ToList();
            }
            if (doctorNameChecked)
            {
                // Console.WriteLine(Convert.ToInt32(ComboBoxDoctorName.SelectedValue));
                roomsArray = roomsArray.Where(x => x.Worker.WorkerId == Convert.ToInt32(ComboBoxDoctorName.SelectedValue)).ToList();
            }

            // Вывод
            RoomsTable.ItemsSource = null;
            RoomsTable.ItemsSource = roomsArray;
        }

        #endregion

        #region Добавление

        /// <summary>
        /// Добавить палату
        /// </summary>
        private void Add()
        {
            string roomNumberFilter = TextBoxAddRoomNumber.Text;
            int roomNumberCased = -1;
            bool roomNumberChecked = false;

            string departmentNameFilter = ComboBoxDepartmentNameAdd.Text;
            string departmentNameCased = "";
            bool departmentChecked = false;

            string doctorNameFilter = ComboBoxDoctorNameAdd.Text;
            string doctorNameCased = "";
            bool doctorNameChecked = false;




            // Существует ли номер комнаты
            if (roomNumberFilter != "")
            {
                Int32.TryParse(roomNumberFilter, out roomNumberCased);
                roomNumberChecked = true;
            }

            // Существует ли отделение
            if (departmentNameFilter != String.Empty || ComboBoxDepartmentName.SelectedItem != null)
            {
                departmentNameCased = ComboBoxDepartmentName.Text;
                departmentChecked = true;
            }

            // Существует ли имя доктора
            if (doctorNameFilter != String.Empty || ComboBoxDoctorName.SelectedItem != null)
            {
                doctorNameCased = ComboBoxDoctorName.Text;
                doctorNameChecked = true;
            }


            if (roomNumberChecked & departmentChecked & doctorNameChecked)
            {
                int getDepartmentId = Convert.ToInt32(ComboBoxDepartmentNameAdd.SelectedValue);
                int getDoctorId = Convert.ToInt32(ComboBoxDoctorNameAdd.SelectedValue);

                Room newRoom = new Room();

                newRoom = new Room
                {
                    RoomNumber = roomNumberCased,
                    RoomDepartmentId = getDepartmentId,
                    DoctorId = getDoctorId
                };

                // добавляем комнату в БД
                db.context.Room.Add(newRoom);
                db.context.SaveChanges();

                // обновление таблицы
                UpdateTable();
            }

        }

        #endregion

        #region Удаление

        /// <summary>
        /// Удаление палат
        /// </summary>
        private void Delete()
        {
            int count = delRoomArray.Count;
            if (count > 0)
            {
                // удаление выбранных палат
                foreach (Room room in delRoomArray)
                {
                    db.context.Room.Remove(room);
                }
                db.context.SaveChanges();
            }

            UpdateTable();
        }

        #endregion

        #region Изменение

        /// <summary>
        /// Редактировать палату
        /// </summary>
        private void EditGet()
        {
            TextBoxEditRoomNumber.Text = editRoom.RoomNumber+"";
            ComboBoxDepartmentNameEdit.SelectedValue = editRoom.Department.DepartmentId;
            ComboBoxDoctorNameEdit.SelectedValue = editRoom.Worker.WorkerId;
        }

        /// <summary>
        /// Подтверждение об изменении получено
        /// </summary>
        private void EditRoomConfirm()
        {
            Room editedRoom = new Room()
            {
                RoomNumber = Convert.ToInt32(TextBoxEditRoomNumber.Text.Trim()),
                RoomDepartmentId = Convert.ToInt32(ComboBoxDepartmentNameEdit.SelectedValue),
                DoctorId = Convert.ToInt32(ComboBoxDoctorNameEdit.SelectedValue)
            };

            Room selectedRoom = db.context.Room.Where(x => x.RoomId == editRoom.RoomId).FirstOrDefault();
            selectedRoom.RoomNumber = editedRoom.RoomNumber;
            selectedRoom.RoomDepartmentId = editedRoom.RoomDepartmentId;
            selectedRoom.DoctorId = editedRoom.DoctorId;

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
            TextBoxEditRoomNumber.Text = "";
            ComboBoxDepartmentNameEdit.SelectedIndex = -1;
            ComboBoxDoctorNameEdit.SelectedIndex = -1;
        }

        #endregion

        #region Общее

        /// <summary>
        /// Обновить таблицу
        /// </summary>
        private void UpdateTable()
        {
            roomsArray = db.context.Room.ToList();
            RoomsTable.ItemsSource = roomsArray;
        }

        #endregion

        #endregion
    }
}
