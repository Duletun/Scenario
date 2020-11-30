using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using meta.ViewModels;
using Xamarin.Forms;

namespace meta.Views
{
    public partial class NotePage : ContentPage
    {
        public NoteViewModel ViewModel { get; private set; }
        public NotePage(NoteViewModel vm)
        {
            InitializeComponent();
            ViewModel = vm;
            this.BindingContext = ViewModel;
        }
        protected override bool OnBackButtonPressed()
        {
            if (!String.IsNullOrEmpty(ViewModel.Text) )
            {
                if (ViewModel.IsCreated == true)
                {
                    App.Database3.UpdateItem(ViewModel.Note);
                }
                else
                {
                    this.ViewModel.lvm.Notes.Add(ViewModel);
                    App.Database3.SaveItem(ViewModel.Note);
                    ViewModel.IsCreated = true;
                }
            }
            Navigation.PopAsync();
            System.Console.WriteLine("ROBITnote");
            Navigation.PopAsync();
            return true;
        }
    }
}