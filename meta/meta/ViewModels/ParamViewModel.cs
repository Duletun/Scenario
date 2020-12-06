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
using System.Collections.ObjectModel;

namespace meta.ViewModels
{
    public class ParamViewModel : INotifyPropertyChanged
    {
        public CharacterViewModel lvm;
        public bool IsVisible { get; set; } = false;
        public CharacterViewModel ListViewModel
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
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation Navigation { get; set; }
        public Param Param { get; set; }
        //static int a = 0;
        public ParamViewModel()
        {
            if (Param == null)
            {
                Param = new Param();
            }
        }
        public int Value
        {
            get { return Param.Value; }
            set
            {
                if (Param.Value != value)
                {
                    Param.Value = value;
                    OnPropertyChanged("Value");
                }
            }
        }
        public string Name
        {
            get { return Param.Name; }
            set
            {
                if (Param.Name != value)
                {
                    Param.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public int atach
        {
            get { return Param.atach; }
            set
            {
                if (Param.atach != value)
                {
                    Param.atach = value;
                    OnPropertyChanged("atach");
                }
            }
        }
        public bool IsCreated { get; set; } = false;
        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(Name.Trim());
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
