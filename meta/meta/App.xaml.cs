using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using meta.Views;

namespace meta
{
    public partial class App : Application
    {
        public const string DATABASE_NAME3 = "notes.db";
        public const string DATABASE_NAME2 = "chapters.db";
        public const string DATABASE_NAME = "characters.db";
        public const string DATABASE_NAMEParam = "params.db";
        public static CharacterRepository database;
        public static ChapterRepository database2;
        public static NoteRepository database3;
        public static ParamRepository databaseParam;
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
        public static NoteRepository Database3
        {
            get
            {
                if (database3 == null)
                {
                    database3 = new NoteRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME3));
                }
                return database3;
            }

        }
        public static ParamRepository DatabaseParam
        {
            get
            {
                if (databaseParam == null)
                {
                    databaseParam = new ParamRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAMEParam));
                }
                return databaseParam;
            }

        }
        public App()
        {
            var tabbedPage = new TabbedPage();
            NavigationPage charsp = new NavigationPage(new CharactersListPage());
            NavigationPage.SetHasNavigationBar(charsp, false);
            tabbedPage.Children.Add(charsp);
            charsp.Title = "Герои";
            tabbedPage.Children.Add(new NavigationPage(new ChaptersListPage()) { Title = "Главы" });
            tabbedPage.Children.Add(new NavigationPage(new TimelinesListPage()) { Title = "Схемы" });
            tabbedPage.Children.Add(new NavigationPage(new NotesListPage()) { Title = "Заметки" });

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
