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
using System.Windows.Shapes;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для Kadri.xaml
    /// </summary>
    public partial class Kadri : Window
    {
        public Kadri()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Sotrydniki sotrydniki = new Sotrydniki();
            sotrydniki.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Doljnosti doljnosti = new Doljnosti();
            doljnosti.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Autorizaciya autorizaciya = new Autorizaciya();
            autorizaciya.Show();
            this.Close();
        }
    }
}
