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
    public partial class Users : Form
    {
        SqlConnection sqlConnection;
        String userType = "";
        int userId = 1;
        public Users(String userType, int userId)
        {
            this.userType = userType;
            this.userId = userId;
            InitializeComponent();
        }
        
        private string GetDirectory(int levelUp) //Сломается если количество переходов превысит количество папок
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


        private async void UpdateUserTab()
        {
            listBox1.Items.Clear();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [UsersData]", sqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["ID"]) + "   " + Convert.ToString(sqlReader["Name"]) + "   " + Convert.ToString(sqlReader["Password"]) + "   " + Convert.ToString(sqlReader["Type"]) );

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
            UpdateUserTab();
        }

        private void Users_Load(object sender, EventArgs e)
        {
            DataBaseLoad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) { sqlConnection.Close(); }
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            bool userType=false;
            if (label6.Visible)
                label6.Visible = false;

            if (!string.IsNullOrEmpty(insertNameTextBox.Text) && !string.IsNullOrWhiteSpace(insertNameTextBox.Text) &&
                !string.IsNullOrEmpty(insertIdTextBox.Text) && !string.IsNullOrWhiteSpace(insertIdTextBox.Text) &&
                !string.IsNullOrEmpty(insertPasswordTextBox.Text) && !string.IsNullOrWhiteSpace(insertPasswordTextBox.Text) &&
                !string.IsNullOrEmpty(insertTypeComboBox.Text) && !string.IsNullOrWhiteSpace(insertTypeComboBox.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [UsersData] (ID, Name, Password, Type) VALUES(@ID, @Name, @Password, @Type)", sqlConnection);
                command.Parameters.AddWithValue("ID", insertIdTextBox.Text);
                command.Parameters.AddWithValue("Name", insertNameTextBox.Text);
                command.Parameters.AddWithValue("Password", insertPasswordTextBox.Text);
                switch (insertTypeComboBox.Text)
                {
                    case "Врач-оптометрист":
                    case "Врач-исследователь":
                    case "Администратор":
                        userType = true;
                    break;
                    default:
                        MessageBox.Show("Ошибка - неверный тип пользователя");
                        userType = false;
                        break;
                }
                command.Parameters.AddWithValue("Type", insertTypeComboBox.Text);



                if (userType)
                {
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
                    label6.Text = "Ошибка.  Все поля должны быть заполнены.";
                }
            }
            UpdateUserTab();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void вернутьсяВМенюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Menu menuForm = new Menu(userType, userId);
            menuForm.ShowDialog();
            Close();
        }

        private void подключениеБазыДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataBaseLoad();
        }

        private void расположениеБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
#if DEBUG

            MessageBox.Show("DEBUG. CURR : " + Directory.GetCurrentDirectory() + "\n" + "PATH : " + GetDirectory(3) + @"\Database1.mdf");
#else
            
            MessageBox.Show("RELEASE. CURR : " + Directory.GetCurrentDirectory() + "\n" + "PATH : " +  Directory.GetCurrentDirectory() + @"\Database1.mdf" );
#endif
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateUserTab();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;

            if (!string.IsNullOrEmpty(deleteIdTextBox.Text) && !string.IsNullOrWhiteSpace(deleteIdTextBox.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [UsersData] WHERE [ID]=@ID", sqlConnection);
                command.Parameters.AddWithValue("ID", deleteIdTextBox.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Ошибка. Id должен быть заполнен только числом.";
            }
            UpdateUserTab();

        }

       

        private async void button2_Click_1(object sender, EventArgs e)
        {
            Boolean userType = false;
            if (label8.Visible)
                label8.Visible = false;
            if (!string.IsNullOrEmpty(updateIdTextBox.Text) && !string.IsNullOrWhiteSpace(updateIdTextBox.Text) &&
                    !string.IsNullOrEmpty(updateNameTextBox.Text) && !string.IsNullOrWhiteSpace(updateNameTextBox.Text) &&
                        !string.IsNullOrEmpty(updatePasswordTextBox.Text) && !string.IsNullOrWhiteSpace(updatePasswordTextBox.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [UsersData] SET [Name]=@Name, [Password]=@Password, [Type]=@Type  WHERE[ID]=@ID", sqlConnection);
                //command.Parameters.AddWithValue("Id",updateIdTextBox.Text);
                command.Parameters.AddWithValue("Name", updateNameTextBox.Text);
                command.Parameters.AddWithValue("ID", updateIdTextBox.Text);
                command.Parameters.AddWithValue("Password", updatePasswordTextBox.Text);

                switch (insertTypeComboBox.Text)
                {
                    case "Врач-оптометрист":
                    case "Врач-исследователь":
                    case "Администратор":
                        userType = true;
                        break;
                    default:
                        MessageBox.Show("Ошибка - неверный тип пользователя");
                        userType = false;
                        break;
                }
                command.Parameters.AddWithValue("Type", updateTypeComboBox.Text);




                if (userType)
                {
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
                    label6.Text = "Ошибка.  Все поля должны быть заполнены.";
                }
            }
            UpdateUserTab();

        }
    }
}
