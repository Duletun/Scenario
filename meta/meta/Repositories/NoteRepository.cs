using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using meta.Models;
using meta.ViewModels;
using Xamarin.Forms;
namespace meta
{
    public class NoteRepository
    {
        SQLiteConnection database3;
        public NoteRepository(string databasePath)
        {
            database3 = new SQLiteConnection(databasePath);
            database3.CreateTable<Note>();
        }
        public Note GetItem(int id)
        {
            System.Console.WriteLine("Пытаюсь загетить Note с iq {0} ", id);
            return database3.Get<Note>(id);
        }
        public int SaveItem(Note item)
        {
            // if (item.Id != 0) 
            // { 
            //     database.Update(item);
            //      System.Console.WriteLine("Заапдейчен {0} iq {1}. Всего {2} первонажей", item.Name, item.Id, (database.Table<Character>().ToList()).Count); 
            //     return item.Id; 
            // }
            // else
            //  {
            System.Console.WriteLine("Заинсерчен {0} iq {1}. Всего {2} Notes", item.Title, item.Id, (database3.Table<Note>().ToList()).Count);
            return database3.Insert(item);
            //}
        }
        public IEnumerable<Note> GetItems()
        {
            System.Console.WriteLine("Notes загечены");
            return database3.Table<Note>().ToList();
        }
        public int DeleteItem(int id)
        {
            System.Console.WriteLine("Note с айди {0} удалён", id);
            return database3.Delete<Note>(id);
        }
        public void UpdateItem(Note item)
        {
            database3.Update(item);
            System.Console.WriteLine("Заапдейчен{0} iq {1}. Всего {2} Notes", item.Title, item.Id, (database3.Table<Note>().ToList()).Count);
        }
    }
}
