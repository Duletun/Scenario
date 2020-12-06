using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

using meta.Models;
using System.Collections.ObjectModel;

namespace meta
{
    [Table("Params")]
    public class Param
    {
        static int count = 1;
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; } = count;
        public int Value { get; set; } = 50;
        public string Name { get; set; } = "";
        public int atach { get; set; } = -1;
        public Param()
        {
            count++;
        }
    }
}