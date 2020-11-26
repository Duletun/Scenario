using System.ComponentModel;
using meta.Models;

namespace meta.ViewModels
{
    public class NoteViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public NotesListViewModel lvm;
        public bool IsCreated { get; set; } = false;
        public Note Note { get; set; }

        public NoteViewModel()
        {
            Note = new Note();
        }

        public NotesListViewModel ListViewModel
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
        public string Title
        {
            get { return Note.Title; }
            set
            {
                if (Note.Title != value)
                {
                    Note.Title = value;
                    OnPropertyChanged("Title");
                }
            }
        }
        public string Text
        {
            get { return Note.Text; }
            set
            {
                if (Note.Text != value)
                {
                    Note.Text = value;
                    OnPropertyChanged("Text");
                }
            }
        }

        public bool IsValid
        {
            get
            {
                return ((!string.IsNullOrEmpty(Title.Trim())) ||
                    (!string.IsNullOrEmpty(Text.Trim())));
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}