using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            vocab.Add("Wurst", WT.NOMEN);
            vocab.Add("Karotte", WT.NOMEN);
            vocab.Add("Baum", WT.NOMEN);

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
            vocab.Add("nehmen", WT.VERB);
            vocab.Add("nimm", WT.VERB);
            
            vocab.Add("ablegen", WT.VERB);
            vocab.Add("lege", WT.VERB);
            vocab.Add("ansehen", WT.VERB);
            vocab.Add("umsehen", WT.VERB);
            vocab.Add("öffne", WT.VERB);
            vocab.Add("schließe", WT.VERB);
            vocab.Add("ziehe", WT.VERB);
            vocab.Add("drücke", WT.VERB);
            vocab.Add("n", WT.VERB);
            vocab.Add("s", WT.VERB);
            vocab.Add("w", WT.VERB);
            vocab.Add("o", WT.VERB);
            vocab.Add("rauf", WT.VERB);
            vocab.Add("hoch", WT.VERB);
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

        private string ProcessVerbNounPrepositionNoun(List<WordAndType> command)
        {
            WordAndType wt1 = command[0];
            WordAndType wt2 = command[1];
            WordAndType wt3 = command[2];
            WordAndType wt4 = command[3];
            string s = "";
            if((wt1.Type != WT.VERB) || (wt3.Type != WT.PRÄPOSITION)) 
            {
                s = $"Ich weiss nicht wie ich {wt1.Word} mit {wt3.Word} anwenden soll!";
            } else if (wt2.Type != WT.NOMEN)
            {
                s = $"Das kann ich nicht machen, da {wt2.Word} kein Objekt ist!";
            } else if (wt4.Type != WT.NOMEN)
            {
                s = $"Das kann ich nicht machen, da {wt4.Word} kein Objekt ist!";
            } else
            {
                switch(wt1.Word + wt2.Word)
                {
                    case "legehinein":
                    case "legeauf":
                        s = PutObInContainer(wt2.Word, wt3.Word);
                        break;
                    default:
                        s = $"Ich weiss nicht wie ich folgendes machen soll: {wt1.Word} {wt2.Word} {wt3.Word} {wt4.Word} ";
                        break;
                }
            }
            return s;
        }

        private string ProcessVerbPrepositionNoun(List<WordAndType> command)
        {
            WordAndType wt = command[0];
            WordAndType wt2 = command[1];
            WordAndType wt3 = command[2];
            string s = "";
            if ((wt.Type != WT.VERB) || (wt2.Type != WT.PRÄPOSITION))
            {
                s = $"Ich weiss nicht wie ich {wt.Word} mit {wt2.Word} anwenden soll!";
            }
            else if (wt3.Type != WT.NOMEN)
            {
                s = $"Das kann ich nicht machen, da {wt3.Word} kein Objekt ist!";
            }
            else
            {
                switch (wt.Word + wt2.Word)
                {
                    case "ansehen":
                        s = LookAtOb(wt3.Word);
                        break;
                    default:
                        s = $"Ich weiss nicht wie ich folgendes machen soll: {wt.Word} {wt2.Word} {wt3.Word} ";
                        break;
                }
            }
            return s;
        }

        private string ProcessVerbNoun(List<WordAndType> command)
        {
            WordAndType wt = command[0];
            WordAndType wt2 = command[1];

            /*
             * Im deutschen kann das Verb vor dem nomen kommen. Öffne Tür oder Tür ansehen.
             * Daher muss hier eventuell getauscht werden.
             */
            if ((wt.Type != WT.VERB) && (wt2.Type != WT.NOMEN))
            {
                wt = command[1];
                wt2 = command[0];
            }
            
            string s = "";
            if ((wt.Type != WT.VERB))
            {
                s = $"Ich kann {wt.Word} nicht ausführen, da es kein Befehl ist!";
            }
            else if (wt2.Type != WT.NOMEN)
            {
                s = $"Das kann ich nicht machen da {wt2.Word} kein Objekt ist!";
            }
            else
            {
                switch (wt.Word)
                {
                    case "nehmen":
                        s = TakeOb(wt2.Word);
                        break;
                    case "nehme":
                        s = TakeOb(wt2.Word);
                        break;
                    case "ansehen":
                        s = LookAtOb(wt2.Word);
                        break;
                    case "nimm":
                        s = TakeOb(wt2.Word);
                        break;
                    case "ablegen":
                        s = DropOb(wt2.Word);
                        break;
                    case "öffne":
                        s = OpenOb(wt2.Word); // Nicht implementiert
                        break;
                    case "schließe":
                        s = CloseOb(wt2.Word); // Nicht implementiert
                        break;
                    case "ziehe":
                        s = PullOb(wt2.Word); // Nicht implementiert
                        break;
                    case "drücke":
                        s = PushOb(wt2.Word); // Nicht implementiert
                        break;
                    default:
                        s = $"Ich weiss nicht wie ich folgendes machen soll: {wt.Word} {wt2.Word}";
                        break;
                }
            }
            return s;
        }

        private string ProcessVerb(List<WordAndType> command)
        {
            WordAndType wt = command[0];
            string s = "";
            if (wt.Type != WT.VERB)
            {
                s = $"Ich kann {wt.Word} nicht ausführen, da es kein Befehl ist!";
            }
            else
            {
                switch (wt.Word)
                {
                    case "umsehen":
                        s = Look();
                        break;
                    case "n":
                        s = MovePlayerTo(Dir.NORDEN);
                        break;
                    case "s":
                        s = MovePlayerTo(Dir.SUEDEN);
                        break;
                    case "w":
                        s = MovePlayerTo(Dir.WESTEN);
                        break;
                    case "o":
                        s = MovePlayerTo(Dir.OSTEN);
                        break;
                    case "hoch":
                        s = MovePlayerTo(Dir.HOCH);
                        break;
                    case "rauf":
                        s = MovePlayerTo(Dir.HOCH);
                        break;
                    case "runter":
                        s = MovePlayerTo(Dir.RUNTER);
                        break;
                    default:
                        s = $"Ich weiss nicht wie ich folgendes machen soll: {wt.Word}";
                        break;
                }
            }
            return s;
        }

        private string ProcessCommand(List<WordAndType> command)
        {
            string s = "";
            if (command.Count == 0)
            {
                s = $"Sie müssen eine Anweisung eingeben!";
            }
            else if (command.Count > 4)
            {
                s = $"Die Anweisung ist zu lang!";
            } else
            {
                s = $"Die Anweisung wird verarbeitet.";

                switch (command.Count)
                {
                    case 1:
                        s = ProcessVerb(command);
                        break;
                    case 2:
                        s = ProcessVerbNoun(command);
                        break;
                    case 3:
                        s = ProcessVerbPrepositionNoun(command);
                        break;
                    case 4:
                        s = ProcessVerbNounPrepositionNoun(command);
                        break;
                    default:
                        s = $"Ich konnte die Anweisung nicht verarbeiten!";
                        break;
                }
            }
            return s;
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
            List<WordAndType> command = new List<WordAndType>();
            WT wordtype;
            string errmsg = "";
            string s = "";

            foreach (string k in wordlist)
            {
                if (vocab.ContainsKey(k))
                {
                    wordtype = vocab[k];
                    if(wordtype == WT.ARTIKEL)
                    {
                        // Ignoriere Artikel wie der, die, das oder ein
                    } else
                    {
                        command.Add(new WordAndType(k, wordtype));
                    }
                } else
                {
                    command.Add(new WordAndType(k, WT.ERROR));
                    errmsg = $"Entschuldigung, ich weiss nicht was mit: '{k}' gemeint ist.";
                }
            }
            if(errmsg != "")
            {
                s = errmsg;
            } else
            {
                s = ProcessCommand(command);
            }
            return s;
        }
    }
}
