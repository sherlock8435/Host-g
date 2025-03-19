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
        readonly DBFunctions dbf = new DBFunctions();


        private Order CreateModel(Order order)
        {
            order.UserEmail = reader["UserEmail"].ToString();
            order.CartID = int.Parse(reader["CartID"].ToString());
            order.OrderDate = reader["OrderDate"].ToString();
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

                reader?.Close();
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
            string insertSql = $"Insert into OrderTbl (CartID,UserEmail,OrderDate,Price,VisaNumber) values" +
                $" ({od.CartID},'{od.UserEmail}','{od.OrderDate}',{od.Price},'{od.VisaNumber}')";
            return dbf.ChangeTable(insertSql, "DB.accdb");
        }

        public OrderList SelectOrdersByOrderDate(string uEmail, string orderDate)
        {

            string sqlStr = string.Format("Select*From OrdersTbl where userEmail=" + uEmail + "'and OrderDate='" + orderDate + "'");
            return SelectOrders(sqlStr);
        }

        public Order SelectOneOrder(string uEmail, string orderDate, int ItemCode)
        {

            string SqlStr = string.Format("Select*From OrdersTbl where userEmail='" + uEmail + "'and OrderDate'" + orderDate + "'and CartID='" + ItemCode);
            list = SelectOrders(SqlStr);
            Order order = list.Find(item => item.CartID == ItemCode && item.OrderDate == orderDate);
            return order;




        }
    }
}