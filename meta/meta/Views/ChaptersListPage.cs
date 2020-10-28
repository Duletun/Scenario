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
        public ChaptersListViewModel List { get; set; }
        public ChaptersListPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            List = new ChaptersListViewModel() { Navigation = this.Navigation };
            Button AddButton = new Button() { Text = "Добавить",  Command = List.CreateChapterCommand };
            AddButton.Clicked += OnButtonClicked;
            void OnButtonClicked(object sender, System.EventArgs e)
            {
                    

            }
            Grid grid = new Grid
            {
                Margin = 15,
                ColumnSpacing = 30,
                RowSpacing = 35,
                RowDefinitions =
                {
                    new RowDefinition { Height = 200 },
                    new RowDefinition { Height = 200 },
                    new RowDefinition { Height = 200 },
                    new RowDefinition { Height = 200 },
                    new RowDefinition { Height = 200 }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = 164},
                    new ColumnDefinition { Width = 164},
                }
            };
            int chapcounter = 0;
            foreach (ChapterViewModel c in List.Chapters)
            {
                Frame frame = new Frame { BorderColor = Color.Accent };
                frame.Content = new Label
                {
                    Text = c.Title
                };
                grid.Children.Add(frame, chapcounter % 2, chapcounter / 2);
                chapcounter++;
            }
            StackLayout stackLayout = new StackLayout();
            stackLayout.Children.Add(AddButton);
            stackLayout.Children.Add(grid);
            Content = stackLayout;
            NavigationPage.SetHasNavigationBar(this, false);

        }
    }
}