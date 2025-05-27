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
        private readonly MailBoxList list = new MailBoxList();
        
        private MailBox CreateModel(MailBox m)
        {
            m.SenderEmail = reader["SenderEmail"].ToString();
            m.msgDate = reader["msgDate"].ToString();
            m.msgRead = (bool)reader["msgRead"];
            m.msgSubject = reader["msgSubject"].ToString();
            m.SenderName = reader["Sendername"].ToString();
            m.msgBody = reader["msgBody"].ToString();
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

                cmd = GenerateOleDBCommand(sqlStr, "DB.accdb");
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
                reader?.Close();
                if (this.conObj.State == System.Data.ConnectionState.Open)
                    this.conObj.Close();
            }
            return list;
        }

        public MailBoxList SelectMsgByEmail(string email)
        {
            string sqlStr = $"SELECT * FROM MailBoxtbl where SenderEmail='{email}'";
            return SelectAll(sqlStr);
        }

        public int AddMassage(MailBox m)
        {
            string insertSql = string.Format($"INSERT INTO MailBoxtbl(SenderEmail, msgDate, SenderName, msgSubject, msgBody, msgRead) " +
                $"VALUES('{m.SenderEmail}', '{m.msgDate}', '{m.SenderName}', '{m.msgSubject}', '{m.msgBody}', {m.msgRead})");
            return ChangeTable(insertSql, "DB.accdb");
        }
    }
}
