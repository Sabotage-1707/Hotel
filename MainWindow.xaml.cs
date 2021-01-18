using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Praktika
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString;
        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
           
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
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            LogIn l = new LogIn();
            l.Show();
            this.Close();

        }
        
        public void Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                DataTable Users = Select($"SELECT * FROM Арендодатель WHERE Phone = '{Phone.Text}'");
                if(Users.Rows.Count == 0)
                {
                    Regex r = new Regex(@"^(\s*)?(\+)?([- _():=+]?\d[- _():=+]?){10,14}(\s*)?$");
                    Regex r2 = new Regex(@"^([А-Я]{1}[а-яё]{1,23}|[A-Z]{1}[a-z]{1,23})$");
                    if (r.IsMatch(Phone.Text)==true && Pass.Password == Pass2.Password && r2.IsMatch(Surname.Text) && r2.IsMatch(Name.Text) && r2.IsMatch(Ott.Text))
                    {
                        SqlCommand cmd;
                        SqlConnection sqlConnection = new SqlConnection(connectionString);
                        cmd = new SqlCommand();
                        cmd.CommandType = System.Data.CommandType.Text;

                        cmd.CommandText = string.Format("INSERT INTO Арендодатель(Фамилия, Имя, Отчество, Phone, Password) VALUES ('{0}','{1}','{2}','{3}','{4}')", Surname.Text,Name.Text, Ott.Text, Phone.Text, Pass.Password);
                        cmd.Connection = sqlConnection;
                        sqlConnection.Open();
                        cmd.ExecuteNonQuery();
                        sqlConnection.Close();
                        MessageBox.Show("Вы успешно зарегестрировались,\nпожалуйста запомните свой пароль.");
                        LogIn logIn = new LogIn();
                        logIn.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show($"Данные были введены не верно!");
                    }

                }
                else
                {
                    MessageBox.Show("Пользователь с таким номером уже зрегистрирован, пожалуйста выберете другой номер.");
                }
                
            }
            catch
            {
                MessageBox.Show($"Данные были введены не верно!");
            }


        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("HelpPls.chm");
        }
    }
}
