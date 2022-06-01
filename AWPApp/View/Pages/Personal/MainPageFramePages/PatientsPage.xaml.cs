using AWPApp.Assets.Access;
using AWPApp.Model;
using AWPLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для PatientsPage.xaml
    /// </summary>
    public partial class PatientsPage : Page
    {
        #region [ объявление переменных ]
        Core db = new Core();
        CheckClass cc = new CheckClass();
        List<Patient> patientArray;
        List<Patient> delPatientArray = new List<Patient>();
        Patient editPatient = new Patient();
        Patient selectedPatient = null;
        // bool doctorNameChecked = false;

        #endregion
        public PatientsPage()
        {
            InitializeComponent();

            HideDailyMap();
            ShowPatients();

            // ContextMenuDataContext.DataContext = db.context.Worker.ToList();

            ComboBoxSearchFilterDoctorName.ItemsSource = db.context.Worker.Where(x => x.Job.JobAccessLevel != (int)Roles.System).ToList();
            ComboBoxSearchFilterDoctorName.SelectedValuePath = "WorkerId";
            ComboBoxSearchFilterDoctorName.DisplayMemberPath = "FullName";

            // таблица
            patientArray = db.context.Patient.Where(x => x.PatientInArchive != 1).ToList();
            PatientsTable.ItemsSource = patientArray;

            Worker worker = App.ActiveWorker;

            // Архивариус
            if (worker.Job.JobAccessLevel == (int)Roles.Archivist)
            {
                // закладки
                TabItemAdd.IsEnabled = false;
                TabItemEdit.IsEnabled = false;

                // контекстное меню
                /*ButtonTempertureList.IsEnabled = false;
                ButtonDailyMap.IsEnabled = false;
                ButtonEdit.IsEnabled = false;*/
            }
        }

        #region [ события ]

        #region { Пациент }

        #region Поиск

        /// <summary>
        /// Лайв обновление таблицы при изменении
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxSearchFilterNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        /// <summary>
        /// Лайв обновление таблицы при изменении
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxSearchFilterDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

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
            TextBoxSearchFilterFullName.Text = "";
            TextBoxSearchFilterNumber.Text = "";
            TextBoxSearchFilterDate.Text = "";

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
        private void ButtonAddClear_Click(object sender, RoutedEventArgs e)
        {
            TextBoxAddFirstName.Text = "";
            TextBoxAddLastName.Text = "";
            TextBoxAddDate.Text = "";

            UpdateTable();
        }

        #endregion

        #region Удаление

        /// <summary>
        /// Добавление в список на удаление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkedBox = (CheckBox)sender;
            Patient newPatient = checkedBox.DataContext as Patient;

            delPatientArray.Add(newPatient);
        }

        /// <summary>
        /// Удаление из списка на удаление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkedBox = (CheckBox)sender;
            Patient newPatient = checkedBox.DataContext as Patient;

            delPatientArray.Remove(newPatient);
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
            MenuItem menuItem = (MenuItem)sender;
            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;
            Button button = (Button)contextMenu.PlacementTarget;

            editPatient = button.DataContext as Patient;

            TabControlPatients.SelectedIndex = 2;

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

        #region { Дневная карта }

        #region Просмотр

        /// <summary>
        /// Переход на дневную карту выбранного пациента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDailyMap_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;
            Button button = (Button)contextMenu.PlacementTarget;
            selectedPatient = button.DataContext as Patient;

            HidePatients();
            ShowDailyMap();
        }

        /// <summary>
        /// Возврат к списку пациентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ReturnClick(object sender, RoutedEventArgs e)
        {
            HideDailyMap();
            ShowPatients();
        }
        #endregion Просмотр

        #endregion { Дневная карта }

        #endregion [ события ]

        #region [ классы ]

        #region { Пациент }

        #region Поиск

        /// <summary>
        /// Просмотр отделений
        /// </summary>
        private void Search()
        {
            patientArray = db.context.Patient.Where(x => x.PatientInArchive != 1).ToList();

            string fullNameFilter = TextBoxSearchFilterFullName.Text;
            string fullNameCased = "";
            bool fullNameChecked = false;

            string numberFilter = TextBoxSearchFilterNumber.Text;
            int numberCased = 0;
            bool numberChecked = false;

            string dateFilter = TextBoxSearchFilterDate.Text;
            string dateCased = "";
            bool dateChecked = false;

            int doctorFilter = (int)ComboBoxSearchFilterDoctorName.SelectedValue;
            int doctorCased = -1;
            bool doctorChecked = false;

            // Существует ли имя
            if (fullNameFilter != "")
            {
                fullNameCased = fullNameFilter.Trim();
                fullNameChecked = true;
            }

            // Существует ли номер
            if (numberFilter != "")
            {
                numberCased = Convert.ToInt32(numberFilter.Trim());
                numberChecked = true;
            }

            // Существует ли дата
            if (cc.DateCheck(dateFilter) & dateFilter != "")
            {
                dateCased = dateFilter.Trim();
                dateChecked = true;
            }

            // Существует ли доктор
            if (doctorFilter != -1)
            {
                doctorCased = doctorFilter;
                doctorChecked = true;
            }


            // Фильтры поиска
            if (numberChecked)
            {
                patientArray = patientArray.Where(x => x.PatientId == numberCased).ToList();
            }
            if (fullNameChecked)
            {
                patientArray = patientArray.Where(x => x.FullName.ToLower().Contains(TextBoxSearchFilterFullName.Text.ToLower())).ToList();
            }
            if (dateChecked)
            {
                try
                {
                    patientArray = patientArray.Where(x => x.PatientDate == Convert.ToDateTime(dateCased)).ToList();
                }
                catch
                { 

                }
            }
            if (doctorChecked)
            {
                patientArray = patientArray.Where(x => x.GetDoctorId == doctorCased).ToList();
            }

            // Вывод
            PatientsTable.ItemsSource = patientArray;
        }

        #endregion Поиск

        #region Архивирование

        /// <summary>
        /// Добавление в архив
        /// </summary>
        /// <param name="patient"></param>
        private void AddToArchive(Patient patient)
        {
            try
            {
                patient.PatientInArchive = 1;
                db.context.SaveChanges();
                MessageBox.Show("Пациент успешно отправлен в архив!");
                UpdateTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Добавление

        /// <summary>
        /// Добавить отделение
        /// </summary>
        private void Add()
        {
            string firstNameFilter = TextBoxAddFirstName.Text;
            string firstNameCased = "";
            bool firstNameChecked = false;

            string lastNameFilter = TextBoxAddLastName.Text;
            string lastNameCased = "";
            bool lastNameChecked = false;

            string patronicNameFilter = TextBoxAddLastName.Text;
            string patronicNameCased = "";
            bool patronicNameChecked = false;

            string dateFilter = TextBoxAddDate.Text;
            string dateCased = "";
            bool dateChecked = false;

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

            // Существует ли отчество
            if (patronicNameFilter != "")
            {
                patronicNameCased = lastNameFilter.Trim();
                patronicNameChecked = true;
            }

            // Существует ли дата
            if (cc.DateCheck(dateFilter) & dateFilter != "")
            {
                dateCased = dateFilter.Trim();
                dateChecked = true;
            }


            if (firstNameChecked && lastNameChecked && patronicNameChecked && dateChecked)
            {

                Patient newPatient = new Patient
                {
                    PatientFirstName = firstNameCased,
                    PatientLastName = lastNameCased,
                    PatientPatronicName = patronicNameCased,
                    PatientDate = Convert.ToDateTime(dateCased)
                };

                // добавляем комнату в БД
                db.context.Patient.Add(newPatient);
                db.context.SaveChanges();

                // обновление таблицы
                UpdateTable();
            }

        }

        #endregion Добавление

        #region Изменение

        /// <summary>
        /// Редактировать отделение
        /// </summary>
        private void EditGet()
        {
            TextBoxEditFirstName.Text = editPatient.PatientFirstName+ "";
            TextBoxEditLastName.Text = editPatient.PatientLastName+ "";
            TextBoxEditPatronicName.Text = editPatient.PatientPatronicName+ "";
            TextBoxEditDate.Text = editPatient.PatientFormatedDate+ "";
        }

        /// <summary>
        /// Подтверждение об изменении получено
        /// </summary>
        private void EditPatientConfirm()
        {
                Patient editedPatient = new Patient()
                {
                    PatientFirstName = TextBoxEditFirstName.Text.Trim(),
                    PatientLastName = TextBoxEditLastName.Text.Trim(),
                    PatientPatronicName = TextBoxEditPatronicName.Text.Trim(),
                    PatientDate = Convert.ToDateTime(TextBoxEditDate.Text.Trim())
                };

                Patient selectedPatient = db.context.Patient.Where(x => x.PatientId == editPatient.PatientId).FirstOrDefault();

                selectedPatient.PatientFirstName = editedPatient.PatientFirstName;
                selectedPatient.PatientLastName = editedPatient.PatientLastName;
                selectedPatient.PatientPatronicName = editedPatient.PatientPatronicName;
                selectedPatient.PatientDate = Convert.ToDateTime(editedPatient.PatientFormatedDate);

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
            TextBoxEditFirstName.Text = "";
        }

        #endregion Изменение

        #region Общее

        /// <summary>
        /// Обновить таблицу
        /// </summary>
        private void UpdateTable()
        {
            patientArray = db.context.Patient.Where(x => x.PatientInArchive != 1).ToList();
            PatientsTable.ItemsSource = patientArray;
        }

        private void HidePatients()
        {
            PatientsTable.Visibility = Visibility.Collapsed;
            TabControlPatients.Visibility = Visibility.Collapsed;
        }

        private void ShowPatients()
        {
            PatientsTable.Visibility = Visibility.Visible;
            TabControlPatients.Visibility = Visibility.Visible;
        }

        #endregion Общее

        #endregion { Пациент }

        #region { Дневная карта }

        private void HideDailyMap()
        {
            GridDailyMap.Visibility = Visibility.Collapsed;
        }

        private void ShowDailyMap()
        {
            GridDailyMap.Visibility = Visibility.Visible;

            TextBlockDailyPatientFullName.Text = selectedPatient.FullName;
            TextBlockDailyPatientDate.Text = selectedPatient.PatientFormatedDate;

            if (selectedPatient.Ticket.Where(x => x.TicketRoomId == x.Room.RoomId).FirstOrDefault() != null)
            {
                TextBlockDailyDepartment.Text = selectedPatient.Ticket.Where(x => x.TicketRoomId == x.Room.RoomId).FirstOrDefault().Room.Department.DepartmentName.ToString();
                TextBlockDailyRoom.Text = selectedPatient.PatientGetRoomNumberAndDoctorFullName;
            }
            else
            {
                TextBlockDailyDepartment.Text = "Не определен";
                TextBlockDailyRoom.Text = "Не определен";
            }

            ComboBoxSearchFilterSpecial.ItemsSource = db.context.Special.Where(x => x.SpecialJobId == 1).ToList();
            ComboBoxSearchFilterSpecial.DisplayMemberPath = "SpecialName";
            ComboBoxSearchFilterSpecial.SelectedValuePath = "SpecialId";

            ComboBoxSearchFilterDoctor.ItemsSource = db.context.Worker.Where(x => x.WorkerJobId == 1).ToList();
            ComboBoxSearchFilterDoctor.DisplayMemberPath = "FullName";
            ComboBoxSearchFilterDoctor.SelectedValuePath = "WorkerId";

            UpdateRecord();
        }

        private void UpdateRecord()
        {
            ListViewDailyMap.ItemsSource = db.context.Record.Where(x => x.PacientId == selectedPatient.PatientId).ToList();
        }

        private void SearchRecord()
        {
            List<Record> records = db.context.Record.Where(x => x.PacientId == selectedPatient.PatientId).ToList();

            if (TextBoxSearchFilterRecordHeaderText.Text.Trim() != "")
            {
                records = records.Where(x => x.RecordHeader.Trim().ToLower().Contains(TextBoxSearchFilterRecordHeaderText.Text.Trim().ToLower())).ToList();
            }
            if (ComboBoxSearchFilterSpecial.SelectedIndex != -1)
            {
                records = records.Where(x => x.Worker.Special.SpecialId == (int)ComboBoxSearchFilterSpecial.SelectedValue).ToList();
            }
            if (ComboBoxSearchFilterDoctor.SelectedIndex != -1)
            {
                records = records.Where(x => x.DoctorId == (int)ComboBoxSearchFilterDoctor.SelectedValue).ToList();
            }
            if (DatePickerSearchFilterDate.SelectedDate != null)
            {
                records = records.Where(x => x.RecordDate == (DateTime)DatePickerSearchFilterDate.SelectedDate).ToList();
            }

            ListViewDailyMap.ItemsSource = records;
        }

        #endregion { Дневная карта }

        #endregion [ классы ]

        private void ButtonAddRecord_Click(object sender, RoutedEventArgs e)
        {
            AddRecord();
        }

        public void AddRecord()
        {
            string header = TextBoxHeaderAdd.Text;
            FlowDocument text = RichTextBoxTextAdd.Document;
            if (!String.IsNullOrWhiteSpace(header))
            {
                try
                {
                    Record newRecord = new Record
                    {
                        RecordHeader = header,
                        RecordText = ConvertationClass.RichTextToString(text),
                        RecordDate = DateTime.Now,
                        DoctorId = App.ActiveWorker.WorkerId,
                        PacientId = selectedPatient.PatientId,
                    };

                    db.context.Record.Add(newRecord);
                    db.context.SaveChanges();

                    ClearRecordFields();
                    UpdateRecord();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void ClearRecordFields()
        {
            TextBoxHeaderAdd.Text = "";
            RichTextBoxTextAdd.Document = new FlowDocument();
        }

        private void ComboBoxSearchFilterSpecial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchRecord();
            SearchWorkersAtSpecial();
        }

        /// <summary>
        /// Поиск врача в зависимости от выбранной специальность
        /// </summary>
        private void SearchWorkersAtSpecial()
        {
            if (ComboBoxSearchFilterSpecial.SelectedIndex != -1)
            {
                ComboBoxSearchFilterDoctor.ItemsSource = db.context.Worker.Where(x => x.WorkerJobId == 1 && x.WorkerSpecialId == (int)ComboBoxSearchFilterSpecial.SelectedValue).ToList();
            }
            else
            {
                ComboBoxSearchFilterDoctor.ItemsSource = db.context.Worker.Where(x => x.WorkerJobId == 1).ToList();
            }
        }

        private void ComboBoxSearchFilterDoctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchRecord();
        }

        private void DatePickerSearchFilterDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchRecord();
        }

        private void Button_ClearRecordFilterClick(object sender, RoutedEventArgs e)
        {
            TextBoxSearchFilterRecordHeaderText.Text = "";
            ComboBoxSearchFilterSpecial.SelectedIndex = -1;
            ComboBoxSearchFilterDoctor.SelectedIndex = -1;
            DatePickerSearchFilterDate.SelectedDate = null;
            SearchRecord();
        }

        private void TextBoxSearchFilterRecordHeader_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchRecord();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabControlRecords.SelectedIndex != 0)
            {
                StackPanelSearchFilterBar.Visibility = Visibility.Collapsed;
                ClearButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                StackPanelSearchFilterBar.Visibility = Visibility.Visible;
                ClearButton.Visibility = Visibility.Visible;
            }
        }

        private void ButtonTempertureList_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;
            Button button = (Button)contextMenu.PlacementTarget;

            NavigationService.Navigate(new TempertureListPage());
        }

        private void ButtonAddToArchive_Click(object sender, RoutedEventArgs e)
        {

            MenuItem menuItem = (MenuItem)sender;
            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;
            Button button = (Button)contextMenu.PlacementTarget;

            AddToArchive(button.DataContext as Patient);
        }

        private void PatientsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*if (PatientsTable.SelectedItem != null)
            {
                PatientsTable.SelectedItem;
            }*/
        }

        private void ComboBoxDoctorNameEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }
    }
}
