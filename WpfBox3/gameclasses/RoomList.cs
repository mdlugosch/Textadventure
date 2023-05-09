using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WpfBox3.gameclasses
{
    [Serializable]
    public class RoomList : Dictionary<Rm, Room>
    {
        public RoomList() { }

        /*
         * Die Serialisierungsinformationen müssen an die Basisklasse (Dictionary)
         * weitergegeben werden. Daher der zusätzliche Konstruktor.
         */
        protected RoomList(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public Room RoomAt(Rm id)
        {
            return this[id];
        }

        public string Describe()
        {
            string s = "";
            if(this.Count==0)
            {
                s = "Es befindet sich nichts in der RoomList";
            } else
            {
                foreach (KeyValuePair<Rm,Room> kv in this)
                {
                    s = s + kv.Value.Describe() + "\r\n";
                }
            }
            return s;
        }
    }
}
