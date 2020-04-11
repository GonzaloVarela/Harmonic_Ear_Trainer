using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba1
{
    //creo los enums correspondientes (valores que elementos que variables pueden tomar, o sea que por ejemplo una variable de tipo "ChordType" solo podría tomar los valores enumerados en ChordType). Para leer más comentarios de esto y sobre cómo se relacionan con los Dictionaries correspondientes, leer lo que comenté en la class "Dictionaries".
    //hago a los enums public porque voy a querer hacer public a variables que creo con estos enums como tipo.
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
        DominantSeventhFlatFive = 11,
        DominantSeventhSharpFive = 12,
        DominantNinth = 13,
        DominantMinorNinth = 14,
        MajorNinth = 15,
        MinorNinth = 16,
        MajorSixNine = 17,
        SusFour = 18,
        SusTwo = 19,
        SevenSusFour = 20,
        SevenSusTwo = 21,
        PowerChord = 22,
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

