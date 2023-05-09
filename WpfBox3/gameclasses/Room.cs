using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBox3.gameclasses
{
    [Serializable]
    public class Room : ThingHolder
    {
        public Room(string aName, string aDescription, Rm aN, Rm aS, Rm aW, Rm aO, ThingList tl) : base(aName, aDescription, tl)
        {
            _s = aS;
            _w = aW;
            _o = aO;
            _n = aN;
        }

        private Rm _n;
        private Rm _s;
        private Rm _w;
        private Rm _o;

        public Rm n {
            get => _n;
            set => _n = value; 
        }
        public Rm s {
            get => _s; 
            set => _s = value; 
        }
        public Rm w {
            get => _w; 
            set => _w = value; 
        }
        public Rm o {
            get => _o; 
            set => _o = value; 
        }

        public override string Describe()
        {
            return $"[{Name}] : {Description}" + "\r\nHier findest du: " + Things.Describe();
        }
    }
}
