using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using meta.Models;
using meta.Services;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace meta
{
    public class TimeRepository
    {
        SQLiteConnection databaseTime;
        public TimeRepository(string databasePath)
        {
            databaseTime = new SQLiteConnection(databasePath);
            databaseTime.CreateTable<SillyDude>();
        }
        public SillyDude GetItem(int id)
        {
            System.Console.WriteLine("Пытаюсь загетить чела с iq {0} ", id);
            return databaseTime.Get<SillyDude>(id);
        }
        public int SaveItem(SillyDude item)
        {
            // if (item.Id != 0) 
            // { 
            //     database.Update(item);
            //      System.Console.WriteLine("Заапдейчен {0} iq {1}. Всего {2} первонажей", item.Name, item.Id, (database.Table<Character>().ToList()).Count); 
            //     return item.Id; 
            // }
            // else
            //  {
            System.Console.WriteLine("Заинсерчен {0} iq {1}. Всего {2} первонажей", item.Name, item.Id, (databaseTime.Table<SillyDude>().ToList()).Count);
            return databaseTime.Insert(item);
            //}
        }
        public IEnumerable<SillyDude> GetItems()
        {
            System.Console.WriteLine("Айтемы загечены");
            return databaseTime.Table<SillyDude>().ToList();
        }
        public int DeleteItem(int id)
        {
            System.Console.WriteLine("Айтем с айди {0} удалён", id);
            return databaseTime.Delete<SillyDude>(id);
        }

        public int DeleteAllItems()
        {
            //System.Console.WriteLine("Айтем с айди {0} удалён", id);
            return databaseTime.DeleteAll<SillyDude>();
        }


        public void UpdateItem(SillyDude item)
        {
            databaseTime.Update(item);
        }

        public void UpdateAllItems(IEnumerable<SillyDude> items)
        {
            databaseTime.UpdateAll(items);
        }

        public void ReplaceItem(SillyDude item)
        {
            databaseTime.InsertOrReplace(item);
        }


    }
}
