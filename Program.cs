using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BD_test
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static  void Main()
        {
            
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Examinations("",123));
            //Application.Run(new ChosenOneExamination());
             Application.Run(new Login());


        }
    }
}
