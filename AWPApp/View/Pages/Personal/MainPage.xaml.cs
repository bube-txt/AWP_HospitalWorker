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
using AWPApp.Assets.Access;
using AWPApp.Model;
using AWPApp.View.Pages.Doctor.mpf;
using AWPApp.View.Pages.Personal.MainPageFramePages;

namespace AWPApp.View.Pages.Doctor
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        Core db = new Core();
        public MainPage()
        {
            InitializeComponent();

            App.MPF = MainPageFrame;

            Users user = App.ActiveUser;
            Worker worker = App.ActiveWorker;

            if (worker != null)
            {
                TextBlockFirstName.Text = worker.WorkerFirstName;
                TextBlockLastName.Text = worker.WorkerLastName;
            }
            if (user != null)
            {
                TextBlockJobName.Text = user.Job.JobName;
            }

            #region Уровни доступа

            // Администратор
            if (worker.Job.JobAccessLevel == (int)Roles.Administrator)
            {
                Departments.IsEnabled = false;
                Rooms.IsEnabled = false;
                Doctors.IsEnabled = false;
            }

            // Архивариус
            if (worker.Job.JobAccessLevel == (int)Roles.Archivist)
            {
                Departments.IsEnabled = false;
                Rooms.IsEnabled = false;
                Doctors.IsEnabled = false;
            }

            #endregion

        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = new PatientsPage();
        }

        private void ListBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = new RoomsPage();
        }

        private void ListBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = new DepartmentsPage();
        }

        private void ListBoxItem_Selected_3(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = new TicketsPage();
        }

        private void Button_LogOutClick(object sender, RoutedEventArgs e)
        {
            App.ActiveWorker = new Worker();
            App.ActiveUser = new Users();

            MainWindow.mf.Navigate(new LoginPage());
        }

        private void ListBoxItem_Selected_4(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = new WorkersPage();
        }

        private void ListBoxItem_Selected_5(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = new ArchivePage();
        }

        private void ListBoxItem_Selected_6(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = new UsersPage();
        }
    }
}
