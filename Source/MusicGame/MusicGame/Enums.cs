using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame
{
    //creo los enums correspondientes (valores que elementos que variables pueden tomar, o sea que por ejemplo una variable de tipo "ChordType" solo podría tomar los valores enumerados en ChordType). Para leer más comentarios de esto y sobre cómo se relacionan con los Dictionaries correspondientes, leer lo que comenté en la class "Dictionaries".
    //hago a los enums public porque voy a querer hacer public a variables que creo con estos enums como tipo.

    public enum BangCategory  //Categorías de los bangs, que las por ahora no las uso para nadapero me parece que está bueno tener.
    {
        Dialog = 0, //por ejemplo para el aboutDialog
        ChordPlaying = 1, //de los bangs relacionados con tocar acordes
    }
    public enum CheckboxCategory //Categorías de los checkboxes, que las por ahora no las uso para nadapero me parece que está bueno tener.
    {
        AudioToggle = 0, //del checkbox que habilita o deshabilita el tipo de audio
        ChordType = 1, //de los checkboxes de tipos de acordes
        ChordInversion = 2, //de los checkboxes de inversiones de acordes
    }

    public enum ChordType
    {
        Major = 0,
        Minor = 1,
        Diminished = 2,
        Augmented = 3,
        DominantSeventh = 4,
        MinorSeventh = 5,
        HalfDiminishedSeventh = 6,
        MajorSeventh = 7,
        MinorMajorSeventh = 8,
        AugmentedMajorSeventh = 9,
        DiminishedSeventh = 10,
        MajorNinth = 11,
        MinorNinth = 12,
        MajorSixNine = 13,
        DominantNinth = 14,
        DominantMinorNinth = 15,
        DominantSeventhFlatFive = 16,
        DominantSeventhSharpFive = 17,
        SusFour = 18,
        SusTwo = 19,
        SevenSusFour = 20,
        SevenSusTwo = 21,
        NoThird = 22,
    }

    public enum ChordInversion
    {
        RootPosition = 0,
        FirstInversion = 1,
        SecondInversion = 2,
        ThirdInversion = 3,
    }


    public enum Voice //las voces posibles, lo uso para cuando arpegio
    {
        None = 0,
        Bass = 1,
        Tenor = 2,
        Alto = 3,
        Soprano = 4,
    }
}

