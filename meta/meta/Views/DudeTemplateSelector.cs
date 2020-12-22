using Sharpnado.HorizontalListView.RenderedViews;
using meta.ViewModels;

using Xamarin.Forms;

namespace meta.Views
{
    public class DudeTemplateSelector: DataTemplateSelector
    {

        public DataTemplate GridTemplate { get; set; }

        public DataTemplate HorizontalTemplate { get; set; }

        public DataTemplate VerticalTemplate { get; set; }

        public DataTemplate AddSillyDude { get; set; }

        public DataTemplate SillyDude { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var horizontalList = (HorizontalListView)container;
            HorizontalListViewLayout layout = horizontalList.ListLayout;

            if (item is AddSillyDudeVmo)
            {
                return AddSillyDude;
            }


                    return VerticalTemplate;
            
        }

       

    }
}