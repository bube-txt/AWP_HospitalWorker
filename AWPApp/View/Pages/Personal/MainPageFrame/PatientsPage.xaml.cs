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

namespace AWPApp.View.Pages.Doctor.MainPageFrame
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

            // таблица
            patientArray = db.context.Patient.ToList();
            PatientsTable.ItemsSource = patientArray;
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
        /// Изменить выбранное отделение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Button ButtonClicked = (Button)sender;
            editPatient = ButtonClicked.DataContext as Patient;

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
            Button button = (Button)sender;
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
            patientArray = db.context.Patient.ToList();

            string fullNameFilter = TextBoxSearchFilterFullName.Text;
            string fullNameCased = "";
            bool fullNameChecked = false;

            string numberFilter = TextBoxSearchFilterNumber.Text;
            int numberCased = 0;
            bool numberChecked = false;

            string dateFilter = TextBoxSearchFilterDate.Text;
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

            // Вывод
            PatientsTable.ItemsSource = patientArray;
        }

        #endregion Поиск

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

            // Существует ли дата
            if (cc.DateCheck(dateFilter) & dateFilter != "")
            {
                dateCased = dateFilter.Trim();
                dateChecked = true;
            }


            if (firstNameChecked & lastNameChecked & dateChecked)
            {

                Patient newPatient = new Patient
                {
                    PatientFirstName = firstNameCased,
                    PatientLastName = lastNameCased,
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

        #region Удаление

        /// <summary>
        /// Удаление отделений
        /// </summary>
        private void Delete()
        {
            // Добавить столбик с CheckBox`ами
            // Добавить проверки дат в коде через DateCheck()
            int count = delPatientArray.Count;
            if (count > 0)
            {
                // удаление выбранных пациентов
                foreach (Patient patient in delPatientArray)
                {
                    db.context.Patient.Remove(patient);
                }
                db.context.SaveChanges();
            }

            UpdateTable();
        }

        #endregion Удаление

        #region Изменение

        /// <summary>
        /// Редактировать отделение
        /// </summary>
        private void EditGet()
        {
            TextBoxEditFirstName.Text = editPatient.PatientFirstName+ "";
            TextBoxEditLastName.Text = editPatient.PatientLastName+ "";
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
                    PatientDate = Convert.ToDateTime(TextBoxEditDate.Text.Trim())
                };

                Patient selectedPatient = db.context.Patient.Where(x => x.PatientId == editPatient.PatientId).FirstOrDefault();

                selectedPatient.PatientFirstName = editedPatient.PatientFirstName;
                selectedPatient.PatientLastName = editedPatient.PatientLastName;
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
            patientArray = db.context.Patient.ToList();
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

            TextBlockDailyPatientLastName.Text = selectedPatient.PatientLastName;
            TextBlockDailyPatientFirstName.Text = selectedPatient.PatientFirstName;
            TextBlockDailyPatientDate.Text = selectedPatient.PatientFormatedDate;

            ListViewDailyMap.ItemsSource = db.context.Record.Where(x => x.PacientId == selectedPatient.PatientId).ToList();
        }

        #endregion { Дневная карта }

        #endregion [ классы ]
    }
}
