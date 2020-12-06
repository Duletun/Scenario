using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using meta.ViewModels;
using System.Windows.Input;
using System.ComponentModel;
using meta.Views;

namespace meta.Views
{
    public partial class CharacterPage : ContentPage
    {
        public CharacterViewModel ViewModel { get; private set; }
        public ObservableCollection<Param> arr;
        private void SliderChanged(object sender,ValueChangedEventArgs e)
        {
        }
        private void plusParamShow(object sender, System.EventArgs e)
        {
            paramName.IsVisible = true;
            addParam.IsVisible = true;
            paramName.Text = "";
        }
        private void plusParamHide(object sender, System.EventArgs e)
        {
            paramName.IsVisible = false;
            addParam.IsVisible = false;
        }
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
            /*List<Param> paramss = new List<Param>();
            paramss = App.DatabaseParam.GetItems().ToList();
            if (ViewModel.Params.Count == 0)
            {
                foreach (Param c in paramss)
                {
                    if (c.atach == ViewModel.Character.Id)
                    {
                        System.Console.WriteLine("Trevoga");
                        System.Console.WriteLine(c.Name);
                        System.Console.WriteLine(c.Value);
                        System.Console.WriteLine(ViewModel.Character.Id);
                        ViewModel.Params.Add(c);
                    }
                }
            }*/
            this.BindingContext = ViewModel;
        }
    }
}