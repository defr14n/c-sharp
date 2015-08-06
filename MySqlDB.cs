/*
kowe kudu nginstall mysql connecter ngge .NET disek sak durunge gawe library iki

*/
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace DB
{
   
    public class MySqlDB
    {
        private MySqlConnection con;
        private MySqlCommand cmd;
        private MySqlDataAdapter DA;
        private DataSet DS = new DataSet();

        public Mysql()
        {
            con = new MySqlConnection("server=localhost;port=3306;uid=root;pwd=;database=laravel");
        }

        public MySqlConnection Open()
        {
            if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
            {
                con.Open();
            }
            
            return con;
        }
        public MySqlConnection Close()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return con;
        }
        public void Execute(string query)
        {
            cmd = new MySqlCommand();
            try
            {
                cmd.Connection = Open();
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException Ex)
            {
                throw Ex;
            }
            finally
            {
                cmd = null;
            }
        }
        public void Insert(string table, string[,] Field)
        {
            string query = "";
            query = "INSERT INTO " + table + " SET ";
            
            for (int i = 0; i <= Field.GetUpperBound(0); i++)
            {
                query += MySqlHelper.EscapeString(Field[i, 0])+ "='" + MySqlHelper.EscapeString(Field[i, 0]) + "',";
            }
            
            query = query.TrimEnd(',');
            Execute(query);
        }

        public void Delete(string table, string key, string value)
        {
            string query = "DELETE FROM " + table + " WHERE " + MySqlHelper.EscapeString(key) + "='" + MySqlHelper.EscapeString(value) + "'";
            Execute(query);
        }

        public void Update(string table, string[,] Field, string key, string value)
        {
            string query = "";
            query = "UPDATE " + table + " SET ";

            for (int i = 0; i <= Field.GetUpperBound(0); i++)
            {
                query += MySqlHelper.EscapeString(Field[i, 0]) + "='" + MySqlHelper.EscapeString(Field[i, 0]) + "',";
            }

            query = query.TrimEnd(',');
            query += " WHERE " + MySqlHelper.EscapeString(key) + " ='" + MySqlHelper.EscapeString(value) + "'";
            Execute(query);
        }
        public DataSet DataSet(string query)
        {
            con.Open();
            cmd = con.CreateCommand();
            DA = new MySqlDataAdapter(query, con);
            DS.Reset();
            DA.Fill(DS);
            con.Close();
            return (DS);
        }

        public DataTable getDataTable(string query)
        {
            cmd = new MySqlCommand();
            MySqlDataAdapter DA;
            try
            {
                cmd.Connection = Open();
                cmd.CommandText = query;
                DA = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                DA.Fill(dt);

                return dt;
            }
            catch (MySqlException Ex)
            {
                throw Ex;
            }
            finally
            {
                cmd = null;
            }
        }

        public MySqlDataReader getDataReader(string query)
        {
            cmd = new MySqlCommand();
            try
            {
                cmd.Connection = Open();
                cmd.CommandText = query;
                MySqlDataReader DR;
                DR = cmd.ExecuteReader();
                return DR;
            }
            catch (MySqlException Ex)
            {
                throw Ex;
            }
            finally
            {
                cmd = null;
            }
        }
        public void Backup()
        {
            try
               {
                 DateTime Time = DateTime.Now;
                 int year = Time.Year;
                 int month = Time.Month;
                 int day = Time.Day;
                 int hour = Time.Hour;
                 int minute = Time.Minute;
                 int second = Time.Second;
                 int millisecond = Time.Millisecond;
         
                 //Save file to C:\ with the current date as a filename
                 string path;
                 path = "C:\\MySqlBackup" + year + "-" + month + "-" + day + 
         	"-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";
                 StreamWriter file = new StreamWriter(path);
         
                 
                 ProcessStartInfo psi = new ProcessStartInfo();
                 psi.FileName = "mysqldump";
                 psi.RedirectStandardInput = false;
                 psi.RedirectStandardOutput = true;
                 psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", 
         			uid, password, server, database);
                 psi.UseShellExecute = false;
         
                 Process process = Process.Start(psi);
         
                 string output;
                 output = process.StandardOutput.ReadToEnd();
                 file.WriteLine(output);
                 process.WaitForExit();
                 file.Close();
                 process.Close();
             }
             catch (IOException ex)
             {
                 MessageBox.Show("Error , unable to backup!");
             }
         }
         public void Restore()
         {
             try
             {
                 //Read file from C:\
                 string path;
                 path = "C:\\MySqlBackup.sql";
                 StreamReader file = new StreamReader(path);
                 string input = file.ReadToEnd();
                 file.Close();
         
                 ProcessStartInfo psi = new ProcessStartInfo();
                 psi.FileName = "mysql";
                 psi.RedirectStandardInput = true;
                 psi.RedirectStandardOutput = false;
                 psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", 
         			uid, password, server, database);
                 psi.UseShellExecute = false;
         
                 
                 Process process = Process.Start(psi);
                 process.StandardInput.WriteLine(input);
                 process.StandardInput.Close();
                 process.WaitForExit();
                 process.Close();
             }
             catch (IOException ex)
             {
                 MessageBox.Show("Error , unable to Restore!");
             }
         }
    }
}
