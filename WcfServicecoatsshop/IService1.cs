using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Model;
using ViewModel1;

namespace WcfServicecoatsshop
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        int AddToCart(Cart c);
        [OperationContract]
        int UpdateCart(Cart c);
        [OperationContract]
        int DeleteCart(Cart c);
        [OperationContract]
        CartList SelectAllCarts();
        [OperationContract]
        Cart SelectCartByEmail(string email);
        [OperationContract]
        Item[] GetCartItems(string email);
        [OperationContract]
        int GetTotalPrice(string email);
        [OperationContract]
        int AddItemToCart(string email, Item item);
        [OperationContract]
        CityList SelectAllCities();
        [OperationContract]
        Cities SelectCityByName(string cityName);
        [OperationContract]
        Cities SelectCityById(int id);
        [OperationContract]
        List<Cities> OrderByName();
        [OperationContract]
        ItemList SelectAllItemList();
        [OperationContract]
        ItemList SelectItemListByCategory(string CategoryName);
        [OperationContract]
        Item SelectItemByName(string ItemName);
        [OperationContract]
        ItemList SelectItemListByPrice(double Price1, double Price2);
        [OperationContract]
        int AddItem(Item item);
        [OperationContract]
        int UpdateItem(Item item);
        [OperationContract]
        int DeleteItem(Item item);
        [OperationContract]
        DataTable GetItems();
        [OperationContract]
        Item SelectItemByID(int ItemID);
        [OperationContract]
        int ItemAmount();
        [OperationContract]
        OrderList SelectAllOrders(string uEmail);
        [OperationContract]
        int AddOrder(Order od);
        [OperationContract]
        OrderList SelectOrdersByOrderDate(string uEmail, string orderDate);
        [OperationContract]
        Order SelectOneOrder(string uEmail, string orderDate, int ItemCode);
        [OperationContract]
        int DeleteUserByEmail(string UserEmail, string UserPass);
        [OperationContract]
        UserList SeletAllUsers();
        [OperationContract]
        int AddUser(User user);
        [OperationContract]
        String SelectUserIDByEmail(string UserEmail);
        [OperationContract]
        bool CheckUserExist(string uPass, string uEmail);
        [OperationContract]
        bool CheckAdminExist(string uPass, string uEmail);
        [OperationContract]
        string GetQuestion(string uEmail);
        [OperationContract]
        string PassRecovery(string uEmail, string uAnswer);
        [OperationContract]
        User GetUserByEmail(string uEmail);
        [OperationContract]
        bool CheckUserExistByEmail(string uEmail);
        [OperationContract]
        int UpdateUserProfile(User usr);
        [OperationContract]
        int CountUsers();
        [OperationContract]
        MailBoxList SelectAllMsg();
        [OperationContract]
        MailBoxList SelectMsgByEmail(string email);
        [OperationContract]
        int AddMassage(MailBox m);
        [OperationContract]
        CategoryList SelectAllCategories();
        [OperationContract]
        DataTable GetCategories();
        [OperationContract]

        Category SelectCategoryByName(string categoryName);
        [OperationContract]

        int AddCategory(Category c);
        [OperationContract]

        int UpdateCategory(int ID, string name);
        [OperationContract]

        int DeleteCategory(int ID);




        // TODO: Add your service operations here
    }






}

