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
    /// Логика взаимодействия для Doljnosti.xaml
    /// </summary>
    public partial class Doljnosti : Window
    {
        public Doljnosti()
        {
            InitializeComponent();
            Naimenovanie.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            Oklad.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            RefreshData();
        }

        public void OnPasteCommand(object sender, ExecutedRoutedEventArgs e)
        {

        }

        public void RefreshData()
        {
            PostTableAdapter adapter = new PostTableAdapter();
            DataSet1.PostDataTable table = new DataSet1.PostDataTable();
            adapter.Fill(table);
            DoljnostTable.ItemsSource = table;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Naimenovanie.Text != "" && Oklad.Text != "")
            {
                try
                {
                    new PostTableAdapter().InsertQuery(Naimenovanie.Text, Convert.ToInt32(Oklad.Text));
                    RefreshData();
                    Naimenovanie.Text = "";
                    Oklad.Text = "";
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
            if (Naimenovanie.Text != "" && Oklad.Text != "")
            {
                try
                {
                    new PostTableAdapter().UpdateQuery(Naimenovanie.Text, Convert.ToInt32(Oklad.Text), Convert.ToInt32((DoljnostTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                    RefreshData();
                    Naimenovanie.Text = "";
                    Oklad.Text = "";
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
                new PostTableAdapter().DeleteQuery(Convert.ToInt32((DoljnostTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                RefreshData();
                Naimenovanie.Text = "";
                Oklad.Text = "";
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
                Naimenovanie.Text = row_selected["Наименование"].ToString();
                Oklad.Text = row_selected["Оклад"].ToString();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            PostTableAdapter adapter = new PostTableAdapter();
            DataSet1.PostDataTable table = new DataSet1.PostDataTable();
            adapter.Fill(table);
            DataView dv = new DataView(table);
            dv.RowFilter = $@"[Должность] LIKE '%{poiskk.Text}%'";
            DoljnostTable.ItemsSource = dv.ToTable().DefaultView;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Kadri kadri = new Kadri();
            kadri.Show();
            this.Close();
        }

        private void Naimenovanie_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Я]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
