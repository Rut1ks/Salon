using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;


namespace Salon
{
    public partial class Susadmin : Window
    {
        SqlConnection con = new SqlConnection(@"Data Source = LAPTOP-02T55PA4\MSSQLSERVER01; Initial Catalog = Salon; Integrated Security = True");

        public Susadmin()
        {
            InitializeComponent();

            con.ConnectionString = ConfigurationManager.ConnectionStrings["Salon.Properties.Settings.SalonConnectionString"].ConnectionString.ToString();

          
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

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menu = new MainWindow();
            menu.Show();
            this.Close();
        }
        

        private static void BckpBrao(string connection, string salon, string fileBrao)
        {
            try
            {
                var cmd = "BACKUP DATABASE @Salon TO DISK = @FileBrao";

                using (var con = new SqlConnection(connection))
                using (var cmmd = new SqlCommand(cmd, con))
                {
                    con.Open();

                    cmmd.Parameters.AddWithValue("@Salon", salon);
                    cmmd.Parameters.AddWithValue("@FileBrao", fileBrao);

                    cmmd.ExecuteNonQuery();
                }
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show($"{e}");
            }
        }

        private void Brow_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog().ToString().Equals("OK"))
            {
                pyti.Text = dialog.SelectedPath;
            }
        }

        private void Backup_Click(object sender, RoutedEventArgs e)
        {
            //if (pyti.Text == string.Empty)
            //{
            //}
            //else
            //{

                BckpBrao(@"Server=LAPTOP-02T55PA4\MSSQLSERVER01;Database=Salon;Trusted_Connection=True;MultipleActiveResultSets=true", "Salon", pyti.Text + "\\" + "Salon" + " " + DateTime.Now.ToString("dd.MM.yyyy--HH-mm-ss") + ".bak");
                //System.Windows.MessageBox.Show("hhhnhhhh");
            //}
        }

        private void Brow2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog odialog = new OpenFileDialog();

            odialog.Filter = "files (.bak)|.bak";
            odialog.Title = "Database restore";

            if (odialog.ShowDialog().ToString().Equals("OK"))
            {
                pyt.Text = odialog.FileName;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string database = con.Database.ToString();
            con.Open();

            try
            {
                string str1 = string.Format("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                SqlCommand cmd1 = new SqlCommand(str1, con);
                cmd1.ExecuteNonQuery();

                string str2 = "USE MASTER RESTORE DATABASE [" + database + "] FROM DISK= '" + pyt.Text + "' WITH REPLACE;";
                SqlCommand cmd2 = new SqlCommand(str2, con);
                cmd2.ExecuteNonQuery();

                string str3 = "ALTER DATABASE [" + database + "] SET MULTI_USER";
                SqlCommand cmd3 = new SqlCommand(str3, con);
                cmd3.ExecuteNonQuery();
                con.Close();
            }
            catch
            {

            }
        }

       
    }
}
