using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using meta.Models;
using meta.ViewModels;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace meta
{   
    public class CharacterRepository
    {
        SQLiteConnection database;
        public CharacterRepository(string databasePath )
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable< Character >();
        }
        public Character GetItem(int id)
        {
            System.Console.WriteLine("Пытаюсь загетить чела с iq {0} ", id);
            return database.Get<Character>(id);
        }
        public int SaveItem(Character item)
        {
           // if (item.Id != 0) 
           // { 
           //     database.Update(item);
          //      System.Console.WriteLine("Заапдейчен {0} iq {1}. Всего {2} первонажей", item.Name, item.Id, (database.Table<Character>().ToList()).Count); 
           //     return item.Id; 
           // }
           // else
          //  {
                System.Console.WriteLine("Заинсерчен {0} iq {1}. Всего {2} первонажей", item.Name, item.Id, (database.Table<Character>().ToList()).Count);
                return database.Insert(item);
            //}
        }
        public IEnumerable<Character> GetItems()
        {
            System.Console.WriteLine("Айтемы загечены");
            return database.Table<Character>().ToList();
        }
        public int DeleteItem(int id)
        {
            System.Console.WriteLine("Айтем с айди {0} удалён", id);
            return database.Delete<Character>(id);
        }
        public void UpdateItem(Character item)
        {
            database.Update(item);
        }
    }
}
