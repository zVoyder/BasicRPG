using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


namespace BasicRPG.Music
{
    /// Console.Beep(262, 250); //Do - C
    /// Console.Beep(294, 250); //Re - D
    /// Console.Beep(330, 250); //Mi - E
    /// Console.Beep(349, 250); //Fa - F
    /// Console.Beep(392, 250); //Sol - G
    /// Console.Beep(440, 250); //La - A
    /// Console.Beep(493, 250); //Si - B

    public enum NoteNames
    {
        DO,
        DOD,
        REB,
        RE,
        MIB,
        MI,
        MID,
        FA,
        FAD,
        SOLB,
        SOL,
        SOLD,
        LAB,
        LA,
        SIB,
        SI,
        REST
    }

    class MusicNote
    {
        const double 
            C = 16.35, // DO
            Db = 17.32, // Reb
            D = 18.35, // Re
            Eb = 19.45, // Mib
            E = 20.60, // Mi
            F = 21.83, // Fa
            Gb = 23.12, //Solb
            G = 24.5, // Sol
            Ab = 25.96, // Lab
            A = 27.5, // La
            Bb = 29.14, // Sib
            B = 30.87; // Si

        NoteNames note;
        int octave;
        int duration;

        public MusicNote(NoteNames note, int octave=4, int duration=250)
        {
            this.note = note;
            this.octave = octave;
            this.duration = duration;
        }

        public void Play()
        {
            PlayNote(note, octave, duration);
        }
        
        public static void Do(int duration = 250, int octave = 4)
        {
            Console.Beep(ConvertToNoteOctave(C, octave), duration);
        }

        public static void Reb(int duration = 250, int octave = 4)
        {
            Console.Beep(ConvertToNoteOctave(Db, octave), duration);
        }

        public static void Re(int duration = 250, int octave = 4)
        {
            Console.Beep(ConvertToNoteOctave(D, octave), duration);
        }

        public static void Mib(int duration = 250, int octave = 4)
        {
            Console.Beep(ConvertToNoteOctave(Eb, octave), duration);
        }

        public static void Mi(int duration = 250, int octave = 4)
        {
            Console.Beep(ConvertToNoteOctave(E, octave), duration);
        }

        public static void Fa(int duration = 250, int octave = 4)
        {
            Console.Beep(ConvertToNoteOctave(F, octave), duration);
        }

        public static void Solb(int duration = 250, int octave = 4)
        {
            Console.Beep(ConvertToNoteOctave(Gb, octave), duration);
        }

        public static void Sol(int duration = 250, int octave = 4)
        {
            Console.Beep(ConvertToNoteOctave(G, octave), duration);
        }

        public static void Lab(int duration = 250, int octave = 4)
        {
            Console.Beep(ConvertToNoteOctave(Ab, octave), duration);
        }

        public static void La(int duration = 250, int octave = 4)
        {
            Console.Beep(ConvertToNoteOctave(A, octave), duration);
        }

        public static void Sib(int duration = 250, int octave = 4)
        {
            Console.Beep(ConvertToNoteOctave(Bb, octave), duration);
        }

        public static void Si(int duration = 250, int octave = 4)
        {
            Console.Beep(ConvertToNoteOctave(B, octave), duration);
        }

        public static void Rest(int duration = 250)
        {
            Thread.Sleep(duration);
        }

        public static void PlayNote(NoteNames note, int octave = 4, int duration = 250)
        {
            switch (note)
            {
                case NoteNames.DO:
                    Do(duration, octave);
                    break;

                case NoteNames.REB:
                case NoteNames.DOD:
                    Reb(duration, octave);
                    break;

                case NoteNames.RE:
                    Re(duration, octave);
                    break;

                case NoteNames.MIB:
                    Mib(duration, octave);
                    break;

                case NoteNames.MI:
                    Mi(duration, octave);
                    break;

                case NoteNames.FA:
                case NoteNames.MID:
                    Fa(duration, octave);
                    break;

                case NoteNames.FAD:
                case NoteNames.SOLB:
                    Solb(duration, octave);
                    break;

                case NoteNames.SOL:
                    Sol(duration, octave);
                    break;

                case NoteNames.LAB:
                case NoteNames.SOLD:
                    Lab(duration, octave);
                    break;

                case NoteNames.LA:
                    La(duration, octave);
                    break;

                case NoteNames.SIB:
                    Sib(duration, octave);
                    break;

                case NoteNames.SI:
                    Si(duration, octave);
                    break;

                case NoteNames.REST:
                    Rest(duration);
                    break;
            }
        }

        public static int ConvertToNoteOctave(double frequency, int octave = 4)
        {
            return (int)(frequency * Math.Pow(2, octave));
        }
    }
}
