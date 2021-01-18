using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;
using MessageBox = System.Windows.MessageBox;

namespace Praktika
{
    /// <summary>
    /// Логика взаимодействия для ForAdmin.xaml
    /// </summary>
    public partial class ForAdmin : Window
    {
        SqlDataAdapter sqlDataAdapter;
        DataTable dataTable;
        string connectionString;
        public ForAdmin()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        }
        
        
        private void Guests_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = null;
            Clear.Visibility = Visibility.Hidden;
            DeleteButton.Visibility = Visibility.Visible;
            UpdateButton.Visibility = Visibility.Visible;
            OtchetButton.Visibility = Visibility.Hidden;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                string select = "SELECT * FROM Арендодатель";
                dataTable = new DataTable();
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(select, sqlConnection);
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                sqlDataAdapter.Fill(dataTable);
                eGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            LogIn l = new LogIn();
            l.Show();
            this.Close();

        }
        private void Employeers_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = null;
            Clear.Visibility = Visibility.Hidden;
            DeleteButton.Visibility = Visibility.Visible;
            UpdateButton.Visibility = Visibility.Visible;
            OtchetButton.Visibility = Visibility.Hidden;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                string select = "SELECT * FROM Сотрудник";
                dataTable = new DataTable();
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(select, sqlConnection);
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                sqlDataAdapter.Fill(dataTable);
                eGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }
        private void CreateOtchet_Click(object sender, RoutedEventArgs e)
        {
            List<Nomer> l = new List<Nomer>();
            try
            {
                Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = app.Documents.Add();
      
                for (int i = 0 ; i != eGrid.Items.Count -1 ; ++i)
                {
                    DataRowView x = eGrid.Items[i] as DataRowView;
                    string firstPart = x[4].ToString().Split(' ')[0];
                    string SecondPart = firstPart +" "+ x[4].ToString().Split(' ')[1];
                    l.Add(new Nomer(Convert.ToInt32(x[0]), Convert.ToDouble(x[1]), Convert.ToString(x[2]), Convert.ToString(x[3]), Convert.ToDateTime(SecondPart)));
                    if(i==0)
                    {
                        var pText = doc.Paragraphs.Add();
                        pText.Range.Text = "\t\t\t\tОтчет по занятым и забронированнм номерам\n";
                        var pText2 = doc.Paragraphs.Add();
                        pText2.Range.Text += $"Общее количество занятых или забронированных номеров:{eGrid.Items.Count}\n";
                        var pText3 = doc.Paragraphs.Add();
                        pText3.Range.Text = $"Занятые или забронированные номера:\n";
                    }
                   
                    var pText4 = doc.Paragraphs.Add();
                    pText4.Range.Text = $"№ номера:{Convert.ToInt32(x[0])}\n";
                    var pText5 = doc.Paragraphs.Add();
                    pText5.Range.Text = $"Статус:{Convert.ToString(x[3])}\n";
                    var pText6 = doc.Paragraphs.Add();
                    pText6.Range.Text = $"До какого числа закрыт для бронирования:{Convert.ToDateTime(SecondPart)}\n";
                }
                
                XmlSerializer fo = new XmlSerializer(typeof(List<Nomer>));
                using (FileStream fs = new FileStream("./request.xml", FileMode.OpenOrCreate))
                {
                    fo.Serialize(fs, l);
                }
                MessageBox.Show("Отчет Word успешно составлен, также составлен отчет в xml формате, он находится в bin проекта под названием reauest1.xml.");
                app.Visible = true;



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Zakaz_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = null;
            Clear.Visibility = Visibility.Hidden;
            DeleteButton.Visibility = Visibility.Visible;
            UpdateButton.Visibility = Visibility.Visible;
            OtchetButton.Visibility = Visibility.Hidden;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                string select = "SELECT * FROM Orders";
                dataTable = new DataTable();
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(select, sqlConnection);
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                sqlDataAdapter.Fill(dataTable);
                eGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }
        private void Nomera_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = null;
            DeleteButton.Visibility = Visibility.Visible;
            UpdateButton.Visibility = Visibility.Visible;
            OtchetButton.Visibility = Visibility.Hidden;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                string select = "SELECT * FROM Nomer";
                dataTable = new DataTable();
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(select, sqlConnection);
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                sqlDataAdapter.Fill(dataTable);
                eGrid.ItemsSource = dataTable.DefaultView;
                Clear.Visibility = Visibility.Visible;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }
        private void Otchet_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = null;
            DeleteButton.Visibility = Visibility.Hidden;
            UpdateButton.Visibility = Visibility.Hidden;
            Clear.Visibility = Visibility.Hidden;
            OtchetButton.Visibility = Visibility.Visible;
            
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                string select = "SELECT Nomer.id,Square, Name, Status, End_Time FROM Nomer JOIN Type ON(Nomer.id_Type = Type.id) WHERE Status <> 'Free' ORDER BY Name";
                dataTable = new DataTable();
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(select, sqlConnection);
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                sqlDataAdapter.Fill(dataTable);
                eGrid.ItemsSource = dataTable.DefaultView;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateDB();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateDB()
        {
            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(sqlDataAdapter);
            sqlDataAdapter.Update(dataTable);
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
        private void ClearNomer_Click(object sender, RoutedEventArgs e)
        {
            if (eGrid.SelectedItems != null)
            {
                for (int i = 0; i < eGrid.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = eGrid.SelectedItems[i] as DataRowView;

                    if (datarowView != null)
                    {
                        try
                        {
                            
                            SqlCommand cmd;
                            SqlConnection sqlConnection = new SqlConnection(connectionString);
                            cmd = new SqlCommand();
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.CommandText = $"UPDATE Nomer SET Status ='Free', End_Time = null  WHERE id = {eGrid.SelectedIndex + 1}";
                            cmd.Connection = sqlConnection;
                            sqlConnection.Open();
                            cmd.ExecuteNonQuery();
                            sqlConnection.Close();
                            MessageBox.Show("Успешно.");
                            UpdateDB();
                            sqlConnection = new SqlConnection(connectionString);
                            string select = "SELECT * FROM Nomer";
                            dataTable = new DataTable();
                            sqlConnection.Open();
                            SqlCommand sqlCommand = new SqlCommand(select, sqlConnection);
                            sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                            sqlDataAdapter.Fill(dataTable);
                            eGrid.ItemsSource = dataTable.DefaultView;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                       

                    }
                }

            }
            
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (eGrid.SelectedItems != null)
            {
                for (int i = 0; i < eGrid.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = eGrid.SelectedItems[i] as DataRowView;

                    if (datarowView != null)
                    {
                        DataRow dataRow = (DataRow)datarowView.Row;

                        dataRow.Delete();

                    }
                }

            }
            UpdateDB();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("HelpPls.chm");
        }
    }
}
