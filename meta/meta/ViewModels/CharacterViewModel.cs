using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using meta.Models;
using System.Windows.Input;
using meta.Views;
using System.Linq;
using SQLite;

namespace meta.ViewModels
{
    public class CharacterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        CharactersListViewModel lvm;
        public INavigation Navigation { get; set; }
        public ICommand SwitchImageCommand { protected set; get; }
        public Character Character { get;  set; }
        //static int a = 0;
        public CharacterViewModel()
        {
            if (Character == null)
            {
                Character = new Character();
            }

            SwitchImageCommand = new Command(SwitchImage);
        }
        private void SwitchImage( object characterObject)
        {
            CharacterViewModel cm = characterObject as CharacterViewModel; 
            if (Navigation != null)
            {
                Navigation.PushAsync(new ImagesListPage(cm));
            }
        }

        public CharactersListViewModel ListViewModel
        {
            get { return lvm; }
            set
            {
                if (lvm != value)
                {
                    lvm = value;
                    OnPropertyChanged("ListViewModel");
                }
            }
        }
        public string Name
        {
            get { return Character.Name; }
            set
            {
                if (Character.Name != value)
                {
                    Character.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public string ImagePath
        {
            get { return Character.ImagePath; }
            set
            {
                if (Character.ImagePath != value)
                {
                    Character.ImagePath = value;
                    OnPropertyChanged("ImagePath");
                }
            }
        }
        public string Description
        {
            get { return Character.Description; }
            set
            {
                if (Character.Description != value)
                {
                    Character.Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
        public bool IsCreated { get; set; } = false;
        public bool IsValid
        {
            get
            {
                return ((!string.IsNullOrEmpty(Name.Trim())) ||
                    (!string.IsNullOrEmpty(Description.Trim())));
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
