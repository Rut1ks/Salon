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
    /// Логика взаимодействия для Sotrydniki.xaml
    /// </summary>
    public partial class Sotrydniki : Window
    {
        public Sotrydniki()
        {
            InitializeComponent();
            RefreshData();
            DP1.DisplayDateEnd = DateTime.Today;
        }

        public void RefreshData()
        {
            PostTableAdapter adapt = new PostTableAdapter();
            DataSet1.PostDataTable table1 = new DataSet1.PostDataTable();
            adapt.Fill(table1);
            Doljnost.ItemsSource = table1;
            Doljnost.DisplayMemberPath = "Должность";
            Doljnost.SelectedValuePath = "Код";

            SalonTableAdapter adapte = new SalonTableAdapter();
            DataSet1.SalonDataTable table11 = new DataSet1.SalonDataTable();
            adapte.Fill(table11);
            Salon.ItemsSource = table11;
            Salon.DisplayMemberPath = "Салон";
            Salon.SelectedValuePath = "Код";

            UserTableAdapter adapter1 = new UserTableAdapter();
            DataSet1.UserDataTable table111 = new DataSet1.UserDataTable();
            adapter1.Fill(table111);
            Login.ItemsSource = table111;
            Login.DisplayMemberPath = "Логин";
            Login.SelectedValuePath = "Код";

            EmployeeTableAdapter adapter = new EmployeeTableAdapter();
            DataSet1.EmployeeDataTable table = new DataSet1.EmployeeDataTable();
            adapter.Fill(table);
            SotrudnikiTable.ItemsSource = table;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Familiya.Text != "" && Imya.Text != "" && DP1.Text != "" && Adress.Text != "" && Telephone.Text != "" && Doljnost.Text != "" && Salon.Text!="" && Login.Text!="")
            {
                try
                {
                    new EmployeeTableAdapter().InsertQuery(Familiya.Text, Imya.Text, Otchestvo.Text, DP1.Text, Adress.Text, Telephone.Text, Convert.ToInt32(Doljnost.SelectedValue), Convert.ToInt32(Salon.SelectedValue),Convert.ToInt32(Login.SelectedValue));
                    RefreshData();
                    Familiya.Text = "";
                    Imya.Text = "";
                    Otchestvo.Text = "";
                    DP1.Text = "";
                    Adress.Text = "";
                    Telephone.Text = "";
                    Doljnost.Text = "";
                    Salon.Text = "";
                    Login.Text = "";
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
            if (Familiya.Text != "" && Imya.Text != "" && DP1.Text != "" && Adress.Text != "" && Telephone.Text != "" && Doljnost.Text != "" && Salon.Text!="" && Login.Text!="")
            {
                try
                {
                    new EmployeeTableAdapter().UpdateQuery(Familiya.Text, Imya.Text, Otchestvo.Text, DP1.Text, Adress.Text, Telephone.Text, Convert.ToInt32(Doljnost.SelectedValue), Convert.ToInt32(Salon.SelectedValue), Convert.ToInt32(Login.SelectedValue), Convert.ToInt32((SotrudnikiTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                    RefreshData();
                    Familiya.Text = "";
                    Imya.Text = "";
                    Otchestvo.Text = "";
                    DP1.Text = "";
                    Adress.Text = "";
                    Telephone.Text = "";
                    Doljnost.Text = "";
                    Salon.Text = "";
                    Login.Text = "";
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
                new EmployeeTableAdapter().DeleteQuery(Convert.ToInt32((SotrudnikiTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                RefreshData();
                Familiya.Text = "";
                Imya.Text = "";
                Otchestvo.Text = "";
                DP1.Text = "";
                Adress.Text = "";
                Telephone.Text = "";
                Doljnost.Text = "";
                Salon.Text = "";
                Login.Text = "";
            }
            catch
            {
                MessageBox.Show("Вы не выбрали поле");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Kadri kadri = new Kadri();
            kadri.Show();
            this.Close();
        }

        private void SotrudnikiTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                Familiya.Text = row_selected["Фамилия сотрудника"].ToString();
                Imya.Text = row_selected["Имя сотрудника"].ToString();
                Otchestvo.Text = row_selected["Отчество сотрудника"].ToString();
                DP1.Text = row_selected["Дата рождения сотрудника"].ToString();
                Telephone.Text = row_selected["Телефон сотрудника"].ToString();
                Adress.Text = row_selected["Адрес сотрудника"].ToString();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            EmployeeTableAdapter adapter = new EmployeeTableAdapter();
            DataSet1.EmployeeDataTable table = new DataSet1.EmployeeDataTable();
            adapter.Fill(table);
            DataView dv = new DataView(table);
            dv.RowFilter = $@"[Фамилия сотрудника] LIKE '%{poisk.Text}%'";
            SotrudnikiTable.ItemsSource = dv.ToTable().DefaultView;
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

        private void SotrudnikiTable_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime)) (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
        }
    }
}
