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
    public partial class Menu : Form
    {
      String userType="";
        int userId = 0;

        public Menu(String userType, int userId)
        {
            InitializeComponent();
           // MessageBox.Show(userType);
            Text = Text + " - " + userType + " - ID=" + userId;
            this.userType = userType;
            this.userId = userId;
        }
      


        private void menuButton2_Click(object sender, EventArgs e)
        {
            Hide();
            Patients patientsForm = new Patients(userType, userId);
            patientsForm.ShowDialog();
            Close();
            // Patients.Show();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            //TODO: разный доступ
            switch (userType) {
                case("Оптометрист"):
                    menuButton3.Visible = false;
                    //MessageBox.Show("йоу, оптометрист!");
                    break;

                case ("Врач-исследователь"):
                    menuButton2.Visible = false;
                    menuButton3.Visible = false;
                    // MessageBox.Show("йоу, исследователь!");
                    break;
                case ("Администратор"):

                   // MessageBox.Show("йоу, администратор!");
                    break;
                default:
                    MessageBox.Show("вы неизвестны! ошибка");
                    break;
            
            }
        }

        private void menuButton1_Click(object sender, EventArgs e)
        {
            Hide();
            Examinations examinationsForm = new Examinations(userType, userId);
            examinationsForm.ShowDialog();
            Close();
        }

        private void menuButton3_Click(object sender, EventArgs e)
        {
            
            Hide();
            Users usersForm = new Users(userType,userId);
            usersForm.ShowDialog();
            Close();
        }

        private void menuButton4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
