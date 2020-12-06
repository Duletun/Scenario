using System;
using System.Collections.Generic;
using System.Text;

using meta.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using meta.Views;
using System.Linq;

namespace meta.ViewModels
{
    public class CharactersListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CharacterViewModel> Characters { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateCharacterCommand { protected set; get; }
        public ICommand DeleteCharacterCommand { protected set; get; }
        public ICommand SaveCharacterCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }
        public ICommand BackSaveCommand { protected set; get; }
        CharacterViewModel selectedCharacter;

        public INavigation Navigation { get; set; }

        public CharactersListViewModel()
        {
            Characters = new ObservableCollection<CharacterViewModel>();
            List<CharacterViewModel> charlik = new List<CharacterViewModel>();
            charlik = (App.Database.GetItems().ToList()).ConvertAll(x => new CharacterViewModel
            {
                Character = x,
                ListViewModel = this,
                IsCreated = true
            });
            List<ParamViewModel> paramss = new List<ParamViewModel>();
            paramss = (App.DatabaseParam.GetItems().ToList()).ConvertAll(x => new ParamViewModel
            {
                Param = x,
                IsCreated = true
            });
            foreach (CharacterViewModel c in charlik)
            {
                /*До этого парамы гетились в пейдже, это попытка загетить их один раз
                 на этапе загечивания карактеров в лист*/
                    foreach (ParamViewModel p in paramss)
                    {
                        if (p.Param.atach == c.Character.Id)
                        {
                            c.Params.Add(p);
                        }
                    }
                this.Characters.Add(c);
            }
            CreateCharacterCommand = new Command(CreateCharacter);
            DeleteCharacterCommand = new Command(DeleteCharacter);
            SaveCharacterCommand = new Command(SaveCharacter);
            BackCommand = new Command(Back);
            BackSaveCommand = new Command(BackSave);

        }

        public CharacterViewModel SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (selectedCharacter != value)
                {
                    CharacterViewModel tempCharacter = value;
                    selectedCharacter = null;
                    OnPropertyChanged("SelectedCharacter");
                    Navigation.PushAsync(new CharacterPage(tempCharacter));
                }
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void CreateCharacter()
        {
            Navigation.PushAsync(new CharacterPage(new CharacterViewModel() { ListViewModel = this }));
        }
        private void BackSave(object characterObject)   
        {
            System.Console.WriteLine("BACKSAVEBACKSAVEBACKSAVEBACKSAVEBACKSAVEBACKSAVEBACKSAVEBACKSAVEBACKSAVEBACKSAVEBACKSAVEBACKSAVEBACKSAVE");

            CharacterViewModel character = characterObject as CharacterViewModel;
            {
                if (!String.IsNullOrEmpty(character.Name))
                {
                    App.Database.UpdateItem(character.Character);
                    foreach (ParamViewModel c in character.Params)
                    {
                        System.Console.WriteLine("Updated {0}, {1}, {2}", c.Name, c.Value, c.atach);
                        App.DatabaseParam.UpdateItem(c.Param);
                    }
                }
                Navigation.PopAsync();
                List<Param> Qparamss = new List<Param>();
                Qparamss = App.DatabaseParam.GetItems().ToList();
                foreach (Param c in Qparamss)
                {
                    System.Console.WriteLine(c.Name);
                    System.Console.WriteLine(c.Value);
                    System.Console.WriteLine(c.atach);
                }
            }


        }
        private void Back()
        {
            Navigation.PopAsync();
        }
        private void SaveCharacter(object characterObject)
        {
            CharacterViewModel character = characterObject as CharacterViewModel;
            if (character != null && character.IsValid)
            {
                Characters.Add(character);
                character.IsCreated = true; 
                App.Database.SaveItem(character.Character);
            }
            Back();
        }
        private void DeleteCharacter(object characterObject)
        {
            CharacterViewModel character = characterObject as CharacterViewModel;
            if (character != null)
            {

                Characters.Remove(character);
                App.Database.DeleteItem(character.Character.Id);
            }
            List<Param> paramss = new List<Param>();
            paramss = App.DatabaseParam.GetItems().ToList();
            foreach (Param p in paramss)
            {
                if (p.atach == character.Character.Id)
                {
                    App.DatabaseParam.DeleteItem(p.Id);
                }
            }
            Back();
        }
        private void SaveAll()
        {
            //App.Current.Properties["AllChars"] = this.Characters[0].Name ; 
        }
    }
}