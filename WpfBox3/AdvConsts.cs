using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBox3
{
    /// <summary>
    /// Die AdventureConstants beinhaltet Klassen die
    /// Konstanten für die globale nutzung im Projekt definiert werden.
    /// </summary>
    
    /*
     * Dir ist ein Enum für die Himmelsrichtungen.
     * Die Eventhandler geben die Himmelsrichtung weiter
     * an Adventure.cs Methode MoveTo die dies auswertet.
     */
    public enum Dir
    {
        NORDEN,
        SUEDEN,
        OSTEN,
        WESTEN,
        HOCH,
        RUNTER
    }

    /*
     * Ein Enum das die Keys-Definiert die in einem
     * Dictionary verwendet werden die in Adventure.cs
     * im Konstruktor angelegt werden.
     */
    public enum Rm
    {
        TrollRaum,
        Wald,
        Höhle,
        Verlies,
        NOEXIT
    }
}
