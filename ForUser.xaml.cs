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
    /// Логика взаимодействия для ForUser.xaml
    /// </summary>
    public partial class ForUser : Window
    {
        string connectionString;
        string Passwo="";
        string Phone = "";
        double price = 0;
        double stavka = 0;
        string ID;
        DateTime date = DateTime.Now;
        public ForUser(string id, string phone , string pass)
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            Passwo = pass;
            Phone = phone;
            ID = id;
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
        public void UpdateData()
        {
            try
            {

                DataTable Users = Select($"SELECT * FROM Nomer WHERE id ={Convert.ToInt32(NomerList.SelectedItem)}");
                SqlCommand cmd;
                date = date.AddHours(6);
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = $"UPDATE Nomer SET Status ='Booked', End_Time ='{date}'  WHERE id = {Convert.ToInt32(NomerList.SelectedItem)}";
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();

                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                
                cmd.CommandText = string.Format("INSERT INTO Orders(Date,id_Nomer, id_Арендодателя) VALUES ('{0}',{1},{2})", date, Convert.ToInt32(NomerList.SelectedItem), ID);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch
            {
                MessageBox.Show($"Данные были введены не верно!");
            }


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Period.Visibility == Visibility.Hidden||Time.Visibility == Visibility.Hidden||AcceptPass.Visibility == Visibility.Hidden||Password.Visibility == Visibility.Hidden)
            {
                MessageBox.Show("Не все данные были выбраны или введены!");
            }
            else
            {
                if (Convert.ToString(Password.Password) == Passwo)
                {
                    MessageBox.Show($"Вы успешно забронировали номер {NomerList.SelectedItem},\nбронь действует в течении 6 часов,\nцена составит: {price}$ \nждем вас в нашем отеле!");
                    UpdateData();
                    LogIn l = new LogIn();
                    l.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пароль введен не верно");
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            DataTable types = Select("SELECT Name FROM Type");
            for(int i = 0; i!=types.Rows.Count; ++i)
            {
                TypeNomerList.Items.Add(Convert.ToString(types.Rows[i][0]));
            }
            Time.Items.Add("1 день");
            Time.Items.Add("2 дня");
            Time.Items.Add("3 дня");
            Time.Items.Add("4 дня");
            Time.Items.Add("5 дней");
            Time.Items.Add("6 дней");
            Time.Items.Add("1 неделя"); 
            Time.Items.Add("8 дней");
            Time.Items.Add("9 дней");
            Time.Items.Add("10 дней");
            Time.Items.Add("11 дней");
            Time.Items.Add("12 дней");
            Time.Items.Add("13 дней");
            Time.Items.Add("2 недели");

        }

        private void TypeNomerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string str = "";
            switch(TypeNomerList.SelectedItem.ToString())
            {
                case "Президентский":
                    str = "SELECT * FROM Nomer WHERE id_Type = 1";
                    stavka = 1000;
                    break;
                case "Люкс":
                    str = "SELECT * FROM Nomer WHERE id_Type = 2";
                    stavka = 550;
                    break;
                case "Объединенный":
                    str = "SELECT * FROM Nomer WHERE id_Type = 3";
                    stavka = 350;
                    break;
                case "Стандартный":
                    str = "SELECT * FROM Nomer WHERE id_Type = 4";
                    stavka = 150;
                    break;
                default:
                    MessageBox.Show("Ошибка при выборе номера!");
                    break;
            }

            
            NomerList.Items.Clear();
            DataTable nomera = Select(str);
            for (int i = 0; i != nomera.Rows.Count; ++i)
            {
                NomerList.Items.Add(Convert.ToString(nomera.Rows[i][0]));
            }
            VibNomer.Visibility = Visibility.Visible;
            NomerList.Visibility = Visibility.Visible;
            NomerList.SelectedItem = null;
            Period.Visibility = Visibility.Hidden;
            Time.Visibility = Visibility.Hidden;
            AcceptPass.Visibility = Visibility.Hidden;
            Password.Visibility = Visibility.Hidden;
        }

        private void NomerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(NomerList.SelectedItem != null)
            {
                DataTable nomera = Select($"SELECT Status FROM Nomer WHERE id = {Convert.ToInt32(NomerList.SelectedItem.ToString())}");
                string path = "";
                bool x = false;
                switch (Convert.ToString(nomera.Rows[0][0]))
                {
                    case "Free":
                        path = "Images/Green.png";
                        x = true;
                        break;

                    case "Busy":
                        path = "Images/Krasniy.png";
                        break;
                    case "Booked":
                        path = "Images/Krasniy.png";
                        break;
                        
                    default:
                        path = "Images/Krasniy.png";
                        break;

                }

                BitmapImage myBitmapImage = new BitmapImage();
                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri($"pack://application:,,,/{path}");
                myBitmapImage.EndInit();
                State.ImageSource = myBitmapImage;

                if (x == true)
                {
                    Period.Visibility = Visibility.Visible;
                    Time.Visibility = Visibility.Visible;
                    Ellipse.Visibility = Visibility.Visible;
                }
                else
                {
                    Period.Visibility = Visibility.Hidden;
                    Time.Visibility = Visibility.Hidden;
                    Ellipse.Visibility = Visibility.Visible;
                    AcceptPass.Visibility = Visibility.Hidden;
                    Password.Visibility = Visibility.Hidden;
                }

            }
            
            

           
            
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            LogIn l = new LogIn();
            l.Show();
            this.Close();

        }
        private void Time_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (Time.SelectedItem.ToString())
            {
                case "1 день":
                    price = stavka * 1;
                    date = date.AddDays(1);
                    break;
                case "2 дня":
                    price = stavka * 2;
                    date = date.AddDays(2);
                    break;

                case "3 дня":
                    price = stavka * 3;
                    date = date.AddDays(3);
                    break;

                case "4 дня":
                    price = stavka * 4;
                    date = date.AddDays(4);
                    break;
                case "5 дней":
                    price = stavka * 5;
                    date = date.AddDays(5);
                    break;

                case "6 дней":
                    price = stavka * 6;
                    date = date.AddDays(6);
                    break;
                case "1 неделя":
                    price = stavka * 6.5;
                    date = date.AddDays(7);
                    break;
                case "8 дней":
                    price = stavka * 7;
                    date = date.AddDays(8);
                    break;
                case "9 дней":
                    price = stavka * 8;
                    date = date.AddDays(9);
                    break;
                case "10 дней":
                    price = stavka * 9;
                    date = date.AddDays(10);
                    break;
                case "11 дней":
                    price = stavka * 10;
                    date = date.AddDays(11);
                    break;
                case "12 дней":
                    price = stavka * 11;
                    date = date.AddDays(12);
                    break;
                case "13 дней":
                    price = stavka * 12;
                    date = date.AddDays(13);
                    break;
                case "2 недели":
                    price = stavka * 12.5;
                    date = date.AddDays(14);
                    break;

            }
            AcceptPass.Visibility = Visibility.Visible;
            Password.Visibility = Visibility.Visible;
            Btn.Visibility = Visibility.Visible;

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("HelpPls.chm");
        }
    }
}
