using meta.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace meta.Views
{
    public class ImagesListPage : ContentPage
    {
        public List<string> ImagesList { get; set; } = new List<string> { "guyimg.jpg", "jokerimg.jpg", "pepeimg.jpg", "ghostimg.jpg", "elfimg.jpg" };
        public CharacterViewModel Character { get; set; }
        public void OnButtonClicked(object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            this.Character.ImagePath = (((button.ImageSource).ToString()).Substring(5)).Trim();
            Navigation.PopAsync();

        }
        public ImagesListPage(CharacterViewModel cm)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            Character = cm;
            Grid grid = new Grid
            {   Margin = 15,
                ColumnSpacing = 10,
                RowDefinitions =
                {
                    new RowDefinition { Height = 82 },
                    //new RowDefinition { Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition { Height = 82 },
                    new RowDefinition { Height = 82 },
                    new RowDefinition { Height = 82 },
                    new RowDefinition { Height = 82 }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = 82},
                    new ColumnDefinition { Width = 82},
                    new ColumnDefinition { Width = 82},
                    new ColumnDefinition { Width = 82}
                }
            };
            int imgcounter = 0;
            foreach (string imgpath in ImagesList)
            {
                Button but = new Button { ImageSource = imgpath, BorderWidth = 0, };
                //grid.Children.Add(new Button { ImageSource = imgpath, BorderWidth = 0, }, imgcounter%4, imgcounter/4);
                grid.Children.Add(but, imgcounter % 4, imgcounter / 4);
                but.Clicked += OnButtonClicked;
                imgcounter++;

            }
            Content = grid;
        }
    }
}