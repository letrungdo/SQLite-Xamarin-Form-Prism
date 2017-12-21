using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteDemo.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
    }
}
