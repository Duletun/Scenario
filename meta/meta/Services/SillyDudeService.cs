// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SillyFrontService.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   The SillyFrontService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Sharpnado.HorizontalListView;
using Sharpnado.HorizontalListView.Services;
using meta.ViewModels;

namespace meta.Services
{
    public class SillyDudeService : ISillyDudeService
    {
        private readonly ErrorEmulator _errorEmulator;
        private int _peopleCounter = 0;
        private Dictionary<int, SillyDude> _repository;
        private int First_start = 0;

        private readonly Random _randomizer = new Random();

        public SillyDudeService(ErrorEmulator errorEmulator)
        {
            _errorEmulator = errorEmulator;
            _repository = new Dictionary<int, SillyDude>();
           /* {
                {
                    ++_peopleCounter, new SillyDude(
                        _peopleCounter,
                        "Will Ferrell",
                        "Actor",
                        "Hey.\nThey laughed at Louis Armstrong when he said he was gonna go to the moon.\nNow he’s up there, laughing at them.",
                        "Logo.png",
                        4,
                        Filmos.Ferrell,
                        "https://sayingimages.com/wp-content/uploads/dear-monday-will-ferrell-memes.jpg",
                        "https://youtu.be/sPFRZP4qY7I?t=26")
                },
                 {
                    ++_peopleCounter, new SillyDude(
                        _peopleCounter,
                        "Will Ferrell",
                        "Actor",
                        "Hey.\nThey laughed at Louis Armstrong when he said he was gonna go to the moon.\nNow he’s up there, laughing at them.",
                        "Logo.png",
                        4,
                        Filmos.Ferrell,
                        "https://sayingimages.com/wp-content/uploads/dear-monday-will-ferrell-memes.jpg",
                        "https://youtu.be/sPFRZP4qY7I?t=26")
                }
            };*/

            /*List<SillyDudeVmo> charlik = new List<SillyDudeVmo>();
            charlik = (App.DatabaseTime.GetItems().ToList()).ConvertAll(x => new SillyDudeVmo(x, null));

            for (int id = 0; id < charlik.Count(); id++)
            {
                //var dudeToClone = _repository[id];
                _repository.Add(++_peopleCounter, charlik[id].timeline);
            }*/



        }


        public void AddSilly(SillyDude add_silly)
        {
            _repository.Add(++_peopleCounter, add_silly);
        }

        public async Task<IReadOnlyCollection<SillyDude>> GetSillyPeople()
        {
            await Task.Delay(_errorEmulator.DefaultLoadingTime);

            return _repository.Values.Take(9).ToList();
        }

        public async Task<PageResult<SillyDude>> GetSillyPeoplePage(int pageNumber, int pageSize)
        {
            Contract.Requires(() => pageNumber > 0);
            Contract.Requires(() => pageSize >= 10);


            List<SillyDudeVmo> charlik = new List<SillyDudeVmo>();
            charlik = (App.DatabaseTime.GetItems().ToList()).ConvertAll(x => new SillyDudeVmo( x, null));
            _repository.Clear();
            _peopleCounter = 0;

            for (int id = 0; id < charlik.Count(); id++)
            {
                //var dudeToClone = _repository[id];
                _repository.Add(++_peopleCounter, charlik[id].timeline);
            }

            await Task.Delay(TimeSpan.FromSeconds(First_start == 0 ? 3 : 0));
            First_start++;



            return new PageResult<SillyDude>(
                _repository.Count,
                _repository.Values.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
        }

        public async Task<SillyDude> GetSilly(int id)
        {
            await Task.Delay(_errorEmulator.DefaultLoadingTime);

            return _repository[id];
        }

        public async Task<SillyDude> GetRandomSilly(int waitTime = -1)
        {
            await Task.Delay(waitTime > -1 ? TimeSpan.FromSeconds(waitTime) : _errorEmulator.DefaultLoadingTime);

            int minId = _repository.Keys.Min();
            int maxId = _repository.Keys.Max();

            return _repository[_randomizer.Next(minId, maxId)];
        }
    }
}