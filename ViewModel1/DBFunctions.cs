using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace ViewModel1
{
    public class DBFunctions
    {

        protected OleDbConnection conObj;
        protected OleDbCommand cmd;
        protected OleDbDataReader reader;

        public DBFunctions()
        {

            conObj = new OleDbConnection();
            cmd = new OleDbCommand();
        }

        public OleDbCommand GenerateOleDBCommand(string sqlStr, string dbFileName)
        {
            conObj = GenerateConnection(dbFileName);
            cmd = new OleDbCommand(sqlStr, conObj);
            return cmd;
        }

        private string path()
        {
            string currentDir = Environment.CurrentDirectory;
            string[] dirStr = currentDir.Split('\\');
            int index = dirStr.Length - 3;
            dirStr[index] = "ViewModel";
            Array.Resize(ref dirStr, index + 1);
            string pathStr = String.Join("\\", dirStr);
            return pathStr;
        }

        public OleDbConnection GenerateConnection(string dbFileName)
        {
            try
            {

                if (dbFileName.Contains(".mdb"))
                    conObj.ConnectionString = "Provider=Microsoft.jet.OLEDB.4.0;Data Source=" + path() + "\\App_Data\\" + dbFileName;

                else
                    conObj.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path() + "\\App_Data\\" + dbFileName;

                conObj.Open();

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);

            }
            finally
            {

                conObj.Close();
            }
            return conObj;
        }

        public DataTable Select(string sqlString, string dbFileName)
        {

            conObj = GenerateConnection(dbFileName);
            DataTable dt = null;
            DataSet dsUser = new DataSet();

            try
            {

                OleDbDataAdapter daObj = new OleDbDataAdapter(sqlString, conObj);

                daObj.Fill(dsUser);
                dt = dsUser.Tables[0];

            }

            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine("error:" + ex.Message);

            }
            finally
            {

                conObj.Close();

            }

            return dt;
        }

        public int ChangeTable(string dbFileName, string sqlString)
        {

            int records = 0;

            cmd = GenerateOleDBCommand(sqlString, dbFileName);

            conObj.Open();

            try
            {
                records = cmd.ExecuteNonQuery();

            }

            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine("error:" + ex.Message);

            }

            finally
            {
                conObj.Close();
            }

            return records;
        }

        public bool CheckAdmin(string Email_,string Password_)
        {

            string strPass = "", id_ = "";
            DataSet ds = new DataSet();
            string strPath = path() + "\\App_Data\\XMLloginFile.xml";
              
            ds.ReadXml(strPath);
            DataTable dt = ds.Tables[0];
            if(dt != null )
                if(dt.Rows.Count > 0)
                    for(int i = 0; i < dt.Rows.Count; i++)
                    {

                        id_=dt.Rows[i]["name"].ToString();
                        strPass = dt.Rows[i]["Password"].ToString();
                        if(strPass==Password_&&Email_==id_)
                            return true;
                    }

            return false;





        }
    

            


        
           



    }
    }


