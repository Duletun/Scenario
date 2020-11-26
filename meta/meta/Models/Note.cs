using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace meta
{
    [Table("Chapters")]
    public class Note
    {
        static int count = 1;
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; } = count;
        public string Title { get; set; }
        public string Text { get; set; }
        public Note()
        {
            count++;
        }
    }
}
