using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Threading.Tasks;
using meta.Navigables.Impl;
using meta.Services;
using meta.ViewModels;
using meta.Views;


using MetroLog;


//xmlns:android="clr-namespace:Xamarin.Forms.PlatformConfiguration.AndroidSpecific;assembly=Xamarin.Forms.Core"
//android: Application.WindowSoftInputModeAdjust = "Resize"

namespace meta
{
    public partial class App : Application
    {
        public static CharactersListPage charlist;
        public const string DATABASE_NAME3 = "notes.db";
        public const string DATABASE_NAME2 = "chapters.db";
        public const string DATABASE_NAME = "characters.db";
        public const string DATABASE_NAMEParam = "params.db";
        public const string DATABASE_NAMETime = "time.db";
        public static CharacterRepository database;
        public static ChapterRepository database2;
        public static NoteRepository database3;
        public static TimeRepository databaseTime;
        public static ParamRepository databaseParam;
        private static readonly ILogger Logger = LoggerFactory.GetLogger(nameof(App));
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
        public static TimeRepository DatabaseTime
        {
            get
            {
                if (databaseTime == null)
                {
                    databaseTime = new TimeRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAMETime));
                }
                return databaseTime;
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
            InitializeComponent();
            Sharpnado.HorizontalListView.Initializer.Initialize(true, true);
            Sharpnado.Tabs.Initializer.Initialize(true, true);
            Sharpnado.Shades.Initializer.Initialize(loggerEnable: true, true);

            var navigationService = new FormsNavigationService(
                new Lazy<NavigationPage>(() => (NavigationPage)Current.MainPage),
                new ViewLocator());

            var sillyDudeService = new SillyDudeService(new ErrorEmulator());

            var viewModel = new GridPageViewModel(navigationService, sillyDudeService);

            var tabbedPage = new TabbedPage() { BindingContext = viewModel };
            
            tabbedPage.SelectedTabColor = Color.White;
            tabbedPage.UnselectedTabColor = Color.FromHex("#095995");
            NavigationPage.SetHasNavigationBar(tabbedPage, false);

            GridPage tabgrid = new GridPage
            {
                BindingContext = viewModel,
            };
            viewModel.Navigation = tabgrid.Navigation;

            charlist = new CharactersListPage();
            NavigationPage charsp = new NavigationPage(charlist);
            NavigationPage.SetHasNavigationBar(charsp, false);
            tabbedPage.Children.Add(charsp);
            charsp.Title = "Герои";
            charsp.IconImageSource = "baseline_face_white_36.png";
            tabbedPage.Children.Add(new NavigationPage(new ChaptersListPage()) { Title = "Главы" , IconImageSource = "baseline_article_white_36.png" });
            tabbedPage.Children.Add(new NavigationPage(tabgrid) { Title = "Схемы", IconImageSource = "baseline_timeline_white_36.png" });
            tabbedPage.Children.Add(new NavigationPage(new NotesListPage()) { Title = "Заметки", IconImageSource = "baseline_sticky_note_2_black_36dp.png" });
            NavigationPage.SetHasNavigationBar(this, false);

            Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetToolbarPlacement(tabbedPage, Xamarin.Forms.PlatformConfiguration.AndroidSpecific.ToolbarPlacement.Top);
            Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetIsSwipePagingEnabled(tabbedPage, false);

            MainPage = new NavigationPage(tabbedPage);
            viewModel.Load(null);
            //MainPage = new TabbedMain();
            //MainPage = new NavigationPage(new CharactersListPage());
            // MainPage = new ImagesListPage();
            System.Console.WriteLine("Hello");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        { }

        protected override void OnResume()
        { }
    }
}
