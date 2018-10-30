using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using Translator.Core.Services;

namespace Translator.Core.Models
{
    [Table(Constants.DataBase.TableNames.OriginalWords)]
    public class Word
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string Original { get; set; }

        public DateTime DateAdded { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Translation> Translations { get; set; }


        [Ignore]
        public string TranslationsString
        {
            get
            {
                if (Translations != null && Translations.Count != 0)
                    return string.Join(", ", Translations);

                return string.Empty;
            }
        }
    }
}
