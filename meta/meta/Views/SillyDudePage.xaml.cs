using meta.Models;
using meta.Services;
using System;
using System.IO;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using meta.ViewModels;
using Xamarin.Essentials;

using System.Threading.Tasks;

using Sharpnado.HorizontalListView.RenderedViews;


namespace meta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SillyDudePage : ContentPage
    {

        public SillyDudeVm ViewModel { get; private set; }

        public SillyDudePage(SillyDudeVm vm)
        {
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            ViewModel = vm;
            vm.Navigation = this.Navigation;

            InitializeComponent();
            if (vm.IsCreated == true)
            {
                addButton.IsVisible = false;
                backButton.IsVisible = false;
            }
            else
            {
                delButton.IsVisible = false;
                saveButton.IsVisible = false;
            }
            this.BindingContext = ViewModel;

        }
        async void button_clicked(System.Object sender, System.EventArgs e)
        {
            var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Please pick media" });
            if (photo == null)
            {
                ViewModel.ImageUrl = "point.png";
                return;
            }

            //Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)

            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            ViewModel.ImageUrl = newFile;
        }
    }
}