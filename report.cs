using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BD_test
{
    public partial class report : Form
    {
        DataGridView data;
        public report(DataGridView data)
        {
            this.data = data;
            InitializeComponent();
        }

        private void report_Load(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.exportComboBox, "Выбор вида выходного документа");
            toolTip1.SetToolTip(this.nameTextBox, "Введите название документа, в который будет помещен отчет");
            toolTip1.SetToolTip(this.headerSize, "Выбор размера шрифта шапки таблицы");
            toolTip1.SetToolTip(this.textSize, "Выбор размера шрифта ячеек таблицы");
            toolTip1.SetToolTip(this.headerText, "Введите название отчета. Название будет отображено внутри документа");
            toolTip1.SetToolTip(this.font, "Выбор шрифта таблицы");
            toolTip1.SetToolTip(this.borderSize, "Выбор толщины границ ячеек");

            exportComboBox.SelectedIndex = 0;
            textSize.SelectedIndex = 1;
            font.SelectedIndex = 2;
            borderSize.SelectedIndex = 0;
            headerSize.SelectedIndex = 3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string exportTo = Convert.ToString(exportComboBox.SelectedItem);
                Export expo = new Export();
                switch (exportTo) {

                    case ("Microsoft Word"):
                        expo.boopWord(data, nameTextBox.Text, font.Text, Convert.ToInt32(headerSize.Text), Convert.ToInt32(textSize.Text), headerText.Text, Convert.ToInt32(borderSize.Text));
                        
                        break;
                    
                    case ("Microsoft Excel"):
                        expo.boopExcel(data, nameTextBox.Text);
                        //expo.btnExportToExcel(data);
                        break;
                    
                    default:
                        MessageBox.Show("Пожалуйста, выбирите вид отчета");
                        break;
                }
                
            }
            catch (Exception) { MessageBox.Show("Пожалуйста, заполните и выбирите все поля"); }
        }

        private void exportComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (exportComboBox.Text == "Microsoft Word")
            {
                ToolTip toolTip1 = new ToolTip();
                
                toolTip1.SetToolTip(this.headerSize, "Выбор размера шрифта шапки таблицы");
                toolTip1.SetToolTip(this.textSize, "Выбор размера шрифта ячеек таблицы");
                toolTip1.SetToolTip(this.headerText, "Введите название отчета. Название будет отображено внутри документа");
                toolTip1.SetToolTip(this.font, "Выбор шрифта таблицы");
                toolTip1.SetToolTip(this.borderSize, "Выбор толщины границ ячеек");

                headerSize.Enabled = true;
                textSize.Enabled = true;
                headerText.Enabled = true;
                font.Enabled = true;
                borderSize.Enabled = true;

            }
            else {
                ToolTip toolTip1 = new ToolTip();
                
              //  toolTip1.SetToolTip(this.headerSize, "Вид отчета Microsoft Excel не учитывает данный параметр");
              //  toolTip1.SetToolTip(this.textSize, "Вид отчета Microsoft Excel не учитывает данный параметр");
              //  toolTip1.SetToolTip(this.headerText, "Вид отчета Microsoft Excel не учитывает данный параметр");
              //  toolTip1.SetToolTip(this.font, "Вид отчета Microsoft Excel не учитывает данный параметр");
              //  toolTip1.SetToolTip(this.borderSize, "Вид отчета Microsoft Excel не учитывает данный параметр");

                headerSize.Enabled = false;
                textSize.Enabled = false;
                headerText.Enabled = false;
                font.Enabled = false;
                borderSize.Enabled = false;


            }
        }

        private void exportComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
