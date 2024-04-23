using KeyComparison;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScaleTool
{
    internal class Program
    {
        public static List<string> Notes = new List<string>()
        {
            "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#"
        };
        static void Main(string[] args)
        {
            Console.WriteLine("Key Comparison Tool by Joseph Mecham\n");

            // To be populated in the main loop
            List<List<string>> keyScaleNotes = new List<List<string>>();
            List<string> keyNames = new List<string>();

            // Main loop
            for (int k = 1; k <= 2; k++)
            {
                // Prompt user for key tonic
                string tonic;
                do
                {
                    Console.Write($"Key #{k} Tonic? ");
                    tonic = Console.ReadLine().ToUpper();
                } while (Notes.IndexOf(tonic) == -1);

                // Prompt use for key mode
                Key.ModeKeys modeKey;
                do
                {
                    Console.WriteLine("\nMode Options:");
                    Console.WriteLine("(I)onian / Major\n(D)orian\n(P)hrygian\n(Ly)dian\n(M)ixolydian\n(A)eolian / Natural Minor\n(Lo)crian\n");
                    Console.Write($"Key #{k} Mode? ");
                } while (!Enum.TryParse(Console.ReadLine().ToLower(), out modeKey));

                // Create new key instance
                Key newKey = new Key(tonic, modeKey);

                // Add scale notes to keyScaleNotes and add to keyNames
                keyScaleNotes.Add(newKey.ScaleNotes.ToList());
                keyNames.Add($"{tonic} {newKey.Mode}");

                // Display key information
                Console.WriteLine($"\n{tonic} {newKey.Mode}");
                Console.WriteLine("================================");

                Console.Write("Scale Notes: ");
                for (int i = 0; i < newKey.ScaleNotes.Count; i++)
                {
                    Console.Write($"{newKey.ScaleNotes[i]}");
                    if (i < newKey.ScaleNotes.Count - 1)
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("\n");
                    }
                }

                Console.Write("Scale Chords: ");
                for (int i = 0; i < newKey.ScaleNotes.Count; i++)
                {
                    Console.Write($"{newKey.ScaleNotes[i]}{newKey.ChordPattern[i]}");
                    if (i < newKey.ScaleNotes.Count - 1)
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("\n");
                    }
                }

                if ((int)modeKey != 0)
                {
                    Console.WriteLine($"Relative Major: {newKey.RelativeMajorNotes[0]}");
                }

                if ((int)modeKey != 5)
                {
                    Console.WriteLine($"Relative Minor: {newKey.RelativeMinorNotes[0]}");
                }

                Console.Write("\n");
            }

            // Calculate common scale notes
            List<string> commonScaleNotes = new List<string>();
            for (int i = 0; i < keyScaleNotes[0].Count; i++)
            {
                if (keyScaleNotes[1].Contains(keyScaleNotes[0][i]))
                {
                    commonScaleNotes.Add(keyScaleNotes[0][i]);
                }
            }

            // Display common scale notes
            Console.Write($"{keyNames[0]} and {keyNames[1]} Common Scale Notes: ");
            for (int i = 0; i < commonScaleNotes.Count; i++)
            {
                Console.Write($"{commonScaleNotes[i]} ");
            }
            Console.Write("\n\n");

            // Display guitar layout with common scale notes
            Guitar myGuitar = new Guitar(commonScaleNotes);

            // Prompt user to exit
            Console.WriteLine("\nPress enter to exit...");
            Console.Read();
        }
    }
}
