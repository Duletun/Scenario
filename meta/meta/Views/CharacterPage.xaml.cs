using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using meta.ViewModels;
using System.Windows.Input;
using System.ComponentModel;
using meta.Views;
using Microcharts;
using SkiaSharp;
using Entry = Microcharts.ChartEntry;

namespace meta.Views
{
    public partial class CharacterPage : ContentPage
    {
        public CharacterViewModel ViewModel { get; private set; }
        public ObservableCollection<Param> arr;
        public List<Entry> entries;
        public void RadarAppear()
        {

        }
        private void SliderChanged(object sender,ValueChangedEventArgs e)
        {
            ParamViewModel from = (ParamViewModel)sender;
            for (int q = 0; q < 5; q++)
            { 
                if (entries[q].Label == from.Name)
                {
                    int val = Convert.ToInt32(e);
                    entries[q] = new Entry(val) {
                        Label = entries[q].Label,
                        ValueLabel = entries[q].ValueLabel.ToString(),
                        Color = entries[q].Color
                    };
                }
            }

        }
        private void plusParamShow(object sender, System.EventArgs e)
        {
            paramName.IsVisible = true;
            addParam.IsVisible = true;
            paramName.Text = "";
        }
        private void plusParamHide(object sender, System.EventArgs e)
        {
            paramName.IsVisible = false;
            addParam.IsVisible = false;
        }
        public CharacterPage(CharacterViewModel vm)
        {
            ViewModel = vm;
            vm.Navigation = this.Navigation;
            entries = new List<Entry>();
            for (int o = 0; o <5; o++)
            {
                if (o < ViewModel.Params.Count)
                {
                    System.Console.WriteLine("WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW");
                    Param parad = ViewModel.Params[o].Param;
                    SKColor color = new SKColor();
                    if (o == 0) color = SKColor.Parse("#2c3e50");
                    if (o == 1) color = SKColor.Parse("#77d065");
                    if (o == 2) color = SKColor.Parse("#b455b6");
                    if (o == 3) color = SKColor.Parse("#3498db");
                    if (o == 4) color = SKColor.Parse("#DC143C");
                    double dabl = parad.Value / 4;
                    int integer = Convert.ToInt32(dabl);
                    entries.Add(new Entry(integer)
                    {
                        Label = parad.Name,
                        ValueLabel = parad.Value.ToString(),
                        Color = color
                    }) ;

                }
            }

            var chart = new RadarChart() { Entries = entries };
            InitializeComponent();
            if (vm.IsCreated == true)
            {
                addButton.IsVisible = false;
                backButton.IsVisible = false;
            }
            else
            {
                delButton.IsVisible = false;
                saveButton.IsVisible = false;
            }
            chart.LabelTextSize = 30;
            this.chartView.Chart = chart;
            /*List<Param> paramss = new List<Param>();
            paramss = App.DatabaseParam.GetItems().ToList();
            if (ViewModel.Params.Count == 0)
            {
                foreach (Param c in paramss)
                {
                    if (c.atach == ViewModel.Character.Id)
                    {
                        System.Console.WriteLine("Trevoga");
                        System.Console.WriteLine(c.Name);
                        System.Console.WriteLine(c.Value);
                        System.Console.WriteLine(ViewModel.Character.Id);
                        ViewModel.Params.Add(c);
                    }
                }
            }*/
            this.BindingContext = ViewModel;
        }
    }
}