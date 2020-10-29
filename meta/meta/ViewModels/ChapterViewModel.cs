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
    public class ChapterViewModel : INotifyPropertyChanged
    {
        public ChaptersListViewModel lvm;
        public bool IsVisible { get; set; } = false;
        public ChaptersListViewModel ListViewModel
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
        public Chapter Chapter { get; set; }
        //static int a = 0;
        public ChapterViewModel()
        {
            if (Chapter == null)
            {
                Chapter = new Chapter();
            }
        }
        public string Title
        {
            get { return Chapter.Title; }
            set
            {
                if (Chapter.Title != value)
                {
                    Chapter.Title = value;
                    OnPropertyChanged("Title");
                }
            }
        }
        public string Text
        {
            get { return Chapter.Text; }
            set
            {
                if (Chapter.Text != value)
                {
                    Chapter.Text = value;
                    OnPropertyChanged("Text");
                }
            }
        }
        public int Words
        {
            get { return Chapter.Words; }
            set
            {
                if (Chapter.Words != value)
                {
                    OnPropertyChanged("Words");
                }
            }
        }
        public bool IsCreated { get; set; } = false;
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
