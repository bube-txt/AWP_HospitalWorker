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

namespace AWPApp.View.Pages.Doctor.mpf
{
    /// <summary>
    /// Логика взаимодействия для TicketsPage.xaml
    /// </summary>
    public partial class TicketsPage : Page
    {
        #region [ объявление переменных ]
        Core db = new Core();
        List<Ticket> ticketArray;
        List<TicketHistory> ticketHistoryArray;
        List<Ticket> delTicketArray = new List<Ticket>();

        #endregion
        public TicketsPage()
        {
            InitializeComponent();

            ComboBoxSearchFilterHistoryDepartment.ItemsSource = db.context.Department.ToList();
            ComboBoxSearchFilterHistoryDepartment.SelectedValuePath = "DepartmentId";
            ComboBoxSearchFilterHistoryDepartment.DisplayMemberPath = "DepartmentName";

            ComboBoxSearchFilterDepartment.ItemsSource = db.context.Department.ToList();
            ComboBoxSearchFilterDepartment.SelectedValuePath = "DepartmentId";
            ComboBoxSearchFilterDepartment.DisplayMemberPath = "DepartmentName";

            ComboBoxAddDepartment.ItemsSource = db.context.Department.ToList();
            ComboBoxAddDepartment.SelectedValuePath = "DepartmentId";
            ComboBoxAddDepartment.DisplayMemberPath = "DepartmentName";

            ComboBoxAddFullName.ItemsSource = db.context.Patient.ToList();
            ComboBoxAddFullName.SelectedValuePath = "PatientId";
            ComboBoxAddFullName.DisplayMemberPath = "FullNameAndBirthday";

            ComboBoxSearchFilterHistoryStatus.ItemsSource = db.context.Status.ToList();
            ComboBoxSearchFilterHistoryStatus.SelectedValuePath = "StatusId";
            ComboBoxSearchFilterHistoryStatus.DisplayMemberPath = "StatusName";

            TicketsTable.ItemsSource = db.context.Ticket.Where(x => x.Patient.PatientInArchive != 1).ToList();
            TicketsHistoryTable.ItemsSource = db.context.TicketHistory.Where(x => x.Patient.PatientInArchive != 1).ToList();
        }

        #region [ события ]

        #region Поиск

        /// <summary>
        /// Лайв обновление таблицы при изменении
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxSearchFilterPatientAndressCity_TextChanged(object sender, TextChangedEventArgs e)
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
            TextBoxSearchFilterFirstName.Text = "";
            TextBoxSearchFilterLastName.Text = "";
            DatePickerSearchFilterDate.Text = "";
            ComboBoxSearchFilterDepartment.SelectedIndex = -1;
            ComboBoxAddRoom.SelectedIndex = -1;
            DatePickerAddDate.SelectedDate = null;

            UpdateTable();
        }

        private void ComboBoxPatientName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
            TextBoxSearchFilterFirstName.Text = "";
            TextBoxSearchFilterLastName.Text = "";
            DatePickerSearchFilterDate.SelectedDate = null;
            ComboBoxSearchFilterDepartment.SelectedIndex = -1;
            ComboBoxSearchFilterRoom.SelectedIndex = -1;

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
            Ticket newTicket = checkedBox.DataContext as Ticket;

            delTicketArray.Add(newTicket);
        }

        /// <summary>
        /// Удаление из списка на удаление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkedBox = (CheckBox)sender;
            Ticket newTicket = checkedBox.DataContext as Ticket;

            delTicketArray.Remove(newTicket);
        }


        #endregion

        #endregion

        #region [ классы ]

        #region Поиск

        /// <summary>
        /// Просмотр направлений
        /// </summary>
        private void Search()
        {
            ticketArray = db.context.Ticket.Where(x => x.Patient.PatientInArchive != 1).ToList();

            string firstName = TextBoxSearchFilterFirstName.Text.Trim();
            string lastName = TextBoxSearchFilterLastName.Text.Trim();
            int departmentId = Convert.ToInt32(ComboBoxSearchFilterDepartment.SelectedValue);
            int roomId = Convert.ToInt32(ComboBoxSearchFilterRoom.SelectedValue);
            DateTime date = new DateTime();
            try
            {
                date = (DateTime)DatePickerSearchFilterDate.SelectedDate;
            }
            catch{}

            // Фильтры поиска
            if (ComboBoxSearchFilterDepartment.SelectedIndex != -1)
            {
                ticketArray = ticketArray.Where(x => x.Room.Department.DepartmentId == departmentId).ToList();
            }

            if (ComboBoxSearchFilterRoom.SelectedIndex != -1)
            {
                ticketArray = ticketArray.Where(x => x.Room.RoomId == roomId).ToList();
            }

            if (firstName != "")
            {
                ticketArray = ticketArray.Where(x => x.Patient.PatientFirstName.ToLower().Contains(firstName.ToLower())).ToList();
            }

            if (lastName != "")
            {
                ticketArray = ticketArray.Where(x => x.Patient.PatientLastName.ToLower().Contains(lastName.ToLower())).ToList();
            }

            if (ConvertationClass.GetFormatedDate(date) != "0001-01-01")
            {
                ticketArray = ticketArray.Where(x => x.TicketDate == date).ToList();
            }

            // Вывод
            TicketsTable.ItemsSource = ticketArray;
        }

        /// <summary>
        /// Просмотр истории направлений
        /// </summary>
        private void SearchHistory()
        {
            ticketHistoryArray = db.context.TicketHistory.Where(x => x.Patient.PatientInArchive != 1).ToList();

            string firstName = TextBoxSearchFilterHistoryFirstName.Text;
            string lastName = TextBoxSearchFilterHistoryLastName.Text;
            int departmentId = Convert.ToInt32(ComboBoxSearchFilterHistoryDepartment.SelectedValue);
            int roomId = Convert.ToInt32(ComboBoxSearchFilterHistoryRoom.SelectedValue);
            string statusId = Convert.ToString(ComboBoxSearchFilterHistoryStatus.SelectedValue);
            DatePicker datePicker = DatePickerSearchFilterHistoryDate;

            // Фильтры поиска
            if (ComboBoxSearchFilterHistoryStatus.SelectedIndex != -1)
            {
                ticketHistoryArray = ticketHistoryArray.Where(x => x.Status.StatusId == statusId).ToList();
            }
            if (ComboBoxSearchFilterHistoryDepartment.SelectedIndex != -1)
            {
                ticketHistoryArray = ticketHistoryArray.Where(x => x.Room.Department.DepartmentId == departmentId).ToList();
            }
            if (ComboBoxSearchFilterHistoryRoom.SelectedIndex != -1)
            {
                ticketHistoryArray = ticketHistoryArray.Where(x => x.Room.RoomId == roomId).ToList();
            }
            if (DatePickerSearchFilterHistoryDate.SelectedDate != null)
            {
                ticketHistoryArray = ticketHistoryArray.Where(x => x.TicketHistoryDate == datePicker.SelectedDate).ToList();
            }
            if (firstName != "")
            {
                ticketHistoryArray = ticketHistoryArray.Where(x => x.Patient.PatientFirstName == firstName).ToList();
            }
            if (lastName != "")
            {
                ticketHistoryArray = ticketHistoryArray.Where(x => x.Patient.PatientLastName == lastName).ToList();
            }

            // Вывод
            TicketsHistoryTable.ItemsSource = ticketHistoryArray;
        }

        #endregion

        #region Добавление

        /// <summary>
        /// Добавить направление
        /// </summary>
        private void Add()
        {
            int patientChecked = -1;
            int departmentChecked = -1;
            int roomChecked = -1;
            DateTime dateTimeChecked = new DateTime();

            patientChecked = Convert.ToInt32(ComboBoxAddFullName.SelectedValue);
            departmentChecked = Convert.ToInt32(ComboBoxAddDepartment.SelectedValue);
            roomChecked = Convert.ToInt32(ComboBoxAddRoom.SelectedValue);
            dateTimeChecked = (DateTime)DatePickerAddDate.SelectedDate;


            if (departmentChecked != -1 && departmentChecked != 0 && patientChecked != -1 && patientChecked != 0 && roomChecked != -1 && roomChecked != 0 && dateTimeChecked != new DateTime())
            {
                Ticket newTicket = new Ticket();
                newTicket = new Ticket
                {
                    TicketPatientId = patientChecked,
                    TicketDate = dateTimeChecked,
                    TicketDoctorId = (int)db.context.Room.Where(x => x.RoomId == roomChecked).FirstOrDefault().DoctorId,
                    TicketRoomId = roomChecked,
                };

                TicketHistory newTicketHistory = new TicketHistory();
                newTicketHistory = new TicketHistory
                {
                    TicketHistoryPatientId = patientChecked,
                    TicketHistoryDate = dateTimeChecked,
                    TicketHistoryDoctorId = (int)db.context.Room.Where(x => x.RoomId == roomChecked).FirstOrDefault().DoctorId,
                    TicketHistoryRoomId = roomChecked,
                    TicketHistoryFinishDate = new DateTime(),
                    TicketHistoryStatusId = "P",
                };

                // добавляем комнату в БД
                db.context.Ticket.Add(newTicket);
                db.context.TicketHistory.Add(newTicketHistory);
                db.context.SaveChanges();

                // обновление таблицы
                UpdateTable();
            }

        }

        #endregion

        #region Удаление

        /// <summary>
        /// Удаление отделений
        /// </summary>
        private void Delete()
        {
            int count = delTicketArray.Count;
            if (count > 0)
            {
                // удаление выбранных отделений
                foreach (Ticket ticket in delTicketArray)
                {
                    db.context.TicketHistory.Where(x => x.TicketHistoryId == ticket.TicketId).FirstOrDefault().TicketHistoryStatusId = "C";
                    db.context.TicketHistory.Where(x => x.TicketHistoryId == ticket.TicketId).FirstOrDefault().TicketHistoryFinishDate = DateTime.Now;
                    db.context.Ticket.Remove(ticket);
                }
                db.context.SaveChanges();
            }

            UpdateTable();
        }

        #endregion

        #region Возврат

        /// <summary>
        /// Удаление отделений
        /// </summary>
        private void Return()
        {
            int count = delTicketArray.Count;
            if (count > 0)
            {
                // удаление выбранных отделений
                foreach (Ticket ticket in delTicketArray)
                {
                    db.context.TicketHistory.Where(x => x.TicketHistoryId == ticket.TicketId).FirstOrDefault().TicketHistoryStatusId = "R";
                    db.context.TicketHistory.Where(x => x.TicketHistoryId == ticket.TicketId).FirstOrDefault().TicketHistoryFinishDate = DateTime.Now;
                    db.context.Ticket.Remove(ticket);
                }
                db.context.SaveChanges();
            }

            UpdateTable();
        }

        #endregion

        #region Общее

        /// <summary>
        /// Обновить таблицу
        /// </summary>
        private void UpdateTable()
        {
            ticketArray = db.context.Ticket.ToList();
            TicketsTable.ItemsSource = ticketArray;
            ticketHistoryArray = db.context.TicketHistory.ToList();
            TicketsHistoryTable.ItemsSource = ticketHistoryArray;
        }

        #endregion

        #endregion

        private void ButtonAddClear_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxAddFullName.SelectedIndex = -1;
            ComboBoxAddDepartment.SelectedIndex = -1;
            ComboBoxAddRoom.SelectedIndex = -1;
            DatePickerAddDate.SelectedDate = null;
        }

        private void ComboBoxAddDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxAddRoom.ItemsSource = db.context.Room.AsEnumerable().Where(x => x.RoomDepartmentId == Convert.ToInt32(ComboBoxAddDepartment.SelectedValue)).ToList();
            ComboBoxAddRoom.SelectedValuePath = "RoomId";
            ComboBoxAddRoom.DisplayMemberPath = "RoomNumber";
        }

        private void ComboBoxSearchFilterDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
            ComboBoxSearchFilterRoom.ItemsSource = db.context.Room.AsEnumerable().Where(x => x.RoomDepartmentId == Convert.ToInt32(ComboBoxSearchFilterDepartment.SelectedValue)).ToList();
            ComboBoxSearchFilterRoom.SelectedValuePath = "RoomId";
            ComboBoxSearchFilterRoom.DisplayMemberPath = "RoomNumber";
        }

        private void TextBoxAddFilterFullName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxAddFilterFullName.Text != "")
            {
                ComboBoxAddFullName.ItemsSource = db.context.Patient.AsEnumerable().Where(x => x.FullName.Trim().Contains(TextBoxAddFilterFullName.Text.Trim()) && x.PatientInArchive != 1).ToList();
            }
            else
            {
                ComboBoxAddFullName.ItemsSource = db.context.Patient.Where(x => x.PatientInArchive != 1).ToList();
            }
        }

        private void ButtonReturn_Click(object sender, RoutedEventArgs e)
        {
            Return();
        }

        private void TextBoxSearchFilterHistoryFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchHistory();
        }

        private void TextBoxSearchFilterHistoryLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchHistory();
        }

        private void DatePickerSearchFilterHistoryDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchHistory();
        }

        private void ComboBoxSearchFilterHistoryDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchHistory();
        }

        private void ComboBoxSearchFilterHistoryRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchHistory();
        }

        private void ButtonFilterForceUpdate_Click(object sender, RoutedEventArgs e)
        {
            ticketHistoryArray = db.context.TicketHistory.Where(x => x.Patient.PatientInArchive != 1).ToList();
            TicketsHistoryTable.ItemsSource = ticketHistoryArray;
        }

        private void ComboBoxSearchFilterHistoryStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchHistory();
        }

        private void ButtonFilterHistoryClear_Click(object sender, RoutedEventArgs e)
        {
            TextBoxSearchFilterHistoryFirstName.Text = "";
            TextBoxSearchFilterHistoryLastName.Text = "";
            DatePickerSearchFilterHistoryDate.SelectedDate = null;
            ComboBoxSearchFilterHistoryDepartment.SelectedIndex = -1;
            ComboBoxSearchFilterHistoryRoom.SelectedIndex = -1;
            ComboBoxSearchFilterHistoryStatus.SelectedIndex = -1;
        }

        private void TextBoxSearchFilterHistoryPatronicName_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchHistory();
        }

        private void TextBoxSearchFilterLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void TextBoxSearchFilterFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void DatePickerSearchFilterDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void ComboBoxSearchFilterRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }
    }
}
