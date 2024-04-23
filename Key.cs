using System.Collections.Generic;
using System.Linq;

namespace ScaleTool
{
    internal class Key
    {
        public enum ModeKeys
        {
            i = 0,
            ionian = 0,
            major = 0,
            d = 1,
            dorian = 1,
            p = 2,
            phrygian = 2,
            ly = 3,
            lydian = 3,
            m = 4,
            mixolydian = 4,
            a = 5,
            aeolian = 5,
            minor = 5,
            lo = 6,
            locrian = 6,
        }

        public static List<(string Mode, List<int> Intervals, List<string> ChordPattern)> Modes = new List<(string, List<int>, List<string>)>
        {
            ("Ionian", new List<int> { 2, 2, 1, 2, 2, 2, 1 }, new List<string> { "", "m", "m", "", "", "m", "dim" }),
            ("Dorian", new List<int> { 2, 1, 2, 2, 2, 1, 2 }, new List<string> { "m", "m", "", "", "m", "dim", "" }),
            ("Phrygian", new List<int> { 1, 2, 2, 2, 1, 2, 2 }, new List<string> { "m", "", "", "m", "dim", "", "m" }),
            ("Lydian", new List<int> { 2, 2, 2, 1, 2, 2, 1 }, new List<string> { "", "", "m", "dim", "", "m", "m" }),
            ("Mixolydian", new List<int> { 2, 2, 1, 2, 2, 1, 2 }, new List<string> { "", "m", "dim", "", "m", "m", "" }),
            ("Aeolian", new List<int> { 2, 1, 2, 2, 1, 2, 2 }, new List<string> { "m", "dim", "", "m", "m", "", "" }),
            ("Locrian", new List<int> { 1, 2, 2, 1, 2, 2, 2 }, new List<string> { "dim", "", "m", "m", "", "", "m" })
        };

        public string Tonic { get; set; }
        public string Mode { get; set; }
        public List<int> Intervals { get; set; }
        public List<string> ChordPattern { get; set; }
        public List<string> ScaleNotes { get; set; }
        public List<string> RelativeMajorNotes { get; set; }
        public List<string> RelativeMinorNotes { get; set; }
        public string Dominant { get; set; }
        public string Subdominant { get; set; }

        public Key(string newTonic, ModeKeys newModeKey)
        {
            Tonic = newTonic;
            Mode = Modes[(int)newModeKey].Mode;
            Intervals = Modes[(int)newModeKey].Intervals;
            ChordPattern = Modes[(int)newModeKey].ChordPattern;
            ScaleNotes = calcScaleNotes(Tonic, Intervals);
            RelativeMajorNotes = calcRelativeMajorNotes(ScaleNotes, newModeKey);
            RelativeMinorNotes = calcRelativeMinorNotes(ScaleNotes, newModeKey);
        }

        private List<string> calcScaleNotes(string tonic, List<int> numIntervals)
        {
            tonic = tonic.ToUpper();
            int noteIndex = Program.Notes.IndexOf(tonic);
            List<string> noteIntervals = new List<string>();

            for (int i = 0; i < numIntervals.Count; i++)
            {
                noteIntervals.Add(Program.Notes[noteIndex]);
                noteIndex = (noteIndex + numIntervals[i]) % Program.Notes.Count;
            }

            return noteIntervals;
        }

        private List<string> calcRelativeMajorNotes(List<string> scaleNotes, ModeKeys newModeKey)
        {
            int scaleDifference = (7 - (int)newModeKey) % 7;
            List<string> relativeMajorScaleNotes = scaleNotes.ToList();
            for (int i = 0; i < scaleDifference; i++)
            {
                relativeMajorScaleNotes.Add(relativeMajorScaleNotes[0]);
                relativeMajorScaleNotes.RemoveAt(0);
            }
            return relativeMajorScaleNotes;
        }

        private List<string> calcRelativeMinorNotes(List<string> scaleNotes, ModeKeys newModeKey)
        {
            int scaleDifference = (12 - (int)newModeKey) % 7;
            List<string> relativeMinorScaleNotes = scaleNotes.ToList();
            for (int i = 0; i < scaleDifference; i++)
            {
                relativeMinorScaleNotes.Add(relativeMinorScaleNotes[0]);
                relativeMinorScaleNotes.RemoveAt(0);
            }
            return relativeMinorScaleNotes;
        }
    }
}
