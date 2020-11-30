using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using meta.ViewModels;
using meta.Models;

namespace meta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChapterPage : ContentPage
    {
        public ChapterViewModel ViewModel { get; private set; }
        public ChapterPage(ChapterViewModel vm)
        {
            vm.Navigation = this.Navigation;
            InitializeComponent();
            ViewModel = vm;
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
    }
}