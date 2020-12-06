using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using meta.Models;
using System.Collections.ObjectModel;


namespace meta.Models
{
    [Table("Characters")]
    public class Character
    {
        static int count = 1;
        [PrimaryKey, AutoIncrement, Column("_id")]

        public int Id { get; set; } = count;
        public string Name { get; set; }
        public string ImagePath { get; set; } = "guyimg.jpg";
        public string Description { get; set; }
        public Character()
        {
            count++;
        }
    }
}