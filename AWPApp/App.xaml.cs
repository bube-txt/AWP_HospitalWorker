using AWPApp.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AWPApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Frame MPF { get; set; } = null;
        public static Users ActiveUser { get; set; } = null;
        public static Worker ActiveWorker { get; set; } = null;
    }
}
