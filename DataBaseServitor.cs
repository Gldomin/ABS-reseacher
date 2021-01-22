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
    static class DataBaseServitor
    {

        public async static void DeleteFromDB(List<String> rows, SqlConnection sqlConnection) {
            /* bool closed = true;
             try { sqlConnection.Open(); } catch (Exception) { closed = !closed; }
             for (int i = 0; i < rows.Count; i++) {
                 SqlCommand command = new SqlCommand("DELETE FROM [ExaminationsData] WHERE [Id]=@Id", sqlConnection);
                 command.Parameters.AddWithValue("Id", rows[i].Cells["id"].Value.ToString());
             }
             if (closed) sqlConnection.Close();*/
           // string temp = "";
            for (int i = 0; i < rows.Count; i++)
            {
                //temp = rows[i].Cells["id"].Value.ToString();
                if (!string.IsNullOrEmpty(rows[i]) && !string.IsNullOrWhiteSpace(rows[i]))
                {
                    SqlCommand command = new SqlCommand("DELETE FROM [ExaminationsData] WHERE [Id]=@Id", sqlConnection);
                    command.Parameters.AddWithValue("Id", rows[i]);

                    await command.ExecuteNonQueryAsync();
                }   
            }
        
        }

        public async static void AddToDB(ExaminationOne row, SqlConnection sqlConnection)
        {
            bool closed = true;
            try { sqlConnection.Open(); } catch (Exception) { closed = !closed; }

            SqlCommand command = new SqlCommand("UPDATE [ExaminationsData] SET [doctorID]=@doctorID, [patientID]=@patientID, [date]=@date, [p1]=@p1, [p2]=@p2, [avgP]=@avgP, [h1]=@h1, [h2]=@h2, [P]=@P, [H]=@H, [S]=@S, [comment]=@comment  WHERE[Id]=@Id", sqlConnection);
            command.Parameters.AddWithValue("Id", Convert.ToInt32(row.examId));
            command.Parameters.AddWithValue("doctorID", Convert.ToInt32(row.docId));
            command.Parameters.AddWithValue("patientID", Convert.ToInt32(row.patientId));
            command.Parameters.AddWithValue("date", Convert.ToDateTime(row.date));
            command.Parameters.AddWithValue("p1", float.Parse(0 + row.p1));
            command.Parameters.AddWithValue("p2", float.Parse(0 + row.p2));
            command.Parameters.AddWithValue("avgP", float.Parse(0 + row.avgP));
            command.Parameters.AddWithValue("h1", float.Parse(0 + row.h1));
            command.Parameters.AddWithValue("h2", float.Parse(0 + row.h2));
            command.Parameters.AddWithValue("P", float.Parse(0 + row.P));
            command.Parameters.AddWithValue("H", float.Parse(0 + row.H));
            command.Parameters.AddWithValue("S", float.Parse(0 + row.S));
            command.Parameters.AddWithValue("comment", row.comment);
                                 

                    await command.ExecuteNonQueryAsync();
            if (closed) sqlConnection.Close();
        }

        public static void DataTableInfusion(DataGridView table, Boolean infuseAll, SqlConnection sqlConnection ) //производит загрузку DataGridView
        { //infuseAll включает автодобавление столбцов таблицы, при отключении добавляет лишь необходимые столбцы (нужно сначала создать столбцы и указать их источник)
            bool closed = true;
            try { sqlConnection.Open(); } catch (Exception) { closed = !closed; }
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM ExaminationsData", sqlConnection);
            DataTable tempDataTable = new DataTable();
            if (infuseAll) table.AutoGenerateColumns = false; else table.AutoGenerateColumns = true; //infuseAll
            sqlDa.Fill(tempDataTable);
            // direct method
            table.DataSource = tempDataTable;
            if (closed) sqlConnection.Close();
        }
        public static void DataTableInfusion(DataGridView table, SqlConnection sqlConnection) //Function overloading
        { //infuseAll включает автодобавление столбцов таблицы, при отключении добавляет лишь необходимые столбцы (нужно сначала создать столбцы и указать их источник)
            bool closed = true;
            try { sqlConnection.Open(); } catch (Exception) { closed = !closed; }
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM ExaminationsData", sqlConnection);
            DataTable tempDataTable = new DataTable();
            sqlDa.Fill(tempDataTable);
            // direct method
            table.DataSource = tempDataTable;
            if (closed) sqlConnection.Close();
        }

        public static void DataTableInfusion(DataGridView table, SqlConnection sqlConnection, string fromData) //Function overloading
        { //infuseAll включает автодобавление столбцов таблицы, при отключении добавляет лишь необходимые столбцы (нужно сначала создать столбцы и указать их источник)
            bool closed = true;
            try { sqlConnection.Open(); } catch (Exception) { closed = !closed; }
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM "+fromData, sqlConnection);
            
            DataTable tempDataTable = new DataTable();
            sqlDa.Fill(tempDataTable);
            // direct method
            table.DataSource = tempDataTable;
            if (closed) sqlConnection.Close();
        }
        public static int getPatientFromExam( SqlConnection sqlConnection, int id) //берет пациента из БД по номеру обследования
        {
            bool closed = true;
            try { sqlConnection.Open(); } catch (Exception) { closed = !closed; }
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM ExaminationsData WHERE [Id]=@Id", sqlConnection);

            MessageBox.Show(Convert.ToString(sqlReader["patientID"]));
            
            int MedId = Convert.ToInt32(sqlReader["patientID"]);

          
           
            






            /*
            DataGridView table = new DataGridView();
            
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM ExaminationsData WHERE [Id]=@Id", sqlConnection);
            sqlDa.SelectCommand.Parameters.Add("Id", id);
            DataTable tempDataTable = new DataTable();
            sqlDa.Fill(tempDataTable);
            // direct method
            table.DataSource = tempDataTable;
            */
            if (closed) sqlConnection.Close();
            //return Convert.ToInt32(table.Rows[0].Cells["MedBookID"]);
            if (sqlReader != null)
                sqlReader.Close();
            return MedId;
        }

    }
}
