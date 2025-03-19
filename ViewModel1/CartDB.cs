using Model;
using ViewModel1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel1
{
    public class CartDB : DBFunctions
    {
        private readonly CartList list = new CartList();
        readonly DBFunctions CaDB = new DBFunctions();


        private Cart CreateModel()
        {
            Cart c = new Cart
            {
                CartID = (int)reader["CartID"],
                ItemCount = reader["ItemCount"].ToString(),
                UserEmail = reader["UserEmail"].ToString(),
                Items = reader["Items"].ToString()
            };
            return c;
        }

        public int AddToCart(Cart c)
        {
            string sqlStr = $"Insert into CartTbl (ItemCount,UserEmail,Items) values ('{c.ItemCount}','{c.UserEmail}','{c.Items}')";
            return CaDB.ChangeTable(sqlStr, "DB.accdb");
        }

        public int UpdateCart(Cart c)
        {
            string sqlStr = $"Update CartTbl set ItemCount = '{c.ItemCount}', UserEmail = '{c.UserEmail}', Items = '{c.Items}' where CartID = {c.CartID}";
            return CaDB.ChangeTable(sqlStr, "DB.accdb");
        }
        public int DeleteCart(Cart c)
        {
            string sqlStr = $"Delete from CartTbl where CartID = {c.CartID}";
            return CaDB.ChangeTable(sqlStr, "DB.accdb");
        }

        public CartList SelectAllCarts()
        {
            try
            {
                string sqlStr = "Select * From CartTbl";
                cmd = GenerateOleDBCommand(sqlStr, "DB.accdb");
                conObj.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(CreateModel());
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
        public Cart SelectCartByEmail(string email)
        {

            string sqlStr = $"Select * From CartTbl where UserEmail = '{email}'";
            DataTable dt = Select(sqlStr, "DB.accdb");
            if (dt.Rows.Count > 0)
            {
                Cart c = new Cart
                {
                    CartID = (int)dt.Rows[0]["CartID"],
                    ItemCount = dt.Rows[0]["ItemCount"].ToString(),
                    UserEmail = dt.Rows[0]["UserEmail"].ToString(),
                    Items = dt.Rows[0]["Items"].ToString()
                };
                return c;
            }
            else
            {
                return null;
            }
        }

        public Item[] GetCartItems(string email)
        {
            string a = SelectCartByEmail(email).Items,
                q = SelectCartByEmail(email).ItemCount.ToString();

            string[] b, c;

            b = a.Split(',');
            c = q.Split(',');

            Item[] items = new Item[b.Length];
            ItemDB itemDB = new ItemDB(); // Create an instance of ItemDB

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = itemDB.SelectItemByID(int.Parse(b[i])); // Use the instance to call the method
                items[i].Quantity = int.Parse(c[i]);
            }
            return items;
        }

    }

}

