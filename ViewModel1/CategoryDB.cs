using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel1
{
    public class CategoryDB : DBFunctions
    {
        public CategoryDB() { }


        private CategoryList list = new CategoryList();
        readonly DBFunctions CatDB = new DBFunctions();
        private Category CreateModel(Category c)
        {
            c.CategoryName = reader["CategoryName"].ToString();
            return c;
        }
        public CategoryList SelectAllCategories()
        {
            try
            {
                string sqlStr = "Select * From Categorytbl";
                cmd = GenerateOleDBCommand(sqlStr, "DB.accdb");
                conObj.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Category c = new Category();
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
        public DataTable GetCategories()
        {
            string sqlStr = "Select * From Category";
            DataTable dt = CatDB.Select(sqlStr, "DB.accdb");
            return dt;
        }
        public Category SelectCategoryByName(string categoryName)
        {
            list = SelectAllCategories();
            Category c = list.Find(Item => Item.CategoryName == categoryName);
            return c;
        }
        public int AddCategory(Category c)
        {
            string sqlStr = $"Insert into Categorytbl (CategoryName) values ('{c.CategoryName}')";
            return CatDB.ChangeTable(sqlStr, "DB.accdb");
        }
        public int UpdateCategory(int ID, string name)
        {
            string sqlStr = $"Update Categorytbl set CategoryName = '{name}' where CategoryID = {ID}";
            return CatDB.ChangeTable(sqlStr, "DB.accdb");
        }
        public int DeleteCategory(int ID)
        {
            string sqlStr = $"Delete from Categorytbl where CategoryID = {ID}";
            return CatDB.ChangeTable(sqlStr, "DB.accdb");
        }
    }
}
