using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using meta.Models;
using System.Windows.Input;
using meta.Views;
using System.Linq;
using System.Collections.ObjectModel;
using SQLite;

namespace meta.ViewModels
{
    public class CharacterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ParamViewModel> Params { get; set; }
        CharactersListViewModel lvm;
        public INavigation Navigation { get; set; }
        public ICommand CreateParamCommand { set; get; }
        public ICommand SwitchImageCommand { protected set; get; }
        public ICommand MoveToTopCommand { protected set; get; }
        public ICommand MoveToBottomCommand { protected set; get; }
        public ICommand RemoveCommand { protected set; get; }
        public Character Character { get;  set; }
        //static int a = 0;
        public CharacterViewModel()
        {
            MoveToTopCommand = new Command(MoveToTop);
            MoveToBottomCommand = new Command(MoveToBottom);
            RemoveCommand = new Command(Remove);
            CreateParamCommand = new Command(CreateParam);
            SwitchImageCommand = new Command(SwitchImage);
            if (Character == null)
            {
                Character = new Character();
            }
            Params = new ObservableCollection<ParamViewModel>();
            
            List<Param> paramss = new List<Param>();
            paramss = App.DatabaseParam.GetItems().ToList();
            System.Console.WriteLine("OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO");
            foreach (Param c in paramss)
            {
                System.Console.WriteLine("Trevogasdasdasdasdasda");
                System.Console.WriteLine(c.Name);
                System.Console.WriteLine(c.atach);
                System.Console.WriteLine(Character.Id);
                if (c.atach == Character.Id)
                {
                    System.Console.WriteLine("Trevoga");
                    System.Console.WriteLine(c.Name);
                    System.Console.WriteLine(Character.Id);
                    this.Params.Add(new ParamViewModel() { Param = c });
                }
            }
        }
        private void CreateParam(object name)
        {
            ParamViewModel AddedPar = new ParamViewModel() { Param = new Param { atach = Character.Id, Name = name.ToString(), Value = 50 }, ListViewModel = this };
            Params.Add(AddedPar);
            App.DatabaseParam.SaveItem(AddedPar.Param);
            /*List<Param> Qparamss = new List<Param>();
            Qparamss = App.DatabaseParam.GetItems().ToList();
            foreach (Param c in Qparamss)
            {
                System.Console.WriteLine(c.Name);
                System.Console.WriteLine(c.atach);
                System.Console.WriteLine(Character.Id);
            }*/
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
        private void MoveToTop(object paramObj)
        {
            ParamViewModel param = paramObj as ParamViewModel;
            if (param == null) return;
            int oldIndex = Params.IndexOf(param);
            if (oldIndex > 0)
                Params.Move(oldIndex, oldIndex - 1);
        }
        private void MoveToBottom(object paramObj)
        {
            ParamViewModel param = paramObj as ParamViewModel;
            if (param == null) return;
            int oldIndex = Params.IndexOf(param);
            if (oldIndex < Params.Count - 1)
                Params.Move(oldIndex, oldIndex + 1);
        }
        private void Remove(object paramObj)
        {
            ParamViewModel param = paramObj as ParamViewModel;
            if (param == null)
            {
                System.Console.WriteLine("ParamisNullParamisNullParamisNullParamisNullParamisNullParamisNullParamisNullParamisNullParamisNull");
                return;
            }
            System.Console.WriteLine("ParamisNOTNullParamisNOTNullParamisNOTNullParamisNOTNullParamisNOTNullParamisNOTNull");

            Params.Remove(param);
            App.DatabaseParam.DeleteItem(param.Param.Id);
        }
    }
}
