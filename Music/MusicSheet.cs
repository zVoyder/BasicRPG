using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Linq;
using System.Diagnostics;

namespace BasicRPG.Music
{
    /// https://musescore.com/user/11636/scores/123498 -> for the octaves

    public enum BpmTempo
    {
        Larghissimo = 20,
        Adagissimo = 24,
        Grave = 30,
        Largo = 40,
        Lento = 45,
        Larghetto = 60,
        Adagio = 66,
        Adagietto = 72,
        Andante = 76,
        Andantino = 80,
        Marcia = 83,
        Moderato = 108,
        Allegretto = 112,
        Allegro = 120,
        Vivace = 156,
        Vivacissimo = 172,
        Allegrissimo = 176,
        Presto = 200,
        Prestissimo = 210
    }

    class MusicSheet
    {
        string resourcename;
        BpmTempo tempo;
        float fraction;
        ManualResetEvent mrse;
        List<MusicNote> notes;

        public string ResourceName { get => resourcename; }
        public MusicSheet(string resourcename, BpmTempo tempo = BpmTempo.Moderato, float fraction = 4f / 4f)
        {
            this.resourcename = resourcename;
            this.tempo = tempo;
            this.fraction = fraction;
            mrse = new ManualResetEvent(true);
            notes = new List<MusicNote>();

            string[] notelns = Properties.Resources.ResourceManager.GetString(resourcename).Split("\n");

            foreach (string note in notelns.Where(note => note != "\r" && note != ""))
            {
                string[] fullNote = note.Split("_");

                notes.Add(
                    new MusicNote(
                    Enum.Parse<NoteNames>(fullNote[0].ToUpper()),
                    int.Parse(fullNote[1]),
                    BpmToBpms(tempo, Convert.ToInt32(fullNote[2]), fraction)
                    ));
            }
        }

       
        public void Play()
        {
            new Thread(() =>
            {
                for (int i = 0; i < notes.Count - 1 ; i++)
                {
                    notes[i].Play();
                }
            }).Start();
        }

        public void Loop()
        {
            new Thread( () =>
            {
                for (int i = 0; mrse.WaitOne(); i++)
                {
                    if (i > notes.Count - 1)
                        i = 0;

                    notes[i].Play();
                }
            }).Start();
            
        }

        public void Resume() => mrse.Set();
        public void Pause() => mrse.Reset();

        /// <summary>
        /// Convert a note duration in BPM to a note duration in Beat Per MilliSeconds (Useful for Console.Beep duration)
        /// </summary>
        /// <param name="bpm">The tempo (BPM)</param>
        /// <param name="noteduration">The note duration in Milliseconds</param>
        /// <param name="fraction">The fraction of the sheet 4/4, 3/4</param>
        /// <returns>Total duration in milliseconds</returns>
        private static int BpmToBpms(BpmTempo bpm, int noteduration, float fraction)
        {
            fraction *= 4f;
            return (int)(((float)(60f / (int)bpm)) * (float)noteduration * fraction);
        }
    }
}
