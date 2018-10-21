using System;
using System.IO;
using Translator.Droid.Services;
using Translator.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(FilePathService))]
namespace Translator.Droid.Services
{
    public class FilePathService : IFilePathService
    {
        public FilePathService() { }


        public string GetFilePath(string fileName)
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(folderPath, fileName);
        }
    }
}