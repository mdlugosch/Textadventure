using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBox1.gameclasses
{
    public class Thing
    {
        public Thing() { }
        public Thing(string aName, string aDescription) 
        {
            _name = aName;
            _description = aDescription;
            _canTake = true;
        }

        public Thing(string aName, string aDescription, bool aCanTake)
        {
            _name = aName;
            _description = aDescription;
            _canTake = aCanTake;
        }

        private string _name; 
        private string _description;
        private bool _canTake;
        public string Name { 
            get => _name; 
            set => _name = value; 
        }

        public string Description {
            get => _description; 
            set => _description = value; 
        }

        public bool CanTake { 
            get => _canTake; 
            set => _canTake = value; 
        }

        /*
         * Describe() beschreibt ein Objekt dieser Klasse.
         * Die Methode besitzt den Modifikator virtual damit,
         * Describe() in allen abgeleiteten Klasse überschrieben
         * (override) werden kann. So kann über eine Liste aus Thing
         * Objekten iteriert werden in der sich abgeleitete Klassenobjekte
         * befinden. Jedes abgeleitete Klassenobjekt verfügt über eine
         * Describe() Methode die allerdings individuellen Code enthalten
         * kann. Dies macht sich dann bei der ausführung von Describe() bemerkbar.
         */
        public virtual string Describe()
        {
            return Name + " " + Description;
        }
    }
}
