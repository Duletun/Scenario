﻿using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using meta.Views;

namespace meta
{
    public partial class App : Application
    {
        public const string DATABASE_NAME2 = "chapters.db";
        public const string DATABASE_NAME = "characters.db";
        public static CharacterRepository database;
        public static ChapterRepository database2;
        public static CharacterRepository Database
        {
            get
            {
                if(database == null)
                {
                    database = new CharacterRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }

        }
        public static ChapterRepository Database2
        {
            get
            {
                if (database2 == null)
                {
                    database2 = new ChapterRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME2));
                }
                return database2;
            }

        }
        public App()
        {
            var tabbedPage = new TabbedPage();
            NavigationPage charsp = new NavigationPage(new CharactersListPage());
            NavigationPage.SetHasNavigationBar(charsp, false);
            tabbedPage.Children.Add(charsp);
            charsp.Title = "Персонажи";
            tabbedPage.Children.Add(new NavigationPage(new ChaptersListPage()) { Title = "Главы" });

            MainPage = tabbedPage;
            //MainPage = new TabbedMain();
             //MainPage = new NavigationPage(new CharactersListPage());
            // MainPage = new ImagesListPage();
            System.Console.WriteLine("Hello");
        }

        protected override void OnStart()
        { }

        protected override void OnSleep()
        { }

        protected override void OnResume()
        { }
    }
}
