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

namespace AWPApp.View.Pages.Doctor.MainPageFrame
{
    /// <summary>
    /// Логика взаимодействия для PatientListPage.xaml
    /// </summary>
    public partial class PatientListPage : Page
    {
        Core db = new Core();
        public PatientListPage()
        {
            InitializeComponent();
            List<Patient> patientsArray = db.context.Patient.ToList();
            PatientsTable.ItemsSource = patientsArray;
        }
        /// <summary>
        /// Применить фильтр поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonFilter_Click(object sender, RoutedEventArgs e)
        {
            List<Patient> patientsArray = db.context.Patient.ToList();

            string firstNameFilter = TextBoxSearchFilterFirstName.Text.Trim();
            string firstNameCased = "";
            bool firstNameChecked = false;

            string LastNameFilter = TextBoxSearchFilterLastName.Text.Trim();
            string lastNameCased = "";
            bool lastNameChecked = false;

            int regNumberFilter = -1;
            bool regNumberChecked = false;

            string dateFilter = TextBoxSearchFilterDate.Text;
            bool dateChecked = false;


            // Существует ли имя
            if (firstNameFilter != "")
            {
                firstNameCased = firstNameFilter.Substring(0, 1).ToUpper() + firstNameFilter.Substring(1, TextBoxSearchFilterFirstName.Text.Length - 1).ToLower();
                firstNameChecked = true;
            }

            // Существует ли фамилия
            if (LastNameFilter != "")
            {
                lastNameCased = LastNameFilter.Substring(0, 1).ToUpper() + LastNameFilter.Substring(1, TextBoxSearchFilterLastName.Text.Length - 1).ToLower();
                lastNameChecked = true;
            }

            // Существует ли регистрационный номер
            if (Int32.TryParse(TextBoxSearchFilterRegNumber.Text.Trim(), out int i))
            {
                regNumberFilter = i;
                regNumberChecked = true;
            }

            // Существует ли дата приёма
            if (dateFilter != "")
            {
                dateChecked = true;
            }

            // Фильтры поиска
            if (regNumberChecked)
            {
                patientsArray = db.context.Patient.Where(x => x.PatientRegNumber == regNumberFilter).ToList();
            }
            else if (firstNameChecked & lastNameChecked & dateChecked)
            {
                patientsArray = db.context.Patient.Where(x => x.PatientFirstName.Contains(firstNameCased) & x.PatientLastName.Contains(lastNameCased) & x.PatientDate.ToString().Contains(dateFilter)).ToList();
            }
            else if (firstNameChecked & lastNameChecked)
            {
                patientsArray = db.context.Patient.Where(x => x.PatientFirstName.Contains(firstNameCased) & x.PatientLastName.Contains(lastNameCased)).ToList();
            }
            else if (firstNameChecked)
            {
                patientsArray = db.context.Patient.Where(x => x.PatientFirstName.Contains(firstNameCased)).ToList();
            }
            else if (lastNameChecked)
            {
                patientsArray = db.context.Patient.Where(x => x.PatientLastName.Contains(lastNameCased)).ToList();
            }
            else if (dateChecked)
            {
                patientsArray = db.context.Patient.Where(x => x.PatientDate.ToString().Contains(dateFilter)).ToList();
            }


            // Вывод
            PatientsTable.ItemsSource = patientsArray;

        }
        /// <summary>
        /// Сброс применения фильтра к поиску
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDropFilter_Click(object sender, RoutedEventArgs e)
        {
            List<Patient> patientsArray = db.context.Patient.ToList();
            PatientsTable.ItemsSource = patientsArray;
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

            List<Patient> patientsArray = db.context.Patient.ToList();
            PatientsTable.ItemsSource = patientsArray;
        }
    }
}
