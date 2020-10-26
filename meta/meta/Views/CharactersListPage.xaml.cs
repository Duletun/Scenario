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
    public partial class CharactersListPage : ContentPage
    {
        public CharactersListPage()
        {
            InitializeComponent();
            BindingContext = new CharactersListViewModel() { Navigation = this.Navigation };
        }
    }
}