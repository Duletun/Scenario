using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using meta.Models;
using meta.ViewModels;
using Xamarin.Forms;
namespace meta
{
    public class ChapterRepository
    {
        SQLiteConnection database2;
        public ChapterRepository(string databasePath)
        {
            database2 = new SQLiteConnection(databasePath);
            database2.CreateTable<Chapter>();
        }
        public Chapter GetItem(int id)
        {
            System.Console.WriteLine("Пытаюсь загетить чаптер с iq {0} ", id);
            return database2.Get<Chapter>(id);
        }
        public int SaveItem(Chapter item)
        {
            // if (item.Id != 0) 
            // { 
            //     database.Update(item);
            //      System.Console.WriteLine("Заапдейчен {0} iq {1}. Всего {2} первонажей", item.Name, item.Id, (database.Table<Character>().ToList()).Count); 
            //     return item.Id; 
            // }
            // else
            //  {
          //  System.Console.WriteLine("Заинсерчен {0} iq {1}. Всего {2} чаптеров", item.Title, item.Id, (database.Table<Character>().ToList()).Count);
            return database2.Insert(item);
            //}
        }
        public IEnumerable<Chapter> GetItems()
        {
            System.Console.WriteLine("Чаптеры загечены");
            return database2.Table<Chapter>().ToList();
        }
        public int DeleteItem(int id)
        {
            System.Console.WriteLine("Чаптер с айди {0} удалён", id);
            return database2.Delete<Chapter>(id);
        }
        public void UpdateItem(Chapter item)
        {
            database2.Update(item);
        }
    }
}
