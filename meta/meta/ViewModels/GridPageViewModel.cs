using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Sharpnado.Tasks;

using meta.Navigables;
using meta.Services;
using meta.Navigables.Impl;
using meta.Views;

using Sharpnado.HorizontalListView.Paging;
using Sharpnado.HorizontalListView.Services;
using Sharpnado.HorizontalListView.ViewModels;
using Sharpnado.Presentation.Forms;

using Xamarin.Forms;

namespace meta.ViewModels
{
    public enum ListMode
    {
        Grid = 0,
        Horizontal = 1,
        Vertical = 2,
    }

    public class GridPageViewModel : ANavigableViewModel
    {
        private const int PageSize = 20;
        private readonly ISillyDudeService _sillyDudeService;
        private INavigationService _navigationService;

        private ObservableRangeCollection<SillyDudeVmo> _sillyPeople;
        private ListMode _mode;
        private int _currentIndex = -1;

        private int? _selectedDudeId;

        public GridPageViewModel(INavigationService navigationService, ISillyDudeService sillyDudeService)
            : base(navigationService)
        {
            _sillyDudeService = sillyDudeService;
            _navigationService = navigationService;

            InitCommands();

            OnDragAndDropEndCommand = new Command(PosChanged);

            //CreateDudeCommand = new Command(CreateDude);

            DeleteDudeCommand = new Command(DeleteDude);
            SaveDudeCommand = new Command(SaveDude);
            BackCommand = new Command(Back);
            BackSaveCommand = new Command(BackSave);

            SillyPeople = new ObservableRangeCollection<SillyDudeVmo>();
            SillyPeoplePaginator = new Paginator<SillyDude>(LoadSillyPeoplePageAsync, pageSize: PageSize);
            SillyPeopleLoaderNotifier = new TaskLoaderNotifier<IReadOnlyCollection<SillyDude>>();
        }

        public LogoLetterVmo[] Logo { get; } = new[]
        {
            new LogoLetterVmo("H", Color.FromHex("#FF0266"), "ThinAccentNeumorphism"),
            new LogoLetterVmo("L", Color.White, "ThinDarkerNeumorphism"),
            new LogoLetterVmo("V", Color.White, "ThinDarkerNeumorphism"),
        };

        public int CurrentIndex
        {
            get => _currentIndex;
            set => SetAndRaise(ref _currentIndex, value);
        }


        // Комманды

        public ICommand DeleteDudeCommand { protected set; get; }
        public ICommand SaveDudeCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }
        public ICommand BackSaveCommand { protected set; get; }

        public ICommand OnSillyDudeAddedCommand { get; private set; }
        public ICommand TapCommand { get; private set; }

        public ICommand OnScrollBeginCommand { get; private set; }

        public ICommand OnScrollEndCommand { get; private set; }

        public ICommand OnDragAndDropEndCommand { get; private set; }

        public INavigation Navigation { get; set; }


        ///
        ///
        /// 
        ///
        ///

        public TaskLoaderNotifier<IReadOnlyCollection<SillyDude>> SillyPeopleLoaderNotifier { get; }

        public ListMode Mode
        {
            get => _mode;
            set => SetAndRaise(ref _mode, value);
        }

        public Paginator<SillyDude> SillyPeoplePaginator { get; }

        public ObservableRangeCollection<SillyDudeVmo> SillyPeople
        {
            get => _sillyPeople;
            set => SetAndRaise(ref _sillyPeople, value);
        }

        public int? SelectedDudeId
        {
            get => _selectedDudeId;
            set => SetAndRaise(ref _selectedDudeId, value);
        }

        public override void Load(object parameter)
        {
            SillyPeople = new ObservableRangeCollection<SillyDudeVmo>();

            SillyPeopleLoaderNotifier.Load(async () => (await SillyPeoplePaginator.LoadPage(1)).Items);
        }

        private async Task AddSillyDudeAsync()
        {
            var newPage = new SillyDudePage(new SillyDudeVm(new SillyDude()) { ListViewModel = this });

            Navigation.PushAsync(newPage);


            /*App.DatabaseTime.SaveItem(lolkek);
            _sillyDudeService.AddSilly(lolkek);
            SillyPeople.Add(lolkek1);

            if (SillyPeople.Count == 1)
            {
                SillyPeople = new ObservableRangeCollection<SillyDudeVmo>();
                SillyPeopleLoaderNotifier.Load(async () => (await SillyPeoplePaginator.LoadPage(1)).Items);
            }*/

        }



        private void PosChanged()
        {

            App.DatabaseTime.DeleteAllItems();

            for (int i = 0; i < SillyPeople.Count; i++)
            {
                SillyPeople[i].Id = i + 1;
                SillyPeople[i].timeline.Id = i + 1;
            }
            for (int i = 0; i < SillyPeople.Count; i++)
            {
                App.DatabaseTime.ReplaceItem(SillyPeople[i].timeline);
            }



        }

        private void InitCommands()
        {

            OnSillyDudeAddedCommand = new Command(() => TaskMonitor.Create(AddSillyDudeAsync()));

            TapCommand = new Command<SillyDudeVmo>(
                async (vm) =>  await Navigation.PushAsync(new SillyDudePage(new SillyDudeVm(vm.timeline) { ListViewModel = this, IsCreated = true})));

            OnDragAndDropEndCommand = new Command(
               () => System.Diagnostics.Debug.WriteLine("SortSillyPeopleVm: OnDragAndDropEndCommand"));

            OnScrollBeginCommand = new Command(
                () => System.Diagnostics.Debug.WriteLine("SillyInfiniteGridPeopleVm: OnScrollBeginCommand"));
            OnScrollEndCommand = new Command(
                () => System.Diagnostics.Debug.WriteLine("SillyInfiniteGridPeopleVm: OnScrollEndCommand"));
        }

        private async Task<PageResult<SillyDude>> LoadSillyPeoplePageAsync(int pageNumber, int pageSize, bool isRefresh)
        {
            PageResult<SillyDude> resultPage = await _sillyDudeService.GetSillyPeoplePage(pageNumber, pageSize);
            var viewModels = resultPage.Items.Select(dude => new SillyDudeVmo(dude, TapCommand)).ToList();

            if (isRefresh)
            {
                SillyPeople = new ObservableRangeCollection<SillyDudeVmo>();
            }

            SillyPeople.AddRange(viewModels);

            // Uncomment to test CurrentIndex property
            // TaskMonitor.Create(
            //    async () =>
            //    {
            //        await Task.Delay(2000);
            //        CurrentIndex = 15;
            //    });

            // Uncomment to test Reset action
            // TaskMonitor.Create(
            //   async () =>
            //   {
            //       await Task.Delay(6000);
            //       SillyPeople.Clear();
            //       await Task.Delay(3000);
            //       SillyPeople = new ObservableRangeCollection<SillyDudeVmo>(viewModels);
            //   });

            return resultPage;
        }








        // Комманды

        private void BackSave(object characterObject)
        {
            System.Console.WriteLine("BACKSAVEBACKSAVEBACKSAVEBACKSAVEBACKSAVEBACKSAVEBACKSAVEBACKSAVEBACKSAVEBACKSAVEBACKSAVEBACKSAVEBACKSAVE");

            SillyDudeVm character = characterObject as SillyDudeVm;
            {
                if (character != null && character.IsValid)
                {
                    SillyDudeVmo new_dudevmo = new SillyDudeVmo(character.TimeLineEvent, TapCommand);
                    SillyPeople[character.TimeLineEvent.Id-1] = new_dudevmo;
                    PosChanged();

                    SillyPeopleLoaderNotifier.Load(async () => (await SillyPeoplePaginator.LoadPage(1)).Items);

                    //SillyPeople.Insert(character.TimeLineEvent.Id - 1, new_dudevmo);
                    //SillyPeople.RemoveAt(character.TimeLineEvent.Id - 1);



                }
                Back();
            }


        }
        private async void Back()
        {
            await Navigation.PopAsync();
        }
        private void SaveDude(object characterObject)
        {
            SillyDudeVm character = characterObject as SillyDudeVm;
            if (character != null && character.IsValid)
            {

                SillyDudeVmo new_dudevmo = new SillyDudeVmo(character.TimeLineEvent, TapCommand);

                //App.DatabaseTime.SaveItem(character.TimeLineEvent);
                _sillyDudeService.AddSilly(character.TimeLineEvent);
                SillyPeople.Add(new_dudevmo);

                PosChanged();

                if (SillyPeople.Count == 1)
                {
                    SillyPeople = new ObservableRangeCollection<SillyDudeVmo>();
                    SillyPeopleLoaderNotifier.Load(async () => (await SillyPeoplePaginator.LoadPage(1)).Items);
                }
                character.IsCreated = true;
            }
            Back();
        }
        private void DeleteDude(object characterObject)
        {
            SillyDudeVm character = characterObject as SillyDudeVm;
            if (character != null)
            {
                SillyPeople.RemoveAt(character.TimeLineEvent.Id - 1);

                PosChanged();

            }
            
            Back();
        }
    }
}