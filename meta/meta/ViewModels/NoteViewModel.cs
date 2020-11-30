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
                    OnPropertyChanged("title");
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
                    OnPropertyChanged("text");
                }
            }
        }
        public string text
        {
            get
            {
                if (Note.Text.Length > 40)
                {
                        string tmp = Note.Text;
                        string returning = (Note.Text.Substring(0, 40) + "...").Trim();
                        Note.Text = tmp;
                        return returning;
                }
                else
                    return Note.Text;
            }
        }
        public string title
        {
            get
            {
                if (Note.Title.Length > 34)
                {
                        string tmp = Note.Title;
                        string returning = (Note.Title.Substring(0, 34) + "...").Trim();
                        Note.Title = tmp;
                        return returning;
                }
                else
                    return Note.Title;
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