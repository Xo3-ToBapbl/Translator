using SQLite;
using SQLiteNetExtensions.Attributes;
using Translator.Core.Services;

namespace Translator.Core.Models
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
