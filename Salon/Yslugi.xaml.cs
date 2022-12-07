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
    /// Логика взаимодействия для Yslugi.xaml
    /// </summary>
    public partial class Yslugi : Window
    {
        public Yslugi()
        {
            InitializeComponent();
            RefreshData();
            Nazvanie.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            Cena.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            VremyaProvedeniya.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
        }

        public void OnPasteCommand(object sender, ExecutedRoutedEventArgs e)
        {

        }

        public void RefreshData()
        {
            KitsTableAdapter adapte = new KitsTableAdapter();
            DataSet1.KitsDataTable table1 = new DataSet1.KitsDataTable();
            adapte.Fill(table1);
            Nabori.ItemsSource = table1;
            Nabori.DisplayMemberPath = "Набор";
            Nabori.SelectedValuePath = "Код";

            TipYslygiTableAdapter adapte1 = new TipYslygiTableAdapter();
            DataSet1.TipYslygiDataTable table11 = new DataSet1.TipYslygiDataTable();
            adapte.Fill(table1);
            Nabori.ItemsSource = table1;
            Nabori.DisplayMemberPath = "Тип";
            Nabori.SelectedValuePath = "Код";

            EmployeeTableAdapter adapte11 = new EmployeeTableAdapter();
            DataSet1.EmployeeDataTable table111 = new DataSet1.EmployeeDataTable();
            adapte.Fill(table1);
            Nabori.ItemsSource = table1;
            Nabori.DisplayMemberPath = "Сотрудник";
            Nabori.SelectedValuePath = "Код";

            ServicesTableAdapter adapter = new ServicesTableAdapter();
            DataSet1.ServicesDataTable table = new DataSet1.ServicesDataTable();
            adapter.Fill(table);
            YslugiTable.ItemsSource = table;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Nazvanie.Text != "" && Cena.Text != "" && VremyaProvedeniya.Text != "" && Nabori.Text != "")
            {
                try
                {
                    new ServicesTableAdapter().InsertQuery(Nazvanie.Text, Convert.ToInt32(Cena.Text), VremyaProvedeniya.Text, Convert.ToInt32(Nabori.SelectedValue), Convert.ToInt32(Tip.SelectedValue), Convert.ToInt32(Sotrudnik.SelectedValue));
                    RefreshData();
                    Nazvanie.Text = "";
                    Cena.Text = "";
                    VremyaProvedeniya.Text = "";
                    Nabori.Text = "";
                    Tip.Text = "";
                    Sotrudnik.Text = "";
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
            if (Nazvanie.Text != "" && Cena.Text != "" && VremyaProvedeniya.Text != "" && Nabori.Text != "")
            {
                try
                {
                    new ServicesTableAdapter().UpdateQuery(Nazvanie.Text, Convert.ToInt32(Cena.Text), VremyaProvedeniya.Text, Convert.ToInt32(Nabori.SelectedValue), Convert.ToInt32(Tip.SelectedValue), Convert.ToInt32(Sotrudnik.SelectedValue), Convert.ToInt32((YslugiTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                    RefreshData();
                    Nazvanie.Text = "";
                    Cena.Text = "";
                    VremyaProvedeniya.Text = "";
                    Nabori.Text = "";
                    Tip.Text = "";
                    Sotrudnik.Text = "";
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
                new ServicesTableAdapter().DeleteQuery(Convert.ToInt32((YslugiTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                RefreshData();
                Nazvanie.Text = "";
                Cena.Text = "";
                VremyaProvedeniya.Text = "";
                Nabori.Text = "";
                Tip.Text = "";
                Sotrudnik.Text = "";
            }
            catch
            {
                MessageBox.Show("Вы не выбрали поле");
            }
        }

        private void YslugiTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                Nazvanie.Text = row_selected["Услуги"].ToString();
                Cena.Text = row_selected["Цена"].ToString();
                VremyaProvedeniya.Text = row_selected["Время проведения"].ToString();
                Nabori.Text = row_selected["Наборы"].ToString();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            admin.Show();
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ServicesTableAdapter adapter = new ServicesTableAdapter();
            DataSet1.ServicesDataTable table = new DataSet1.ServicesDataTable();
            adapter.Fill(table);
            DataView dv = new DataView(table);
            dv.RowFilter = $@"[Услуга] LIKE '%{poisk.Text}%'";
            YslugiTable.ItemsSource = dv.ToTable().DefaultView;
        }

        private void Nazvanie_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Я]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Cena_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
