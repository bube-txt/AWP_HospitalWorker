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
    /// Логика взаимодействия для ArchivePage.xaml
    /// </summary>
    public partial class ArchivePage : Page
    {
        #region [ объявление переменных ]
        Core db = new Core();
        List<Patient> patientArray;
        Patient selectedPatient;
        CheckClass cc = new CheckClass();

        #endregion
        public ArchivePage()
        {
            InitializeComponent();

            HideDailyMap();
            ShowPatients();

            // таблица
            patientArray = db.context.Patient.Where(x => x.PatientInArchive == 1).ToList();
            PatientsTable.ItemsSource = patientArray;
        }


        #region Поиск

        /// <summary>
        /// Просмотр отделений
        /// </summary>
        private void Search()
        {
            patientArray = db.context.Patient.Where(x => x.PatientInArchive == 1).ToList();

            string fullNameFilter = TextBoxArchiveFullName.Text;
            string fullNameCased = "";
            bool fullNameChecked = false;

            string numberFilter = TextBoxArchiveNumber.Text;
            int numberCased = 0;
            bool numberChecked = false;

            string dateFilter = TextBoxArchiveDate.Text;
            string dateCased = "";
            bool dateChecked = false;

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


            // Фильтры поиска
            if (numberChecked)
            {
                patientArray = patientArray.Where(x => x.PatientId == numberCased).ToList();
            }
            if (fullNameChecked)
            {
                patientArray = patientArray.Where(x => x.FullName.ToLower().Contains(TextBoxArchiveFullName.Text.ToLower())).ToList();
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

            // Вывод
            PatientsTable.ItemsSource = patientArray;
        }

        #region Общее

        /// <summary>
        /// Обновить таблицу
        /// </summary>
        private void UpdateTable()
        {
            patientArray = db.context.Patient.Where(x => x.PatientInArchive == 1).ToList();
            PatientsTable.ItemsSource = patientArray;
        }
        #endregion

        #endregion Поиск
        private void ButtonFilterClear_Click(object sender, RoutedEventArgs e)
        {
            TextBoxArchiveFullName.Text = "";
            TextBoxArchiveNumber.Text = "";
            TextBoxArchiveDate.Text = "";

            UpdateTable();
        }

        private void TextBoxArchiveFullName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void TextBoxArchiveNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void TextBoxArchiveDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        #region { Дневная карта }

        private void ButtonDailyMap_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;
            Button button = (Button)contextMenu.PlacementTarget;
            selectedPatient = button.DataContext as Patient;

            HidePatients();
            ShowDailyMap();
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
        private void HideDailyMap()
        {
            GridDailyMap.Visibility = Visibility.Collapsed;
        }
        private void ShowDailyMap()
        {
            GridDailyMap.Visibility = Visibility.Visible;

            TextBlockDailyPatientFullName.Text = selectedPatient.FullName;
            TextBlockDailyPatientDate.Text = selectedPatient.PatientFormatedDate;

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
        private void Button_ReturnClick(object sender, RoutedEventArgs e)
        {
            HideDailyMap();
            ShowPatients();
        }

        private void ComboBoxSearchFilterSpecial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchWorkersAtSpecial();
            SearchRecord();
        }

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
    }
}
