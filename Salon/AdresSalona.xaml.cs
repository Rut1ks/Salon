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
    /// Логика взаимодействия для AdresSalona.xaml
    /// </summary>
    public partial class AdresSalona : Window
    {
        public AdresSalona()
        {
            InitializeComponent();
            Adres.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            RefreshData();
        }

        public void OnPasteCommand(object sender, ExecutedRoutedEventArgs e)
        {

        }

        public void RefreshData()
        {
            AdresSalonaTableAdapter adapter = new AdresSalonaTableAdapter();
            DataSet1.AdresSalonaDataTable table = new DataSet1.AdresSalonaDataTable();
            adapter.Fill(table);
            AdresTable.ItemsSource = table;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Adres.Text != "")
            {
                try
                {
                    new AdresSalonaTableAdapter().InsertQuery(Adres.Text);
                    RefreshData();
                    Adres.Text = "";
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
            if (Adres.Text != "")
            {
                try
                {
                    new AdresSalonaTableAdapter().UpdateQuery(Adres.Text, Convert.ToInt32((AdresTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                    RefreshData();
                    Adres.Text = "";
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
                new AdresSalonaTableAdapter().DeleteQuery(Convert.ToInt32((AdresTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                RefreshData();
                Adres.Text = "";
            }
            catch
            {
                MessageBox.Show("Вы не выбрали поле");
            }
        }

        private void DoljnostTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                Adres.Text = row_selected["Адрес"].ToString();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AdresSalonaTableAdapter adapter = new AdresSalonaTableAdapter();
            DataSet1.AdresSalonaDataTable table = new DataSet1.AdresSalonaDataTable();
            adapter.Fill(table);
            DataView dv = new DataView(table);
            dv.RowFilter = $@"[Адрес] LIKE '%{poisk.Text}%'";
            AdresTable.ItemsSource = dv.ToTable().DefaultView;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            admin.Show();
            this.Close();
        }

        private void Naimenovanie_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Я]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
