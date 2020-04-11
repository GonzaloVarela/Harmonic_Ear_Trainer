using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame
{
    class Dictionaries
    {
        //para manejar los tipos de acorde creo enums y dictionaries.
        //los enums determinan los distintos valores que una variable a la que le asigno ese tipo puede tomar (por ejemplo, cada acorde puede estar ser mayor o menor, y estar en posición fundamental o 1era inv), y pueden tomar distintos valores para cada instancia de Chord (porque puedo querer tener en la memoria dos acordes, uno mayor y otro menor)
        //los dictionaries mapean los índices de los enums a strings, para poder mostrarlos en el juego (mientras que el enum del tipo de acorde me dice que hay un tipo de índice 4 llamado "MajorSeventh", el dictionary correspondiente me va a ayudar para que siempre que quiera imprimir ese índice 4 se imprima "Major 7th".

        public static Dictionary<int, string> chordType = new Dictionary<int, String>();
        public static Dictionary<int, string> chordInversion = new Dictionary<int, String>();
        public static Dictionary<int, string> pitchClass = new Dictionary<int, String>();
        public static Dictionary<int, string> pitchMidi = new Dictionary<int, String>();


        public static void CreateDictionaries()
        {
            chordType.Add(0, "Major");
            chordType.Add(1, "Minor");
            chordType.Add(2, "Diminished");
            chordType.Add(3, "Augmented");
            chordType.Add(4, "Dominant seventh");
            chordType.Add(5, "Minor Seventh");
            chordType.Add(6, "Half-diminished seventh");
            chordType.Add(7, "Major seventh");
            chordType.Add(8, "Minor major seventh");
            chordType.Add(9, "Augmented major seventh");
            chordType.Add(10, "Diminished seventh");
            chordType.Add(11, "Dominant Seventh flat five (7b5)");
            chordType.Add(12, "Dominant Seventh sharp five (7#5)");
            chordType.Add(13, "Dominant ninth");
            chordType.Add(14, "Dominant minor ninth");
            chordType.Add(15, "Major ninth");
            chordType.Add(16, "Minor ninth");
            chordType.Add(17, "Major 6/9");
            chordType.Add(18, "Sus4");
            chordType.Add(19, "Sus2");
            chordType.Add(20, "7 sus4");
            chordType.Add(21, "7 sus2");
            chordType.Add(22, "Power Chord");

            chordInversion.Add(0, "Root position");
            chordInversion.Add(1, "First inversion");
            chordInversion.Add(2, "Second inversion");
            chordInversion.Add(3, "Third inversion");

            pitchClass.Add(0, "C");
            pitchClass.Add(1, "C#");
            pitchClass.Add(2, "D");
            pitchClass.Add(3, "D#");
            pitchClass.Add(4, "E");
            pitchClass.Add(5, "F");
            pitchClass.Add(6, "F#");
            pitchClass.Add(7, "G");
            pitchClass.Add(8, "G#");
            pitchClass.Add(9, "A");
            pitchClass.Add(10, "A#");
            pitchClass.Add(11, "B");


            pitchMidi.Add(0, "C-1");
            pitchMidi.Add(1, "C#-1");
            pitchMidi.Add(2, "D-1");
            pitchMidi.Add(3, "D#-1");
            pitchMidi.Add(4, "E-1");
            pitchMidi.Add(5, "F-1");
            pitchMidi.Add(6, "F#-1");
            pitchMidi.Add(7, "G-1");
            pitchMidi.Add(8, "G#-1");
            pitchMidi.Add(9, "A-1");
            pitchMidi.Add(10, "A#-1");
            pitchMidi.Add(11, "B-1");
            pitchMidi.Add(12, "C0");
            pitchMidi.Add(13, "C#0");
            pitchMidi.Add(14, "D0");
            pitchMidi.Add(15, "D#0");
            pitchMidi.Add(16, "E0");
            pitchMidi.Add(17, "F0");
            pitchMidi.Add(18, "F#0");
            pitchMidi.Add(19, "G0");
            pitchMidi.Add(20, "G#0");
            pitchMidi.Add(21, "A0");
            pitchMidi.Add(22, "A#0");
            pitchMidi.Add(23, "B0");
            pitchMidi.Add(24, "C1");
            pitchMidi.Add(25, "C#1");
            pitchMidi.Add(26, "D1");
            pitchMidi.Add(27, "D#1");
            pitchMidi.Add(28, "E1");
            pitchMidi.Add(29, "F1");
            pitchMidi.Add(30, "F#1");
            pitchMidi.Add(31, "G1");
            pitchMidi.Add(32, "G#1");
            pitchMidi.Add(33, "A1");
            pitchMidi.Add(34, "A#1");
            pitchMidi.Add(35, "B1");
            pitchMidi.Add(36, "C2");
            pitchMidi.Add(37, "C#2");
            pitchMidi.Add(38, "D2");
            pitchMidi.Add(39, "D#2");
            pitchMidi.Add(40, "E2");
            pitchMidi.Add(41, "F2");
            pitchMidi.Add(42, "F#2");
            pitchMidi.Add(43, "G2");
            pitchMidi.Add(44, "G#2");
            pitchMidi.Add(45, "A2");
            pitchMidi.Add(46, "A#2");
            pitchMidi.Add(47, "B2");
            pitchMidi.Add(48, "C3");
            pitchMidi.Add(49, "C#3");
            pitchMidi.Add(50, "D3");
            pitchMidi.Add(51, "D#3");
            pitchMidi.Add(52, "E3");
            pitchMidi.Add(53, "F3");
            pitchMidi.Add(54, "F#3");
            pitchMidi.Add(55, "G3");
            pitchMidi.Add(56, "G#3");
            pitchMidi.Add(57, "A3");
            pitchMidi.Add(58, "A#3");
            pitchMidi.Add(59, "B3");
            pitchMidi.Add(60, "C4");
            pitchMidi.Add(61, "C#4");
            pitchMidi.Add(62, "D4");
            pitchMidi.Add(63, "D#4");
            pitchMidi.Add(64, "E4");
            pitchMidi.Add(65, "F4");
            pitchMidi.Add(66, "F#4");
            pitchMidi.Add(67, "G4");
            pitchMidi.Add(68, "G#4");
            pitchMidi.Add(69, "A4");
            pitchMidi.Add(70, "A#4");
            pitchMidi.Add(71, "B4");
            pitchMidi.Add(72, "C5");
            pitchMidi.Add(73, "C#5");
            pitchMidi.Add(74, "D5");
            pitchMidi.Add(75, "D#5");
            pitchMidi.Add(76, "E5");
            pitchMidi.Add(77, "F5");
            pitchMidi.Add(78, "F#5");
            pitchMidi.Add(79, "G5");
            pitchMidi.Add(80, "G#5");
            pitchMidi.Add(81, "A5");
            pitchMidi.Add(82, "A#5");
            pitchMidi.Add(83, "B5");
            pitchMidi.Add(84, "C6");
            pitchMidi.Add(85, "C#6");
            pitchMidi.Add(86, "D6");
            pitchMidi.Add(87, "D#6");
            pitchMidi.Add(88, "E6");
            pitchMidi.Add(89, "F6");
            pitchMidi.Add(90, "F#6");
            pitchMidi.Add(91, "G6");
            pitchMidi.Add(92, "G#6");
            pitchMidi.Add(93, "A6");
            pitchMidi.Add(94, "A#6");
            pitchMidi.Add(95, "B6");
            pitchMidi.Add(96, "C7");
            pitchMidi.Add(97, "C#7");
            pitchMidi.Add(98, "D7");
            pitchMidi.Add(99, "D#7");
            pitchMidi.Add(100, "E7");
            pitchMidi.Add(101, "F7");
            pitchMidi.Add(102, "F#7");
            pitchMidi.Add(103, "G7");
            pitchMidi.Add(104, "G#7");
            pitchMidi.Add(105, "A7");
            pitchMidi.Add(106, "A#7");
            pitchMidi.Add(107, "B7");
            pitchMidi.Add(108, "C8");
            pitchMidi.Add(109, "C#8");
            pitchMidi.Add(110, "D8");
            pitchMidi.Add(111, "D#8");
            pitchMidi.Add(112, "E8");
            pitchMidi.Add(113, "F8");
            pitchMidi.Add(114, "F#8");
            pitchMidi.Add(115, "G8");
            pitchMidi.Add(116, "G#8");
            pitchMidi.Add(117, "A8");
            pitchMidi.Add(118, "A#8");
            pitchMidi.Add(119, "B8");
            pitchMidi.Add(120, "C9");
            pitchMidi.Add(121, "C#9");
            pitchMidi.Add(122, "D9");
            pitchMidi.Add(123, "D#9");
            pitchMidi.Add(124, "E9");
            pitchMidi.Add(125, "F9");
            pitchMidi.Add(126, "F#9");
            pitchMidi.Add(127, "G9");
        }
        
    }
}
