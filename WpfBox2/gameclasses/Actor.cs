using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBox2.gameclasses
{
    [Serializable]
    public class Actor : ThingHolder
    {
        private Room _location; // Raum in der sich der Actor begindet

        public Actor(string aName, string aDescription, Room aRoom, ThingList tl) : base(aName, aDescription, tl) {
                _location = aRoom; 
        }

        public Room Location
        {
            get => _location;
            set => _location = value;
        }

        public override string Describe()
        {
            return $"[{Name}] ({Description}) befindet sich in {_location.Describe()}";
        }
    }
}
