using System.Threading.Tasks;

using Sharpnado.HorizontalListView.RenderedViews;
using meta.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using meta.ViewModels;

namespace meta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GridPage : ContentPage
    {
        public GridPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();



            HorizontalListView.PreRevealAnimationAsync = async (viewCell) =>
            {
                viewCell.View.Opacity = 0;

                if (HorizontalListView.ListLayout == HorizontalListViewLayout.Vertical)
                {
                    viewCell.View.RotationX = 0;
                }
                else
                {
                    viewCell.View.RotationY = 0;
                }

                await Task.Delay(200);
            };

            HorizontalListView.RevealAnimationAsync = async (viewCell) =>
            {
                await viewCell.View.FadeTo(1);

                if (HorizontalListView.ListLayout == HorizontalListViewLayout.Vertical)
                {
                    await viewCell.View.RotateXTo(0);
                }
                else
                {
                    await viewCell.View.RotateYTo(0);
                }
            };

            HorizontalListView.ItemWidth = 0;
            HorizontalListView.ScrollSpeed = ScrollSpeed.Slowest;
            HorizontalListView.ItemSpacing = 1;
            HorizontalListView.ItemHeight = 110;
            HorizontalListView.Margin = new Thickness(0);


        }

        private void ListLayoutChanging(object sender, ListLayoutChangedEventArgs e)
        {
            switch (e.ListLayout)
            {
                case HorizontalListViewLayout.Linear:
                    HorizontalListView.ItemWidth = 260;
                    HorizontalListView.ItemHeight = 260;
                    HorizontalListView.ColumnCount = 0;
                    HorizontalListView.Margin = Device.RuntimePlatform == Device.Android
                        ? new Thickness(0, 60, 0, 0)
                        : new Thickness(0, -60, 0, 0);

                    break;

                case HorizontalListViewLayout.Grid:
                    HorizontalListView.ItemWidth = 120;
                    HorizontalListView.ItemHeight = 120;
                    HorizontalListView.ColumnCount = 0;
                    HorizontalListView.Margin = new Thickness(0);
                    break;

                case HorizontalListViewLayout.Vertical:
                    HorizontalListView.ItemWidth = 0;
                    HorizontalListView.ScrollSpeed = ScrollSpeed.Slowest;
                    HorizontalListView.ItemSpacing = -4;
                    HorizontalListView.ItemHeight = 110;
                    HorizontalListView.Margin = new Thickness(0);
                    break;
            }
        }
    }
}