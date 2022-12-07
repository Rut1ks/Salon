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
    /// Логика взаимодействия для Sklad.xaml
    /// </summary>
    public partial class Sklad : Window
    {
        public Sklad()
        {
            InitializeComponent();
            Adres.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            Kolvo.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            RefreshData();
        }

        public void OnPasteCommand(object sender, ExecutedRoutedEventArgs e)
        {

        }

        public void RefreshData()
        {
            SkladTableAdapter adapter = new SkladTableAdapter();
            DataSet1.SkladDataTable table = new DataSet1.SkladDataTable();
            adapter.Fill(table);
            SkladTable.ItemsSource = table;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Adres.Text != "" && Kolvo.Text != "")
            {
                try
                {
                    new SkladTableAdapter().InsertQuery(Adres.Text, Convert.ToInt32(Kolvo.Text));
                    RefreshData();
                    Adres.Text = "";
                    Kolvo.Text = "";
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
            if (Adres.Text != "" && Kolvo.Text != "")
            {
                try
                {
                    new SkladTableAdapter().UpdateQuery(Adres.Text, Convert.ToInt32(Kolvo.Text), Convert.ToInt32((SkladTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                    RefreshData();
                    Adres.Text = "";
                    Kolvo.Text = "";
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
                new SkladTableAdapter().DeleteQuery(Convert.ToInt32((SkladTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                RefreshData();
                Adres.Text = "";
                Kolvo.Text = "";
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
                Kolvo.Text = row_selected["Количество ячеек"].ToString();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SkladTableAdapter adapter = new SkladTableAdapter();
            DataSet1.SkladDataTable table = new DataSet1.SkladDataTable();
            adapter.Fill(table);
            DataView dv = new DataView(table);
            dv.RowFilter = $@"[Адрес] LIKE '%{poisk.Text}%'";
            SkladTable.ItemsSource = dv.ToTable().DefaultView;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Kladovshik kladovshik = new Kladovshik();
            kladovshik.Show();
            this.Close();
        }

        private void Naimenovanie_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Я]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
