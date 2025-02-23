using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using ViewModel1;

namespace ViewModel
{
    public class Mailboxdb : DBFunctions
    {
        private MailBoxList list = new MailBoxList();
        private MailBox CreateModel(MailBox m)
        {
            m.SenderEmail = reader["SenderEmail"].ToString();
            m.msgDate = reader["msgDate"].ToString();
            m.msgRead = (bool)reader["msgRead"];
            m.msgSubject = reader["msgSubject"].ToString();
            m.RecieverEmail = reader["RecieverEmail"].ToString();
            m.SenderFName = reader["SenderFName"].ToString();
            m.SenderLName = reader["SenderLName"].ToString();
            return m;

        }
        public MailBoxList SelectAllMsg()
        {
            string sqlStr = "SELECT * FROM MailBoxtbl";
            return SelectAll(sqlStr);
        }


        private MailBoxList SelectAll(string sqlStr)
        {
            try
            {

                cmd = GenerateOleDBCommand(sqlStr, "GardeningApp_Data/DB.accdb");
                conObj.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    MailBox m = new MailBox();
                    list.Add(CreateModel(m));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (this.conObj.State == System.Data.ConnectionState.Open)
                    this.conObj.Close();
            }
            return list;
        }
    }
}
