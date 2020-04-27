using Microsoft.Xna.Framework; //Lo necesito para usar el GameTime
using Microsoft.Xna.Framework.Audio;
using System;
using System.Diagnostics;


namespace HarmonicEarTrainer
{

    public class ChordVoicing
    {

        public int noteRootPitchClass { get; set; }  //la hago public porque la voy a usar para poner en los textos de la respuesta.
        public ChordType type { get; set; } //creo una variable "chordType" de tipo "ChordType" (que es un enum)
        public ChordInversion inversion { get; set; } //creo una variable "chordInversion" de tipo "ChordInversion" (que es un enum)

        int doublingRandomization; //voy a usar esta variable cuando haga randomizaciones de qué nota se duplica en el acorde (si por ejemplo tengo dos opciones de doubling, voy a ponerle un número aleatorio entre 0 y 1 y elegir la duplicación dependiendo de qué número salga)

        //Cada acorde lo armo a 4 voces, que en un principio las llamo bass, highA, highB y highC, y en un principio no sé (no determino) el órden agudo-grave de las voces superiores. 
        //El bajo va a usar la octava 3, y las voces superiores van a usar las octavas 4 y 5 aleatoriamente (si hay duplicación en las voces superiores, una voz va a usar la octava 4 y la otra la 5).
        //Una vez que a las voces superiores les pongo alturas especificas, cargo las variables "tenor" "alto" y "soprano" con el valor de la voz high que corresponda a cada una, y voy a usar "bass", "tenor", "alto" y "soprano" para hacer los arpegios siempre de grave a agudo.

        private int _noteBassPitchClass;
        private int _noteBassOctave;
        public int noteBassPitchMidi; //la hago public porque la voy a usar para poner en los textos de la respuesta.

        private int _noteHighAPitchClass;
        private int _noteHighAOctave;
        private int _noteHighAPitchMidi;

        private int _noteHighBPitchClass;
        private int _noteHighBOctave;
        private int _noteHighBPitchMidi;

        private int _noteHighCPitchClass;
        private int _noteHighCOctave;
        private int _noteHighCPitchMidi;

        //es importante ver qué nota corresponde a qué "voz" para arpegiar los acordes siempre en orden Bass, Tenor, Alto, Soprano. Además, hago las variables public porque voy a usar sus valores para poner en los textos de la respuesta.
        public int noteTenorPitchMidi;
        public int noteAltoPitchMidi;
        public int noteSopranoPitchMidi;


        //cuando se crea una instancia de la clase no comienza agendada la reproducción de ninguna nota del arpegio (para agendarlas y reproducirlas le iré dando a esta variable distintos valores del enum)
        private Voice _voiceToPlay = Voice.None;


        private double _arpeggioTimer = 0;    //timer para agendar las reproducción de las notas del arpegio
        private double _arpeggioTimeBetweenNotes; // tiempo entre notas consecutivas del arpegio (varía depependiendo de la checkbox correspondiente).


        //array de bools para determinar qué chord types están habilitados
        public static bool[] isTypeEnabled = new bool[23];

        //array de bools para determinar qué inversions están habilitadas
        public static bool[] isInversionEnabled = new bool[4];

        // voy a usar estas variables para cargar a cada una el sonido de una voz, y antes de arrancar una reproducción poder detener la anterior.
        public SoundEffectInstance sfxInstanceSoprano;
        public SoundEffectInstance sfxInstanceAlto;
        public SoundEffectInstance sfxInstanceTenor;
        public SoundEffectInstance sfxInstanceBass;
        

        public void GenerateRandomVoicing()
        {
            _noteBassPitchClass = Main.randomize.Next(0, 12); //randomizo el bajo del acorde (a partir del cual voy a calcular todas las otras notas), que será un entero entre 0 y 11.
            _noteBassOctave = 3; //para el bajo voy a usar la octava 3 (luego tal vez hacer una opción para activar una duplicación del bajo a la octava inferior, es decir, la octava 2-).

            type = (ChordType)RandomizeTypeAmongEnabledTypes(); //llamo a la función que randomiza el tipo de acorde entre los tipos de acorde habilitados, y uso el valor que devuelve como índice para el enum correspondiente a esa variable (no puedo pasarle directamente el valor a la variable "chordType" porque la variable "chordType" no es de tipo "int" de tipo ChordType (el enum).

            DetermineHighNotes(); //calculo cuáles son las notas superiores del acorde en base a cuál es el bajo y cuál es el tipo de acorde.

            CalculateRootNotePitchClass(); // calculo cuál es el pitch class del rootnote (en relación al bajo, y tomando el cuenta el tipo de acorde y la inversión).

            AssignHighNotesToHighVoices(); // Analizo cuál de las notas superiores (HighA, HighB y HighC) es la más aguda (que se la asigno a la soprano), segunda más aguda (que se la asigno al alto) y más grave (que se la asigno al tenor).

        }


        private void DetermineHighNotes()  //calculo cuáles son las notas superiores del acorde en base a cuál es el bajo y cuál es el tipo de acorde.
        {
            switch (type)
            {
                case ChordType.Major:
                    inversion = (ChordInversion)RandomizeTriadInversionAmongEnabledInversions(); //llamo a la función que randomiza la inversión de una tríada entre las inversiones de tríada habilitadas (es decir, sin tomar en cuenta si la tercera inversión está habilitada), y uso el valor que devuelve como índice para el enum correspondiente a esa variable (no puedo pasarle directamente el valor a la variable "chordInversion" porque la variable "chordInversion" no es de tipo "int" sino de tipo ChordInversion (el enum).
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition:
                            doublingRandomization = Main.randomize.Next(0, 2); //Si el acorde es mayor y está en estado fundamental, tiene dos opciones de duplicación
                            if (doublingRandomization == 0) // La primera opción es que duplique la fundamental (el bajo)
                            {
                                _noteHighAPitchClass = _noteBassPitchClass;
                                _noteHighAOctave = Main.randomize.Next(4, 6); //la octava de la nota va a ser 4 o 5.

                                _noteHighBPitchClass = (_noteBassPitchClass + 4) % 12; //hago módulo 12 cuando calculo pitch classes porque si la pitch class del bajo es 11 y sumo 4 me da 15 (y yo no manejo 15 como pitch class, sino 3)
                                _noteHighBOctave = Main.randomize.Next(4, 6);

                                _noteHighCPitchClass = (_noteBassPitchClass + 7) % 12;
                                _noteHighCOctave = Main.randomize.Next(4, 6);
                            }
                            else if (doublingRandomization == 1) // La segunda opción es que duplique la 5ta. En ese caso, como voy a tener dos 5tas en las voces superiores (y las octavas que uso para las voces superiores son la 4 y la 5), voy a tocar con una voz la octava 4 y con la otra la octava 5.
                            {
                                _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                                _noteHighAOctave = Main.randomize.Next(4, 6);

                                _noteHighBPitchClass = (_noteBassPitchClass + 7) % 12;
                                _noteHighBOctave = 4;

                                _noteHighCPitchClass = (_noteBassPitchClass + 7) % 12;
                                _noteHighCOctave = 5;
                            }
                            break;

                        case ChordInversion.FirstInversion:
                            doublingRandomization = Main.randomize.Next(0, 2); //Si el acorde es mayor y está en primera inversión, tiene dos opciones de duplicación
                            if (doublingRandomization == 0) // La primera opción es que duplique la fundamental
                            {
                                _noteHighAPitchClass = (_noteBassPitchClass + 3) % 12;
                                _noteHighAOctave = Main.randomize.Next(4, 6);

                                _noteHighBPitchClass = (_noteBassPitchClass + 8) % 12;
                                _noteHighBOctave = 4;

                                _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                                _noteHighCOctave = 5;
                            }
                            else if (doublingRandomization == 1) // La segunda opción es que duplique la 5ta
                            {
                                _noteHighAPitchClass = (_noteBassPitchClass + 3) % 12;
                                _noteHighAOctave = 4;

                                _noteHighBPitchClass = (_noteBassPitchClass + 3) % 12;
                                _noteHighBOctave = 5;

                                _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                                _noteHighCOctave = Main.randomize.Next(4, 6);
                            }
                            break;

                        case ChordInversion.SecondInversion:
                            //Si el acorde es typeMajor y está en segunda inversión, solo puede duplicar la 5ta (el bajo).
                            _noteHighAPitchClass = _noteBassPitchClass;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 5) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 9) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                    }
                    break;

                case ChordType.Minor:
                    inversion = (ChordInversion)RandomizeTriadInversionAmongEnabledInversions(); //llamo a la función que randomiza la inversión de una tríada entre las inversiones de tríada habilitadas (es decir, sin tomar en cuenta si la tercera inversión está habilitada), y uso el valor que devuelve como índice para el enum correspondiente a esa variable (no puedo pasarle directamente el valor a la variable "chordInversion" porque la variable "chordInversion" no es de tipo "int" sino de tipo ChordInversion (el enum).
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition:
                            doublingRandomization = Main.randomize.Next(0, 2); //Si el acorde es minor y está en estado fundamental, tiene dos opciones de duplicación
                            if (doublingRandomization == 0) // La primera opción es que duplique la fundamental (el bajo)
                            {
                                _noteHighAPitchClass = _noteBassPitchClass;
                                _noteHighAOctave = Main.randomize.Next(4, 6);

                                _noteHighBPitchClass = (_noteBassPitchClass + 3) % 12;
                                _noteHighBOctave = Main.randomize.Next(4, 6);

                                _noteHighCPitchClass = (_noteBassPitchClass + 7) % 12;
                                _noteHighCOctave = Main.randomize.Next(4, 6);
                            }
                            else if (doublingRandomization == 1) // La segunda opción es que duplique la 5ta. En ese caso, como voy a tener dos 5tas en las voces superiores (y las octavas que uso para las voces superiores son la 4 y la 5), voy a tocar con una voz la octava 4 y con la otra la octava 5.
                            {
                                _noteHighAPitchClass = (_noteBassPitchClass + 3) % 12;
                                _noteHighAOctave = Main.randomize.Next(4, 6);

                                _noteHighBPitchClass = (_noteBassPitchClass + 7) % 12;
                                _noteHighBOctave = 4;

                                _noteHighCPitchClass = (_noteBassPitchClass + 7) % 12;
                                _noteHighCOctave = 5;
                            }
                            break;

                        case ChordInversion.FirstInversion:
                            doublingRandomization = Main.randomize.Next(0, 2); //Si el acorde es minor y está en primera inversión, tiene dos opciones de duplicación
                            if (doublingRandomization == 0) // La primera opción es que duplique la fundamental
                            {
                                _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                                _noteHighAOctave = Main.randomize.Next(4, 6);

                                _noteHighBPitchClass = (_noteBassPitchClass + 9) % 12;
                                _noteHighBOctave = 4;

                                _noteHighCPitchClass = (_noteBassPitchClass + 9) % 12;
                                _noteHighCOctave = 5;
                            }
                            else if (doublingRandomization == 1) // La segunda opción es que duplique la 5ta
                            {
                                _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                                _noteHighAOctave = 4;

                                _noteHighBPitchClass = (_noteBassPitchClass + 4) % 12;
                                _noteHighBOctave = 5;

                                _noteHighCPitchClass = (_noteBassPitchClass + 9) % 12;
                                _noteHighCOctave = Main.randomize.Next(4, 6);
                            }
                            break;

                        case ChordInversion.SecondInversion:
                            //Si el acorde es minor y está en segunda inversión, solo puede duplicar la 5ta (el bajo).
                            _noteHighAPitchClass = _noteBassPitchClass;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 5) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                    }
                    break;

                case ChordType.Diminished:
                    inversion = (ChordInversion)RandomizeTriadInversionAmongEnabledInversions(); //llamo a la función que randomiza la inversión de una tríada entre las inversiones de tríada habilitadas (es decir, sin tomar en cuenta si la tercera inversión está habilitada), y uso el valor que devuelve como índice para el enum correspondiente a esa variable (no puedo pasarle directamente el valor a la variable "chordInversion" porque la variable "chordInversion" no es de tipo "int" sino de tipo ChordInversion (el enum).
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition:
                            //Si el acorde es diminished y está en estado fundamental, solo puede duplicar la 3era. En ese caso, como voy a tener dos 3ras en las voces superiores (y las octavas que uso para las voces superiores son la 4 y la 5), voy a tocar con una voz la octava 4 y con la otra la octava 5.
                            _noteHighAPitchClass = (_noteBassPitchClass + 3) % 12;
                            _noteHighAOctave = 4;

                            _noteHighBPitchClass = (_noteBassPitchClass + 3) % 12;
                            _noteHighBOctave = 5;

                            _noteHighCPitchClass = (_noteBassPitchClass + 6) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.FirstInversion:
                            //Si el acorde es diminished y está en primera inversión, solo puede duplicar la 3era (el bajo).
                            _noteHighAPitchClass = _noteBassPitchClass;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 3) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 9) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.SecondInversion:
                            //Si el acorde es diminished y está en segunda inversión, solo puede duplicar la 3era.
                            _noteHighAPitchClass = (_noteBassPitchClass + 6) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 9) % 12;
                            _noteHighBOctave = 4;

                            _noteHighCPitchClass = (_noteBassPitchClass + 9) % 12;
                            _noteHighCOctave = 5;
                            break;

                    }
                    break;

                case ChordType.Augmented:
                    inversion = ChordInversion.RootPosition; //si estoy tirando acordes en abstracto y el acorde es aumentado no voy a randomizar la inversión, sino que lo voy a considerar siempre en root postiion (voy a considerar que el bajo es la fundamental, porque igual es un acorde simétrico)
                                                             //de todas formas voy a decir qué pasa si el acorde está en inversión (que es lo mismo que pasa si está en estado fundamental - maneja las mismas posibilidades de doublings independientemente de la inversión) porque para algún dictado futuro de progresiones puede que use acordes aumentados en inversión.
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition:
                            doublingRandomization = Main.randomize.Next(0, 3); //Si el acorde es aumentado y está en estado fundamental, puede duplicar cualquiera de las tres notas. En casos donde tenga la duplicación en las voces superiores, voy a tocar con una voz la octava 4 y con la otra la octava 5.
                            if (doublingRandomization == 0) // La primera opción es que duplique la fundamental (el bajo)
                            {
                                _noteHighAPitchClass = _noteBassPitchClass;
                                _noteHighAOctave = Main.randomize.Next(4, 6);

                                _noteHighBPitchClass = (_noteBassPitchClass + 4) % 12;
                                _noteHighBOctave = Main.randomize.Next(4, 6);

                                _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                                _noteHighCOctave = Main.randomize.Next(4, 6);
                            }
                            else if (doublingRandomization == 1) // La segunda opción es que duplique la 3era
                            {
                                _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                                _noteHighAOctave = 4;

                                _noteHighBPitchClass = (_noteBassPitchClass + 4) % 12;
                                _noteHighBOctave = 5;

                                _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                                _noteHighCOctave = Main.randomize.Next(4, 6);
                            }
                            else if (doublingRandomization == 2) // La tercera opción es que duplique la 5ta
                            {
                                _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                                _noteHighAOctave = Main.randomize.Next(4, 6);

                                _noteHighBPitchClass = (_noteBassPitchClass + 8) % 12;
                                _noteHighBOctave = 4;

                                _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                                _noteHighCOctave = 5;
                            }
                            break;

                        case ChordInversion.FirstInversion:
                            doublingRandomization = Main.randomize.Next(0, 3); //Si el acorde es aumentado y está en estado fundamental, puede duplicar cualquiera de las tres notas. En casos donde tenga la duplicación en las voces superiores, voy a tocar con una voz la octava 4 y con la otra la octava 5.
                            if (doublingRandomization == 0) // La primera opción es que duplique la fundamental (el bajo)
                            {
                                _noteHighAPitchClass = _noteBassPitchClass;
                                _noteHighAOctave = Main.randomize.Next(4, 6);

                                _noteHighBPitchClass = (_noteBassPitchClass + 4) % 12;
                                _noteHighBOctave = Main.randomize.Next(4, 6);

                                _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                                _noteHighCOctave = Main.randomize.Next(4, 6);
                            }
                            else if (doublingRandomization == 1) // La segunda opción es que duplique la 3era
                            {
                                _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                                _noteHighAOctave = 4;

                                _noteHighBPitchClass = (_noteBassPitchClass + 4) % 12;
                                _noteHighBOctave = 5;

                                _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                                _noteHighCOctave = Main.randomize.Next(4, 6);
                            }
                            else if (doublingRandomization == 2) // La tercera opción es que duplique la 5ta
                            {
                                _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                                _noteHighAOctave = Main.randomize.Next(4, 6);

                                _noteHighBPitchClass = (_noteBassPitchClass + 8) % 12;
                                _noteHighBOctave = 4;

                                _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                                _noteHighCOctave = 5;
                            }
                            break;

                        case ChordInversion.SecondInversion:
                            doublingRandomization = Main.randomize.Next(0, 3); //Si el acorde es aumentado y está en estado fundamental, puede duplicar cualquiera de las tres notas. En casos donde tenga la duplicación en las voces superiores, voy a tocar con una voz la octava 4 y con la otra la octava 5.
                            if (doublingRandomization == 0) // La primera opción es que duplique la fundamental (el bajo)
                            {
                                _noteHighAPitchClass = _noteBassPitchClass;
                                _noteHighAOctave = Main.randomize.Next(4, 6);

                                _noteHighBPitchClass = (_noteBassPitchClass + 4) % 12;
                                _noteHighBOctave = Main.randomize.Next(4, 6);

                                _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                                _noteHighCOctave = Main.randomize.Next(4, 6);
                            }
                            else if (doublingRandomization == 1) // La segunda opción es que duplique la 3era
                            {
                                _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                                _noteHighAOctave = 4;

                                _noteHighBPitchClass = (_noteBassPitchClass + 4) % 12;
                                _noteHighBOctave = 5;

                                _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                                _noteHighCOctave = Main.randomize.Next(4, 6);
                            }
                            else if (doublingRandomization == 2) // La tercera opción es que duplique la 5ta
                            {
                                _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                                _noteHighAOctave = Main.randomize.Next(4, 6);

                                _noteHighBPitchClass = (_noteBassPitchClass + 8) % 12;
                                _noteHighBOctave = 4;

                                _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                                _noteHighCOctave = 5;
                            }
                            break;

                    }
                    break;

                case ChordType.DominantSeventh:
                    inversion = (ChordInversion)RandomizeTetradInversionAmongEnabledInversions(); //llamo a la función que randomiza la inversión de una tétrada entre las inversiones de tétrada habilitadas, y uso el valor que devuelve como índice para el enum correspondiente a esa variable (no puedo pasarle directamente el valor a la variable "chordInversion" porque la variable "chordInversion" no es de tipo "int" sino de tipo ChordInversion (el enum).
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition: //en estado fundamental considero la opción de que sea completa (con la quinta, y sin duplicaciones) o incompleta (sin la quinta, y con la fundamental duplicada).
                            doublingRandomization = Main.randomize.Next(0, 2);
                            if (doublingRandomization == 0) // opción completa
                            {
                                _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                                _noteHighAOctave = Main.randomize.Next(4, 6);

                                _noteHighBPitchClass = (_noteBassPitchClass + 7) % 12;
                                _noteHighBOctave = Main.randomize.Next(4, 6);

                                _noteHighCPitchClass = (_noteBassPitchClass + 10) % 12;
                                _noteHighCOctave = Main.randomize.Next(4, 6);
                            }
                            else if (doublingRandomization == 1) // opción incompleta
                            {
                                _noteHighAPitchClass = _noteBassPitchClass;
                                _noteHighAOctave = Main.randomize.Next(4, 6);

                                _noteHighBPitchClass = (_noteBassPitchClass + 4) % 12;
                                _noteHighBOctave = 4;

                                _noteHighCPitchClass = (_noteBassPitchClass + 10) % 12;
                                _noteHighCOctave = 5;
                            }
                            break;

                        case ChordInversion.FirstInversion: //en primera inversión considero la opción de que sea completa (con la quinta, y sin duplicaciones) o incompleta (sin la quinta, y con la fundamental duplicada).
                            doublingRandomization = Main.randomize.Next(0, 2);
                            if (doublingRandomization == 0) // opción completa
                            {
                                _noteHighAPitchClass = (_noteBassPitchClass + 3) % 12;
                                _noteHighAOctave = Main.randomize.Next(4, 6);

                                _noteHighBPitchClass = (_noteBassPitchClass + 6) % 12;
                                _noteHighBOctave = Main.randomize.Next(4, 6);

                                _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                                _noteHighCOctave = Main.randomize.Next(4, 6);
                            }
                            else if (doublingRandomization == 1) // opción incompleta
                            {
                                _noteHighAPitchClass = (_noteBassPitchClass + 6) % 12;
                                _noteHighAOctave = Main.randomize.Next(4, 6);

                                _noteHighBPitchClass = (_noteBassPitchClass + 8) % 12;
                                _noteHighBOctave = 4;

                                _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                                _noteHighCOctave = 5;
                            }
                            break;

                        case ChordInversion.SecondInversion: //en segunda inversión obviamente no puede haber versión incompleta, porque la quinta está en el bajo.
                            _noteHighAPitchClass = (_noteBassPitchClass + 3) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 5) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 9) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.ThirdInversion: //en tercera inversión considero la opción de que sea completa (con la quinta, y sin duplicaciones) o incompleta (sin la quinta, y con la fundamental duplicada).
                            doublingRandomization = Main.randomize.Next(0, 2);
                            if (doublingRandomization == 0) // opción completa
                            {
                                _noteHighAPitchClass = (_noteBassPitchClass + 2) % 12;
                                _noteHighAOctave = Main.randomize.Next(4, 6);

                                _noteHighBPitchClass = (_noteBassPitchClass + 6) % 12;
                                _noteHighBOctave = Main.randomize.Next(4, 6);

                                _noteHighCPitchClass = (_noteBassPitchClass + 9) % 12;
                                _noteHighCOctave = Main.randomize.Next(4, 6);
                            }
                            else if (doublingRandomization == 1) // opción incompleta
                            {
                                _noteHighAPitchClass = (_noteBassPitchClass + 2) % 12;
                                _noteHighAOctave = 4;

                                _noteHighBPitchClass = (_noteBassPitchClass + 2) % 12;
                                _noteHighBOctave = 5;

                                _noteHighCPitchClass = (_noteBassPitchClass + 6) % 12;
                                _noteHighCOctave = Main.randomize.Next(4, 6);
                            }
                            break;

                    }
                    break;

                case ChordType.MinorSeventh:
                    inversion = (ChordInversion)RandomizeTetradInversionAmongEnabledInversions(); //llamo a la función que randomiza la inversión de una tétrada entre las inversiones de tétrada habilitadas, y uso el valor que devuelve como índice para el enum correspondiente a esa variable (no puedo pasarle directamente el valor a la variable "chordInversion" porque la variable "chordInversion" no es de tipo "int" sino de tipo ChordInversion (el enum).
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition: //Para esta tétrada no considero duplicaciones en ninguna inversión (siempre la hago completa)
                            _noteHighAPitchClass = (_noteBassPitchClass + 3) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 7) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 10) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.FirstInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 7) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 9) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.SecondInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 3) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 5) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.ThirdInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 2) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 5) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 9) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                    }
                    break;

                case ChordType.HalfDiminishedSeventh:
                    inversion = (ChordInversion)RandomizeTetradInversionAmongEnabledInversions(); //llamo a la función que randomiza la inversión de una tétrada entre las inversiones de tétrada habilitadas, y uso el valor que devuelve como índice para el enum correspondiente a esa variable (no puedo pasarle directamente el valor a la variable "chordInversion" porque la variable "chordInversion" no es de tipo "int" sino de tipo ChordInversion (el enum).
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition: //Para esta tétrada no considero duplicaciones en ninguna inversión (siempre la hago completa)
                            _noteHighAPitchClass = (_noteBassPitchClass + 3) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 6) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 10) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.FirstInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 3) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 7) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 9) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.SecondInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 6) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 9) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.ThirdInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 2) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 5) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                    }
                    break;

                case ChordType.MajorSeventh:
                    inversion = (ChordInversion)RandomizeTetradInversionAmongEnabledInversions(); //llamo a la función que randomiza la inversión de una tétrada entre las inversiones de tétrada habilitadas, y uso el valor que devuelve como índice para el enum correspondiente a esa variable (no puedo pasarle directamente el valor a la variable "chordInversion" porque la variable "chordInversion" no es de tipo "int" sino de tipo ChordInversion (el enum).
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition: //Para esta tétrada no considero duplicaciones en ninguna inversión (siempre la hago completa)
                            _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 7) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 11) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.FirstInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 3) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 7) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.SecondInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 5) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 9) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.ThirdInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 1) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 5) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                    }
                    break;

                case ChordType.MinorMajorSeventh:
                    inversion = (ChordInversion)RandomizeTetradInversionAmongEnabledInversions(); //llamo a la función que randomiza la inversión de una tétrada entre las inversiones de tétrada habilitadas, y uso el valor que devuelve como índice para el enum correspondiente a esa variable (no puedo pasarle directamente el valor a la variable "chordInversion" porque la variable "chordInversion" no es de tipo "int" sino de tipo ChordInversion (el enum).
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition: //Para esta tétrada no considero duplicaciones en ninguna inversión (siempre la hago completa)
                            _noteHighAPitchClass = (_noteBassPitchClass + 3) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 7) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 11) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.FirstInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 8) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 9) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.SecondInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 5) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.ThirdInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 1) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 4) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                    }
                    break;

                case ChordType.AugmentedMajorSeventh:
                    inversion = (ChordInversion)RandomizeTetradInversionAmongEnabledInversions(); //llamo a la función que randomiza la inversión de una tétrada entre las inversiones de tétrada habilitadas, y uso el valor que devuelve como índice para el enum correspondiente a esa variable (no puedo pasarle directamente el valor a la variable "chordInversion" porque la variable "chordInversion" no es de tipo "int" sino de tipo ChordInversion (el enum).
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition: //Para esta tétrada no considero duplicaciones en ninguna inversión (siempre la hago completa)
                            _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 8) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 11) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.FirstInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 7) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.SecondInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 3) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 4) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.ThirdInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 1) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 5) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 9) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                    }
                    break;

                case ChordType.DiminishedSeventh:
                    inversion = ChordInversion.RootPosition; //si estoy tirando acordes en abstracto y el acorde es tétrada disminuida no voy a randomizar la inversión, sino que lo voy a considerar siempre en root postiion (voy a considerar que el bajo es la fundamental, porque igual es un acorde simétrico)
                                                             //de todas formas voy a decir qué pasa si el acorde está en inversión (que es lo mismo que pasa si está en estado fundamental - maneja las mismas posibilidades de doublings independientemente de la inversión) porque para algún dictado futuro de progresiones puede que use acordes de tétrada disminuida en inversión.
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition: //Para esta tétrada no considero duplicaciones en ninguna inversión (siempre la hago completa)
                            _noteHighAPitchClass = (_noteBassPitchClass + 3) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 6) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 9) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.FirstInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 3) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 6) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 9) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.SecondInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 3) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 6) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 9) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.ThirdInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 3) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 6) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 9) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                    }
                    break;

                case ChordType.DominantSeventhFlatFive:
                    inversion = (ChordInversion)RandomizeTetradInversionAmongEnabledInversions(); //llamo a la función que randomiza la inversión de una tétrada entre las inversiones de tétrada habilitadas, y uso el valor que devuelve como índice para el enum correspondiente a esa variable (no puedo pasarle directamente el valor a la variable "chordInversion" porque la variable "chordInversion" no es de tipo "int" sino de tipo ChordInversion (el enum).
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition: //Para esta tétrada no considero duplicaciones en ninguna inversión (siempre la hago completa)
                            _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 6) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 10) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.FirstInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 2) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 6) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.SecondInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 6) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 10) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.ThirdInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 2) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 6) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                    }
                    break;

                case ChordType.DominantSeventhSharpFive:
                    inversion = (ChordInversion)RandomizeTetradInversionAmongEnabledInversions(); //llamo a la función que randomiza la inversión de una tétrada entre las inversiones de tétrada habilitadas, y uso el valor que devuelve como índice para el enum correspondiente a esa variable (no puedo pasarle directamente el valor a la variable "chordInversion" porque la variable "chordInversion" no es de tipo "int" sino de tipo ChordInversion (el enum).
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition: //Para esta tétrada no considero duplicaciones en ninguna inversión (siempre la hago completa)
                            _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 8) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 10) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.FirstInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 6) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.SecondInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 2) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 4) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 8) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                        case ChordInversion.ThirdInversion:
                            _noteHighAPitchClass = (_noteBassPitchClass + 2) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 6) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 10) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                    }
                    break;

                case ChordType.DominantNinth:
                    inversion = ChordInversion.RootPosition; //si estoy tirando acordes en abstracto y el acorde es tétrada dominante con novena mayor no voy a randomizar la inversión, sino que lo voy a hacer siempre en root position, que me parece que es como tiene más sentido considerar un acorde con novena a cuatro voces.
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition: //Para esta tétrada no considero duplicaciones (siempre la hago sin 5ta)
                            _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 10) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 2) % 12; //pese a que el bajo siempre lo hago en la octava 3 y las voces agudas en octava 4 o 5 que las voces agudas, si la voz aguda estuviera en la octava 4 podría estar a distancia de segunda del bajo en algún caso (por ej. si el bajo fuera Sib3 y esta voz Do4). Por eso, para asegurarme que esté a distancia de 9na (al menos) toco esta voz (a la que le corresponde la novena) en la octava 5, nunca en la 4.
                            _noteHighCOctave = 5;
                            break;

                    }
                    break;

                case ChordType.DominantMinorNinth:
                    inversion = ChordInversion.RootPosition; //si estoy tirando acordes en abstracto y el acorde es tétrada dominante con novena menor no voy a randomizar la inversión, sino que lo voy a hacer siempre en root position, que me parece que es como tiene más sentido considerar un acorde con novena a cuatro voces.
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition: //Para esta tétrada no considero duplicaciones (siempre la hago sin 5ta)
                            _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 10) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 1) % 12; //pese a que el bajo siempre lo hago en la octava 3 y las voces agudas en octava 4 o 5 que las voces agudas, si la voz aguda estuviera en la octava 4 podría estar a distancia de segunda del bajo en algún caso (si el bajo fuera Si3 y esta voz Do4). Por eso, para asegurarme que esté a distancia de 9na (al menos) toco esta voz (a la que le corresponde la novena) en la octava 5, nunca en la 4.
                            _noteHighCOctave = 5;
                            break;

                    }
                    break;

                case ChordType.MajorNinth:
                    inversion = ChordInversion.RootPosition; //si estoy tirando acordes en abstracto y el acorde es tétrada mayor con novena mayor no voy a randomizar la inversión, sino que lo voy a hacer siempre en root position, que me parece que es como tiene más sentido considerar un acorde con novena a cuatro voces.
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition: //Para esta tétrada no considero duplicaciones (siempre la hago sin 5ta)
                            _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 11) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 2) % 12; //pese a que el bajo siempre lo hago en la octava 3 y las voces agudas en octava 4 o 5 que las voces agudas, si la voz aguda estuviera en la octava 4 podría estar a distancia de segunda del bajo en algún caso (si el bajo fuera Si3 y esta voz Do4). Por eso, para asegurarme que esté a distancia de 9na (al menos) toco esta voz (a la que le corresponde la novena) en la octava 5, nunca en la 4.
                            _noteHighCOctave = 5;
                            break;

                    }
                    break;

                case ChordType.MinorNinth:
                    inversion = ChordInversion.RootPosition; //si estoy tirando acordes en abstracto y el acorde es tétrada menor con novena mayor no voy a randomizar la inversión, sino que lo voy a hacer siempre en root position, que me parece que es como tiene más sentido considerar un acorde con novena a cuatro voces.
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition: //Para esta tétrada no considero duplicaciones (siempre la hago sin 5ta)
                            _noteHighAPitchClass = (_noteBassPitchClass + 3) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 10) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 2) % 12; //pese a que el bajo siempre lo hago en la octava 3 y las voces agudas en octava 4 o 5 que las voces agudas, si la voz aguda estuviera en la octava 4 podría estar a distancia de segunda del bajo en algún caso (por ej. si el bajo fuera Sib3 y esta voz Do4). Por eso, para asegurarme que esté a distancia de 9na (al menos) toco esta voz (a la que le corresponde la novena) en la octava 5, nunca en la 4.
                            _noteHighCOctave = 5;
                            break;

                    }
                    break;

                case ChordType.MajorSixNine:
                    inversion = ChordInversion.RootPosition; //si estoy tirando acordes en abstracto y el acorde es 6/9 no voy a randomizar la inversión, sino que lo voy a hacer siempre en root position, que me parece que es como tiene más sentido considerar un acorde con novena a cuatro voces.
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition: //Para esta tétrada no considero duplicaciones (siempre la hago sin 5ta)
                            _noteHighAPitchClass = (_noteBassPitchClass + 4) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 9) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 2) % 12; //pese a que el bajo siempre lo hago en la octava 3 y las voces agudas en octava 4 o 5 que las voces agudas, si la voz aguda estuviera en la octava 4 podría estar a distancia de segunda del bajo en algún caso (por ej. si el bajo fuera Sib3 y esta voz Do4). Por eso, para asegurarme que esté a distancia de 9na (al menos) toco esta voz (a la que le corresponde la novena) en la octava 5, nunca en la 4.
                            _noteHighCOctave = 5;
                            break;

                    }
                    break;

                case ChordType.SusFour:
                    inversion = ChordInversion.RootPosition; //si estoy tirando acordes en abstracto y el acorde es sus4 no voy a randomizar la inversión, sino que lo voy a hacer siempre en root position, que me parece que es como tiene más sentido tocar a este tipo de acorde.
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition: //Para este tipo de acorde siempre duplico la fundamental (el bajo)
                            _noteHighAPitchClass = _noteBassPitchClass;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 5) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 7) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                    }
                    break;

                case ChordType.SusTwo:
                    inversion = ChordInversion.RootPosition; //si estoy tirando acordes en abstracto y el acorde es sus2 no voy a randomizar la inversión, sino que lo voy a hacer siempre en root position, que me parece que es como tiene más sentido tocar a este tipo de acorde.
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition: //Para este tipo de acorde siempre duplico la fundamental (el bajo)
                            _noteHighAPitchClass = _noteBassPitchClass;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 2) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 7) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                    }
                    break;

                case ChordType.SevenSusFour:
                    inversion = ChordInversion.RootPosition; //si estoy tirando acordes en abstracto y el acorde es 7sus4 no voy a randomizar la inversión, sino que lo voy a hacer siempre en root position, que me parece que es como tiene más sentido tocar este tipo de acorde.
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition: //Este tipo de acorde siempre lo hago completo (sin duplicaciones)
                            _noteHighAPitchClass = (_noteBassPitchClass + 5) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 7) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 10) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                    }
                    break;

                case ChordType.SevenSusTwo:
                    inversion = ChordInversion.RootPosition; //si estoy tirando acordes en abstracto y el acorde es 7sus4 no voy a randomizar la inversión, sino que lo voy a hacer siempre en root position, que me parece que es como tiene más sentido tocar este tipo de acorde.
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition: //Este tipo de acorde siempre lo hago completo (sin duplicaciones)
                            _noteHighAPitchClass = (_noteBassPitchClass + 2) % 12;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 7) % 12;
                            _noteHighBOctave = Main.randomize.Next(4, 6);

                            _noteHighCPitchClass = (_noteBassPitchClass + 10) % 12;
                            _noteHighCOctave = Main.randomize.Next(4, 6);
                            break;

                    }
                    break;

                case ChordType.Fifth:
                    inversion = (ChordInversion)RandomizeDyadInversionAmongEnabledInversions(); //llamo a la función que randomiza la inversión de una díada entre las inversiones de díada habilitadas (root position y primera inversión, que en este caso sería con la quinta en el bajo), y uso el valor que devuelve como índice para el enum correspondiente a esa variable (no puedo pasarle directamente el valor a la variable "chordInversion" porque la variable "chordInversion" no es de tipo "int" sino de tipo ChordInversion (el enum).
                    switch (inversion)
                    {
                        case ChordInversion.RootPosition: //En ambos estados duplico tanto la fundamental como la quinta
                            _noteHighAPitchClass = _noteBassPitchClass;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 7) % 12;
                            _noteHighCOctave = 5;

                            _noteHighCPitchClass = (_noteBassPitchClass + 7) % 12;
                            _noteHighCOctave = 6;
                            break;

                        case ChordInversion.FirstInversion:
                            _noteHighAPitchClass = _noteBassPitchClass;
                            _noteHighAOctave = Main.randomize.Next(4, 6);

                            _noteHighBPitchClass = (_noteBassPitchClass + 5) % 12;
                            _noteHighCOctave = 5;

                            _noteHighCPitchClass = (_noteBassPitchClass + 5) % 12;
                            _noteHighCOctave = 6;
                            break;

                    }
                    break;

            }


            // Calculo los PitchMidi de cada nota, estrictamente en base a su PitchClass y su Octave (ya determinados).
            //Para calcular la altura MIDI sumo el número correspondiente a la clase de altura + la octava que corresponda * 12 + 12 de offset (porque las notas MIDI arrancan en la octava -1)
            noteBassPitchMidi = _noteBassPitchClass + _noteBassOctave * 12 + 12;
            _noteHighAPitchMidi = _noteHighAPitchClass + _noteHighAOctave * 12 + 12;
            _noteHighBPitchMidi = _noteHighBPitchClass + _noteHighBOctave * 12 + 12;
            _noteHighCPitchMidi = _noteHighCPitchClass + _noteHighCOctave * 12 + 12;
        }
        private void CalculateRootNotePitchClass() // calculo cuál es el pitch class del rootnote (en relación al bajo, y tomando el cuenta el tipo de acorde y la inversión).
        {
            // Como lo que me interesa es saber el Pitch Class del root not (no el pitch absoluto) lo "encierro en una octava", así que uso módulo 12 para que si por ejemplo la nota es "15" se convierta en un "3" (que en el Dictionary pitchClass defino como "D#").
            if (inversion == ChordInversion.RootPosition) noteRootPitchClass = _noteBassPitchClass; //esto incluye todos los casos de acorde aumentado, tétrada disminuida, y otros acordes que al menos por ahora solo considero o solo toco en estado fundamental, pero de todas formas acá considero la posibilidad de que cualquier acorde esté en cualquier inversión que pueda llegar a tener sentido, porque así si los agrego a la lista anterior no hay problema si me olvido de agregarlos acá.
            else if (type == ChordType.Major && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 8) % 12;
            else if (type == ChordType.Major && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 5) % 12;
            else if (type == ChordType.Minor && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 9) % 12;
            else if (type == ChordType.Minor && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 5) % 12;
            else if (type == ChordType.Diminished && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 9) % 12;
            else if (type == ChordType.Diminished && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 6) % 12;
            else if (type == ChordType.Augmented && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 8) % 12;
            else if (type == ChordType.Augmented && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 4) % 12;
            else if (type == ChordType.DominantSeventh && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 8) % 12;
            else if (type == ChordType.DominantSeventh && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 5) % 12;
            else if (type == ChordType.DominantSeventh && inversion == ChordInversion.ThirdInversion) noteRootPitchClass = (_noteBassPitchClass + 2) % 12;
            else if (type == ChordType.MinorSeventh && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 9) % 12;
            else if (type == ChordType.MinorSeventh && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 5) % 12;
            else if (type == ChordType.MinorSeventh && inversion == ChordInversion.ThirdInversion) noteRootPitchClass = (_noteBassPitchClass + 2) % 12;
            else if (type == ChordType.HalfDiminishedSeventh && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 9) % 12;
            else if (type == ChordType.HalfDiminishedSeventh && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 6) % 12;
            else if (type == ChordType.HalfDiminishedSeventh && inversion == ChordInversion.ThirdInversion) noteRootPitchClass = (_noteBassPitchClass + 2) % 12;
            else if (type == ChordType.MajorSeventh && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 8) % 12;
            else if (type == ChordType.MajorSeventh && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 5) % 12;
            else if (type == ChordType.MajorSeventh && inversion == ChordInversion.ThirdInversion) noteRootPitchClass = (_noteBassPitchClass + 1) % 12;
            else if (type == ChordType.MinorMajorSeventh && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 9) % 12;
            else if (type == ChordType.MinorMajorSeventh && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 5) % 12;
            else if (type == ChordType.MinorMajorSeventh && inversion == ChordInversion.ThirdInversion) noteRootPitchClass = (_noteBassPitchClass + 1) % 12;
            else if (type == ChordType.AugmentedMajorSeventh && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 8) % 12;
            else if (type == ChordType.AugmentedMajorSeventh && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 4) % 12;
            else if (type == ChordType.AugmentedMajorSeventh && inversion == ChordInversion.ThirdInversion) noteRootPitchClass = (_noteBassPitchClass + 1) % 12;
            else if (type == ChordType.DiminishedSeventh && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 9) % 12;
            else if (type == ChordType.DiminishedSeventh && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 6) % 12;
            else if (type == ChordType.DiminishedSeventh && inversion == ChordInversion.ThirdInversion) noteRootPitchClass = (_noteBassPitchClass + 3) % 12;
            else if (type == ChordType.DominantSeventhFlatFive && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 8) % 12;
            else if (type == ChordType.DominantSeventhFlatFive && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 6) % 12;
            else if (type == ChordType.DominantSeventhFlatFive && inversion == ChordInversion.ThirdInversion) noteRootPitchClass = (_noteBassPitchClass + 2) % 12;
            else if (type == ChordType.DominantSeventhSharpFive && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 8) % 12;
            else if (type == ChordType.DominantSeventhSharpFive && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 4) % 12;
            else if (type == ChordType.DominantSeventhSharpFive && inversion == ChordInversion.ThirdInversion) noteRootPitchClass = (_noteBassPitchClass + 2) % 12;
            else if (type == ChordType.DominantNinth && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 8) % 12;
            else if (type == ChordType.DominantNinth && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 5) % 12;
            else if (type == ChordType.DominantNinth && inversion == ChordInversion.ThirdInversion) noteRootPitchClass = (_noteBassPitchClass + 2) % 12;
            else if (type == ChordType.DominantMinorNinth && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 8) % 12;
            else if (type == ChordType.DominantMinorNinth && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 5) % 12;
            else if (type == ChordType.DominantMinorNinth && inversion == ChordInversion.ThirdInversion) noteRootPitchClass = (_noteBassPitchClass + 2) % 12;
            else if (type == ChordType.MajorNinth && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 8) % 12;
            else if (type == ChordType.MajorNinth && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 5) % 12;
            else if (type == ChordType.MajorNinth && inversion == ChordInversion.ThirdInversion) noteRootPitchClass = (_noteBassPitchClass + 1) % 12;
            else if (type == ChordType.MinorNinth && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 9) % 12;
            else if (type == ChordType.MinorNinth && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 5) % 12;
            else if (type == ChordType.MinorNinth && inversion == ChordInversion.ThirdInversion) noteRootPitchClass = (_noteBassPitchClass + 2) % 12;
            else if (type == ChordType.MajorSixNine && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 8) % 12;
            else if (type == ChordType.MajorSixNine && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 5) % 12;
            else if (type == ChordType.MajorSixNine && inversion == ChordInversion.ThirdInversion) noteRootPitchClass = (_noteBassPitchClass + 3) % 12; //considero la 6ta como el bajo en la tercera inversión
            else if (type == ChordType.SusFour && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 7) % 12; //considero la 4ta como el bajo en la primera inversión
            else if (type == ChordType.SusFour && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 5) % 12;
            else if (type == ChordType.SusTwo && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 10) % 12; //considero la 2da como el bajo en la primera inversión
            else if (type == ChordType.SusTwo && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 5) % 12;
            else if (type == ChordType.SevenSusFour && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 7) % 12; //considero la 4ta como el bajo en la primera inversión
            else if (type == ChordType.SevenSusFour && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 5) % 12;
            else if (type == ChordType.SevenSusFour && inversion == ChordInversion.ThirdInversion) noteRootPitchClass = (_noteBassPitchClass + 2) % 12;
            else if (type == ChordType.SevenSusTwo && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 10) % 12; //considero la 2da como el bajo en la primera inversión
            else if (type == ChordType.SevenSusTwo && inversion == ChordInversion.SecondInversion) noteRootPitchClass = (_noteBassPitchClass + 5) % 12;
            else if (type == ChordType.SevenSusTwo && inversion == ChordInversion.ThirdInversion) noteRootPitchClass = (_noteBassPitchClass + 2) % 12;
            else if (type == ChordType.Fifth && inversion == ChordInversion.FirstInversion) noteRootPitchClass = (_noteBassPitchClass + 5) % 12; //considero la 5ta como el bajo en la primera inversión
        }

        private int RandomizeTypeAmongEnabledTypes() //Randomiza el tipo de acorde entre los tipos de acorde que haya habilitados
        {
            int numberOfEnabledTypes = 0; //Cuento la cantidad de tipos de acorde habilitados
            for (int i = 0; i < isTypeEnabled.Length; i++)
            {
                if (isTypeEnabled[i]) numberOfEnabledTypes++;
            }

            //Randomizo el tipo del acorde entre la cantidad de tipos de acorde que haya habilitados (si hay 3 tipos de acorde habilitados, va a randomizar un número entre 0 y 2). Este número NO lo usaré directamente como índice del chordType (sigo explicando abajo)
            int unmappedTypeNumber = Main.randomize.Next(0, numberOfEnabledTypes);

            //El número que me dio"randomize" no lo podré usar directamente como índice para el enum "chordType", porque hay que ver qué tipos están deshabilitados y saltearlos de la cuenta.
            //Por ejemplo, si me dio que el tipo de acorde era el tercer valor posible del random (índice 2), pero sé que en realidad tengo deshabilitados el primer y el tercer acorde de la lista (índices 0 y 2); el tipo de acorde verdadero que tengo que tocar es el quinto (índice 4).

            // Mapeo del número que me salió del random al índice que realmente va a determinar el tipo de acorde ("salteando" los que se hayan deshabilitado)
            // El número unmappedTypeNumber es algo así como la cantidad de "pasos" que tengo para recorrer las opciones de tipo de acorde.
            // Sin embargo, las opciones de tipo de acorde que estén deshabilitadas no me obligan a "gastarme un paso" (porque me las salteo)
            // Por ejemplo, si unmappedTypeNumber es 2, pero el tipo de acorde 1 está deshabilitado, mappedTimeNumber debe ser 3.
            int mappedTypeNumber; //inicializo la variable que va a determinar el número mapeado.
            for (mappedTypeNumber = 0; mappedTypeNumber < isTypeEnabled.Length; mappedTypeNumber++) //comienzo a iterar hasta como máximo la cantidad de índices en isTypeEnabled (no necesariamente voy a llegar a iterar hasta ese máximo), sumándole en cada vuelta 1 a mapped chordType number
            {
                if (isTypeEnabled[mappedTypeNumber]) unmappedTypeNumber--; //si y solo sí el índice IsTypeEnabled por el que se está pasando está stateEnsabled, tengo que gastarme uno de los pasos
                                                                           //en cambio, si está disabled, puedo volver al comienzo del for statement (sumarle 1 a mappedTypeNumber sin necesidad de gastarme uno de los pasos)

                if (unmappedTypeNumber < 0) break; // Si ya no me quedan pasos me tengo que salir del for (y el valor de mappedTypeNumber al que haya llegado hasta este momento va a ser el que se use como índice para el enum correspondiente a esa variable).
            }
            return mappedTypeNumber;         // devuelvo el número de tipo de acorde "real" (es decir, mapeado a los acordes que haya habilitados)
        }


        int RandomizeDyadInversionAmongEnabledInversions() //Ver los comentarios de "RandomizeTypeAmongEnabledTypes", porque es casi lo mismo
        {
            int numberOfEnabledInversions = 0;
            for (int i = 0; i < 2; i++) // en lugar de tomar en cuenta todas las inversions del array "isInversionEnabled" tomo en cuenta solo 2, porque en una díada no me importa si el tercer y cuarto valor de ese array son true o no (si la segunda y tercera inversión están habilitadas o no)
            {
                if (isInversionEnabled[i]) numberOfEnabledInversions++;
            }

            int mappedInversionNumber;
            int unmappedInversionNumber = Main.randomize.Next(0, numberOfEnabledInversions);

            for (mappedInversionNumber = 0; mappedInversionNumber < 2; mappedInversionNumber++) //Idem comentario anterior, en lugar de loopear hasta como máximo la cantidad de índices en isInversionEnabled, lo limito a dos (creo que igual podría iterar hasta como máximo la cantidad de índices en isInversionEnabled, el tema es que ese máximo nunca se alcanzaría)
            {
                if (isInversionEnabled[mappedInversionNumber]) unmappedInversionNumber--;

                if (unmappedInversionNumber < 0) break;
            }
            return mappedInversionNumber; //devuelvo el número de inversión "real" (es decir, mapeado a las inversiones que haya habilitadas y correspondan al tipo de acorde)
        }


        int RandomizeTriadInversionAmongEnabledInversions() //Ver los comentarios de "RandomizeTypeAmongEnabledTypes", porque es casi lo mismo
        {
            int numberOfEnabledInversions = 0;
            for (int i = 0; i < 3; i++) // en lugar de tomar en cuenta todas las inversions del array "isInversionEnabled" tomo en cuenta solo 3, porque en una tríada no me importa si el cuarto valor de ese array es true o no (si la tercera inversión está habilitada o no)
            {
                if (isInversionEnabled[i]) numberOfEnabledInversions++;
            }

            int mappedInversionNumber;
            int unmappedInversionNumber = Main.randomize.Next(0, numberOfEnabledInversions);

            for (mappedInversionNumber = 0; mappedInversionNumber < 3; mappedInversionNumber++) //Idem comentario anterior, en lugar de loopear hasta como máximo la cantidad de índices en isInversionEnabled, lo limito a tres (creo que igual podría iterar hasta como máximo la cantidad de índices en isInversionEnabled, el tema es que ese máximo nunca se alcanzaría)
            {
                if (isInversionEnabled[mappedInversionNumber]) unmappedInversionNumber--;

                if (unmappedInversionNumber < 0) break;
            }
            return mappedInversionNumber; //devuelvo el número de inversión "real" (es decir, mapeado a las inversiones que haya habilitadas y correspondan al tipo de acorde)
        }


        int RandomizeTetradInversionAmongEnabledInversions() //Ver los comentarios de "RandomizeTypeAmongEnabledTypes", porque es casi lo mismo
        {
            int numberOfEnabledInversions = 0;
            for (int i = 0; i < isInversionEnabled.Length; i++)
            {
                if (isInversionEnabled[i]) numberOfEnabledInversions++;
            }

            int mappedInversionNumber;
            int unmappedInversionNumber = Main.randomize.Next(0, numberOfEnabledInversions);

            for (mappedInversionNumber = 0; mappedInversionNumber < isInversionEnabled.Length; mappedInversionNumber++)
            {
                if (isInversionEnabled[mappedInversionNumber]) unmappedInversionNumber--;

                if (unmappedInversionNumber < 0) break;
            }
            return mappedInversionNumber; //devuelvo el número de inversión "real" (es decir, mapeado a las inversiones que haya habilitadas)
        }
        private void AssignHighNotesToHighVoices() // Analizo cuál de las notas superiores (HighA, HighB y HighC) es la más aguda (que se la asigno a la soprano), segunda más aguda (que se la asigno al alto) y más grave (que se la asigno al tenor).
        {

            if (_noteHighAPitchMidi > _noteHighBPitchMidi && _noteHighAPitchMidi > _noteHighCPitchMidi)
            {
                noteSopranoPitchMidi = _noteHighAPitchMidi;

                if (_noteHighBPitchMidi > _noteHighCPitchMidi)
                {
                    noteAltoPitchMidi = _noteHighBPitchMidi;
                    noteTenorPitchMidi = _noteHighCPitchMidi;
                }
                else
                {
                    noteAltoPitchMidi = _noteHighCPitchMidi;
                    noteTenorPitchMidi = _noteHighBPitchMidi;
                }
            }


            else if (_noteHighBPitchMidi > _noteHighAPitchMidi && _noteHighBPitchMidi > _noteHighCPitchMidi)
            {
                noteSopranoPitchMidi = _noteHighBPitchMidi;

                if (_noteHighAPitchMidi > _noteHighCPitchMidi)
                {
                    noteAltoPitchMidi = _noteHighAPitchMidi;
                    noteTenorPitchMidi = _noteHighCPitchMidi;
                }
                else
                {
                    noteAltoPitchMidi = _noteHighCPitchMidi;
                    noteTenorPitchMidi = _noteHighAPitchMidi;
                }
            }

            else if (_noteHighCPitchMidi > _noteHighAPitchMidi && _noteHighCPitchMidi > _noteHighBPitchMidi)
            {
                noteSopranoPitchMidi = _noteHighCPitchMidi;

                if (_noteHighAPitchMidi > _noteHighBPitchMidi)
                {
                    noteAltoPitchMidi = _noteHighAPitchMidi;
                    noteTenorPitchMidi = _noteHighBPitchMidi;
                }
                else
                {
                    noteAltoPitchMidi = _noteHighBPitchMidi;
                    noteTenorPitchMidi = _noteHighAPitchMidi;
                }
            }

            Debug.WriteLine($"Soprano Pitch Midi: {noteSopranoPitchMidi}");
            Debug.WriteLine($"Alto Pitch Midi: {noteAltoPitchMidi}");
            Debug.WriteLine($"Tenor Pitch Midi: {noteTenorPitchMidi}");
            Debug.WriteLine($"Bass Pitch Midi: {noteBassPitchMidi}");
        }

        public void PlaySimultaneous()
        {
            _voiceToPlay = Voice.None; //des-agendo las reproducciones de arpeggio (para que no se superponga esto con nuevas notas de un arpegio que viene sonando de antes).

            StopAll(); // si las variables para sound effect instances están asignadas, detiene las esas instancias (es decir, detiene cualquier nota que se estuviera reproduciendo) (y les hace dispose, por las dudas)

            CreateSoundEffectInstances(); // asigna las variables para sound effect instances a nuevos sound effect instances.

            //comienzo las reproducciones
            sfxInstanceSoprano.Volume = .5f; sfxInstanceSoprano.Play();
            sfxInstanceAlto.Volume = .5f; sfxInstanceAlto.Play();
            sfxInstanceTenor.Volume = .5f; sfxInstanceTenor.Play();
            sfxInstanceBass.Volume = .5f; sfxInstanceBass.Play();

        }

        public void PlayArpeggio()
        {
            StopAll(); // si las variables para sound effect instances están asignadas, detiene las esas instancias (es decir, detiene cualquier nota que se estuviera reproduciendo) (y les hace dispose, por las dudas)

            CreateSoundEffectInstances(); // asigna las variables para sound effect instances a nuevos sound effect instances.

            _voiceToPlay = Voice.Bass;
            if (ChordPlayingManager.slowerArpeggiations.stateSelected == true) //determino la velocidad del arpegio dependiendo de si el checkbox "Slower Arpeggiations" está activado o no.
            {
                _arpeggioTimeBetweenNotes = 1000;
            }
            else
            {
                _arpeggioTimeBetweenNotes = 500;
            }
            _arpeggioTimer = 0; //reseteo el Timer del arpegio (que está en ms)
        }


        private void StopAll() // si las variables para sound effect instances están asignadas, detengo esas instancias (es decir, detengo cualquier nota que se estuviera reproduciendo) (y les hago dispose, por las dudas)
        {
            sfxInstanceSoprano?.Stop();
            sfxInstanceSoprano?.Dispose();
            sfxInstanceAlto?.Stop();
            sfxInstanceAlto?.Dispose();
            sfxInstanceTenor?.Stop();
            sfxInstanceTenor?.Dispose();
            sfxInstanceBass?.Stop();
            sfxInstanceBass?.Dispose();
        }


        private void CreateSoundEffectInstances()
        {
            //cargo cada sonido de nota a la voice correspondiente
            sfxInstanceSoprano = Main.noteSoundFiles[noteSopranoPitchMidi].CreateInstance();
            sfxInstanceAlto = Main.noteSoundFiles[noteAltoPitchMidi].CreateInstance();
            sfxInstanceTenor = Main.noteSoundFiles[noteTenorPitchMidi].CreateInstance();
            sfxInstanceBass = Main.noteSoundFiles[noteBassPitchMidi].CreateInstance();
        }



        public void UpdateArpeggio(GameTime gameTime)
        {
            _arpeggioTimer += gameTime.ElapsedGameTime.TotalMilliseconds; //al timer del arpegio (que funciona con milisegundos) le voy sumando el tiempo que pasa en cada frame (sin eso no sería un timer).

            switch (_voiceToPlay)
            {
                // paso a tocar las notas del acorde (de grave a aguda), separadas por el tiempo determinado por "_arpeggioTimeBetweenNotes" (que se considera en ms). A medida que toco una nota cargo la variable "_voiceToPlay" con el valor correspondiente a la nota siguiente.
                case Voice.Bass:
                    if (_arpeggioTimer >= _arpeggioTimeBetweenNotes * 0)
                    {
                        sfxInstanceBass.Volume = .5f; sfxInstanceBass.Play();
                        _voiceToPlay = Voice.Tenor;
                    }
                    break;

                case Voice.Tenor:
                    if (_arpeggioTimer >= _arpeggioTimeBetweenNotes * 1)
                    {
                        sfxInstanceTenor.Volume = .5f; sfxInstanceTenor.Play();
                        _voiceToPlay = Voice.Alto;
                    }
                    break;

                case Voice.Alto:
                    if (_arpeggioTimer >= _arpeggioTimeBetweenNotes * 2)
                    {
                        sfxInstanceAlto.Volume = .5f; sfxInstanceAlto.Play();
                        _voiceToPlay = Voice.Soprano;
                    }
                    break;

                case Voice.Soprano:
                    if (_arpeggioTimer >= _arpeggioTimeBetweenNotes * 3)
                    {
                        sfxInstanceSoprano.Volume = .5f; sfxInstanceSoprano.Play();
                        _voiceToPlay = Voice.None;
                    }
                    break;
            }
        }
    }
}