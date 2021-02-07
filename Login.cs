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
    public partial class Login : Form
    {
        SqlConnection sqlConnection;
        public Login()
        {

            InitializeComponent();
        }
        private async void DataBaseLoad()
        {


#if DEBUG
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + GetDirectory(2) + @"\Database1.mdf;Integrated Security=True;" +
    "MultipleActiveResultSets=True"; //MARS активирован, многопоточное подключение
             MessageBox.Show("DEBUG. CURR : " + Directory.GetCurrentDirectory() + "\n" + "PATH : " + GetDirectory(3) + @"\Database1.mdf");
#else
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Directory.GetCurrentDirectory() + @"\Database1.mdf;Integrated Security=True;" +
    "MultipleActiveResultSets=True"; //MARS активирован, многопоточное подключение
            MessageBox.Show("RELEASE. CURR : " + Directory.GetCurrentDirectory() + "\n" + "PATH : " +  Directory.GetCurrentDirectory() + @"\Database1.mdf" );
#endif


            //связь с базой
            sqlConnection = new SqlConnection(connectionString);
            try
            {
                await sqlConnection.OpenAsync(); //доступ к базе данных в асинхронном режиме
            }
            catch (Exception ex)
            { 
                if (customProgressBar1.Value==100)
                MessageBox.Show("Подождите, база данных почему-то не успела загрузиться.. Если ошибка не исчезнет, перезапустите программу");
            }
            //dataBaseLoading();


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


        delegate void ProgressBarIncrease(int inc); //ссылка пустая, может взять к себе loadingIncrease

        private void loadingInc(int inc)
        {
            Invoke(new ProgressBarIncrease(loadingIncrease), new object[] { inc }); //позволяет работать с объектами из других потоков
        }

        private void loadingIncrease(int impact) 
        {
            /*
            int percent = (int)(((double)(customProgressBar1.Value - customProgressBar1.Minimum) /
(double)(customProgressBar1.Maximum - customProgressBar1.Minimum)) * 100);
            using (Graphics gr = customProgressBar1.CreateGraphics())
            {
                gr.DrawString(percent.ToString() + "%",
                    SystemFonts.DefaultFont,
                    Brushes.Black,
                    new PointF(customProgressBar1.Width / 2 - (gr.MeasureString(percent.ToString() + "%",
                        SystemFonts.DefaultFont).Width / 2.0F),
                    customProgressBar1.Height / 2 - (gr.MeasureString(percent.ToString() + "%",
                        SystemFonts.DefaultFont).Height / 2.0F)));

               
            }


            */

            if (customProgressBar1.Value <= (100 - impact))
            {
                customProgressBar1.Value += impact;
              // if (customProgressBar1.Value==100)
              //      System.Threading.Thread.Sleep(100);
                
              //  customProgressBar1.Text = "База данных загружается...";
                
            }
            else
            {

                customProgressBar1.Value = 100;
                
                
                //statusLabel.Text = "База данных готова.";
              
            }

            if (customProgressBar1.Value == 100)
            {
                button1.BackColor = Color.FromArgb(192,192,255);
                button1.Text = "Авторизоваться";
                button1.Enabled = true;
            
            }
            else
            {
                button1.Text = "База данных загружается...";
            }  
        }


        private async void login_Load(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.loginTextBox, "Логином является ID врача");
            toolTip1.SetToolTip(this.passwordTextBox, "Пароль врача");
            toolTip1.SetToolTip(this.pictureBox2, "Пароль врача");
            toolTip1.SetToolTip(this.pictureBox3, "Логином является ID врача");
            toolTip1.SetToolTip(this.pictureBox4, "Видимость пароля");
            toolTip1.SetToolTip(this.customProgressBar1, "Процесс загрузки базы данных");

            DataBaseLoad();
            await Task.Run(() => {
                dataBaseLoading();
            });

            //  statusLabel.Parent = progressBar1;
            //  statusLabel.BackColor = Color.Transparent;
            //this.statusLabel.BackColor = System.Drawing.Color.Transparent;

        }

        private async void dataBaseLoading()
        {
            Random d6 = new Random();
            Boolean loading = true;
            while (loading)
            {
                 System.Threading.Thread.Sleep(1000);
                try
                {
                    int userIdd = 1;
                    SqlDataReader sqlReader = null;
                    SqlCommand command = new SqlCommand("SELECT * FROM [UsersData] WHERE [ID]=@ID", sqlConnection);
                    command.Parameters.AddWithValue("ID", Convert.ToInt32(1));
                    sqlReader = await command.ExecuteReaderAsync();
                    if (sqlReader != null)
                        while (await sqlReader.ReadAsync())
                        {
                            //command.Parameters.AddWithValue("Password", loginTextBox.Text);
                            string pass = Convert.ToString(sqlReader["Password"]);
                            if (pass.Trim() == passwordTextBox.Text.Trim()) //удаление лишних пробелов из пароля и проверка пароля
                            {
                                userIdd = Convert.ToInt32(sqlReader["ID"]); //запоминаем айди пользователя для определения пользователя, а userType для выдачи привелегий в приложении
                            }
                            loading = false; loadingInc(100);
                        }
                     //если доходит до этой строки без ошибок, заканчивает загрузку
                }
                catch (Exception ex)
                {
                    if (customProgressBar1.Value<94)
                    loadingInc(d6.Next(1,6));
                }
              










            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string userType = "";
            int userId = 0;
            SqlDataReader sqlReader = null;
            Boolean access = false;
            Boolean error = false;
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [UsersData] WHERE [ID]=@ID", sqlConnection);
                command.Parameters.AddWithValue("ID", Convert.ToInt32(Convert.ToString(loginTextBox.Text)));
                sqlReader = await command.ExecuteReaderAsync();
                if (sqlReader != null) //03.06.2020
                    while (await sqlReader.ReadAsync())
                    {

                        //command.Parameters.AddWithValue("Password", loginTextBox.Text);
                        string pass = Convert.ToString(sqlReader["Password"]);

                        if (pass.Trim() == passwordTextBox.Text.Trim()) //удаление лишних пробелов из пароля и проверка пароля
                        {
                            access = true;
                            userType = Convert.ToString(sqlReader["Type"]);
                            userType = userType.Trim();
                            userId = Convert.ToInt32(sqlReader["ID"]); //запоминаем айди пользователя для определения пользователя, а userType для выдачи привелегий в приложении

                        }
                        else
                        {
                            access = false;
                            MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
            }
            catch (System.FormatException ex) {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

          /*  catch (Exception ex)
            {
                MessageBox.Show("Происходит подключение базы данных, пожалуйста попробуйте попытку позже. Обычно подключение занимает не больше 20 секунд.", ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               // MessageBox.Show(ex.Message);
                error = true;
                //MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/

            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();


            }

            if (access && !error)
            {

                Hide();
                Menu menuForm = new Menu(userType, userId);

                menuForm.ShowDialog();
                Close();
            }
            //else   MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }


        private void button2_Click(object sender, EventArgs e)

        { 
            
           // statusLabel.BackColor = Color.Transparent;
            

            

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            loadingIncrease(20);
        }

        private void button1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
           fireBirdExecuter.testFDB();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            passwordTextBox.UseSystemPasswordChar = !passwordTextBox.UseSystemPasswordChar;
        }

        private void customProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            
          
        }

        private void passwordTextBox_Enter(object sender, EventArgs e)
        {

        }

        private void passwordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                button1_Click(sender, e);
        }
    }
}
