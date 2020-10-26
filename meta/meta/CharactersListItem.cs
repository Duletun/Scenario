using System;
using System.Collections.Generic;
using System.Text;
using meta.ViewModels;
using System.Collections.ObjectModel;
using SQLite;

namespace meta
{
    public class CharactersListItem : ObservableCollection<CharacterViewModel>
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; } = 0;
    }
}
