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
    /// Логика взаимодействия для TipYslygi.xaml
    /// </summary>
    public partial class TipYslygi : Window
    {
        public TipYslygi()
        {
            InitializeComponent();
            Tip.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            RefreshData();
        }

        public void OnPasteCommand(object sender, ExecutedRoutedEventArgs e)
        {

        }

        public void RefreshData()
        {
            TipYslygiTableAdapter adapter = new TipYslygiTableAdapter();
            DataSet1.TipYslygiDataTable table = new DataSet1.TipYslygiDataTable();
            adapter.Fill(table);
            TipTable.ItemsSource = table;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Tip.Text != "")
            {
                try
                {
                    new TipYslygiTableAdapter().InsertQuery(Tip.Text);
                    RefreshData();
                    Tip.Text = "";
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
            if (Tip.Text != "")
            {
                try
                {
                    new TipYslygiTableAdapter().UpdateQuery(Tip.Text, Convert.ToInt32((TipTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                    RefreshData();
                    Tip.Text = "";
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
                new TipYslygiTableAdapter().DeleteQuery(Convert.ToInt32((TipTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                RefreshData();
                Tip.Text = "";
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
                Tip.Text = row_selected["Тип"].ToString();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            TipYslygiTableAdapter adapter = new TipYslygiTableAdapter();
            DataSet1.TipYslygiDataTable table = new DataSet1.TipYslygiDataTable();
            adapter.Fill(table);
            DataView dv = new DataView(table);
            dv.RowFilter = $@"[Тип] LIKE '%{poisk.Text}%'";
            TipTable.ItemsSource = dv.ToTable().DefaultView;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Naimenovanie_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Я]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
