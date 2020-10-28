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
    }
}