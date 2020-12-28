using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using meta.Views;
using Xamarin.Essentials;


using Xamarin.Forms.Xaml;
using meta.ViewModels;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using meta.Models;

namespace meta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChapterPage : ContentPage
    {
        public ChapterViewModel ViewModel { get; private set; }

        public ICommand TapPCommand { set; get; }

        
        //Система опознавания ссылок по имени :)
        public async Task UpdateLabel()
        {
            labLayout.Children.Clear();
            string NameCopy = ViewModel.Text;
            Label lolkek12 = new Label { Text = NameCopy, LineBreakMode = LineBreakMode.WordWrap };
            bool theend = false;
            FormattedString formattedString = new FormattedString();

            double a = DeviceDisplay.MainDisplayInfo.Width;
            //double a = showText.MinimumWidthRequest;
           // double b = labLayout.MinimumWidthRequest;



            List<CharacterViewModel> charlik = new List<CharacterViewModel>();
            charlik = (App.Database.GetItems().ToList()).ConvertAll(x => new CharacterViewModel
            {
                Character = x,
                //ListViewModel = new CharactersListViewModel(), //(CharactersListViewModel)App.charlist.BindingContext,
                IsCreated = true
            }); ;

            while (theend == false)
            {
                theend = true;
                for (int i = 0; i < charlik.Count(); i++)
                {

                    if (NameCopy.Contains("@" + charlik[i].Name + " ") == true || NameCopy.Contains("@" + charlik[i].Name + "\n") == true
                        || NameCopy.EndsWith("@" + charlik[i].Name) == true)
                    {
                        for (int j = 0; j < charlik.Count(); j++)
                        {
                            
                            if (NameCopy.IndexOf("@" + charlik[j].Name) <= NameCopy.IndexOf("@" + charlik[i].Name) 
                                && NameCopy.IndexOf("@" + charlik[j].Name) != -1
                                && NameCopy.IndexOf("@" + charlik[i].Name) != -1
                                && j != i) 
                            {
                                if (charlik[j].Name.Contains(charlik[i].Name) == true && charlik[i].Name.Length <= charlik[j].Name.Length)
                                {
                                    i = j;
                                }
                                i = j;
                            }
                        }
                        if (NameCopy.IndexOf("@" + charlik[i].Name) > 0)
                        {
                            string part = NameCopy.Substring(0, NameCopy.IndexOf("@" + charlik[i].Name));

                            if(part.Contains("\n") == true && part.Length > 1)
                            {
                                 string part1 = NameCopy.Substring(0, part.LastIndexOf("\n"));
                                 formattedString.Spans.Add(new Span { Text = part1, ForegroundColor = Color.Black });

                                Label lolkek = new Label { FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), TextColor = Color.FromHex("#000000"), BackgroundColor = Color.FromHex("#DBEFFD") };
                                lolkek.FormattedText = formattedString;

                                labLayout.Children.Add(lolkek);
                                formattedString = new FormattedString();
                                part = part.Remove(0, part.LastIndexOf("\n")+1);
                            } 
                            else if (part.Contains("\n") == true && part.Length <= 1)
                            {
                                Label lolkek = new Label {  FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), TextColor = Color.FromHex("#000000"), BackgroundColor = Color.FromHex("#DBEFFD") };
                                lolkek.FormattedText = formattedString;
                                //LineBreakMode

                                labLayout.Children.Add(lolkek);
                                formattedString = new FormattedString();
                                part = "";
                            }

                            if (part.Length > 0) { 
                                formattedString.Spans.Add(new Span { Text = part, ForegroundColor = Color.Black }); }
                        }

                        //NavigationPage charsp = new NavigationPage(new CharactersListPage());

                        var span = new Span { Text = charlik[i].Name, ForegroundColor = Color.Red, TextDecorations = TextDecorations.Underline };
                        Console.WriteLine("Name" + charlik[i].Name);
                        //async () => await
                        span.GestureRecognizers.Add(new TapGestureRecognizer {
                            Command = TapPCommand,
                            CommandParameter = i
                        });
                        formattedString.Spans.Add(span);
                        //formattedString.Spans.Add(new Span { Text = charlik[i].Name, ForegroundColor = Color.Red, TextDecorations = TextDecorations.Underline });

                        int len = NameCopy.IndexOf("@" + charlik[i].Name) + charlik[i].Name.Length + 1;
                        NameCopy = NameCopy.Remove(0, len);
                        theend = false;
                    }
                }   
            }
            if (NameCopy.Length > 0) { formattedString.Spans.Add(new Span { Text = NameCopy, ForegroundColor = Color.Black }); }
            Label lolkek1 = new Label { FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), TextColor = Color.FromHex("#000000"), BackgroundColor = Color.FromHex("#DBEFFD") };
            lolkek1.FormattedText = formattedString;

            labLayout.Children.Add(lolkek1);
            await Task.Delay(0);
        }

        private async void onEditClick(object sender, System.EventArgs e)
        {
            await Task.WhenAll(editButton.FadeTo(0, 100), showText.FadeTo(0, 100));

            showText.IsVisible = false;
            editButton.IsVisible = false;

            editText.Opacity = 0;
            doneButton.Opacity = 0;
            //await doneButton.FadeTo(0, 0);
            editText.IsVisible = true;
            doneButton.IsVisible = true;


            await Task.WhenAll(editText.FadeTo(1, 100), doneButton.FadeTo(1, 100));
        }

        private async void onDoneClick(object sender, System.EventArgs e)
        {
            await UpdateLabel();
            await Task.Delay(1);
            await Task.WhenAll(doneButton.FadeTo(0, 100), editText.FadeTo(0, 100));

            editText.IsVisible = false;
            doneButton.IsVisible = false;

            showText.Opacity = 0;
            editButton.Opacity = 0;
            //await doneButton.FadeTo(0, 0);
           // await Task.Delay(100);


            showText.IsVisible = true;
            editButton.IsVisible = true;

            await Task.WhenAll(showText.FadeTo(1, 100), editButton.FadeTo(1, 100));

        }


        public ChapterPage(ChapterViewModel vm)
        {
            vm.Navigation = this.Navigation;
            InitializeComponent();
            ViewModel = vm;


            TapPCommand = new Command<int>(async obj =>
            {
                CharactersListViewModel model = (CharactersListViewModel)App.charlist.BindingContext;
                CharacterPage lol = new CharacterPage(model.Characters[obj]);
                lol.HideButtons();
                await Navigation.PushAsync(lol);
            });

            if (vm.IsCreated == true)
            {
                editText.IsVisible = false;
                showText.IsVisible = true;
                doneButton.IsVisible = false;
                editButton.IsVisible = true;
                UpdateLabel();
            }
            else
            {
                editText.IsVisible = true;
                showText.IsVisible = false;
                doneButton.IsVisible = false;
                editButton.IsVisible = false;
            }

            this.BindingContext = ViewModel;
        }
        protected override bool OnBackButtonPressed()
        {
            if (!String.IsNullOrEmpty(ViewModel.Text))
            {
                Console.WriteLine("tTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTrue");
                if (ViewModel.IsCreated == true)
                {
                    Console.WriteLine("True");
                    App.Database2.UpdateItem(ViewModel.Chapter);
                    //ViewModel.lvm.NeedToReload = true; /*Убрать потом*/
                }
                else
                {
                    Console.WriteLine("Frue");
                    this.ViewModel.lvm.Chapters.Add(ViewModel);
                    App.Database2.SaveItem(ViewModel.Chapter);
                    ViewModel.lvm.NeedToReload = true;
                    ViewModel.IsCreated = true;
                }
            }
            Navigation.PopAsync();
            System.Console.WriteLine("ROBIT");
            Navigation.PopAsync();
            return true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            App.Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Pan);
        }




    }
}