using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using meta.ViewModels;
using meta.Models;
using System.Security.Cryptography.X509Certificates;

namespace meta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChaptersListPage : ContentPage
    {
    int chapcounter = 0;
        public ChaptersListViewModel List { get; set; }
        public Grid grid;
        public ChaptersListPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            List = new ChaptersListViewModel() { Navigation = this.Navigation };
            Button AddButton = new Button() { Text = "Добавить", Command = List.CreateChapterCommand };
            grid = new Grid
            {
                Margin = 15,
                ColumnSpacing = 30,
                RowSpacing = 35,
                RowDefinitions =
                {

                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = 164},
                    new ColumnDefinition { Width = 164},
                }
            };
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (sender, e) =>
            {
                System.Console.WriteLine("TAPPEEEEEEEEEEEEEEEEEEEEEED");
                // cast to an image
                Frame theFrame = (Frame)sender;
                ChapterViewModel a = theFrame.BindingContext as ChapterViewModel;
                Navigation.PushAsync(new ChapterPage(a));

                // now you have a reference to the image
            };
            for (int i = List.Chapters.Count - 1; i > -1; i--)
            {
                ChapterViewModel c = List.Chapters[i];
                Frame frame = new Frame { BorderColor = Color.Accent, BindingContext = c };
                frame.Content = new Label
                {
                    Text = c.Title
                };
                frame.GestureRecognizers.Add(tapGestureRecognizer);
                if (chapcounter % 2 == 0)
                {
                    grid.RowDefinitions.Add(new RowDefinition { Height = 200 });
                }
                grid.Children.Add(frame, chapcounter % 2, chapcounter / 2);
                chapcounter++;
            }
            ScrollView scrollView = new ScrollView();
            scrollView.Content = grid;
            StackLayout stackLayout = new StackLayout();
            stackLayout.Children.Add(AddButton);
            stackLayout.Children.Add(scrollView);
            Content = stackLayout;
            NavigationPage.SetHasNavigationBar(this, false);

        }
        protected override void OnAppearing()
        {
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (sender, e) =>
            {
                System.Console.WriteLine("TAPPEEEEEEEEEEEEEEEEEEEEEED");
                // cast to an image
                Frame theFrame = (Frame)sender;
                ChapterViewModel a = theFrame.BindingContext as ChapterViewModel;
                Navigation.PushAsync(new ChapterPage(a));

                // now you have a reference to the image
            };
            if (List.NeedToReload == true)
            {
                grid.Children.Clear();
                chapcounter = 0;
                for (int i = List.Chapters.Count - 1; i > -1; i--)
                {
                    ChapterViewModel c = List.Chapters[i];
                    Frame frame = new Frame { BorderColor = Color.Accent, BindingContext = c };
                    frame.Content = new Label
                    {
                        Text = c.Title
                    };
                    frame.GestureRecognizers.Add(tapGestureRecognizer);
                    if (chapcounter % 2 == 0)
                    {
                        grid.RowDefinitions.Add(new RowDefinition { Height = 200 });
                    }
                    grid.Children.Add(frame, chapcounter % 2, chapcounter / 2);
                    chapcounter++;
                }
            }
        }
        protected override void OnDisappearing()
        {

        }
    }
}