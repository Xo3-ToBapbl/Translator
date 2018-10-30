using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Translator.Services;

namespace Translator.Models.Repositories
{
    public class WordsRepository
    {
        private readonly SQLiteConnection dataBase;
        private string translations;


        public WordsRepository(string dataBasePath)
        {
            dataBase = new SQLiteConnection(dataBasePath);

            if (CreateTables() == CreateTableResult.Created)
                InitializeDataBase();

            translations = Constants.DataBase.TableNames.Translations;
        }


        public IEnumerable<Word> GetAll()
        {
            return dataBase.GetAllWithChildren<Word>();
        }

        public void Add(Word word)
        {
            dataBase.InsertWithChildren(word);
        }

        public void Update(Word word)
        {
            if (word == null) return;

            dataBase
                .InsertOrReplaceWithChildren(word);
            dataBase
                .Execute($"DELETE FROM {translations} WHERE {translations}.{nameof(Translation.WordId)} IS NULL;");
        }


        private CreateTableResult CreateTables()
        {
            CreateTableResult result = dataBase.CreateTable<Word>();
            dataBase.CreateTable<Translation>();

            return result;
        }

        private void InitializeDataBase()
        {
            #region WordsItialization
            Random random = new Random();

            var word = new Word
            {
                Original = "acceptable",
                DateAdded = DateTime.Now + TimeSpan.FromMinutes(random.Next(-20, 20)),
                Translations = new List<Translation>
                {
                    new Translation { Value = "удовлетворительный", },
                    new Translation { Value = "приемлимый", },
                    new Translation { Value = "пригодный", },
                },
            };
            var word1 = new Word
            {
                Original = "appreciate",
                DateAdded = DateTime.Now + TimeSpan.FromMinutes(random.Next(-20, 20)),
                Translations = new List<Translation>
                {
                    new Translation { Value = "воспринимать", },
                    new Translation { Value = "оценивать", },
                    new Translation { Value = "принимать во внимание", },
                    new Translation { Value = "быть благодарным", },
                    new Translation { Value = "благодарить", },
                    new Translation { Value = "одобрять", },
                },
            };
            var word2 = new Word
            {
                Original = "cumbersome",
                DateAdded = DateTime.Now + TimeSpan.FromMinutes(random.Next(-20, 20)),
                Translations = new List<Translation>
                {
                    new Translation { Value = "объемный", },
                    new Translation { Value = "обреминительный", },
                    new Translation { Value = "громоздкий", },
                    new Translation { Value = "сложный", },
                    new Translation { Value = "трудоемкий", },
                },
            };
            var word3 = new Word
            {
                Original = "impose",
                DateAdded = DateTime.Now + TimeSpan.FromMinutes(random.Next(-20, 20)),
                Translations = new List<Translation>
                {
                    new Translation { Value = "накладывать", },
                    new Translation { Value = "помещать", },
                    new Translation { Value = "облагать", },
                    new Translation { Value = "предъявлять", },
                    new Translation { Value = "предписывать", },
                },
            };
            var word4 = new Word
            {
                Original = "lid",
                DateAdded = DateTime.Now + TimeSpan.FromMinutes(random.Next(-20, 20)),
                Translations = new List<Translation>
                {
                    new Translation { Value = "запрет", },
                    new Translation { Value = "крышка", },
                    new Translation { Value = "века", },
                },
            };
            var word5 = new Word
            {
                Original = "certainly",
                DateAdded = DateTime.Now + TimeSpan.FromMinutes(random.Next(-20, 20)),
                Translations = new List<Translation>
                {
                    new Translation { Value = "именно", },
                    new Translation { Value = "несомненно", },
                    new Translation { Value = "заведомо", },
                    new Translation { Value = "наверняка", },
                    new Translation { Value = "с уверенностью", },
                },
            };
            var word6 = new Word
            {
                Original = "solely",
                DateAdded = DateTime.Now + TimeSpan.FromMinutes(random.Next(-20, 20)),
                Translations = new List<Translation>
                {
                    new Translation { Value = "только", },
                    new Translation { Value = "единолично", },
                    new Translation { Value = "исключительно", },
                },
            };
            var word7 = new Word
            {
                Original = "thirsty",
                DateAdded = DateTime.Now + TimeSpan.FromMinutes(random.Next(-20, 20)),
                Translations = new List<Translation>
                {
                    new Translation { Value = "жаждущий", },
                    new Translation { Value = "испытывающий жажду", },
                    new Translation { Value = "измученный жаждой", },
                },
            };
            var word8 = new Word
            {
                Original = "versatile",
                DateAdded = DateTime.Now + TimeSpan.FromMinutes(random.Next(-20, 20)),
                Translations = new List<Translation>
                {
                    new Translation { Value = "многогранный", },
                    new Translation { Value = "универсальный", },
                    new Translation { Value = "гибкий", },
                    new Translation { Value = "изменчивый", },
                    new Translation { Value = "многосторонний", },
                },
            };
            var word9 = new Word
            {
                Original = "witness",
                DateAdded = DateTime.Now + TimeSpan.FromMinutes(random.Next(-20, 20)),
                Translations = new List<Translation>
                {
                    new Translation { Value = "быть свидетелем", },
                    new Translation { Value = "давать показания", },
                    new Translation { Value = "очевидец", },
                    new Translation { Value = "свидетель", },
                    new Translation { Value = "свидетельство", },
                },
            };
            dataBase.InsertWithChildren(word);
            dataBase.InsertWithChildren(word1);
            dataBase.InsertWithChildren(word2);
            dataBase.InsertWithChildren(word3);
            dataBase.InsertWithChildren(word4);
            dataBase.InsertWithChildren(word5);
            dataBase.InsertWithChildren(word6);
            dataBase.InsertWithChildren(word7);
            dataBase.InsertWithChildren(word8);
            dataBase.InsertWithChildren(word9);
            #endregion
        }
    }
}
