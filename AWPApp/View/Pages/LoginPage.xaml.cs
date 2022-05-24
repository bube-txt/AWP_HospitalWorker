using AWPApp.View.Pages.Doctor;
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
using AWPApp;

namespace AWPApp.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            LoginBox.Text = "system";
            PasswordBox.Password = "system";
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = this.NavigationService;
          
            bool result = UsersViewModel.AuthUser(LoginBox.Text, PasswordBox.Password);

            if (result)
            {
                MainPage mainPage = new MainPage();
                ns.Navigate(mainPage);
            }
            else
            {
                ErrorTextBlock.Text = "Неверные данные!";
            }

        }
    }
}
