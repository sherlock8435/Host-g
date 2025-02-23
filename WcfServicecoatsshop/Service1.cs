using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Model;
using ViewModel;
using ViewModel1;

namespace WcfServicecoatsshop
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        ItemDB Idb = new ItemDB();
        CityDB Cdb = new CityDB();
        OrdersDB Odb = new OrdersDB();
        UserDB Udb = new UserDB();
        Mailboxdb Mdb = new Mailboxdb();


        public CityList SelectAllCities()
        {
            return Cdb.SelectAllCities();
        }


        public Cities SelectCityByName(string cityName)
        {

            return Cdb.SelectCityByName(cityName);

        }

        public Cities SelectCityById(int id)
        {

            return Cdb.SelectCityById(id);

        }

        public List<Cities> OrderByName()
        {
            return Cdb.OrderByName();
        }
        public ItemList SelectAllItemList()
        {
            return Idb.SelectAllItemList();
        }
        public Item SelectItemByName(string ItemName)
        {
            return Idb.SelectItemByName(ItemName);
        }
        public ItemList SelectItemListByCategory(string CategoryName)
        {
            return Idb.SelectItemListByCategory(CategoryName);
        }
        public ItemList SelectItemListByPrice(double Price1, double Price2)
        {
            return Idb.SelectItemListByPrice(Price1, Price2);
        }
        public int AddItem(Item item)
        {
            return Idb.AddItem(item);
        }

        public int UpdateItem(Item item)
        {
            return Idb.UpdateItem(item);
        }

        public int DeleteItem(Item item)
        {
            return Idb.DeleteItem(item);
        }

        public OrderList SelectAllOrders(string uEmail)
        {
            return Odb.SelectAllOrders(uEmail);
        }
        public int AddOrder(Order od)
        {
            return Odb.AddOrder(od);
        }
        public OrderList SelectOrdersByOrderDate(string uEmail, string orderDate)
        {
            return Odb.SelectOrdersByOrderDate(uEmail, orderDate);
        }
        public Order SelectOneOrder(string uEmail, string orderDate, int ItemCode)
        {
            return Odb.SelectOneOrder(uEmail, orderDate, ItemCode);
        }
        public int DeleteUserByEmail(string UserEmail, string UserPass)
        {
            return Udb.DeleteUserByEmail(UserEmail, UserPass);
        }
        public UserList SeletAllUsers()
        {
            return Udb.SeletAllUsers();
        }
        public int AddUser(User user)
        {
            return Udb.AddUser(user);
        }
        public String SelectUserIDByEmail(string UserEmail)
        {
            return Udb.SelectUserIDByEmail(UserEmail);
        }
        public bool CheckUserExist(string uPass, string uEmail)
        {
            return Udb.CheckUserExist(uPass, uEmail);
        }
        public bool CheckAdminExist(string uPass, string uEmail)
        {
            return Udb.CheckAdminExist(uPass, uEmail);
        }
        public bool CheckUserExistByEmail(string uEmail)
        {
            return Udb.CheckUserExistByEmail(uEmail);
        }
        public string GetQuestion(string uEmail)
        {
            return Udb.GetQuestion(uEmail);
        }
        public string PassRecovery(string uEmail, string uAnswer)
        {
            return Udb.PassRecovery(uEmail, uAnswer);
        }
        public User GetUserByEmail(string uEmail)
        {
            return Udb.GetUserByEmail(uEmail);
        }
        public int UpdateUserProfile(User usr)
        {
            return Udb.UpdateUserProfile(usr);
        }
        public int CountUsers()
        {
            return Udb.CountUsers();
        }
        public MailBoxList SelectAllMsg()
        {
            return Mdb.SelectAllMsg();
        }
    }
}
