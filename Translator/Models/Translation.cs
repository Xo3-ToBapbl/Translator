using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Translator.Services;

namespace Translator.Models
{
    [Table(Constants.DataBase.TableNames.Translations)]
    public class Translation
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Value { get; set; }

        [ForeignKey(typeof(Word))]
        public int WordId { get; set; }


        public override string ToString()
        {
            return Value;
        }
    }
}
