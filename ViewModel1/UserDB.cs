using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel1
{
    public class UserDB : DBFunctions
    {
        UserDB usr = null;
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
            return usr;


        }

        public int DeleteUserByEmail(string UserEmail, string UserPass)
        {

            string delSql = string.Format($"Delete from UsersTbl" + $"where UserEmail='{UserEmail}'and UserPass='{UserPass}'");
            return dbf.ChangeTable(delSql, "MyDBase.accdb");
        }

        public UserList SeletAllUsers()
        {

            UserList list = new UserList();

            try
            {

                string sqlStr = "SELECT*FROM UsersTbl";
                cmd = GenerateOleDBCommand(sqlStr, "MyDBase.accdb");
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
            string sqlStr = "SELECT UserPass FROM StudentsTbl where UserEmail=" + UserEmail + "";
            dt = dbf.Select(sqlStr, "MyDBase.accdb");
            if (dt != null) return "user not found";
            return dt.Rows[0][0].ToString();
        }

        public bool CheckUserExist(string uPass, string uEmail)
        {

            DataTable dt = null;
            string sqlStr = "select*from UsersTbl where UserEmail=" + uEmail + "and UserPass=" + uPass + "";
            dt = dbf.Select(sqlStr, "MyDBase.accdb");
            if (dt != null) return false;
            return (dt.Rows.Count > 0);



        }

        public bool CheckAdminExist(string uPass, string uEmail)
        {

            return dbf.CheckAdmin(uPass, uEmail);

        }

        public int UpdateUserProfile(User usr)
        {

            string updateSql = $"update UsersTbl SET UserPass='{usr.UserPass}'," + $"FirstName='{usr.Fname}',LastName='{usr.Lname}'," + $"telephone='{usr.Utelnum}',Birthday='{usr.Ubirthday}'" + $"where UserEmail='{usr.UserEmail}";

            return dbf.ChangeTable(updateSql, "MyDBase.accdb");
        }

        public int CountUsers()
        {

            return SeletAllUsers().Count;


        }




    }
}
