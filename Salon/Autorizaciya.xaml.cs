using Salon.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для Autorizaciya.xaml
    /// </summary>
    public partial class Autorizaciya : Window
    {
        SqlConnection con = new SqlConnection();
        DataSet1 DataSet1;
        UserTableAdapter userTableAdapter;
        public Autorizaciya()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Salon.Properties.Settings.SalonConnectionString"].ConnectionString.ToString();
            DataSet1 = new DataSet1(); userTableAdapter = new UserTableAdapter(); userTableAdapter.Fill(DataSet1.User);
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Log = Login.Text;
            string Pass = Shifrovanie.ShifrovaniePass(Password.Password);
            //string Pass = Password.Password;
            for (int i = 0; i < DataSet1.Tables["User"].Rows.Count; i++)
            {
                if (Log == DataSet1.Tables["User"].Rows[i]["Логин"].ToString() && Pass == DataSet1.Tables["User"].Rows[i]["Пароль"].ToString())
                {
                    string roleID = DataSet1.Tables["User"].Rows[i]["Роль"].ToString();

                    switch (roleID)
                    {
                        case "Админ":
                            {
                                AdminWindow R = new AdminWindow();
                                R.Show();
                                this.Close();
                                break;
                            }
                        case "Кассир":
                            {
                                Kassir A = new Kassir();
                                A.Show();
                                this.Close();
                                break;
                            }

                        case "Кладовщик":
                            {
                                Kladovshik K = new Kladovshik();
                                K.Show();
                                this.Close();
                                break;
                            }

                        case "Кадровик":
                            {
                                Kadri Q = new Kadri();
                                Q.Show();
                                this.Close();
                                break;
                            }
                        case "Системный администратор":
                            {
                                Susadmin susadmin = new Susadmin();
                                susadmin.Show();
                                this.Close();
                                break;
                            }
                        default: break;
                    }
                }
                else { Error.Content = "Ошибка, проверьте правильность введения данных"; }
            }
        }
    }
}
