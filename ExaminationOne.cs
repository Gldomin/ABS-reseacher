using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_test
{
     public class ExaminationOne 

    {
        public ExaminationOne(int examId, string docId, string patientId, string p1, string p2, string avgP, string h1, string h2, string P, string H, string S, string comment, string date) 
        {
            this.examId = examId;
            this.docId = docId;
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
         }

        public ExaminationOne(int examId, string docId, string patientId, string comment, string date)
        {
            this.examId = examId;
            this.docId = docId;
            this.patientId = patientId;
            this.comment = comment;
            this.date = date;
        }
        public ExaminationOne()
        {
            
        }

        public int examId = 0;
        public String docId = "0", patientId = "0", p1 = "0", p2 = "0", avgP = "0", h1 = "0", h2 = "0", P = "0", H = "0", S = "0", comment = "0", date = "0";
        
        public void cogitationOn() //вычисление вторичных характеристик
        {
            avgP = Convert.ToString((Convert.ToDouble("0" + p1) + Convert.ToDouble("0" + p2)) / 2);
            P = Convert.ToString((Math.Abs(Convert.ToDouble("0" + p1) - Convert.ToDouble("0" + p2))));
            H = Convert.ToString((Math.Abs(Convert.ToDouble(0 + h1) - Convert.ToDouble(0 + h2))));
            S = Convert.ToString(Convert.ToDouble(H) * Convert.ToDouble(P));
        }
        public void cogitationOn(string p1, string p2, string h1, string h2)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.h1 = h1;
            this.h2 = h2;

            avgP = Convert.ToString((Convert.ToDouble("0" + p1) + Convert.ToDouble("0" + p2)) / 2);
            P = Convert.ToString((Math.Abs(Convert.ToDouble("0" + p1) - Convert.ToDouble("0" + p2))));
            H = Convert.ToString((Math.Abs(Convert.ToDouble(0 + h1) - Convert.ToDouble(0 + h2))));
            S = Convert.ToString(Convert.ToDouble(H) * Convert.ToDouble(P));
        }

    }
}
