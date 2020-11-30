using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using meta.Models;

namespace meta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Test : ContentPage
    {
        public Test()
        {
            Note[] records = new Note[2] { new Note() { Title = "Qliality", Text = "DFSDFSDF" }, new Note() { Title = "Skook", Text = "DAROUDAROU" } };
            var myDataTemplate = new DataTemplate(() =>
            {
                var cell = new ViewCell();
                var grid = new Grid();

                foreach (Note record in records)
                {
                    grid.RowDefinitions.Add(new RowDefinition());
                }

                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                /*
                 * 
                 * Populate grid here...
                 * 
                 */

                cell.View = grid;
                return cell;
            });
            InitializeComponent();

        }

    }
}