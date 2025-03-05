using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel1
{
    public class OrdersDB : DBFunctions
    {
        private OrderList list = new OrderList();
        DBFunctions dbf = new DBFunctions();


        private Order CreateModel(Order order)
        {

            order.UserEmail = reader["UserEmail"].ToString();
            order.ItemId = int.Parse(reader["ItemId"].ToString());
            order.ItemCode = int.Parse(reader["ItemCode"].ToString());
            order.OrderStatus = reader["OrderStatus"].ToString();
            order.OrderDate = reader["OrderDate"].ToString();
            order.Qnty = int.Parse(reader["Qnty"].ToString());
            order.Price = int.Parse(reader["price"].ToString());
            order.VisaNumber = reader["VisaNumber"].ToString();
            return order;


        }

        private OrderList SelectOrders(string sqlStr)
        {

            try
            {

                cmd = GenerateOleDBCommand(sqlStr, "DB.accdb");
                conObj.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    Order C = new Order();
                    list.Add(CreateModel(C));

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

        public OrderList SelectAllOrders(string uEmail)
        {

            string sqlStr = "Select * From OrdersTbl where uEmail=" + uEmail + "'";
            return SelectOrders(sqlStr);
        }

        public int AddOrder(Order od)
        {

            string insertSql = string.Format("Insert into OrdersTbl" + "(ItemCode,UserEmail,OrderDate,ItemId,Qnty,Price,OrderStatus,VisaNumber) values ({0},'{1}','{2}','{3}',{4},{5},'{6}','{7}')", od.ItemId, od.ItemCode, od.UserEmail, od.OrderDate, od.OrderDate, od.Price, od.Price, od.Qnty, od.VisaNumber);
            return dbf.ChangeTable(insertSql, "DB.accdb");

        }

        public OrderList SelectOrdersByOrderDate(string uEmail, string orderDate)
        {

            string sqlStr = string.Format("Select*From OrdersTbl where userEmail=" + uEmail + "'and OrderDate='" + orderDate + "'");
            return SelectOrders(sqlStr);
        }

        public Order SelectOneOrder(string uEmail, string orderDate, int ItemCode)
        {

            string SqlStr = string.Format("Select*From OrdersTbl where userEmail='" + uEmail + "'and OrderDate'" + orderDate + "'and ItemCode='" + ItemCode);
            list = SelectOrders(SqlStr);
            Order order = list.Find(item => item.ItemCode == ItemCode && item.OrderDate == orderDate);
            return order;




        }
    }
}