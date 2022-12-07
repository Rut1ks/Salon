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
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : Window
    {
        public User()
        {
            InitializeComponent();
            Login.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            Password.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            RefreshData();
        }

        public void OnPasteCommand(object sender, ExecutedRoutedEventArgs e)
        {

        }

        public void RefreshData()
        {
            RoleTableAdapter adapt = new RoleTableAdapter();
            DataSet1.RoleDataTable table1 = new DataSet1.RoleDataTable();
            adapt.Fill(table1);
            Role.ItemsSource = table1;
            Role.DisplayMemberPath = "Наименование";
            Role.SelectedValuePath = "Код";

            UserTableAdapter adapter = new UserTableAdapter();
            DataSet1.UserDataTable table = new DataSet1.UserDataTable();
            adapter.Fill(table);
            UserTable.ItemsSource = table;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text != "" && Password.Text != "" && Role.Text!="")
            {
                try
                {
                    new UserTableAdapter().InsertQuery(Login.Text, Shifrovanie.ShifrovaniePass(Password.Text), Convert.ToInt32(Role.SelectedValue));
                    RefreshData();
                    Login.Text = "";
                    Password.Text = "";
                    Role.Text = "";
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
            if (Login.Text != "" && Password.Text != "" && Role.Text != "")
            {
                try
                {
                    new UserTableAdapter().UpdateQuery(Login.Text, Shifrovanie.ShifrovaniePass(Password.Text), Convert.ToInt32(Role.SelectedValue), Convert.ToInt32((UserTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                    RefreshData();
                    Login.Text = "";
                    Password.Text = "";
                    Role.Text = "";
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
                new UserTableAdapter().DeleteQuery(Convert.ToInt32((UserTable.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                RefreshData();
                Login.Text = "";
                Password.Text = "";
                Role.Text = "";
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
                Login.Text = row_selected["Логин"].ToString();
                Role.Text = row_selected["Роль"].ToString();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            UserTableAdapter adapter = new UserTableAdapter();
            DataSet1.UserDataTable table = new DataSet1.UserDataTable();
            adapter.Fill(table);
            DataView dv = new DataView(table);
            dv.RowFilter = $@"[Логин] LIKE '%{poisk.Text}%'";
            UserTable.ItemsSource = dv.ToTable().DefaultView;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            admin.Show();
            this.Close();
        }

        //private void Naimenovanie_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    Regex regex = new Regex("[^а-яА-Я]");
        //    e.Handled = regex.IsMatch(e.Text);
        //}
    }
}
