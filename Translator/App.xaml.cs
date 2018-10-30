﻿using System;
using Translator.Interfaces;
using Translator.Models;
using Translator.Models.Repositories;
using Translator.Pages;
using Translator.Pages.MasterPages;
using Translator.Resources;
using Translator.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Translator
{
    public partial class App : Application
    {
        public static Localization Localization;
        public static WordsRepository WordsRepository { get; private set; }


        public App()
        {
            InitializeComponent();

            string dataBasePath = DependencyService
                .Get<IFilePathService>()
                .GetFilePath(Constants.DataBase.Name);

            Localization = GetLocalization();

            WordsRepository = new WordsRepository(dataBasePath);

            MainPage = new LoginPage();
        }


        private Localization GetLocalization()
        {
            App.Current.Properties.TryGetValue(nameof(Localization), out var localization);

            if (localization != null)
                return (localization as Localization);

            return new Localization(Constants.AppDefaultLocalization, Constants.AppDefaultLanguage);
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
