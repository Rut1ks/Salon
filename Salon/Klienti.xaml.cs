using Salon.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для Klienti.xaml
    /// </summary>
    public partial class Klienti : Window
    {
        public Klienti()
        {
            InitializeComponent();
            Familiya.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            Imya.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            Otchestvo.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            Telephone.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            RefreshData();
        }

        public void OnPasteCommand(object sender, ExecutedRoutedEventArgs e)
        {

        }

        public void RefreshData()
        {
            CustomersTableAdapter adapter = new CustomersTableAdapter();
            DataSet1.CustomersDataTable table = new DataSet1.CustomersDataTable();
            adapter.Fill(table);
            KlientiTable.ItemsSource = table;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Familiya.Text != "" && Imya.Text != "" && Telephone.Text != "")
            {
                try
                {
                    new CustomersTableAdapter().InsertQuery(Familiya.Text, Imya.Text, Otchestvo.Text, Telephone.Text);
                    RefreshData();
                    Familiya.Text = "";
                    Imya.Text = "";
                    Otchestvo.Text = "";
                    Telephone.Text = "";
                }
                catch
                {
                    MessageBox.Show("Вы неправильно ввели данные в поля");
                }
            }
            else MessageBox.Show("Вы неправильно ввели данные в поля");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Familiya.Text != "" && Imya.Text != "" && Telephone.Text != "")
            {
                try
                {
                    new CustomersTableAdapter().UpdateQuery(Familiya.Text, Imya.Text, Otchestvo.Text, Telephone.Text, Convert.ToInt32((KlientiTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                    RefreshData();
                    Familiya.Text = "";
                    Imya.Text = "";
                    Otchestvo.Text = "";
                    Telephone.Text = "";
                }
                catch
                {
                    MessageBox.Show("Вы неправильно ввели данные в поля");
                }
            }
            else MessageBox.Show("Вы неправильно ввели данные в поля");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                new CustomersTableAdapter().DeleteQuery(Convert.ToInt32((KlientiTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                RefreshData();
                Familiya.Text = "";
                Imya.Text = "";
                Otchestvo.Text = "";
                Telephone.Text = "";
            }
            catch
            {
                MessageBox.Show("Вы не выбрали поле");
            }
        }

        private void KlientiTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                Familiya.Text = row_selected["Фамилия"].ToString();
                Imya.Text = row_selected["Имя"].ToString();
                Otchestvo.Text = row_selected["Отчество"].ToString();
                Telephone.Text = row_selected["Телефон"].ToString();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Kassir kassir = new Kassir();
            kassir.Show();
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            CustomersTableAdapter adapter = new CustomersTableAdapter();
            DataSet1.CustomersDataTable table = new DataSet1.CustomersDataTable();
            adapter.Fill(table);
            DataView dv = new DataView(table);
            dv.RowFilter = $@"[Фамилия] LIKE '%{poisk.Text}%'";
            KlientiTable.ItemsSource = dv.ToTable().DefaultView;
        }

        private void Familiya_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Я]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Imya_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Я]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Otchestvo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Я]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Telephone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
            Telephone.MaxLength = 11;
        }

        private void Familiya_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Familiya.Text.Length == 1)
            {
                Familiya.Text = Familiya.Text.ToUpper();
                Familiya.Select(Familiya.Text.Length, 0);
            }
        }

        private void Imya_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Imya.Text.Length == 1)
            {
                Imya.Text = Imya.Text.ToUpper();
                Imya.Select(Imya.Text.Length, 0);
            }
        }

        private void Otchestvo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Otchestvo.Text.Length == 1)
            {
                Otchestvo.Text = Otchestvo.Text.ToUpper();
                Otchestvo.Select(Otchestvo.Text.Length, 0);
            }
        }
    }
}
