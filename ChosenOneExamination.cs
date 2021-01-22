using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace BD_test
{
    public partial class ChosenOneExamination : Form
    {
        //int examinationsId=0;
        //String docId, patientId, p1, p2, avgP, h1, h2, P, H, S, comment, date;
        ExaminationOne Ex = new ExaminationOne();
        List<ExaminationOne> ExList = new List<ExaminationOne>();
       

        SqlConnection sqlConnection = new SqlConnection();
        string userType="";
        public ChosenOneExamination(List <ExaminationOne> ExList,SqlConnection sqlConnection,string userType) //overload
        {
            this.userType = userType;
            this.sqlConnection = sqlConnection;
            this.ExList = ExList;
            /*this.docId = docId;
              this.patientId = patientId;
              this.p1 = p1;
              this.p2 = p2;
              this.avgP = avgP;
              this.h1 = h1;
              this.h2 = h2;
              this.P = P;
              this.H = H;
              this.S = S;
              this.comment = comment;
              this.date = date;
              this.examinationsId = examinationsId;*/
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void updateElements(ExaminationOne Ex) 
        {

            patientTextBox.Text = Ex.patientId;
            docTextBox.Text = Ex.docId;
            timePicker.Value = Convert.ToDateTime(Ex.date);
            P1TextBox.Text = Ex.p1;
            P2TextBox.Text = Ex.p2;
            AvgPTextBox.Text = Ex.avgP;
            H1TextBox.Text = Ex.h1;
            H2TextBox.Text = Ex.h2;
            HTextBox.Text = Ex.H;
            PTextBox.Text = Ex.P;
            STextBox.Text = Ex.S;
            CommentRichTextBox.Text = Ex.comment;
            pictureBox2.BackColor =absGraph1.listColor[examComboBox.SelectedIndex];
            
            sLabel.Text= Ex.S;
            medLabel.Text= "№ " +Ex.patientId;
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

        private void renderOn() { //перересовка нужного нам списка обследований

            for (int i = 0; i < ExList.Count; i++)
            {
                try
                {
                    //CommentRichTextBox.Text += ExList[i].S + "_"; //проверка работы передачи листа обследования
                    
                    absGraph1.AddExaminationToDiagramm(ExList[i]);
                }
                catch (Exception) { }
            }
        }
        
        private void loadAndReloading() {

           


            if (ExList.Count == 1)          //одиночная сводка
            {
                Ex = ExList[0];

                Text = "Обследование №" + (0 + Ex.examId) + " - подробная сводка";
                button1.Visible = true;
                updateElements(Ex);
                patientTextBox.Text = Ex.patientId;

            }
            else     //множественная сводка
            {
                button1.Visible = false;
                Ex = ExList[examComboBox.SelectedIndex];

            //    updateCombobox(patientsComboBox, "patientsData", "MedBook"); //обновление Комбобокса из БД
            //    updateCombobox(docComboBox, "UsersData", "Id"); //обновление Комбобокса из БД


                updateElements(Ex);
                patientTextBox.Text = Ex.patientId;

                string sumID = "[";
                for (int i = 0; i < ExList.Count; i++)
                {
                    sumID = sumID + ExList[i].examId + "]";
                    if (i != ExList.Count - 1) sumID += ";[";

                }
                Text = "Обследования №" + sumID + " - сводка";


                //DocIDTextBox.Visible = false;

              
            }




        }
        private void ChosenOneExamination_Load(object sender, EventArgs e) //загрузка полей формы
        {
            if (userType == "Врач-исследователь")
                button1.Visible = false;
            
            renderOn();
            
            List<string> exIDList = new List<string>(); //создание листа всех номеров обследований
            for (int i = 0; i < ExList.Count; i++)
                exIDList.Add(Convert.ToString(ExList[i].examId));
            examComboBox.DataSource = exIDList; //обновление Комбобокса

            

            loadAndReloading();
            
            
        }

        private void ChosenOneExamination_MouseDown(object sender, MouseEventArgs e)
        {
            
        }
        bool accept = true;
        private void button1_Click_1(object sender, EventArgs e)
        {
           // Text = "Обследование №" + (0 + Ex.examId) + " - подробная сводка";
            if (accept)
            {
                timePicker.Enabled = true;
               // docComboBox.Enabled = false;
               // patientsComboBox.Enabled = false;
                P1TextBox.ReadOnly = false;
                P2TextBox.ReadOnly = false;
                H1TextBox.ReadOnly = false;
                H2TextBox.ReadOnly = false;
                //docTextBox.ReadOnly = false;
                //patientTextBox.ReadOnly = false;
                timePicker.Enabled = true;
                CommentRichTextBox.ReadOnly = false;
                CommentRichTextBox.Enabled = true;
                button1.Text = "Подтвердить изменения";
                accept = !accept;



              //  docTextBox.Visible = true;
              //  docTextBox.Visible = false;
              //  patientTextBox.Text = Ex.docId;
            }
            
            else {
                
                ExaminationOne newEx = new ExaminationOne(Ex.examId, Convert.ToString(docTextBox.Text), Convert.ToString (patientTextBox.Text), CommentRichTextBox.Text, Convert.ToString(timePicker.Value));
                newEx.cogitationOn(P1TextBox.Text, P2TextBox.Text, H1TextBox.Text, H2TextBox.Text);
                
                //костылище что позволяет обновлять Базу Данных из дочерней формы
                
                DataBaseServitor.AddToDB(newEx, sqlConnection); //TODO: //HACK: надо доделать эту ересь
                Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void absGraph1_Click(object sender, EventArgs e)
        {

        }

        private void DocIDTextBox_VisibleChanged(object sender, EventArgs e)
        {
            timePicker.Visible = docTextBox.Visible;
            patientTextBox.Visible = patientTextBox.Visible;
            P1TextBox.Visible = patientTextBox.Visible;
            P2TextBox.Visible = patientTextBox.Visible;
            AvgPTextBox.Visible = patientTextBox.Visible;
            H1TextBox.Visible = patientTextBox.Visible;
            H2TextBox.Visible = patientTextBox.Visible;
            HTextBox.Visible = patientTextBox.Visible;
            PTextBox.Visible = patientTextBox.Visible;
            STextBox.Visible = patientTextBox.Visible;
            CommentRichTextBox.Visible = patientTextBox.Visible;

            label1.Visible = patientTextBox.Visible;
            label2.Visible = patientTextBox.Visible;
            
            label9.Visible = patientTextBox.Visible;
           
            label11.Visible = patientTextBox.Visible;
            label12.Visible = patientTextBox.Visible;
            label13.Visible = patientTextBox.Visible;
            label14.Visible = patientTextBox.Visible;
            label15.Visible = patientTextBox.Visible;
            label16.Visible = patientTextBox.Visible;
            label17.Visible = patientTextBox.Visible;
            label18.Visible = patientTextBox.Visible;
            label19.Visible = patientTextBox.Visible;
         
        }
       
        private void P1TextBox_TextChanged(object sender, EventArgs e)
        {
            
           // ExList[examComboBox.SelectedIndex].cogitationOn(P1TextBox.Text, P2TextBox.Text, H1TextBox.Text, H2TextBox.Text);
            try
            {

                AvgPTextBox.Text = Convert.ToString((Convert.ToDouble(0 + P1TextBox.Text) + Convert.ToDouble(0 + P2TextBox.Text)) / 2);
                PTextBox.Text = Convert.ToString((Math.Abs(Convert.ToDouble(0 + P1TextBox.Text) - Convert.ToDouble(0 + P2TextBox.Text))));
                H2TextBox.Text = "";
                H1TextBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Введены некорректные символы", ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void examComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateElements(ExList[examComboBox.SelectedIndex]);
            loadAndReloading();







        }

        private void H1TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                HTextBox.Text = Convert.ToString((Math.Abs(Convert.ToDouble(0 + H1TextBox.Text) - Convert.ToDouble(0 + H2TextBox.Text))));
            }
            catch (Exception ex) { MessageBox.Show("Введены некорректные символы", ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void H2TextBox_TextChanged(object sender, EventArgs e)
        {
            H1TextBox_TextChanged(sender, e);
        }

        private void P2TextBox_TextChanged(object sender, EventArgs e)
        {
            P1TextBox_TextChanged(sender, e);
        }

        private void HTextBox_TextChanged(object sender, EventArgs e)
        {
            if (PTextBox.Text != "")
                STextBox.Text = Convert.ToString((0.1 * Convert.ToDouble(0 + PTextBox.Text) * Convert.ToDouble(0 + HTextBox.Text)));
        }

        private void docComboBox_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            patientTextBox.Text = "321321";
        }

       
        private void button3_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = absGraph1.listColor[examComboBox.SelectedIndex];
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                
                absGraph1.listColor[examComboBox.SelectedIndex] = Color.FromArgb(absGraph1.unstaticAlpha, colorDialog1.Color.R, colorDialog1.Color.G, colorDialog1.Color.B);
                
               



            }
            renderOn();
            pictureBox2.BackColor = colorDialog1.Color;


            absGraph1.ClearList();
            renderOn();
            absGraph1.Refresh();
        }

        private void STextBox_TextChanged(object sender, EventArgs e)
        {
           // renderOn();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            absGraph1.ClearList();
         renderOn();
            absGraph1.Refresh();


        }
    }

    class ABSGraph : PictureBox
    {
        Graphics g;
        Rectangle rect;
        BufferedGraphics bf;
        BufferedGraphicsContext bfc;
        List<ExaminationOne> ex = new List<ExaminationOne>();
        
        float stepX;
        float stepY;


        static Color backColor = Color.White; //Фон
        static Color borderColor = Color.Black; //Рамка
        static Color borderBarColor = Color.Green; //Рамка бара
        static Color barStartingColor = Color.Green;  //Начало градиента Color.FromArgb(255, 0, 0); 
        static Color barEndingColor = Color.LightGreen; //Конец градиента бара
        Brush textColor = Brushes.Black; //Цвет надписи бара
        FontFamily font = FontFamily.GenericSerif; //шрифт
        int fontScale = 8; //размер шрифта

        int maxCount = 14;
        public static int Alpha = 150;
        public int unstaticAlpha = Alpha;
       public Color[] listColor = new Color[] { Color.FromArgb(Alpha, 6, 82, 221), Color.FromArgb(Alpha, 255, 195, 18), Color.FromArgb(Alpha, 153, 128, 250), Color.FromArgb(Alpha, 196, 229, 56), Color.FromArgb(Alpha, 18, 203, 196), Color.FromArgb(Alpha, 253, 167, 223), Color.FromArgb(Alpha, 237, 76, 103), Color.FromArgb(Alpha, 247, 159, 31), Color.FromArgb(Alpha, 163, 203, 56), Color.FromArgb(Alpha, 18, 137, 167), Color.FromArgb(Alpha, 217, 128, 250), Color.FromArgb(Alpha, 181, 52, 113), Color.FromArgb(Alpha, 238, 90, 36),Color.FromArgb(Alpha, 0, 148, 50) };

        public ABSGraph()
        {
            bfc = BufferedGraphicsManager.Current;
        }


        public void AddExaminationToDiagramm(ExaminationOne exam)
        {

            ex.Add(new ExaminationOne());
            ex[ex.Count - 1].h1 = exam.h1;
            ex[ex.Count - 1].h2 = exam.h2;
            ex[ex.Count - 1].p1 = exam.p1;
            ex[ex.Count - 1].p2 = exam.p2;
            ex[ex.Count - 1].H = exam.H;
            ex[ex.Count - 1].P = exam.P;
        }

        public void ClearList() {
            ex.Clear();
        }

        private void printText(string str, float x, float y)
        {
            Font f = new Font(font, fontScale);
            SizeF len = g.MeasureString(str, f);
            bf.Graphics.DrawString(str, f, textColor, x, y);
        }

        private void printText(string str, Brush clr, int x, int y)
        {
            Font f = new Font(font, fontScale);
            SizeF len = g.MeasureString(str, f);
            bf.Graphics.DrawString(str, f, clr, x, y);
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            g = e.Graphics;
            rect = ClientRectangle;
            bf = bfc.Allocate(g, rect);

            stepX = (float)(rect.Width / 60.0);
            stepY = (float)(rect.Height / 100.0);

            //L1  фон
            bf.Graphics.FillRectangle(new SolidBrush(backColor), rect);
            bf.Graphics.DrawRectangle(new Pen(borderColor, 1), rect.X, rect.Y, rect.Width - 1, rect.Height - 1);

            //2 вертикальные линии
            for (int i = 0; i < 60; i++)
            {
                if (i % 10 == 0)
                {
                    bf.Graphics.DrawLine(new Pen(Color.Black, 1), stepX * i, 0, stepX * i, rect.Height); //отображений линий
                    printText(Convert.ToString(i + 30), Brushes.Black, (int)(stepX * i), 0);
                   
                }
                else
                {
                    //bf.Graphics.DrawLine(new Pen(Color.White, 1), stepX * i, 0, stepX * i, rect.Height);
                }
            }


            for (int i = 0; i < 100; i++) //горизонтальные линии
            {
                if (i % 10 == 0)
                {
                    bf.Graphics.DrawLine(new Pen(Color.Black, 1), 0, stepY * i, rect.Width, stepY * i);
                }
                else
                {
                    //bf.Graphics.DrawLine(new Pen(Color.White, 1), 0, stepY * i, rect.Width, stepY * i);
                }

            }

            //L() + 1 квадраты
            for (int i = (ex.Count - maxCount) <= 0 ? 0 : ex.Count - maxCount; i < ex.Count; i++)
            {
                try
                {
                    if (ex.Count < 20) //костыль заливает всегда и всегда рисует черную рамку. можно поставить ex.Count < 2, что бы отдельно рендерить 1 квадрат и несколько
                    {
                        bf.Graphics.FillRectangle(
                        new SolidBrush(listColor[i]),
                        (Convert.ToInt32(ex[i].p1) - 30) * stepX,
                        ((Convert.ToInt32(ex[i].h1)) * stepY) + (rect.Height - ((Convert.ToInt32(ex[i].H) * stepY) + ((Convert.ToInt32(ex[i].h1)) * stepY))) - ((Convert.ToInt32(ex[i].h1)) * stepY),
                        Convert.ToInt32(ex[i].P) * stepX,
                        (Convert.ToInt32(ex[i].H) * stepY)
                        );

                        bf.Graphics.DrawRectangle(
                        new Pen(borderColor, 1),
                        (Convert.ToInt32(ex[i].p1) - 30) * stepX,
                        ((Convert.ToInt32(ex[i].h1)) * stepY) + (rect.Height - ((Convert.ToInt32(ex[i].H) * stepY) + ((Convert.ToInt32(ex[i].h1)) * stepY))) - ((Convert.ToInt32(ex[i].h1)) * stepY),
                        Convert.ToInt32(ex[i].P) * stepX,
                        (Convert.ToInt32(ex[i].H) * stepY)
                        );
                    }
               
                else
                {

                    //printText("Obj" + i + "x = " + ex[i].p1 + "y = " + ex[i].h1, textColor, 0, i*10);

                    
                    bf.Graphics.FillRectangle( //заливка
                    new SolidBrush(listColor[i]),
                    (Convert.ToInt32(ex[i].p1) - 30) * stepX,
                    ((Convert.ToInt32(ex[i].h1)) * stepY) + (rect.Height - ((Convert.ToInt32(ex[i].H) * stepY) + ((Convert.ToInt32(ex[i].h1)) * stepY))) - ((Convert.ToInt32(ex[i].h1)) * stepY),
                    Convert.ToInt32(ex[i].P) * stepX,
                    (Convert.ToInt32(ex[i].H) * stepY)
                    );
                    

                    try
                    {
                        bf.Graphics.DrawRectangle(
                            new Pen(listColor[i], 2),
                            (Convert.ToInt32(ex[i].p1) - 30) * stepX,
                            ((Convert.ToInt32(ex[i].h1)) * stepY) + (rect.Height - ((Convert.ToInt32(ex[i].H) * stepY) + ((Convert.ToInt32(ex[i].h1)) * stepY))) - ((Convert.ToInt32(ex[i].h1)) * stepY),
                            Convert.ToInt32(ex[i].P) * stepX,
                            (Convert.ToInt32(ex[i].H) * stepY)
                            );
                    }
                    catch (Exception) { }
                    
                   
                }
                }
                catch (Exception) { }
            }

            bf.Render(); //отрисовка
            bf.Dispose(); //отчистка памяти

            //L(N)
            //foreach (ExaminationOne currentEx in ex)
            ////for (int i = (ex.Count - maxCount)<=0?0: ex.Count - maxCount; i<ex.Count; i++)
            //{
            //    //bf.Graphics.FillRectangle(
            //    //    new SolidBrush(Color.Aqua),
            //    //    Convert.ToInt32(currentEx.p1), 
            //    //    rect.Height - Convert.ToInt32(currentEx.h1),
            //    //    Convert.ToInt32(currentEx.p2),
            //    //    rect.Height - Convert.ToInt32(currentEx.h2)
            //    //    );


            //    if (ex.Count < 2)
            //    {
            //        bf.Graphics.FillRectangle(
            //        new SolidBrush(listColor[i]),
            //        (Convert.ToInt32(currentEx.p1) - 30) * stepX,
            //        ((Convert.ToInt32(currentEx.h1)) * stepY) + (rect.Height - ((Convert.ToInt32(currentEx.H) * stepY) + ((Convert.ToInt32(currentEx.h1)) * stepY))) - ((Convert.ToInt32(currentEx.h1)) * stepY),
            //        Convert.ToInt32(currentEx.P) * stepX,
            //        (Convert.ToInt32(currentEx.H) * stepY)
            //        );

            //        bf.Graphics.DrawRectangle(
            //        new Pen(borderColor, 1),
            //        (Convert.ToInt32(currentEx.p1) - 30) * stepX,
            //        ((Convert.ToInt32(currentEx.h1)) * stepY) + (rect.Height - ((Convert.ToInt32(currentEx.H) * stepY) + ((Convert.ToInt32(currentEx.h1)) * stepY))) - ((Convert.ToInt32(currentEx.h1)) * stepY),
            //        Convert.ToInt32(currentEx.P) * stepX,
            //        (Convert.ToInt32(currentEx.H) * stepY)
            //        );
            //    }
            //    else
            //    {
            //        bf.Graphics.DrawRectangle(
            //        new Pen(borderColor, 1),
            //        (Convert.ToInt32(currentEx.p1) - 30) * stepX,
            //        ((Convert.ToInt32(currentEx.h1)) * stepY) + (rect.Height - ((Convert.ToInt32(currentEx.H) * stepY) + ((Convert.ToInt32(currentEx.h1)) * stepY))) - ((Convert.ToInt32(currentEx.h1)) * stepY),
            //        Convert.ToInt32(currentEx.P) * stepX,
            //        (Convert.ToInt32(currentEx.H) * stepY)
            //        );
            //    }
            //}





        }

    
}
}
