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


        public int CreatCrat()
        {
            string sqlStr = $"SELECT * FROM CartTbl ORDER BY CartID";
            DataTable dt = Select(sqlStr, "DB.accdb");
            int max = int.MinValue;
            foreach (DataRow row in dt.Rows)
            {
                int existingId = int.Parse(row["CartID"].ToString());
                if (existingId > max)
                {
                    max = existingId;
                }
            }


            string sql = $"INSERT INTO CartTbl (UserEmail, Items, ItemCount, CartID) VALUES ('placeholder', '', '', {max})";
            return ChangeTable(sql, "DB.accdb");
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

        public int GetTotalPrice(string email)
        {
            Item[] items = GetCartItems(email);
            int total = 0;
            foreach (Item item in items)
            {
                total += item.Price * item.Quantity;
            }
            return total;
        }

        public int AddItemToCart(string email, Item item)
        {
            Cart cart = SelectCartByEmail(email);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserEmail = email,
                    ItemCount = "1",
                    Items = item.ItemID.ToString()
                };
                return AddToCart(cart);
            }
            else
            {
                string[] items = cart.Items.Split(',');
                string[] counts = cart.ItemCount.Split(',');
                List<string> itemList = items.ToList();
                List<string> countList = counts.ToList();


                if (itemList.Contains(item.ItemID.ToString()))
                {
                    int index = itemList.IndexOf(item.ItemID.ToString());
                    countList[index] = (int.Parse(countList[index]) + 1).ToString();
                }
                else
                {
                    itemList.Add(item.ItemID.ToString());
                    countList.Add("1");
                }

                cart.Items = string.Join(",", itemList);
                cart.ItemCount = string.Join(",", countList);

                //to ensure no leading commas
                if (cart.Items.StartsWith(","))
                {
                    cart.Items = cart.Items.Substring(1);
                }
                if (cart.ItemCount.StartsWith(","))
                {
                    cart.ItemCount = cart.ItemCount.Substring(1);
                }
                return UpdateCart(cart);
            }
        }
        public Cart SelectPlaceholderCart(string email)
        {
            Cart c = new Cart();

            string sqlStr = $"SELECT * FROM CartTbl ORDER BY CartID";
            DataTable dt = Select(sqlStr, "DB.accdb");
            int max = int.MinValue;
            foreach (DataRow row in dt.Rows)
            {
                int existingId = int.Parse(row["CartID"].ToString());
                if (existingId > max)
                {
                    max = existingId;
                }
            }


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

            string sql = $"INSERT INTO CartTbl (UserEmail, Items, ItemCount, CartID) VALUES ('placeholder', '', '', {max})";
            ChangeTable(sql, "DB.accdb");

            return c;
        }

    }

}

