using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using meta.Models;
using meta.ViewModels;
using Xamarin.Forms;
namespace meta
{
    public class ParamRepository
    {
        SQLiteConnection databaseParam;
        public ParamRepository(string databasePath)
        {
            databaseParam = new SQLiteConnection(databasePath);
            databaseParam.CreateTable<Param>();
        }
        public Param GetItem(int id)
        {
            System.Console.WriteLine("Пытаюсь загетить Param с iq {0} ", id);
            return databaseParam.Get<Param>(id);
        }
        public int SaveItem(Param item)
        {
            // if (item.Id != 0) 
            // { 
            //     database.Update(item);
            //      System.Console.WriteLine("Заапдейчен {0} iq {1}. Всего {2} первонажей", item.Name, item.Id, (database.Table<Character>().ToList()).Count); 
            //     return item.Id; 
            // }
            // else
            //  {
            return databaseParam.Insert(item);
            //}
        }
        public IEnumerable<Param> GetItems()
        {
            System.Console.WriteLine("Params загечены");
            return databaseParam.Table<Param>().ToList();
        }
        public int DeleteItem(int id)
        {
            System.Console.WriteLine("Param с айди {0} удалён", id);
            return databaseParam.Delete<Param>(id);
        }
        public void UpdateItem(Param item)
        {
            databaseParam.Update(item);
        }
    }
}
