using System;
using System.Data;
using System.Diagnostics;
using Model;

namespace ViewModel1
{
    public class ItemDB : DBFunctions
    {

        private ItemList list = new ItemList();
        readonly DBFunctions TmDB = new DBFunctions();


        private Item CreateModel(Item i)
        {

            i.ItemID = (int)reader["CartID"];
            i.Name = reader["Name"].ToString();
            i.Price = int.Parse(reader["Price"].ToString());
            i.Description = reader["Description"].ToString();
            i.Category = reader["Category"].ToString();
            i.ItemImg = reader["ItemImg"].ToString();
            i.Quantity = (int)reader["Quantity"];

            return i;


        }

        private ItemList GetItemList(string sqlStr)
        {

            try
            {

                cmd = GenerateOleDBCommand(sqlStr, "DB.accdb");
                conObj.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    Item c = new Item();
                    list.Add(CreateModel(c));
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

        public ItemList SelectAllItemList()
        {

            string sqlStr = "Select*From ItemsTbl";
            list = GetItemList(sqlStr);
            return list;

        }

        public Item SelectItemByName(string ItemName)
        {

            string sqlStr = "Select*From ItemsTbl" + "where ItemName=" + ItemName + "'";
            list = GetItemList(sqlStr);
            Item c = list.Find(item => item.Name == ItemName);
            return c;
        }
        public Item SelectItemByID(int ItemID)
        {

            string sqlStr = $"Select*From ItemsTbl where ItemID= {ItemID}";
            DataTable dt = TmDB.Select(sqlStr, "DB.accdb");
            Item item = new Item
            {
                ItemID = int.Parse(dt.Rows[0]["ItemID"].ToString()),
                Name = dt.Rows[0]["Name"].ToString(),
                Price = int.Parse(dt.Rows[0]["Price"].ToString()),
                ItemImg = dt.Rows[0]["ItemImg"].ToString(),
                Quantity = int.Parse(dt.Rows[0]["Quantity"].ToString()),
                Description = dt.Rows[0]["Description"].ToString(),
                Category = dt.Rows[0]["Category"].ToString()
            };
            return item;

        }

        public ItemList SelectItemListByCategory(string CategoryName)
        {

            string sqlStr = "Select*From ItemsTbl" + "where Category=" + CategoryName + "'";
            list = GetItemList(sqlStr);
            return list;
        }

        public ItemList SelectItemListByPrice(double Price1, double Price2)
        {

            string sqlStr = "Select*From ItemsTbl" + "where price between" + Price1 + "and" + Price2;
            list = GetItemList(sqlStr);
            return list;
        }

        public int AddItem(Item item)
        {

            string insertSql = string.Format("insert into ItemsTbl" + "(CartID,Name,Price,ItemImg,Quantity,Description,Category)" + "values({0},'{1}','{2}','{3}',{4},'{5}','{6}',{7})", item.Name, item.Price, item.Description, item.Quantity, item.ItemID, item.Category, item.ItemImg);
            return TmDB.ChangeTable(insertSql, "DB.accdb");


        }

        public int UpdateItem(Item item)
        {

            string updateSql = string.Format("update ItemsTbl SET" + "',Name='" + item.Name + "',Category='" + item.Category + "',Description='" + item.Description + "',CartID='" + item.ItemID + "',Price='" + item.Price + "',ItemImg='" + item.ItemImg + "',Quantity='" + item.Quantity);
            return TmDB.ChangeTable(updateSql, "DB.accdb");

        }

        public int DeleteItem(Item item)
        {
            string delSql = string.Format("Delete from ItemsTbl" + "where CartID=" + item.ItemID);
            return TmDB.ChangeTable(delSql, "DB.accdb");
        }

        public DataTable GetItems()
        {
            string sqlStr = "Select * From ItemsTbl";
            DataTable dt = TmDB.Select(sqlStr, "DB.accdb");
            return dt;
        }
        public int ItemAmount()
        {
            string sqlStr = "Select * From ItemsTbl";
            int amount = TmDB.Select(sqlStr, "DB.accdb").Rows.Count;
            return amount;
        }



    }
}
