using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBox3.gameclasses;

namespace WpfBox3
{
    [Serializable]
    public class Adventure
    {
        private RoomList _map;
        private Actor _player;

        public Adventure()
        {
            //Karte der Welt in einem Dictionary anlegen
            _map = new RoomList();

            _map.Add(Rm.TrollRaum, new Room("Troll Raum", "Ein sehr dunklen Raum in dem es nach Troll riecht", Rm.NOEXIT, Rm.Höhle, Rm.NOEXIT, Rm.Wald, new ThingList { new Thing("Karotte", "Eine sehr knackige Karotte") }));
            _map.Add(Rm.Wald, new Room("Wald", "Einen sehr lichten Wald der im Sonnenlicht schimmert", Rm.NOEXIT, Rm.NOEXIT, Rm.TrollRaum, Rm.NOEXIT, new ThingList { 
                new Thing("Wurst", "Eine grobe Wurst aus Schweinefleisch"), 
                new Thing("Baum", "Ein gigantischer Eichenbaum", false) 
            }));
            _map.Add(Rm.Höhle, new Room("Höhle", "Eine weite Höhle deren Wände mit leuchtenen Moos bedeckt sind", Rm.TrollRaum, Rm.NOEXIT, Rm.NOEXIT, Rm.Verlies, new ThingList()));
            _map.Add(Rm.Verlies, new Room("Verlies", "Ein düsteres Verlies. Ratten huschen über den Boden", Rm.NOEXIT, Rm.NOEXIT, Rm.Höhle, Rm.NOEXIT, new ThingList()));

            // Spieler mit Namen, Beschreibung und Aufenthaltort anlegen
            _player = new Actor("Du", "Der Spieler", _map.RoomAt(Rm.TrollRaum), new ThingList());
        }

        public Actor Player
        {
            get => _player;
        }

        /*
         * Auswerten der Spielerbewegung.
         * In der Spielerklasse wird ermittelt ob und welcher
         * Raum in der gewählten Richtung liegt und legt das
         * Ergebnis in das Enum "Exit" (Typ: Rm)
         * Spielerobject und Enum werden an MoveActorTo übergeben.
         * Wenn das Enum NOEXIT enthält wird dieser Wert direkt zurück
         * gegeben da sich der Spieler in dem Fall nicht bewegt.
         */
        private Rm MoveTo(Actor anActor, Dir direction)
        {
            Room r = anActor.Location;
            Rm exit;

            switch (direction)
            {
                case Dir.NORDEN:
                    exit = r.n;
                    break;
                case Dir.SUEDEN:
                    exit = r.s;
                    break;
                case Dir.OSTEN:
                    exit = r.o;
                    break;
                case Dir.WESTEN:
                    exit = r.w;
                    break;
                default:
                    exit = Rm.NOEXIT;
                    break;
            }
            if (exit != Rm.NOEXIT)
            {
                MoveActorTo(anActor, _map.RoomAt(exit));
            }
            return exit;
        }

        // Dem Spieler wird die neue Position zugewiesen
        private void MoveActorTo(Actor anActor, Room aRoom)
        {
            anActor.Location = aRoom;
        }

        /*
         * Eventhandler interagieren direkt mit MovePlayerTo
         * Eventhandler können nur die Bewegungsrichtung übergeben
         * aber führen selbst keine Änderungen am Spielerobjekt durch.
         * MoveTo wertet die Richtung aus und führt die Bewegung mit
         * MoveActorTo aus. Das Ergebis der Bewegung wird als Enum
         * zurückgelifert. Das Ergebnis ist entweder eine "Fehlermeldung"
         * oder der Name des Raums in dem sich der Spieler befindet.
         */
        public string MovePlayerTo(Dir direction)
        {
            string s;
            if (MoveTo(_player, direction) == Rm.NOEXIT)
            {
                s = "In dieser Richtung gibt es keinen Ausgang.\r\n";
            }
            else
            {
                s = $"Du befindest dich an folgenden Ort: {_player.Location.Name}.\r\n";
            }

            return s;
        }

        private void TransferOb(Thing t, ThingList fromlist, ThingList tolist)
        {
            fromlist.Remove(t);
            tolist.Add(t);
        }

        public string TakeOb(string obname)
        {
            Thing t = _player.Location.Things.ThisOb(obname);
            string s = "";
            if (obname == "")
            {
                obname = "unbekanntes Objekt"; // Im Falle das kein Objekt angegeben ist.
            }
            if (t == null)
            {
                s = "Hier gibt es kein(e): " + obname;
            }
            else
            {
                if (t.CanTake)
                {
                    TransferOb(t, _player.Location.Things, _player.Things);
                    s = t.Name + " aufgehoben!";
                }
                else
                {
                    s = "Du kannst " + t.Name + " nicht aufheben!";
                }
            }
            return s;
        }

        public string DropOb(string obname)
        {
            Thing t = _player.Things.ThisOb(obname);
            string s = "";

            if (t == null)
            {
                s = "Das hast du nicht in deinem Besitz!";
            }
            else
            {
                TransferOb(t, _player.Things, _player.Location.Things);
                s = t.Name + " abgelegt!";
            }
            return s;
        }

        public string LookAtOb(string obname)
        {
            Thing t;
            string s = "";
            if(obname == "")
            {
                s = "Du musst mir mitteilen was du dir ansehen willst!";
            } else
            {
                t = _player.Location.Things.ThisOb(obname);
                if(t == null)
                {
                    t = _player.Things.ThisOb(obname);
                }
                if(t == null)
                {
                    s = "Es gibt hier kein(e) " + obname + "!";
                } else
                {
                    s = t.Description + ".";
                }
            }
            return s;
        }
    }
}
