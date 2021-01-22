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
using System.Reflection;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office;
using System.Reflection;

namespace BD_test
{


    public partial class Examinations : Form
    {
        String userType = "";
        int userId = 0;
       public SqlConnection sqlConnection;
        public Examinations(String userType, int userId)
        {
            this.userType = userType;
            this.userId = userId;
            InitializeComponent();
        }

        private async void updateTextBox(TextBox name, String dataName, String rowName, String RowValue, String selectedRowName)
        { //selectedRowName это искомое поле, значение которого надо вывести в ТекстБокс. rowName это искомое поле, rowValue это значение искомого поля

            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [" + dataName + "] WHERE [" + rowName + "]=" + RowValue, sqlConnection);
            //command.Parameters.AddWithValue(rowName, selectedRowValue); //функция обновляет необходимый текстбокс необходим значением из Заданной строки Заданной таблицы с Заданным значением другой строки
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    name.Text = Convert.ToString(sqlReader[selectedRowName]);
                    name.Text = name.Text.Trim();
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

        private void updateComboBoxes()
        {
            
            updateCombobox(insertMedBookComboBox, "patientsData", "MedBook"); //обновление ComboBox`a, что бы можно было выбрать номер пациента из уже существующих в БД
            updateCombobox(updateMedBookComboBox, "patientsData", "MedBook");
            
            //  updateCombobox(MedBookValue1, "patientsData", "MedBook"); MedBookValue1.Text = ""; //обновление масок при поиске и отчете данных
            //  updateCombobox(MedBookValue2, "patientsData", "MedBook"); MedBookValue2.Text = "";
            // updateCombobox(MedBookValue1, "patientsData", "MedBook");
            // updateCombobox(MedBookValue1, "patientsData", "MedBook");
        }
        private async void updateExaminationsTab()
        {

            //examinationsDataTableAdapter.GetData();
            //database1DataSet.Clear();
            // this.examinationsDataTableAdapter.Fill(database1DataSet.ExaminationsData); //заливка таблицы данными
            //listBox1.Items.Clear(); //TODO: удалить ListBox как только DataGrid будет нормальной

            
            
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [ExaminationsData]", sqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + "   " + Convert.ToString(sqlReader["doctorID"]) + "   " + Convert.ToString(sqlReader["patientID"]) + "   " + (sqlReader["date"]) + "   " + Convert.ToString(sqlReader["p1"]) + "   " + Convert.ToString(sqlReader["p2"]) + "   " + Convert.ToString(sqlReader["avgP"]) + "   " + Convert.ToString(sqlReader["h1"]) + "   " + Convert.ToString(sqlReader["h2"]) + "   " + Convert.ToString(sqlReader["P"]) + "   " + Convert.ToString(sqlReader["H"]) + "   " + Convert.ToString(sqlReader["S"]) + "   " + Convert.ToString(sqlReader["comment"]));
                    // dataGridView1.Rows.Add(Convert.ToString(sqlReader["Id"]),Convert.ToString(sqlReader["doctorID"]),Convert.ToString(sqlReader["patientID"]),(sqlReader["date"]),Convert.ToString(sqlReader["p1"]),Convert.ToString(sqlReader["p2"]),Convert.ToString(sqlReader["avgP"]),Convert.ToString(sqlReader["h1"]),Convert.ToString(sqlReader["h2"]),Convert.ToString(sqlReader["P"]),Convert.ToString(sqlReader["H"]),Convert.ToString(sqlReader["S"]),Convert.ToString(sqlReader["comment"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                updateComboBoxes();  // заполняет комбобоксы из БД
                if (sqlReader != null)
                    sqlReader.Close();
            }

        }
       

        private async void updateCombobox(ComboBox name, string dataName, string rowName) //обновляет комбоБокс, использует имя комбобокса и имя таблицы из базы данных, которую необходимо вставить в качестве выбора в комбобоксе
        { // а еще юзает имя поля 
            List<string> collections = new List<string>();

            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [" + dataName + "]", sqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {

                    collections.Add(Convert.ToString(sqlReader[rowName]));

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

            name.DataSource = collections;
            //name.AutoCompleteSource = collections;


        }


        private void Examinations_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage1);
            if (userType == "Врач-исследователь")
            {
                
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Remove(tabPage3);
                tabControl1.TabPages.Remove(tabPage4);

            }else if (userType != "Администратор") 
                contextMenuStrip1.Items[1].Visible = false;
            checkBox1_CheckedChanged(sender, e);
            DBConnectionInitialization();
            
            //DataGrid загружает все столбцы, однако некоторые столбцы невидимы 



            /*
                        String[] releationshipCollection1 = new String[5] { "<", ">", "=", "<=", ">=" }; //коллекция для хранения =,<,>,>=,<=
                        String[] releationshipCollection2 = new String[5] { "<", ">", "=", "<=", ">=" }; //коллекция для хранения =,<,>,>=,<=
                        String[] releationshipCollection3 = new String[5] { "<", ">", "=", "<=", ">=" }; //коллекция для хранения =,<,>,>=,<=
                        String[] releationshipCollection4 = new String[5] { "<", ">", "=", "<=", ">=" }; //коллекция для хранения =,<,>,>=,<=
                        String[] releationshipCollection5 = new String[5] { "<", ">", "=", "<=", ">=" }; //коллекция для хранения =,<,>,>=,<=
                        String[] releationshipCollection6 = new String[5] { "<", ">", "=", "<=", ">=" }; //коллекция для хранения =,<,>,>=,<=
                        String[] releationshipCollection7 = new String[5] { "<", ">", "=", "<=", ">=" }; //коллекция для хранения =,<,>,>=,<=
                        String[] releationshipCollection8 = new String[5] { "<", ">", "=", "<=", ">=" }; //коллекция для хранения =,<,>,>=,<=

                        MedBookRelationShip1.DataSource = releationshipCollection1;
                        MedBookRelationShip2.DataSource = releationshipCollection2;
                        */
        }

        private async void DBConnectionInitialization() //Создает соединение с Базой Данных
        {


#if DEBUG
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + GetDirectory(2) + @"\Database1.mdf;Integrated Security=True;" +
    "MultipleActiveResultSets=True"; //MARS активирован, многопоточное подключение
            // MessageBox.Show("DEBUG. CURR : " + Directory.GetCurrentDirectory() + "\n" + "PATH : " + GetDirectory(3) + @"\Database1.mdf");
#else
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Directory.GetCurrentDirectory() + @"\Database1.mdf;Integrated Security=True;" +
    "MultipleActiveResultSets=True";
            //MessageBox.Show("RELEASE. CURR : " + Directory.GetCurrentDirectory() + "\n" + "PATH : " +  Directory.GetCurrentDirectory() + @"\Database1.mdf" );
#endif


            //связь с базой
            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync(); //доступ к базе данных в асинхронном режиме

            updateExaminationsTab();
           
            insertDocIDTextBox.Text = Convert.ToString(userId);
            updateDocIDTextBox.Text = Convert.ToString(userId);

            DataBaseServitor.DataTableInfusion(dataGridView1, sqlConnection);
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
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateExaminationsTab();
        }

        private void подключениеБазыДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBConnectionInitialization();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) { sqlConnection.Close(); }
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            if (label6.Visible)
                label6.Visible = false;

            if (!string.IsNullOrEmpty(insertSTextBox.Text) && !string.IsNullOrWhiteSpace(insertSTextBox.Text) &&
                !string.IsNullOrEmpty(insertMedBookComboBox.Text) && !string.IsNullOrWhiteSpace(insertMedBookComboBox.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [ExaminationsData] (doctorID, patientID, date, p1, p2, avgP, h1, h2, P, H, S, comment) VALUES(@doctorID, @patientID, @date, @p1, @p2, @avgP, @h1, @h2, @P, @H, @S, @comment)", sqlConnection);
                command.Parameters.AddWithValue("doctorID", Convert.ToInt32(insertDocIDTextBox.Text));
                command.Parameters.AddWithValue("patientID", Convert.ToInt32(insertMedBookComboBox.Text));
                command.Parameters.AddWithValue("date", insertTimePicker.Value);
                command.Parameters.AddWithValue("p1", float.Parse(insertP1TextBox.Text));
                command.Parameters.AddWithValue("p2", float.Parse(insertP2TextBox.Text));
                command.Parameters.AddWithValue("avgP", float.Parse(insertAvgPTextBox.Text));
                command.Parameters.AddWithValue("h1", float.Parse(insertH1TextBox.Text));
                command.Parameters.AddWithValue("h2", float.Parse(insertH2TextBox.Text));
                command.Parameters.AddWithValue("P", float.Parse(insertPTextBox.Text));
                command.Parameters.AddWithValue("H", float.Parse(insertHTextBox.Text));
                command.Parameters.AddWithValue("S", float.Parse(insertSTextBox.Text));
                command.Parameters.AddWithValue("comment", insertCommentRichTextBox.Text);

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
                label6.Text = "Ошибка. Поля должны быть заполнены.";
            }
            updateExaminationsTab();
            SilentMessageBox.Show("Обследование добавлено", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DataBaseServitor.DataTableInfusion(dataGridView1, sqlConnection);
           /* var result = SilentMessageBox.Show("Обследование добавлено. Стереть все поля для проведения нового обследования?", "Готово", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                insertH1TextBox.Text = "";
                insertH2TextBox.Text = "";
                insertP1TextBox.Text = "";
                insertP2TextBox.Text = "";
                insertCommentRichTextBox.Text = "";
                // insertMedBookComboBox.Text = "";
            }*/
        }

        private void insertP1TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {

                insertAvgPTextBox.Text = Convert.ToString((Convert.ToDouble(0 + insertP1TextBox.Text) + Convert.ToDouble(0 + insertP2TextBox.Text)) / 2);
                insertPTextBox.Text = Convert.ToString((Math.Abs(Convert.ToDouble(0 + insertP1TextBox.Text) - Convert.ToDouble(0 + insertP2TextBox.Text))));
                insertH2TextBox.Text = "";
                insertH1TextBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Введены некорректные символы", ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void insertP2TextBox_TextChanged(object sender, EventArgs e)
        {
            insertP1TextBox_TextChanged(sender, e);
        }

        private void insertAvgPTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Text != "" && Text != "0")
            {
                insertH1TextBox.ReadOnly = false;
                insertH2TextBox.ReadOnly = false;
            }
            else
            {
                insertH1TextBox.ReadOnly = true;
                insertH2TextBox.ReadOnly = true;
            }
        }

        private void insertH1TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                insertHTextBox.Text = Convert.ToString((Math.Abs(Convert.ToDouble(0 + insertH1TextBox.Text) - Convert.ToDouble(0 + insertH2TextBox.Text))));
            }
            catch (Exception ex) { MessageBox.Show("Введены некорректные символы", ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void insertH2TextBox_TextChanged(object sender, EventArgs e)
        {
            insertH1TextBox_TextChanged(sender, e);
        }

        private void insertPTextBox_TextChanged(object sender, EventArgs e)
        {
            if (insertHTextBox.Text != "")
                insertSTextBox.Text = Convert.ToString((0.1 * Convert.ToDouble(0 + insertPTextBox.Text) * Convert.ToDouble(0 + insertHTextBox.Text)));
        }

        private void insertHTextBox_TextChanged(object sender, EventArgs e)
        {
            if (insertPTextBox.Text != "")
                insertSTextBox.Text = Convert.ToString((0.1* Convert.ToDouble(0 + insertPTextBox.Text) * Convert.ToDouble(0 + insertHTextBox.Text)));
        }

        private void insertTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {

            if (label6.Visible)
                label6.Visible = false;

            if (!string.IsNullOrEmpty(updateSTextBox.Text) && !string.IsNullOrWhiteSpace(updateSTextBox.Text) &&
                !string.IsNullOrEmpty(updateMedBookComboBox.Text) && !string.IsNullOrWhiteSpace(updateMedBookComboBox.Text))
            {
                //SqlCommand command = new SqlCommand("UPDATE [PatientsData] SET [Name]=@Name, [Date]=@Date  WHERE[MedBook]=@MedBook", sqlConnection);

                SqlCommand command = new SqlCommand("UPDATE [ExaminationsData] SET [doctorID]=@doctorID, [patientID]=@patientID, [date]=@date, [p1]=@p1, [p2]=@p2, [avgP]=@avgP, [h1]=@h1, [h2]=@h2, [P]=@P, [H]=@H, [S]=@S, [comment]=@comment  WHERE[Id]=@Id", sqlConnection);
                command.Parameters.AddWithValue("Id", Convert.ToInt32(updateIDTextBox.Text));
                command.Parameters.AddWithValue("doctorID", Convert.ToInt32(updateDocIDTextBox.Text));
                command.Parameters.AddWithValue("patientID", Convert.ToInt32(updateMedBookComboBox.Text));
                command.Parameters.AddWithValue("date", Convert.ToDateTime(updateTimePicker.Value));
                command.Parameters.AddWithValue("p1", float.Parse(updateP1TextBox.Text));
                command.Parameters.AddWithValue("p2", float.Parse(updateP2TextBox.Text));
                command.Parameters.AddWithValue("avgP", float.Parse(updateAvgPTextBox.Text));
                command.Parameters.AddWithValue("h1", float.Parse(updateH1TextBox.Text));
                command.Parameters.AddWithValue("h2", float.Parse(updateH2TextBox.Text));
                command.Parameters.AddWithValue("P", float.Parse(updatePTextBox.Text));
                command.Parameters.AddWithValue("H", float.Parse(updateHTextBox.Text));
                command.Parameters.AddWithValue("S", float.Parse(updateSTextBox.Text));
                command.Parameters.AddWithValue("comment", updateCommentRichTextBox.Text);
                //command.Parameters.AddWithValue("Id", updateCommentRichTextBox.Text);
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
                label6.Text = "Ошибка. Поля должны быть заполнены.";
            }
            updateExaminationsTab();
        }

        private void updateP1TextBox_TextChanged(object sender, EventArgs e)
        {
            updateAvgPTextBox.Text = Convert.ToString((Convert.ToDouble("0" + updateP1TextBox.Text) + Convert.ToDouble("0" + updateP2TextBox.Text)) / 2);
            updatePTextBox.Text = Convert.ToString((Math.Abs(Convert.ToDouble("0" + updateP1TextBox.Text) - Convert.ToDouble("0" + updateP2TextBox.Text))));
        }

        private void updateP2TextBox_TextChanged(object sender, EventArgs e)
        {
            updateP1TextBox_TextChanged(sender, e);
        }

        private void updateAvgPTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Text != "" && Text != "0")
            {
                updateH1TextBox.ReadOnly = false;
                updateH2TextBox.ReadOnly = false;
            }
            else
            {
                updateH1TextBox.ReadOnly = true;
                updateH2TextBox.ReadOnly = true;
            }
        }

        private void updateH1TextBox_TextChanged(object sender, EventArgs e)
        {
            updateHTextBox.Text = Convert.ToString((Math.Abs(Convert.ToDouble("0" + updateH1TextBox.Text) - Convert.ToDouble("0" + updateH2TextBox.Text))));

        }

        private void updateH2TextBox_TextChanged(object sender, EventArgs e)
        {
            updateH1TextBox_TextChanged(sender, e);
        }

        private void updatePTextBox_TextChanged(object sender, EventArgs e)
        {
            if (updateHTextBox.Text != "")
                updateSTextBox.Text = Convert.ToString((Convert.ToDouble("0" + updatePTextBox.Text) * Convert.ToDouble("0" + updateHTextBox.Text)));
        }

        private void updateSTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void updateHTextBox_TextChanged(object sender, EventArgs e)
        {
            if (updatePTextBox.Text != "")
                updateSTextBox.Text = Convert.ToString((Convert.ToDouble("0" + updatePTextBox.Text) * Convert.ToDouble("0" + updateHTextBox.Text)));
        }

        private async void button3_Click(object sender, EventArgs e)
        {
           

            if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [ExaminationsData] WHERE [Id]=@Id", sqlConnection);
                command.Parameters.AddWithValue("Id", textBox6.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Ошибка. Id должен быть заполнен.";
            }
            updateExaminationsTab();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       

        private void examinationsDataBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_Leave(object sender, EventArgs e)
        {

        }

        private void insertP1TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9') || (e.KeyChar == ',') || (e.KeyChar == 8))
            {

                // цифра
                return;
            }
            /*

            Control text;
            try
            {
                text = (TextBox)sender;
            }
           catch (Exception)
            { text = (ComboBox)sender; }
            

            if (sender is TextBox)
            {
                text = (TextBox)sender;
            }
            else if (sender is ComboBox)
            {
                text = (ComboBox)sender;
            }
            else
            {
                throw new Exception();
            }

            if (e.KeyChar == (char)Keys.Back)  //блокировка всего кроме backspace
            {
                if (text.Text.Length > 0)
                {
                    text.Text = text.Text.Remove(text.Text.Length - 1);
                }
                if (sender is TextBox)
                {
                    ((TextBox)sender).SelectionStart = text.Text.Length;
                }
                if (sender is ComboBox)
                {
                    ((ComboBox)sender).SelectionStart = text.Text.Length;
                }

        }*/
            e.Handled = true;
        }




        private void insertSTextBox_TextChanged(object sender, EventArgs e)
        {
            if (insertH1TextBox.Text != "" && insertH2TextBox.Text != "" && insertP1TextBox.Text != "" && insertP2TextBox.Text != "") button1.Enabled = true;
            else button1.Enabled = false;
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  MessageBox.Show("Данная программа является вспомогательным программным обеспечением, спомощью которого врачи-оптометристы и врачи-исследователи могут проводить обследования зоны бинокулярной суммации, а так же фиксировать результаты в базе данных. Присутвует возможность формирования отчетов Версия программы - 'Test_0,21'", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AboutBox1 aba = new AboutBox1();
            aba.ShowDialog();
        }

        private void расположениеБазыДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
#if DEBUG

            MessageBox.Show("DEBUG. CURR : " + Directory.GetCurrentDirectory() + "\n" + "PATH : " + GetDirectory(3) + @"\Database1.mdf");
#else
            
            MessageBox.Show("RELEASE. CURR : " + Directory.GetCurrentDirectory() + "\n" + "PATH : " +  Directory.GetCurrentDirectory() + @"\Database1.mdf" );
#endif
        }

        private void вернутьсяВМенюToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Hide();
            Menu menuForm = new Menu(userType, userId);
            menuForm.ShowDialog();
            Close();
        }
        private void DataSetUpdate()
        {
            SqlCommand command = new SqlCommand("SELECT * [ExaminationsData]", sqlConnection);

            
            try
            {
                //  await command.ExecuteNonQueryAsync();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
        private void обновитьТаблицуДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataBaseServitor.DataTableInfusion(dataGridView1, sqlConnection);

            //updateExaminationsTab();
            
            
            
        }
       

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right) contextMenuStrip1.Show(MousePosition, ToolStripDropDownDirection.Right);
        }

        private void подробнееToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            String docId, patientId, p1, p2, avgP, h1, h2, P, H, S, comment, date;

            /*
            docId = dataGridView1.Rows[chosenOneID].Cells["doctorIDDataGridViewTextBoxColumn"].Value.ToString();
            patientId = dataGridView1.Rows[chosenOneID].Cells["patientIDDataGridViewTextBoxColumn"].Value.ToString();
            */



            /* avgP = dataGridView1.Rows[chosenOneID].Cells["avgPDataGridViewTextBoxColumn"].Value.ToString();
            P = dataGridView1.Rows[chosenOneID].Cells["pDataGridViewTextBoxColumn"].Value.ToString();
            H = dataGridView1.Rows[chosenOneID].Cells["hDataGridViewTextBoxColumn"].Value.ToString();
            S = dataGridView1.Rows[chosenOneID].Cells["sDataGridViewTextBoxColumn"].Value.ToString();
            comment = dataGridView1.Rows[chosenOneID].Cells["commentDataGridViewTextBoxColumn"].Value.ToString();
            date = dataGridView1.Rows[chosenOneID].Cells["dateDataGridViewTextBoxColumn"].Value.ToString(); */

            //dataGridView1.Rows[chosenOneID].Cells["idDataGridViewTextBoxColumn"].Value.ToString(); //хз нужна ли это строка, вроде не нужна ¯\_(ツ)_/¯
            //LNK_id = dataGridViewLinksCheker.Rows[e.RowIndex].Cells["id_lnkcheck"].Value.ToString();

            /* if (!checkBox1.Checked) //конструкция для обработки одного обследования
             {
                 p1 = dataGridView1.Rows[chosenOneID].Cells["p1DataGridViewTextBoxColumn"].Value.ToString();
                 p2 = dataGridView1.Rows[chosenOneID].Cells["p2DataGridViewTextBoxColumn"].Value.ToString(); //считывание полей таблицы с нужным нам ChosenOneID обследованием
                 h1 = dataGridView1.Rows[chosenOneID].Cells["h1DataGridViewTextBoxColumn"].Value.ToString();
                 h2 = dataGridView1.Rows[chosenOneID].Cells["h2DataGridViewTextBoxColumn"].Value.ToString();

                 ExaminationOne tempEx = new ExaminationOne();
                 tempEx.examId = Convert.ToInt32(dataGridView1.Rows[chosenOneID].Cells["idDataGridViewTextBoxColumn"].Value.ToString());
                 tempEx.cogitationOn(p1, p2, h1, h2); //вычисление вторичных параметров
                 tempEx.docId = dataGridView1.Rows[chosenOneID].Cells["doctorIDDataGridViewTextBoxColumn"].Value.ToString();
                 tempEx.patientId = dataGridView1.Rows[chosenOneID].Cells["patientIDDataGridViewTextBoxColumn"].Value.ToString();
                 tempEx.comment = dataGridView1.Rows[chosenOneID].Cells["commentDataGridViewTextBoxColumn"].Value.ToString();
                 tempEx.date = dataGridView1.Rows[chosenOneID].Cells["dateDataGridViewTextBoxColumn"].Value.ToString();
                 ChosenOneExamination COE = new ChosenOneExamination(tempEx);
                 COE.ShowDialog();
             }
             else*/
            { //конструкция для обработки сразу нескольких обследований
                try
                {
                    List<ExaminationOne> tempExCollection = new List<ExaminationOne>(); //создание листа из обследований

                    foreach (DataGridViewRow i in chosenOnesRows)
                    {

                        ExaminationOne tempEx = new ExaminationOne(); //создание объекта обследования
                        int chosenOneID = i.Index; //ID выбранной строки внутри DataGrid`a
                        p1 = dataGridView1.Rows[chosenOneID].Cells["p1"].Value.ToString();
                        p2 = dataGridView1.Rows[chosenOneID].Cells["p2"].Value.ToString(); //считывание полей таблицы с нужным нам ChosenOneID обследованием
                        h1 = dataGridView1.Rows[chosenOneID].Cells["h1"].Value.ToString();
                        h2 = dataGridView1.Rows[chosenOneID].Cells["h2"].Value.ToString();



                        tempEx.examId = Convert.ToInt32(dataGridView1.Rows[chosenOneID].Cells["id"].Value.ToString());
                        tempEx.cogitationOn(p1, p2, h1, h2); //вычисление вторичных параметров
                        tempEx.docId = dataGridView1.Rows[chosenOneID].Cells["doctorID"].Value.ToString();
                        tempEx.patientId = dataGridView1.Rows[chosenOneID].Cells["patientID"].Value.ToString();
                        tempEx.comment = dataGridView1.Rows[chosenOneID].Cells["comment"].Value.ToString();
                        tempEx.date = dataGridView1.Rows[chosenOneID].Cells["date"].Value.ToString();
                        tempExCollection.Add(tempEx);


                        //MessageBox.Show("1");

                        /*{//блок для передачи номера или имени пациента
                            int temp = Convert.ToInt32(i.Cells["patientID"].Value.ToString());
                            MessageBox.Show(Convert.ToString(DataBaseServitor.getPatientFromExam(sqlConnection, temp)));
                        }*/


                    }


                    ChosenOneExamination COE = new ChosenOneExamination(tempExCollection, sqlConnection,userType);
                    COE.ShowDialog();
                }
                catch (Exception) { }
                DataBaseServitor.DataTableInfusion(dataGridView1, sqlConnection);
            } 
        }

      

        

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            //MessageBox.Show(Convert.ToString(dataGridView1.CurrentCell.Value));
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            подробнееToolStripMenuItem_Click(sender, e);
        }


        //int chosenOneID; //ID выбранного обследования
        List<DataGridViewRow> chosenOnesRows = new List<DataGridViewRow>(); //лист из объектов-строк

        // DataGridViewRowCollection chosenOnesRows = new DataGridViewRowCollection();

        int chosenCellRowIndex, chosenCellColumnIndex;
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
               //метод для выделения ячейки, при нажатии на неё правой кнопкой мыши
                
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                chosenCellRowIndex = e.RowIndex; chosenCellColumnIndex = e.ColumnIndex;
                Point point = MousePosition;
                
                //if (!checkBox1.Checked) 
                   // chosenOneID = e.RowIndex; 
                //else 
                { //обработка выбора нескольких обследований
                    chosenOnesRows.Clear();
                    
                    foreach (DataGridViewRow i in dataGridView1.SelectedRows) { //обход по всем выделенным строкам
                        chosenOnesRows.Add(i); //сохранение выделенных строк как коллекцию строк
                    }
                }
                if (e.Button == MouseButtons.Left) return;  //если левая кнопка - не октрывать контекстное меню
                contextMenuStrip1.Show(point.X, point.Y);
            }
            catch (Exception) { }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e) //кнопка Отчет
        {
            DataGridView dgv = new DataGridView();
            DataTable dt = new DataTable();

            //dt = (DataTable)dataGridView1.DataSource;




            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                dt.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }


            /*
            List<string> list = new List<string>();
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
                list.Add(dataGridView1.Columns[i].Name);

                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                dt.Columns.Add(dataGridView1.Columns[i].Name, dataGridView1.Columns[i].ValueType);


            dgv.Rows.Clear();
           
                for (int i = 0; i < chosenOnesRows.Count; i++)
                dt.Rows.Add(chosenOnesRows[i].Cells[list[0]].Value, chosenOnesRows[i].Cells[list[1]].Value, chosenOnesRows[i].Cells[list[2]].Value, chosenOnesRows[i].Cells[list[3]].Value, chosenOnesRows[i].Cells[list[4]].Value, chosenOnesRows[i].Cells[list[5]].Value, chosenOnesRows[i].Cells[list[6]].Value, chosenOnesRows[i].Cells[list[7]].Value, chosenOnesRows[i].Cells[list[8]].Value, chosenOnesRows[i].Cells[list[9]].Value, chosenOnesRows[i].Cells[list[10]].Value, chosenOnesRows[i].Cells[list[11]].Value, chosenOnesRows[i].Cells[list[12]].Value);
            
           */
            
            dataGridView1.DataSource = dt;
            dgv = dataGridView1;



           

            report report = new report(dgv);
            report.ShowDialog();
            
               
            DataBaseServitor.DataTableInfusion(dataGridView1, sqlConnection);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dataGridView1.MultiSelect = true;
            }
            else
            {
                dataGridView1.MultiSelect = false;
            }
        }

        private List<String> getCells(List<DataGridViewRow> rows, String chosenCulumn) { //берет список строк таблицы, вынимает и формирует стринговый список
            string temp = "";
            List<string> newRows = new List<string>();
            for (int i = 0; i < rows.Count; i++)
            {
                newRows.Add(rows[i].Cells[chosenCulumn].Value.ToString());
            }
            return newRows;
        }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List < String > chosens = new List<String>();
            chosens = getCells(chosenOnesRows, "id");

            //foreach (DataGridViewRow i in chosenOnesRows)
            //chosens.Add(dataGridView1.Rows[i.Index].Cells["id"].Value.ToString()); //определение номера обследования с помощью номера в таблице
            string sum="";
            for (int i = 0; i < chosens.Count; i++) {
                if (i < chosens.Count - 1)
                    sum += chosens[i] + "];[";
                else sum += chosens[i];
            }
            var result = SilentMessageBox.Show("Вы уверены, что хотите удалить следующие ["+ sum + "] обследования? ", "Подтвердите удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DataBaseServitor.DeleteFromDB(chosens, sqlConnection);
                SilentMessageBox.Show("Удаление завершено","Удаление успешно" ,MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBaseServitor.DataTableInfusion(dataGridView1, sqlConnection);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            filterOn(sender, e);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            
            sTextBox.Text = "";
            docTextBox.Text = "";
            exTextBox.Text = "";
            patientTextBox.Text = "";
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = "";
            //  dateTimePicker1.Value= Convert.ToDateTime("01.01.2000");
            // dateTimePicker2.Value = Convert.ToDateTime("01.01.2040");
        }

        private void filterOn(object sender, EventArgs e) { //универсальное применение фильтра. собирает из нескольких текстбоксов один фильтр

            try
            {
                string filter= "";

                if (docTextBox.Text != "") filter = "doctorID = '" + docTextBox.Text + "'";//использует имя столбца из Базы Данных! Осторожно
                    if (filter != "" && patientTextBox.Text != "") filter += " And "; //добавляет AND для доп фильтра
                if (patientTextBox.Text != "") filter += "patientID = '" + patientTextBox.Text + "'";
                    if (filter != "" && sTextBox.Text != "") filter += " And ";
                if (sTextBox.Text != "") filter += "s = '" + sTextBox.Text + "'";
                    if (filter != "" && exTextBox.Text != "") filter += " And ";
                if (exTextBox.Text != "") filter += " id = '" + exTextBox.Text + "'";
                
                if (filter != "") (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = filter; 
                 (sender as TextBox).ForeColor = Color.Black;
            }
            catch (Exception) { (sender as TextBox).ForeColor = Color.Red; }



            if (patientTextBox.Text == "" && docTextBox.Text == "" && sTextBox.Text == "" && exTextBox.Text == "") //если все строки пусты, можно отчистить
                button4_Click_1(sender, e);

        }
        private void docTextBox_TextChanged(object sender, EventArgs e)
        {
            filterOn(sender, e);
        }

        private void exTextBox_TextChanged(object sender, EventArgs e)
        {
            filterOn(sender, e);
        }

        private void patientComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void patientTextBox_TextChanged(object sender, EventArgs e)
        {
            filterOn(sender, e);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            //try
            {

               // (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = "date > '" + dateTimePicker2.Value + "'";//использует имя столбца из Базы Данных! Осторожно
               // dateTimePicker2.ForeColor = Color.Black;
            }
           // catch (Exception) {// dateTimePicker2.ForeColor = Color.Red; 
           // }

          //  if (patientTextBox.Text == "" && docTextBox.Text == "" && sTextBox.Text == "" && exTextBox.Text == "") //если все строки пусты, можно отчистить
            //    button4_Click_1(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
           // (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = "date > '" + Convert.ToString(dateTimePicker2.Value) + "'";//использует имя столбца из Базы Данных! Осторожно
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

            
           
            
        }

        private void копироватьПолеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Convert.ToString(dataGridView1.Rows[chosenCellRowIndex].Cells[chosenCellColumnIndex].Value));
        }


       


    }
}
