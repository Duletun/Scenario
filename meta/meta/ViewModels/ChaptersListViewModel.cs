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
    public class ChaptersListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ChapterViewModel> Chapters { get; set; }
        public bool NeedToReload { get; set; } = false;
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateChapterCommand { protected set; get; }
        public ICommand DeleteChapterCommand { protected set; get; }
        public ICommand SaveChapterCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }
        public ICommand BackSaveCommand { protected set; get; }
        ChapterViewModel selectedChapter;

        public INavigation Navigation { get; set; }

        public ChaptersListViewModel()
        {
            Chapters = new ObservableCollection<ChapterViewModel>();
            List<ChapterViewModel> chaplik = new List<ChapterViewModel>();

            chaplik = (App.Database2.GetItems().ToList()).ConvertAll(x => new ChapterViewModel
            {
                Chapter = x,
                ListViewModel = this,
                IsCreated = true
            });

            foreach (ChapterViewModel c in chaplik)
            {
                this.Chapters.Add(c);
            }
            CreateChapterCommand = new Command(CreateChapter);
            DeleteChapterCommand = new Command(DeleteChapter);
            SaveChapterCommand = new Command(SaveChapter);
            BackCommand = new Command(Back);
            BackSaveCommand = new Command(BackSave);

        }

        public ChapterViewModel SelectedChapter
        {
            get { return selectedChapter; }
            set
            {
                if (selectedChapter != value)
                {
                    ChapterViewModel tempChapter = value;
                    selectedChapter = null;
                    OnPropertyChanged("SelectedChapter");
                    Navigation.PushAsync(new ChapterPage(tempChapter));
                }
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void CreateChapter()
        {
            Navigation.PushAsync(new ChapterPage(new ChapterViewModel() { ListViewModel = this }));
        }
        private void BackSave(object chapterObject)
        {
            ChapterViewModel chapter = chapterObject as ChapterViewModel;
            {
                if (!String.IsNullOrEmpty(chapter.Text))
                {
                    App.Database2.UpdateItem(chapter.Chapter);
                }
                Navigation.PopAsync();
            }


        }
        private void Back()
        {
            Navigation.PopAsync();
        }
        private void SaveChapter(object chapterObject)
        {
            ChapterViewModel chapter = chapterObject as ChapterViewModel;
            if (chapter != null && chapter.IsValid)
            {
                chapter.IsCreated = true;
                Chapters.Add(chapter);
                App.Database2.SaveItem(chapter.Chapter);
            }
            Back();
        }
        private void DeleteChapter(object chapterObject)
        {
            ChapterViewModel chapter = chapterObject as ChapterViewModel;
            if (chapter != null)
            {
                Chapters.Remove(chapter);
                App.Database2.DeleteItem(chapter.Chapter.Id);
            }
            Back();
        }
        private void SaveAll()
        {

        }
    }
}