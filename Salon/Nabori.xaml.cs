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
    /// Логика взаимодействия для Nabori.xaml
    /// </summary>
    public partial class Nabori : Window
    {
        public Nabori()
        {
            InitializeComponent();
            RefreshData();
            Nazvanie.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
        }

        public void RefreshData()
        {
            KitsTableAdapter adapter = new KitsTableAdapter();
            DataSet1.KitsDataTable table = new DataSet1.KitsDataTable();
            adapter.Fill(table);
            NaboriTable.ItemsSource = table;
        }

        public void OnPasteCommand(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                if (Nazvanie.Text != "" && AdresSklada.Text!="")
                {
                    try
                    {
                        new KitsTableAdapter().InsertQuery(Nazvanie.Text, Convert.ToInt32(AdresSklada.SelectedValue));
                        RefreshData();
                        Nazvanie.Text = "";
                    AdresSklada.Text = "";
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
            if (Nazvanie.Text !="" && AdresSklada.Text != "")
            {
                try
                {
                    new KitsTableAdapter().UpdateQuery(Nazvanie.Text,Convert.ToInt32(AdresSklada.SelectedValue), Convert.ToInt32((NaboriTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                    RefreshData();
                    Nazvanie.Text = "";
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
                new KitsTableAdapter().DeleteQuery(Convert.ToInt32((NaboriTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                RefreshData();
                Nazvanie.Text = "";
                AdresSklada.Text = "";
            }
            catch
            {
                MessageBox.Show("Вы не выбрали поле");
            }
        }

        private void NaboriTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                Nazvanie.Text = row_selected["Набор"].ToString();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Kladovshik kladovshik = new Kladovshik();
            kladovshik.Show();
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            KitsTableAdapter postavki = new KitsTableAdapter();
            DataSet1.KitsDataTable table = new DataSet1.KitsDataTable();
            postavki.Fill(table);
            DataView dv = new DataView(table);
            dv.RowFilter = $@"[Наименование] LIKE '%{poisk.Text}%'";
            NaboriTable.ItemsSource = dv.ToTable().DefaultView;
        }

        private void Nazvanie_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Я]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
