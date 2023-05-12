using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBox3
{
    /*
     * Teil (partial) der Klasse Adventure. Hinzufügen -> Klasse -> [Hauptklassenname].[Teilklassenname].cs
     * Wichtig ist die Dateiendung mit zu schreiben damit die Datei als Klasse erkannt wird.
     */
    partial class Adventure
    {
        // =================================================================================================
        //                                     --- Parser ---
        //==================================================================================================

        Dictionary<string, WT> vocab = new Dictionary<string, WT>();

        private void InitVocab()
        {
            vocab.Add("acron", WT.NOMEN);
            vocab.Add("Bett", WT.NOMEN);
            vocab.Add("Behälter", WT.NOMEN);
            vocab.Add("Knochen", WT.NOMEN);
            vocab.Add("Knopf", WT.NOMEN);
            vocab.Add("Käse", WT.NOMEN);
            vocab.Add("Kiste", WT.NOMEN);
            vocab.Add("Münze", WT.NOMEN);
            vocab.Add("Tür", WT.NOMEN);
            vocab.Add("Staub", WT.NOMEN);
            vocab.Add("Gardenie", WT.NOMEN);
            vocab.Add("Schlüssel", WT.NOMEN);
            vocab.Add("Messer", WT.NOMEN);
            vocab.Add("Lampe", WT.NOMEN);
            vocab.Add("Flugblatt", WT.NOMEN);
            vocab.Add("Hebel", WT.NOMEN);
            vocab.Add("Perle", WT.NOMEN);
            vocab.Add("Ratte", WT.NOMEN);
            vocab.Add("Sack", WT.NOMEN);
            vocab.Add("Laden", WT.NOMEN);
            vocab.Add("Schild", WT.NOMEN);
            vocab.Add("Steckplatz", WT.NOMEN);
            vocab.Add("Eichhörnchen", WT.NOMEN);
            vocab.Add("nehme", WT.VERB);
            vocab.Add("ablegen", WT.VERB);
            vocab.Add("lege", WT.VERB);
            vocab.Add("ansehen", WT.VERB);
            vocab.Add("öffne", WT.VERB);
            vocab.Add("schließe", WT.VERB);
            vocab.Add("ziehe", WT.VERB);
            vocab.Add("drücke", WT.VERB);
            vocab.Add("n", WT.VERB);
            vocab.Add("s", WT.VERB);
            vocab.Add("w", WT.VERB);
            vocab.Add("o", WT.VERB);
            vocab.Add("rauf", WT.VERB);
            vocab.Add("runter", WT.VERB);
            vocab.Add("ein", WT.ARTIKEL);
            vocab.Add("eine", WT.ARTIKEL);
            vocab.Add("der", WT.ARTIKEL);
            vocab.Add("die", WT.ARTIKEL);
            vocab.Add("das", WT.ARTIKEL);
            vocab.Add("in", WT.PRÄPOSITION);
            vocab.Add("hinein", WT.PRÄPOSITION);
            vocab.Add("bei", WT.PRÄPOSITION);
            vocab.Add("an", WT.PRÄPOSITION);
            vocab.Add("auf", WT.PRÄPOSITION);
        }

        public string RunCommand(string inputstr)
        {
            char[] delims = { ' ', '.' };
            List<string> strlist;
            string s = "";

            string lowstr = inputstr.Trim().ToLower();
            if(lowstr == "")
            {
                s = "Du musst einen Befehl eingeben";
            } else
            {
                strlist = new List<string> (inputstr.Split(delims, StringSplitOptions.RemoveEmptyEntries));
                s = ParseCommand(strlist);
            }
            return s;
        }
        
        private string ParseCommand(List<string> wordlist)
        {
            return "---ausgeführt---";
        }
    }
}
