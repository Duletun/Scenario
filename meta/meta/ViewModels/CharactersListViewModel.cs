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
            //try
            // {
            charlik = (App.Database.GetItems().ToList()).ConvertAll(x => new CharacterViewModel
            {
                Character = x,
                ListViewModel = this
            });
            //  {
            //   Character = x,
            //    IsCreated = true,
            //      Name = x.Name,
            //    Description = x.Description,
            //     ImagePath = x.ImagePath
            //}) ;
            foreach (CharacterViewModel c in charlik)
            {
                this.Characters.Add(c);
            }
            //CharacterViewModel aqq = new CharacterViewModel { Character = new Character { Name = "asdas", Description = "asdasd", }, ListViewModel = this };
            //this.Characters.Add(aqq);
     

          //  catch
            //{

           // }
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
            CharacterViewModel character = characterObject as CharacterViewModel;
            {
                if (!String.IsNullOrEmpty(character.Name))
                {
                    App.Database.UpdateItem(character.Character);
                }
                Navigation.PopAsync();
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
            Back();
        }
        private void SaveAll()
        {
            //App.Current.Properties["AllChars"] = this.Characters[0].Name ; 
        }
    }
}