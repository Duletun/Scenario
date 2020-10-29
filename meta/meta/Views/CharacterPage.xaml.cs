using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using meta.ViewModels;

namespace meta.Views
{
    public partial class CharacterPage : ContentPage
    {
        public CharacterViewModel ViewModel { get; private set; }
        public CharacterPage(CharacterViewModel vm)
        {
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
            ViewModel = vm;
            this.BindingContext = ViewModel;
        }
    }
}