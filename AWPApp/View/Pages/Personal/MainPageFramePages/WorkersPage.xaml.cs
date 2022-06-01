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
    /// Логика взаимодействия для WorkersPage.xaml
    /// </summary>
    public partial class WorkersPage : Page
    {
        Core db = new Core();
        List<Worker> workerArray;
        List<Worker> delWorkerArray = new List<Worker>();
        CheckClass cc = new CheckClass();
        Worker editWorker = new Worker();
        Worker selectedWorker = new Worker();
        Worker selectedWorkerView = new Worker();
        List<Special> specials = new List<Special>();

        public WorkersPage()
        {
            InitializeComponent();

            Update();


            specials = db.context.Special.Where(x => x.SpecialName != "Система").ToList();

            ComboBoxSearchFilterSpecial.ItemsSource = specials;
            ComboBoxSearchFilterSpecial.SelectedValuePath = "SpecialId";
            ComboBoxSearchFilterSpecial.DisplayMemberPath = "SpecialName";

            ComboBoxAddSpecial.ItemsSource = specials;
            ComboBoxAddSpecial.SelectedValuePath = "SpecialId";
            ComboBoxAddSpecial.DisplayMemberPath = "SpecialName";

            ComboBoxEditSpecial.ItemsSource = specials;
            ComboBoxEditSpecial.SelectedValuePath = "SpecialId";
            ComboBoxEditSpecial.DisplayMemberPath = "SpecialName";

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
            TextBoxSearchFilterFullName.Text = "";
            ComboBoxSearchFilterSpecial.SelectedIndex = -1;
            TextBoxSearchFilterTimeStart.Text = "";
            TextBoxSearchFilterTimeEnd.Text = "";

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
            TextBoxAddLastName.Text = "";
            TextBoxAddFirstName.Text = "";
            TextBoxAddPatronicName.Text = "";
            ComboBoxAddSpecial.SelectedIndex = -1;
            TextBoxAddTimeStart.Text = "";
            TextBoxAddTimeEnd.Text = "";

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
            Worker newWorker = checkedBox.DataContext as Worker;

            delWorkerArray.Add(newWorker);
        }

        /// <summary>
        /// Удаление из списка на удаление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkedBox = (CheckBox)sender;
            Worker newWorker = checkedBox.DataContext as Worker;

            delWorkerArray.Remove(newWorker);
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
            editWorker = ButtonClicked.DataContext as Worker;

            TabControlWorkers.SelectedIndex = 2; 
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
            workerArray = db.context.Worker.Where(x => x.Special.SpecialName != "Система").ToList();
            WorkersTable.ItemsSource = workerArray;

            string fullNameFilter = TextBoxSearchFilterFullName.Text;
            string fullNameCased = "";
            bool fullNameChecked = false;

            int specialFilter = ComboBoxSearchFilterSpecial.SelectedValue == null ? -1 : (int)ComboBoxSearchFilterSpecial.SelectedValue;
            bool specialChecked = false;

            string timeStartFilter = TextBoxSearchFilterTimeStart.Text;
            string timeStartCased = "";
            bool timeStartChecked = false;

            string timeEndFilter = TextBoxSearchFilterTimeEnd.Text;
            string timeEndCased = "";
            bool timeEndChecked = false;

            // Существует ли имя
            if (fullNameFilter != "")
            {
                fullNameCased = fullNameFilter.ToLower().Trim();
                fullNameChecked = true;
            }

            // Выбрана ли специальность
            if (specialFilter > 0)
            {
                specialChecked = true;
            }

            // Существует ли имя
            if (timeStartFilter != "")
            {
                timeStartCased = timeStartFilter.Trim();
                timeStartChecked = true;
            }

            // Существует ли имя
            if (timeEndFilter != "")
            {
                timeEndCased = timeEndFilter.Trim();
                timeEndChecked = true;
            }



            // Фильтры поиска
            if (fullNameChecked)
            {
                workerArray = workerArray.Where(x => x.FullName.ToLower().Contains(fullNameCased)).ToList();
            }
            if (specialChecked)
            {
                workerArray = workerArray.Where(x => x.Special.SpecialId == specialFilter).ToList();
            }
            if (timeStartChecked)
            {
                try
                {
                    if (cc.TimeCheck(timeStartFilter))
                    {
                        workerArray = workerArray.Where(x => x.WorkerScheduleStart == TimeSpan.Parse(timeStartFilter)).ToList();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка формата времени в строке \"Начало смены\"\nВерный формат => HH:mm:ss");
                }
            }
            if (timeEndChecked)
            {
                try
                {
                    if (cc.TimeCheck(timeEndFilter))
                    {
                        workerArray = workerArray.Where(x => x.WorkerScheduleStart == TimeSpan.Parse(timeEndFilter)).ToList();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка формата времени в строке \"Конец смены\"\nВерный формат => HH:mm:ss");
                }
            }

            // Вывод
            WorkersTable.ItemsSource = workerArray;
        }

        #endregion Поиск

        #region Добавление

        /// <summary>
        /// Добавить отделение
        /// </summary>
        private void Add()
        {
            Update();

            string firstNameFilter = TextBoxAddFirstName.Text;
            string firstNameCased = "";
            bool firstNameChecked = false;

            string lastNameFilter = TextBoxAddLastName.Text;
            string lastNameCased = "";
            bool lastNameChecked = false;

            string patronicNameFilter = TextBoxAddPatronicName.Text;
            string patronicNameCased = "";
            bool patronicNameChecked = false;

            var specialFilter = ComboBoxAddSpecial.SelectedValue;
            bool specialChecked = false;

            string timeStartFilter = TextBoxAddTimeStart.Text;
            string timeStartCased = "";
            bool timeStartChecked = false;

            string timeEndFilter = TextBoxAddTimeEnd.Text;
            string timeEndCased = "";
            bool timeEndChecked = false;

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
                patronicNameCased = patronicNameFilter.Trim();
                patronicNameChecked = true;
            }

            // Выбрана ли специальность
            if (!(specialFilter is null))
            {
                specialChecked = true;
            }

            // Существует ли имя
            if (timeStartFilter != "")
            {
                timeStartCased = timeStartFilter.Trim();
                timeStartChecked = true;
                MessageBox.Show(TimeSpan.TryParse(timeStartCased, out TimeSpan time)+" " + time);
            }

            // Существует ли имя
            if (timeEndFilter != "")
            {
                timeEndCased = timeEndFilter.Trim();
                timeEndChecked = true;
            }

            

            if (firstNameChecked &&
                lastNameChecked &&
                patronicNameChecked &&
                TimeSpan.TryParse(timeStartCased, out TimeSpan timeStart) &&
                TimeSpan.TryParse(timeEndCased, out TimeSpan timeEnd)
                )
            {

                Worker newWorker = new Worker
                {
                    WorkerFirstName = firstNameCased,
                    WorkerLastName = lastNameCased,
                    WorkerPatronicName = patronicNameCased,
                    WorkerScheduleStart = timeStart,
                    WorkerScheduleEnd = timeEnd,
                    WorkerJobId = 1,
                    WorkerSpecialId = (int)ComboBoxAddSpecial.SelectedValue,
                };

                // добавляем комнату в БД
                db.context.Worker.Add(newWorker);
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
            int count = delWorkerArray.Count;
            if (count > 0)
            {
                // удаление выбранных пациентов
                foreach (Worker worker in delWorkerArray)
                {
                    db.context.Worker.Remove(worker);
                }
                db.context.SaveChanges();
            }

            Update();
        }

        #endregion Удаление

        #region Изменение

        /// <summary>
        /// Редактировать отделение
        /// </summary>
        private void EditGet()
        {
            TextBoxEditFirstName.Text = editWorker.WorkerFirstName;

            TextBoxEditLastName.Text = editWorker.WorkerLastName;

            TextBoxEditPatronicName.Text = editWorker.WorkerPatronicName;

            ComboBoxEditSpecial.SelectedValue = editWorker.Special.SpecialId;

            TextBoxEditTimeStart.Text = editWorker.WorkerScheduleStart.ToString();

            TextBoxEditTimeEnd.Text = editWorker.WorkerScheduleEnd.ToString();
        }

        /// <summary>
        /// Подтверждение об изменении получено
        /// </summary>
        private void EditPatientConfirm()
        {
            if (TimeSpan.TryParse(TextBoxEditTimeStart.Text, out TimeSpan timeStart) && TimeSpan.TryParse(TextBoxEditTimeEnd.Text, out TimeSpan timeEnd))
            {
                Worker editedWorker = new Worker()
                {
                    WorkerFirstName = TextBoxEditFirstName.Text.Trim(),
                    WorkerLastName = TextBoxEditLastName.Text.Trim(),
                    WorkerPatronicName = TextBoxEditPatronicName.Text.Trim(),
                    WorkerScheduleStart = timeStart,
                    WorkerScheduleEnd = timeEnd,
                    WorkerSpecialId = (int)ComboBoxEditSpecial.SelectedValue,
                };

                Worker selectedWorker = db.context.Worker.Where(x => x.WorkerId == editWorker.WorkerId).FirstOrDefault();

                selectedWorker.WorkerFirstName = editedWorker.WorkerFirstName;
                selectedWorker.WorkerLastName = editedWorker.WorkerLastName;
                selectedWorker.WorkerPatronicName = editedWorker.WorkerPatronicName;
                selectedWorker.WorkerScheduleStart = editedWorker.WorkerScheduleStart;
                selectedWorker.WorkerScheduleEnd = editedWorker.WorkerScheduleEnd;
                selectedWorker.WorkerSpecialId = editedWorker.WorkerSpecialId;

                db.context.SaveChanges();


                Update();

            }
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

        public void Update()
        {
            workerArray = db.context.Worker.Where(x => x.Special.SpecialName != "Система").ToList();
            WorkersTable.ItemsSource = workerArray;
        }

        private void ComboBoxSearchFilterSpecial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void Button_BackClick(object sender, RoutedEventArgs e)
        {
            GridWorkers.Visibility = Visibility.Visible;
            GridWorkerTickets.Visibility = Visibility.Collapsed;
        }

        private void ButtonTicketsView_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            selectedWorkerView = button.DataContext as Worker;

            GridWorkers.Visibility = Visibility.Collapsed;
            GridWorkerTickets.Visibility = Visibility.Visible;

            List<Ticket> tickets = db.context.Ticket.Where(x => x.TicketDoctorId == selectedWorkerView.WorkerId).ToList();
            TextBlockTicketsViewFullName.Text = selectedWorkerView.FullName;
            TextBlockTicketsViewSpecial.Text = selectedWorkerView.Special.SpecialName;

            TicketsTable.ItemsSource = tickets;
        }
    }
}
