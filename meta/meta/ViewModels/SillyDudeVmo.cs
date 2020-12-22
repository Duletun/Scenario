// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SillyVmo.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   The silly vmo.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows.Input;

using meta.Services;

namespace meta.ViewModels
{
    public class SillyDudeVmo 
    {
        public SillyDude timeline { get; set; }
        public SillyDudeVmo(SillyDude dude, ICommand tapCommand)
        {
            if (dude != null)
            {
                timeline = dude;
                Id = dude.Id;
                Name = dude.Name;
                Role = dude.Role;
                Description = dude.Description;
                ImageUrl = dude.ImageUrl;
                SillinessDegree = dude.SillinessDegree;
                SourceUrl = dude.SourceUrl;
            }

            TapCommand = tapCommand;
        }

        public bool IsMovable { get; protected set; } = true;

        public ICommand TapCommand { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int SillinessDegree { get; set; }

        public string SourceUrl { get; set; }

        public void Lock()
        {
            IsMovable = false;
        }
    }

    public class AddSillyDudeVmo : SillyDudeVmo
    {
        public AddSillyDudeVmo(ICommand tapCommand)
            : base(null, tapCommand)
        {
        }
    }
}