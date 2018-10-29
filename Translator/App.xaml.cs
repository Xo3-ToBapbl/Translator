using System;
using Translator.Interfaces;
using Translator.Models.Repositories;
using Translator.Pages;
using Translator.Pages.MasterPages;
using Translator.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Translator
{
    public partial class App : Application
    {
        public static string Localization;
        public static WordsRepository WordsRepository { get; private set; }


        public App()
        {
            InitializeComponent();

            Localization = Constants.Localization.Ru;

            string dataBasePath = DependencyService
                .Get<IFilePathService>()
                .GetFilePath(Constants.DataBase.Name);

            WordsRepository = new WordsRepository(dataBasePath);

            MainPage = new LoginPage();
        }


        #region AppLifecycle methods
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        #endregion
    }
}
