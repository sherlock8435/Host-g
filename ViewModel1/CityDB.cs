﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Model;

namespace ViewModel1
{
    public class CityDB : DBFunctions
    {
        private CityList list = new CityList();

        private Cities CreateModel(Cities c)
        {
            c.Id = (int)reader["cityID"];
            c.CityName = reader["cityname"].ToString();
            return c;
        }

        public CityList SelectAllCities()
        {
            try
            {

                string sqlStr = "SELECT * FROM Citytbl";
                cmd = GenerateOleDBCommand(sqlStr, "DB.accdb");
                conObj.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cities c = new Cities();
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

        public Cities SelectCityByName(string cityName)
        {
            list = SelectAllCities();
            Cities c = list.Find(Item => Item.CityName == cityName);
            return c;
        }

        public Cities SelectCityById(int id)
        {

            list = SelectAllCities();
            Cities c = list.Find(Item => Item.Id == id);
            return c;

        }

        public List<Cities> OrderByName()
        {
            list = SelectAllCities();
            return list.OrderBy(item => item.CityName).ToList();

        }

    }

}
