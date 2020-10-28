using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace meta.Models
{
    public class Chapter
    {
        static int count = 1;
        [PrimaryKey, AutoIncrement, Column("_id")]

        public int Id { get; set; } = count;
        string Title { get; set; }
        string Text { get; set; }
        public Chapter()
        {
            count++;
        }
        int Words 
        { 
            get
            {
                return (Text.Split(' ')).Length;
            }
        } 
    }
}
