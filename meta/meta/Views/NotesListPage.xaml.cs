using Xamarin.Forms;
using meta.ViewModels;

namespace meta.Views
{
    public partial class NotesListPage : ContentPage
    {
        public NotesListPage()
        {
            InitializeComponent();
            BindingContext = new NotesListViewModel() { Navigation = this.Navigation };
        }
    }
}