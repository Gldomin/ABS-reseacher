using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace BD_test
{
    public enum ProgressBarDisplayText
    {
        Percentage,
        CustomText
    }
    class CustomProgressBar : ProgressBar
    {
        BufferedGraphics bf;
        BufferedGraphicsContext bfc;

        static Color backColor = Color.White; //Фон
        static Color borderColor = Color.Black; //Рамка
        static Color borderBarColor = Color.Green; //Рамка бара
        static Color barStartingColor = Color.Green;  //Начало градиента Color.FromArgb(255, 0, 0); 
        static Color barEndingColor = Color.LightGreen; //Конец градиента бара
        Brush textColor = Brushes.Black; //Цвет надписи бара
        FontFamily font = FontFamily.GenericSerif; //шрифт
        int fontScale = 12; //размер шрифта

        //Property to set to decide whether to print a % or Text
        public ProgressBarDisplayText DisplayStyle { get; set; }

        //Property to hold the custom text
        public String CustomText { get; set; }

        public CustomProgressBar()
        {
            // Modify the ControlStyles flags
            //http://msdn.microsoft.com/en-us/library/system.windows.forms.controlstyles.aspx
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
        }

       LinearGradientBrush linGrBrush = new LinearGradientBrush(
       new Point(0, 10),
       new Point(240, 10), //ширина прогрессбара
       barStartingColor,   
       barEndingColor
           );  



        protected override void OnPaint(PaintEventArgs e)
        {

            Rectangle rect = ClientRectangle;
            Graphics g = e.Graphics;


            bfc = BufferedGraphicsManager.Current;

            bf = bfc.Allocate(g, rect);

                //ProgressBarRenderer.DrawHorizontalBar(g, rect);//LAYER
                //System.Threading.Thread.Sleep(250);           
            bf.Graphics.FillRectangle(new SolidBrush(backColor), rect);
            bf.Graphics.DrawRectangle(new Pen(borderColor, 1),rect.X, rect.Y, rect.Width-1, rect.Height-1);

            rect.Inflate(-3, -3);
            if (Value > 0)
            {
                // As we doing this ourselves we need to draw the chunks on the progress bar
                Rectangle clip = new Rectangle(rect.X, rect.Y, (int)Math.Round(((float)Value / Maximum) * rect.Width), rect.Height);

                bf.Graphics.FillRectangle(linGrBrush, clip);
                bf.Graphics.DrawRectangle(new Pen(borderBarColor), clip);
                //ProgressBarRenderer.DrawHorizontalChunks(g, clip);//LAYER
                //System.Threading.Thread.Sleep(250);
            }


            // Set the Display text (Either a % amount or our custom text
            string text = DisplayStyle == ProgressBarDisplayText.Percentage ? Value.ToString() + '%' : CustomText;


            using (Font f = new Font(font, fontScale))
            {

                SizeF len = g.MeasureString(text, f);
                // Calculate the location of the text (the middle of progress bar)
                // Point location = new Point(Convert.ToInt32((rect.Width / 2) - (len.Width / 2)), Convert.ToInt32((rect.Height / 2) - (len.Height / 2)));
                Point location = new Point(Convert.ToInt32((Width / 2) - len.Width / 2), Convert.ToInt32((Height / 2) - len.Height / 2));
                // The commented-out code will centre the text into the highlighted area only. This will centre the text regardless of the highlighted area.
                // Draw the custom text
                bf.Graphics.DrawString(text, f, textColor, location);
                    //g.DrawString(text, f, Brushes.Black, location);
                    //System.Threading.Thread.Sleep(250);
            }
            bf.Render();
            bf.Dispose();
        }
    }
}


