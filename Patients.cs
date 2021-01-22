using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;



namespace BD_test
{
    public partial class Patients : Form
    {
        String userType = "";
        SqlConnection sqlConnection;
        int userId = 1;
        public Patients(String userType, int userId)
        {
           
            this.userType = userType;
            this.userId = userId;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        /// <summary>
        /// Возвращает путь в виде строки
        /// </summary>
        /// <param name="levelUp">Указывается количество переходов вверх</param>
        /// <returns>String путь</returns>
        private string GetDirectory (int levelUp) //Сломается если количество переходов превысит количество папок
        {          
            string dir = Directory.GetCurrentDirectory();
            string trimDir = dir;
            for (int i = 0; i < levelUp; i++)
            {
                int index = trimDir.LastIndexOf(@"\");
                trimDir = trimDir.Remove(index, trimDir.Length - index);
            }
            if (trimDir == "")
            {
                return dir;
            }
            else
            {
                return trimDir;
            }
            
        }


        private async void DataBaseLoad()
        {


#if DEBUG
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + GetDirectory(2) + @"\Database1.mdf;Integrated Security=True";
           // MessageBox.Show("DEBUG. CURR : " + Directory.GetCurrentDirectory() + "\n" + "PATH : " + GetDirectory(3) + @"\Database1.mdf");
#else
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Directory.GetCurrentDirectory() + @"\Database1.mdf;Integrated Security=True";
            //MessageBox.Show("RELEASE. CURR : " + Directory.GetCurrentDirectory() + "\n" + "PATH : " +  Directory.GetCurrentDirectory() + @"\Database1.mdf" );
#endif


            //связь с базой
            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync(); //доступ к базе данных в асинхронном режиме
            UpdatePatientTab();
           // DataBaseServitor.DataTableInfusion(dataGridView1, sqlConnection, "PatientsData");
        }
        private void Form1_Load(object sender, EventArgs e) //тут был Async
        {

         DataBaseLoad();
           
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) { sqlConnection.Close(); }
        } //закрытие подключения на всякий подключение, что бы не было утечки

        private async void button1_Click(object sender, EventArgs e)
        {
            if (label6.Visible)
                label6.Visible = false;

            if (!string.IsNullOrEmpty(insertNameTextBox.Text) && !string.IsNullOrWhiteSpace(insertNameTextBox.Text) &&
                !string.IsNullOrEmpty(insertMedBookTextBox.Text) && !string.IsNullOrWhiteSpace(insertMedBookTextBox.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [PatientsData] (MedBook, Name , Date) VALUES(@MedBook, @Name, @Date)", sqlConnection);
                command.Parameters.AddWithValue("Name", insertNameTextBox.Text);
                command.Parameters.AddWithValue("MedBook", insertMedBookTextBox.Text);
                command.Parameters.AddWithValue("Date", insertTimePicker.Value);



                
               try
                {
                    await command.ExecuteNonQueryAsync();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                {
                label6.Visible = true;
                label6.Text = "Ошибка.  Поля 'ФИО' и 'Номер медицинской книжки' должны быть заполнены.";
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private async void UpdatePatientTab() {
            listBox1.Items.Clear();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [PatientsData]", sqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["MedBook"]) + "   "  + Convert.ToString(sqlReader["Name"]) +  "   " +  Convert.ToString(sqlReader["Date"]));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }


        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e) 
        {
            UpdatePatientTab();
           // DataBaseServitor.DataTableInfusion(dataGridView1, sqlConnection, "PatientsData");
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;

            if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [PatientsData] WHERE [MedBook]=@MedBook", sqlConnection);
                command.Parameters.AddWithValue("MedBook", textBox6.Text);
            
                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Ошибка. Id должен быть заполнен.";
            }
            
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (label8.Visible)
                label8.Visible = false;
            if (!string.IsNullOrEmpty(updateMedBookTextBox.Text) && !string.IsNullOrWhiteSpace(updateMedBookTextBox.Text) &&
                    !string.IsNullOrEmpty(updateNameTextBox.Text) && !string.IsNullOrWhiteSpace(updateNameTextBox.Text) &&
                        !string.IsNullOrEmpty(updateNameTextBox.Text) && !string.IsNullOrWhiteSpace(updateNameTextBox.Text)) 
            {
                SqlCommand command = new SqlCommand("UPDATE [PatientsData] SET [Name]=@Name, [Date]=@Date  WHERE[MedBook]=@MedBook", sqlConnection);
                //command.Parameters.AddWithValue("Id",updateIdTextBox.Text);
                command.Parameters.AddWithValue("Name",updateNameTextBox.Text);
                command.Parameters.AddWithValue("MedBook", updateMedBookTextBox.Text);
                command.Parameters.AddWithValue("Date", updateTimePicker.Value);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty (updateNameTextBox.Text)&&!string.IsNullOrWhiteSpace(updateNameTextBox.Text))
            {
                label8.Visible = true;
                label8.Text = "Ошибка.ен быть заполнен.";
            }
            else
            {
                label8.Visible = true;
                label8.Text = "Ошибка. Поля 'ФИО' и 'Номер медицинской книжки' должны быть заполнены.";
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            UpdatePatientTab();
        }

        private void подключениеБазыДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataBaseLoad();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            insertDateTextBox.Text = Convert.ToString(insertTimePicker.Value);
        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            updateDateTextBox.Text = Convert.ToString(updateTimePicker.Value);
        }

        private void updateMedBookTextBox_KeyPress(object sender, KeyPressEventArgs e) //Андрюхин метод для блокировки букв
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                // цифра
                return;
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                if (updateMedBookTextBox.Text.Length > 0)
                {
                    updateMedBookTextBox.Text = updateMedBookTextBox.Text.Remove(updateMedBookTextBox.Text.Length - 1);
                }
                updateMedBookTextBox.SelectionStart = updateMedBookTextBox.Text.Length;
            }
        }

        private void insertMedBookTextBox_KeyPress(object sender, KeyPressEventArgs e) //блокировка буков
        {

            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                
                // цифра
                return;
            }

             if (e.KeyChar == (char)Keys.Back)  //блокировка всего кроме backspace
            {
                 if (insertMedBookTextBox.Text.Length > 0)
                 {
                     insertMedBookTextBox.Text = insertMedBookTextBox.Text.Remove(insertMedBookTextBox.Text.Length - 1);
                 }
                 insertMedBookTextBox.SelectionStart = insertMedBookTextBox.Text.Length;
             }
            e.Handled = true; 
        }

        private void вернутьсяВМенюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Menu menuForm = new Menu(userType, userId);
            menuForm.ShowDialog();
            Close();
        }

        private void расположениеБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
#if DEBUG
            
            MessageBox.Show("DEBUG. CURR : " + Directory.GetCurrentDirectory() + "\n" + "PATH : " + GetDirectory(3) + @"\Database1.mdf");
#else
            
            MessageBox.Show("RELEASE. CURR : " + Directory.GetCurrentDirectory() + "\n" + "PATH : " +  Directory.GetCurrentDirectory() + @"\Database1.mdf" );
#endif
        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void инструментыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void updateDateTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void updateNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void insertDateTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void insertMedBookTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void insertNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
