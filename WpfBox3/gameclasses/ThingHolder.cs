using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBox3.gameclasses
{
    [Serializable]
    public class ThingHolder : Thing
    {
        private ThingList _things = new ThingList();

        public ThingHolder(string aName, string aDescription, ThingList ti) : base(aName, aDescription)
        {
            _things = ti;
        }

        public ThingList Things { 
            get => _things; 
            set => _things = value; 
        }

        public void AddThing(Thing aThing)
        {
            _things.Add(aThing);
        }

        public void AddThings(ThingList aThingList)
        {
            _things.AddRange(aThingList);
        }

        public override String Describe()
        {
            return $"Name {Name}, Beschreibung {Description}" +
                $"Es enthält -> {_things.Describe()}";
        }
    }
}
