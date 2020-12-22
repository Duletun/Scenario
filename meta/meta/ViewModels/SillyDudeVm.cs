// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SillyDudeVm.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   Class SillyDudeVm.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Sharpnado.Presentation.Forms;
using Sharpnado.Tabs;
using Sharpnado.Tasks;

using meta.Views;
using meta.Services;
using meta.Navigables;
using meta.Navigables.Impl;

using Xamarin.Forms;
using System.ComponentModel;
using Xamarin.Essentials;
using meta.Models;


namespace meta.ViewModels
{
    /// <summary>
    /// Class SillyDudeVm.
    /// </summary>
    /// 

    public class SillyDudeVm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        GridPageViewModel lvm;


        public INavigation Navigation { get; set; }

        public SillyDudeVm(SillyDude other)
        {
            TimeLineEvent = other;
        }

        public GridPageViewModel ListViewModel
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



        /*public void UpdateDudeData()
        {
            Name = TimeLineEvent.Name;
            Role = TimeLineEvent.Role;
            Description = TimeLineEvent.Description;
            ImageUrl = TimeLineEvent.ImageUrl;
            SillinessDegree = TimeLineEvent.SillinessDegree;
            SourceUrl = TimeLineEvent.SourceUrl;
        }*/

        public SillyDude TimeLineEvent { get; set; }


        public string Name
        {
            get { return TimeLineEvent.Name; }
            set
            {
                if (TimeLineEvent.Name != value)
                {
                    TimeLineEvent.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public string Role
        {
            get { return TimeLineEvent.Role; }
            set
            {
                if (TimeLineEvent.Role != value)
                {
                    TimeLineEvent.Role = value;
                    OnPropertyChanged("Role");
                }
            }
        }
        public string ImageUrl
        {
            get { return TimeLineEvent.ImageUrl; }
            set
            {
                if (TimeLineEvent.ImageUrl != value)
                {
                    TimeLineEvent.ImageUrl = value;
                    OnPropertyChanged("ImageUrl");
                }
            }
        }

        public string Description
        {
            get { return TimeLineEvent.Description; }
            set
            {
                if (TimeLineEvent.Description != value)
                {
                    TimeLineEvent.Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }


        public int SillinessDegree { get; set; }

        public string SourceUrl { get; set; }

        public bool IsCreated { get; set; } = false;
        public bool IsValid
        {
            get
            {
                return ((!string.IsNullOrEmpty(Name.Trim())) ||
                    (!string.IsNullOrEmpty(Role.Trim())));
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }


    }
}