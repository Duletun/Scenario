using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

using meta.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using meta.Views;
using System.Linq;

namespace meta.Services
{
    [Table("SillyDude")]
    public class SillyDude
    {

        public SillyDude(int id, string name, string role, string description, string imageUrl, int sillinessDegree, string filmoMarkdown, string memeUrl, string sourceUrl = null)
        {
            if (sillinessDegree > 5 || sillinessDegree < 0)
            {
                throw new ArgumentException(@"sillinessDegree must be between 0 and 5", nameof(sillinessDegree));
            }

            Id = id;
            Name = name;
            Role = role;
            Description = description;
            ImageUrl = imageUrl;
            SillinessDegree = sillinessDegree;
            SourceUrl = sourceUrl;
            FilmoMarkdown = filmoMarkdown;
            MemeUrl = memeUrl;
        }
        public SillyDude()
        {
            Name = "";
            Role = "";
            Description = "";
            ImageUrl = "point.png";
            SillinessDegree = 1;
            SourceUrl = "";
            FilmoMarkdown = "";
            MemeUrl ="";
        }

        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        //public Image

        public int SillinessDegree { get; set; }

        public string SourceUrl { get; set; }

        public string FilmoMarkdown { get; set; }

        public string MemeUrl { get; set; }

        public SillyDude Clone(int id)
        {
            return new SillyDude(
                id,
                Name,
                Role,
                Description,
                ImageUrl,
                SillinessDegree,
                FilmoMarkdown,
                MemeUrl,
                SourceUrl);
        }

        private string TruncateName(int maxChars)
        {
            return Name.Length <= maxChars ? Name : Name.Substring(0, maxChars) + "...";
        }
    }
}