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
    /// Логика взаимодействия для Salon.xaml
    /// </summary>
    public partial class Salons : Window
    {
        public Salons()
        {
            InitializeComponent();
            Naimenovanie.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            Kolvomest.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            Vremya.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            Ploshyad.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            RefreshData();
        }

        public void OnPasteCommand(object sender, ExecutedRoutedEventArgs e)
        {

        }

        public void RefreshData()
        {

            AdresSalonaTableAdapter adapte = new AdresSalonaTableAdapter();
            DataSet1.AdresSalonaDataTable table1 = new DataSet1.AdresSalonaDataTable();
            adapte.Fill(table1);
            Adres.ItemsSource = table1;
            Adres.DisplayMemberPath = "Адрес";
            Adres.SelectedValuePath = "Код";


            SalonTableAdapter adapter = new SalonTableAdapter();
            DataSet1.SalonDataTable table = new DataSet1.SalonDataTable();
            adapter.Fill(table);
            SalonTable.ItemsSource = table;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Naimenovanie.Text != "" && Ploshyad.Text != "" && Kolvomest.Text != "" && Vremya.Text != "" && Adres.Text != "")
            {
                try
                {
                    new SalonTableAdapter().InsertQuery(Naimenovanie.Text, Ploshyad.Text, Convert.ToInt32(Kolvomest.Text), Vremya.Text, Convert.ToInt32(Adres.SelectedValue));
                    RefreshData();
                    Naimenovanie.Text = "";
                    Ploshyad.Text = "";
                    Kolvomest.Text = "";
                    Vremya.Text = "";
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
            if (Naimenovanie.Text != "" && Ploshyad.Text != "" && Kolvomest.Text != "" && Vremya.Text!="" && Adres.Text!="")
            {
                try
                {
                    new SalonTableAdapter().UpdateQuery(Naimenovanie.Text, Ploshyad.Text, Convert.ToInt32(Kolvomest.Text), Vremya.Text,Convert.ToInt32(Adres.SelectedValue), Convert.ToInt32((SalonTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                    RefreshData();
                    Naimenovanie.Text = "";
                    Ploshyad.Text = "";
                    Kolvomest.Text = "";
                    Vremya.Text = "";
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
                new SalonTableAdapter().DeleteQuery(Convert.ToInt32((SalonTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                RefreshData();
                Naimenovanie.Text = "";
                Ploshyad.Text = "";
                Kolvomest.Text = "";
                Vremya.Text = "";
                Adres.Text = "";
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
                Naimenovanie.Text = row_selected["Наименование"].ToString();
                Ploshyad.Text = row_selected["Площадь"].ToString();
                Kolvomest.Text = row_selected["Количество мест"].ToString();
                Vremya.Text = row_selected["Время работы"].ToString();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SalonTableAdapter adapter = new SalonTableAdapter();
            DataSet1.SalonDataTable table = new DataSet1.SalonDataTable();
            adapter.Fill(table);
            DataView dv = new DataView(table);
            dv.RowFilter = $@"[Наименование] LIKE '%{poisk.Text}%'";
            SalonTable.ItemsSource = dv.ToTable().DefaultView;
        }
    }
}
