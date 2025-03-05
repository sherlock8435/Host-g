using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel1
{
    public class UserDB : DBFunctions
    {
        private UserDB usr = null;
        private UserList list = new UserList();
        DBFunctions dbf = new DBFunctions();

        public UserDB() : base() { }

        private User CreateModel(User usr)
        {

            CityDB c = new CityDB();
            usr.Fname = reader["Fname"].ToString();
            usr.Lname = reader["LName"].ToString();
            usr.UserPass = reader["userPass"].ToString();
            usr.Utelnum = reader["Utelnum"].ToString();
            int CityID = (int)reader["City"];
            usr.UserEmail = reader["userEmail"].ToString();
            usr.Ubirthday = reader["birthDay"].ToString();
            usr.Ugender = reader["Gender"].ToString();
            usr.uQuestion = reader["Question"].ToString();
            usr.uAnswer = reader["Answer"].ToString();
            return usr;


        }

        public int AddUser(User user)
        {
            string updateSql = string.Format("INSERT INTO Usertbl " +
                "(Uanswer, Uquestion, Utelnum, Ubirthday, Ugender, CityID, UserPass, Lname, Fname, UserEmail)" +
                 $" VALUES ('{user.uAnswer}', '{user.uQuestion}', '{user.Utelnum}', '{user.Ubirthday:yyyy-MM-dd}', '{user.Ugender}', {user.CityID}, '{user.UserPass}', '{user.Lname}', '{user.Fname}', '{user.UserEmail}')");

            return dbf.ChangeTable(updateSql, "DB.accdb");

        }
        public int DeleteUserByEmail(string UserEmail, string UserPass)
        {

            string delSql = string.Format($"Delete from Usertbl" + $"where UserEmail='{UserEmail}'and UserPass='{UserPass}'");
            return dbf.ChangeTable(delSql, "DB.accdb");
        }

        public UserList SeletAllUsers()
        {

            UserList list = new UserList();

            try
            {

                string sqlStr = "SELECT*FROM Usertbl";
                cmd = GenerateOleDBCommand(sqlStr, "DB.accdb");
                conObj.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    User usr = new User();
                    list.Add(CreateModel(usr));
                }


            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);

            }
            finally
            {

                if (reader != null) conObj.Close();
                if (this.conObj.State == System.Data.ConnectionState.Open)
                    this.conObj.Close();
            }
            return list;

        }

        public String SelectUserIDByEmail(string UserEmail)
        {

            DataTable dt = null;
            string sqlStr = $"SELECT UserPass FROM Usertbl where UserEmail= '{UserEmail}' ";
            dt = dbf.Select(sqlStr, "DB.accdb");
            if (dt != null) return "user not found";
            return dt.Rows[0][0].ToString();
        }

        public bool CheckUserExist(string uPass, string uEmail)
        {
            DataTable dt = null;
            string sqlStr = $"select * from Usertbl where UserEmail='{uEmail}'  and UserPass=  '{uPass} '";
            dt = dbf.Select(sqlStr, "DB.accdb");
            if (dt != null)
                return false;
            return true;
        }

        public bool CheckUserExistByEmail(string uEmail)
        {

            DataTable dt = null;
            string sqlStr = $"select * from Usertbl where UserEmail= '{uEmail}'";
            dt = dbf.Select(sqlStr, "DB.accdb");
            if (dt != null)
                return false;
            return true;

        }

        public User GetUserByEmail(string uEmail)
        {
            DataTable dt = null;
            string sqlStr = "select*from Usertbl where UserEmail = " + uEmail + "";
            dt = dbf.Select(sqlStr, "DB.accdb");
            if (dt.Rows.Count == 0)
                return null;
            User user = new User
            {
                UserEmail = dt.Rows[0]["UserEmail"].ToString(),
                Fname = dt.Rows[0]["Fname"].ToString(),
                Lname = dt.Rows[0]["Lname"].ToString(),
                UserPass = dt.Rows[0]["UserPass"].ToString(),
                CityID = (int)dt.Rows[0]["CityID"],
                Ugender = dt.Rows[0]["Ugender"].ToString(),
                Ubirthday = dt.Rows[0]["Ubirthday"].ToString(),
                Utelnum = dt.Rows[0]["Utelnum"].ToString(),
                uQuestion = dt.Rows[0]["Uquestion"].ToString(),
                uAnswer = dt.Rows[0]["Uanswer"].ToString()
            };
            return user;
        }

        public bool CheckAdminExist(string uPass, string uEmail)
        {

            return dbf.CheckAdmin(uPass, uEmail);

        }

        public string GetQuestion(string uEmail)
        {
            DataTable dt = null;
            string sqlStr = "SELECT Uquestion FROM Usertbl where UserEmail=" + uEmail + "";
            dt = dbf.Select(sqlStr, "DB.accdb");
            if (dt != null) return null;
            return dt.Rows[0][0].ToString();

        }

        public string PassRecovery(string uEmail, string uAnswer)
        {
            User user = GetUserByEmail(uEmail);

            if (user.uAnswer.Equals(uAnswer))
                return user.UserPass;

            return null;
        }



        public int UpdateUserProfile(User usr)
        {

            string updateSql = $"update Usertbl SET UserPass='{usr.UserPass}'," + $"FirstName='{usr.Fname}',LastName='{usr.Lname}'," + $"telephone='{usr.Utelnum}',Birthday='{usr.Ubirthday}'" + $"where UserEmail='{usr.UserEmail}";

            return dbf.ChangeTable(updateSql, "DB.accdb");
        }

        public int CountUsers()
        {

            return SeletAllUsers().Count;


        }




    }
}
