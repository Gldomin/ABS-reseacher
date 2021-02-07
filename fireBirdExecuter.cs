using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using FirebirdSql;
using FirebirdSql.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Windows.Forms;
namespace BD_test
{
    class fireBirdExecuter
    {

        public static void testFDB()
            {
                string connString = "User=SYSDBA;" +
                                    "Password=masterkey;" +
                                    "Database=C:\\SpeedRequiredPrograms\\firebirdSharpedTest.FDB;" +
                                    "DataSource=localhost;" +
                                    "Port=3050;";
                FbConnection conn = new FbConnection(connString);
                conn.Open();
                String sql = "SELECT name FROM pipe ORDER BY id";
                FbCommand com = new FbCommand(sql, conn);
                FbDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                   MessageBox.Show(dr.GetString(0));
                }
                dr.Close();
                conn.Close();
            }
        }
    }


