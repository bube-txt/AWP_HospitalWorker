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
using AWPApp.ViewModel;
using AWPApp.View.Pages;
using AWPApp.View.Pages.Doctor;
using AWPApp.Model;

namespace AWPApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Frame mf;
        public MainWindow()
        {
            InitializeComponent();
            LoginPage loginPage = new LoginPage();
            MainFrame.Navigate(loginPage);
            //MainPage mainPage = new MainPage();
            //MainFrame.Navigate(mainPage);
            mf = MainFrame;
        }
    }
}
