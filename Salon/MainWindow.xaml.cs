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

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AdresSalona nabori = new AdresSalona();
            nabori.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Salons klienti = new Salons();
            klienti.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Doljnosti doljnosti = new Doljnosti();
            doljnosti.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Yslugi yslugi = new Yslugi();
            yslugi.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Sotrydniki sotrydniki = new Sotrydniki();
            sotrydniki.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Zapisi zapisi = new Zapisi();
            zapisi.Show();
        }
    }
}
