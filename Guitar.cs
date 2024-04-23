using ScaleTool;
using System;
using System.Collections.Generic;

namespace KeyComparison
{
    internal class Guitar
    {
        private List<string> CommonScaleNotes { get; set; }
        private List<(string StringName, List<string> FretNotes)> GuitarStringNotes = new List<(string, List<string>)>()
        {
            ("E", new List<string>()),
            ("B", new List<string>()),
            ("G", new List<string>()),
            ("D", new List<string>()),
            ("A", new List<string>()),
            ("E", new List<string>())
        };

        public Guitar(List<string> newCommonScaleNotes)
        {
            CommonScaleNotes = newCommonScaleNotes;

            // Build guitar string notes
            for (int gs = 0; gs < GuitarStringNotes.Count; gs++)
            {
                int startingIndex = Program.Notes.IndexOf(GuitarStringNotes[gs].StringName);
                for (int fret = 0; fret <= 12; fret++)
                {
                    string note = Program.Notes[(startingIndex + fret) % Program.Notes.Count];
                    GuitarStringNotes[gs].FretNotes.Add(note);
                }

                // Display guitar string
                DisplayGuitarString(gs);
            }

            // Display fret key
            Console.Write("\n    "); ;
            for (int fretNum = 1; fretNum <= 12; fretNum++)
            {
                string fretKey = $"    {(fretNum >= 10 ? fretNum.ToString() : fretNum.ToString() + " ")}  ";
                Console.Write(fretKey);
            }
            Console.Write("\n");
        }

        private void DisplayGuitarString(int stringIndex)
        {
            string nutDesign = $" {GuitarStringNotes[stringIndex].FretNotes[0]} |";
            Console.Write(nutDesign);

            for (int n = 1; n < GuitarStringNotes[stringIndex].FretNotes.Count; n++)
            {
                string noteName = GuitarStringNotes[stringIndex].FretNotes[n];
                if (!CommonScaleNotes.Contains(noteName))
                {
                    noteName = "-";
                }
                string fretDesign = $"|---{(noteName.Length == 2 ? noteName : noteName + "-")}--";
                Console.Write(fretDesign);
            }

            Console.Write("\n");
        }
    }
}
