using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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

namespace Praktika
{
    /// <summary>
    /// Логика взаимодействия для LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        string connectionString;
        public LogIn()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            MainWindow f = new MainWindow();
            f.Show();
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool x = true;
            DataTable dt_User = Select("SELECT id, Phone, Password From Арендодатель");
            if (Login.Text == "Loli" && Password.Password == "12345678")
            {
                ForAdmin f = new ForAdmin();
                f.Show();
                this.Close();
                x = false;
            }
            for (int i = 0; i != dt_User.Rows.Count; ++i)
            {

                if (Login.Text == Convert.ToString(dt_User.Rows[i][1]) && Password.Password == Convert.ToString(dt_User.Rows[i][2]))
                {
                    ForUser m = new ForUser(Convert.ToString(dt_User.Rows[i][0]), Login.Text, Password.Password);
                    m.Show();
                    this.Close();
                    x = false;
                }

            }
            if (x == true)
                MessageBox.Show("Неверный логин или пароль");

        }
        public DataTable Select(string selectSQL)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(selectSQL, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception exec)
            {
                MessageBox.Show(exec.Message);
            }
            return dataTable;

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("HelpPls.chm");
        }
    }
}
