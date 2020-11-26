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
    public class NotesListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<NoteViewModel> Notes { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateNoteCommand { protected set; get; }
        public ICommand DeleteNoteCommand { protected set; get; }
        public ICommand SaveNoteCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }
        public ICommand BackSaveCommand { protected set; get; }
        NoteViewModel selectedNote;

        public INavigation Navigation { get; set; }

        public NotesListViewModel()
        {
            Notes = new ObservableCollection<NoteViewModel>();
            List<NoteViewModel> charlik = new List<NoteViewModel>();
            charlik = (App.Database3.GetItems().ToList()).ConvertAll(x => new NoteViewModel
            {
                Note = x,
                ListViewModel = this,
                IsCreated = true
            });
            foreach (NoteViewModel c in charlik)
            {
                this.Notes.Add(c);
            }
            CreateNoteCommand = new Command(CreateNote);
            DeleteNoteCommand = new Command(DeleteNote);
            SaveNoteCommand = new Command(SaveNote);
            BackCommand = new Command(Back);
            BackSaveCommand = new Command(BackSave);
        }

        public NoteViewModel SelectedNote
        {
            get { return selectedNote; }
            set
            {
                if (selectedNote != value)
                {
                    NoteViewModel tempNote = value;
                    selectedNote = null;
                    OnPropertyChanged("SelectedNote");
                    Navigation.PushAsync(new NotePage(tempNote));
                }
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void CreateNote()
        {
            Navigation.PushAsync(new NotePage(new NoteViewModel() { ListViewModel = this }));
        }
        private void Back()
        {
            Navigation.PopAsync();
        }
        private void SaveNote(object noteObject)
        {
            NoteViewModel note = noteObject as NoteViewModel;
            if (note != null && note.IsValid)
            {
                Notes.Add(note);
            }
            Back();
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
        private void DeleteNote(object noteObject)
        {
            NoteViewModel note = noteObject as NoteViewModel;
            if (note != null)
            {
                Notes.Remove(note);
            }
            Back();
        }
    }
}