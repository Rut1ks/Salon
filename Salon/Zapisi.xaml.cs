using Salon.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для Zapisi.xaml
    /// </summary>
    public partial class Zapisi : Window
    {
        public Zapisi()
        {
            InitializeComponent();
            
            RefreshData();
        }

        public void RefreshData()
        {
            CustomersTableAdapter adapt = new CustomersTableAdapter();
            DataSet1.CustomersDataTable table1 = new DataSet1.CustomersDataTable();
            adapt.Fill(table1);
            Klient.ItemsSource = table1;
            Klient.DisplayMemberPath = "Фамилия";
            Klient.SelectedValuePath = "Код";

            ServicesTableAdapter adap = new ServicesTableAdapter();
            DataSet1.ServicesDataTable table2 = new DataSet1.ServicesDataTable();
            adap.Fill(table2);
            Yslyga.ItemsSource = table2;
            Yslyga.DisplayMemberPath = "Услуга";
            Yslyga.SelectedValuePath = "Код";

            RecordsTableAdapter adapter = new RecordsTableAdapter();
            DataSet1.RecordsDataTable table = new DataSet1.RecordsDataTable();
            adapter.Fill(table);
            ZapisiTable.ItemsSource = table;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new RecordsTableAdapter().InsertQuery(DP1.Text,Vremya.Text,Poseshenie.IsEnabled,Convert.ToInt32(Klient.SelectedValue),Convert.ToInt32(Yslyga.SelectedValue));
                RefreshData();
                DP1.Text = "";
                Vremya.Text = "";
                Klient.Text = "";
                Yslyga.Text = "";
            }
            catch
            {
                MessageBox.Show("Вы неправильно ввели данные в поля");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
                try
                {
                    new RecordsTableAdapter().UpdateQuery(DP1.Text,Vremya.Text,Poseshenie.IsEnabled,Convert.ToInt32(Klient.SelectedValue),Convert.ToInt32(Yslyga.SelectedValue), Convert.ToInt32((ZapisiTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                    RefreshData();
                    DP1.Text = "";
                    Vremya.Text = "";
                    Klient.Text = "";
                    Yslyga.Text = "";
                }
                catch
                {
                    MessageBox.Show("Вы неправильно ввели данные в поля");
                }   
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                new RecordsTableAdapter().DeleteQuery(Convert.ToInt32((ZapisiTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                RefreshData();
                DP1.Text = "";
                Vremya.Text = "";
                Klient.Text = "";
                Yslyga.Text = "";
            }
            catch
            {
                MessageBox.Show("Вы не выбрали поле");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Kassir kassir = new Kassir();
            kassir.Show();
            this.Close();
        }

        private void ZapisiTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                DP1.Text = row_selected["Дата записи"].ToString();
                Vremya.Text = row_selected["Время начала"].ToString();
                Poseshenie.IsChecked = (bool?)row_selected["Посещение"];
                Klient.Text = row_selected["Фамилия"].ToString();
                Yslyga.Text = row_selected["Услуга"].ToString();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                RecordsTableAdapter adapter = new RecordsTableAdapter();
                DataSet1.RecordsDataTable table = new DataSet1.RecordsDataTable();
                adapter.Fill(table);
                DataView dv = new DataView(table);
                dv.RowFilter = $@"Convert(Клиент,'System.String') LIKE '%{poisk.Text}%'";
                ZapisiTable.ItemsSource = dv.ToTable().DefaultView;
            }
            catch
            {
                MessageBox.Show("Вы неправильно ввели данные в поля");
            }
        }

        private void ZapisiTable_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime)) (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
        }
    }
}
