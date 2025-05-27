using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;
using ViewModel;
using ViewModel1;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        readonly ItemDB Idb = new ItemDB();
        readonly CityDB Cdb = new CityDB();
        readonly OrdersDB Odb = new OrdersDB();
        readonly UserDB Udb = new UserDB();
        readonly Mailboxdb Mdb = new Mailboxdb();
        readonly CartDB CartDB = new CartDB();
        readonly CategoryDB Catdb = new CategoryDB();
        public int AddToCart(Cart c)
        {
            return CartDB.AddToCart(c);
        }
        public int UpdateCart(Cart c)
        {
            return CartDB.UpdateCart(c);
        }
        public int DeleteCart(Cart c)
        {
            return CartDB.DeleteCart(c);
        }
        public int CreatCrat()
        {
            return CartDB.CreatCrat();
        }
        public CartList SelectAllCarts()
        {
            return CartDB.SelectAllCarts();
        }
        public Cart SelectCartByEmail(string email)
        {
            return CartDB.SelectCartByEmail(email);
        }
        public Item[] GetCartItems(string email)
        {
            return CartDB.GetCartItems(email);
        }
        public int GetTotalPrice(string email)
        {
            return CartDB.GetTotalPrice(email);
        }
        public int AddItemToCart(string email, Item item)
        {
            return CartDB.AddItemToCart(email, item);
        }
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
        public DataTable GetItems()
        {
            return Idb.GetItems();
        }
        public Item SelectItemByID(int ItemID)
        {
            return Idb.SelectItemByID(ItemID);
        }
        public int ItemAmount()
        {
            return Idb.ItemAmount();
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

        public MailBoxList SelectMsgByEmail(string email)
        {
            return Mdb.SelectMsgByEmail(email);
        }

        public int AddMassage(MailBox m)
        {
            return Mdb.AddMassage(m);
        }
        public CategoryList SelectAllCategories()
        {

            return Catdb.SelectAllCategories();
        }
        public Category SelectCategoryByName(string catName)
        {
            return Catdb.SelectCategoryByName(catName);
        }
        public DataTable GetCategories()
        {
            return Catdb.GetCategories();
        }

        public int AddCategory(Category cat)
        {
            return Catdb.AddCategory(cat);
        }
        public int UpdateCategory(int ID, string name)
        {
            return Catdb.UpdateCategory(ID, name);

        }
        public int DeleteCategory(int ID)
        {
            return Catdb.DeleteCategory(ID);
        }
        public string UploadFile(byte[] fileBytes, string fileName)
        {
            // Path to public images folder (one level up from /bin)
            string destinationFolder = "C:\\Users\\35the\\source\\repos\\finle_store\\gym_equipment_store\\resources\\products_images";

            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            string newFileName = fileName;
            int counter = 1;

            // Keep incrementing counter until we find a filename that doesn't exist
            while (File.Exists(Path.Combine(destinationFolder, newFileName)))
            {
                newFileName = $"{fileNameWithoutExt}_ext_{counter}{extension}";
                counter++;
            }

            string filePath = Path.Combine(destinationFolder, newFileName);
            File.WriteAllBytes(filePath, fileBytes);

            // Return a public URL based on localhost
            return $"http://localhost:51971/resources/products_images/{newFileName}";
        }
    }
}

