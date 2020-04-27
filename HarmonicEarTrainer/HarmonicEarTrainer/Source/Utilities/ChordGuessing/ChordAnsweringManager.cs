using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonicEarTrainer
{
    static class ChordAnsweringManager
    {
        public static string evaluatedChordTypeAnswer { get; set; } = ""; // para poner si la respuesta es correcta o equivocada
        static Color _evaluatedChordTypeAnswerColor; // va a cambiar dependiendo de si la respuesta fue correcta o equivocada
        public static string evaluatedChordInversionAnswer { get; set; } = ""; // para poner si la respuesta es correcta o equivocada
        static Color _evaluatedChordInversionAnswerColor; // va a cambiar dependiendo de si la respuesta fue correcta o equivocada

        public static string chordDescriptionPart1 { get; set; } = ""; // en estos string voy a guardar los textos de descripción del acorde.
        public static string chordDescriptionPart2 { get; set; } = ""; // en estos string voy a guardar los textos de descripción del acorde.


        private static Bang _submitAnswer = null; //no la referencio ahora sino en Initialize, así sé que para ese momento cargó todos los assets.
        private static Bang _revealAnswer = null; //no la referencio ahora sino en Initialize, así sé que para ese momento cargó todos los assets.

        public static bool allowAnswer { get; set; } = false; //dependiendo de si se está esperando por una respuesta o no distintas opciones van a estar habilitadas

        public static void Initialize()
        {
            _submitAnswer = new Bang(new Vector2(LayoutManager.marginLeft, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 18), "Submit", (int)BangCategory.Chord, true, Keys.Enter);
            _revealAnswer = new Bang(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 18), "Reveal", (int)BangCategory.Chord, true, Keys.Down);

            _submitAnswer.BangClickedWithLeftButton += OnSubmitBangClickedWithLeftButton;
            _revealAnswer.BangClickedWithLeftButton += OnRevealBangClickedWithLeftButton;
        }

        static void OnSubmitBangClickedWithLeftButton(Bang bang, int category, bool stateEnabled)  //le sumo uno al contador de respuestas totales, evalúo si la respuesta es correcta o no y actúo en consecuencia
        {
            allowAnswer = false; // deshabilito la opción de responder

            ScoreManager.totalAnswers++; //sumo uno al contador de cuántas respuestas dio el jugador (que es visible en la UI).

            if (ChordListManager.selectedType == (int)ChordPlayingManager.chord1.type)
            {
                ScoreManager.correctTypes++;
                evaluatedChordTypeAnswer = "CORRECT TYPE!!";
                _evaluatedChordTypeAnswerColor = Main.fontColorForCorrectAnswer;
            }
            else
            {
                evaluatedChordTypeAnswer = "WRONG TYPE...";
                _evaluatedChordTypeAnswerColor = Main.fontColorForWrongAnswer;
            }

            if (ChordListManager.selectedInversion == (int)ChordPlayingManager.chord1.inversion)
            {
                ScoreManager.correctInversions++;
                evaluatedChordInversionAnswer = "CORRECT INVERSION!!";
                _evaluatedChordInversionAnswerColor = Main.fontColorForCorrectAnswer;
            }
            else
            {
                evaluatedChordInversionAnswer = "WRONG INVERSION...";
                _evaluatedChordInversionAnswerColor = Main.fontColorForWrongAnswer;
            }

            //actualizo los porcentajes de aciertos
            ScoreManager.correctTypesPercentage = (ScoreManager.correctTypes * 100) / ScoreManager.totalAnswers; 
            ScoreManager.correctInversionsPercentage = (ScoreManager.correctInversions * 100) / ScoreManager.totalAnswers;

            DisplayChordDescription();
        }

        static void OnRevealBangClickedWithLeftButton(Bang bang, int category, bool stateEnabled)
        {
            allowAnswer = false; // deshabilito la opción de responder

            DisplayChordDescription();
        }

        private static void DisplayChordDescription()
        {
            //En la descripción de part1, para estas variables uso los diccionarios (que cargo de la class "Dictionaries", y como índices de los diccionarios uso valores de ChordPlayingManager.chord1.
            //Los casos del chordType y el chordInversion son más elaborados, porque para obtener los índices de los diccionarios como integers (que es como los preciso) debo castear como integers los valores de las variables chordType e chordInversion, alojadas en ChordPlayingManager.chord1 (como uso enums esos valores son de tipo ChordType y ChordInversion, por eso debo castearlos como integers).
            chordDescriptionPart1 = $"The random chord generated was {Dictionaries.pitchClass[ChordPlayingManager.chord1.noteRootPitchClass]} {Dictionaries.chordType[(int)ChordPlayingManager.chord1.type].ToLower()} in {Dictionaries.chordInversion[(int)ChordPlayingManager.chord1.inversion].ToLower()}."; //(paso los nombres de los diccionarios chordType y chordInversion a lowercase)
            chordDescriptionPart2 = $"The MIDI pitches were (in order from low to high) {ChordPlayingManager.chord1.noteBassPitchMidi}, {ChordPlayingManager.chord1.noteTenorPitchMidi}, {ChordPlayingManager.chord1.noteAltoPitchMidi} and {ChordPlayingManager.chord1.noteSopranoPitchMidi} ({Dictionaries.pitchMidi[ChordPlayingManager.chord1.noteBassPitchMidi]}, {Dictionaries.pitchMidi[ChordPlayingManager.chord1.noteTenorPitchMidi]}, {Dictionaries.pitchMidi[ChordPlayingManager.chord1.noteAltoPitchMidi]} and {Dictionaries.pitchMidi[ChordPlayingManager.chord1.noteSopranoPitchMidi]}).";
        }


        public static void Update(GameTime gameTime, MouseState mouseState)
        {
            if (allowAnswer == true)
            {
                _submitAnswer.stateEnabled = true;
                _revealAnswer.stateEnabled = true;
            }
            else
            {
                _submitAnswer.stateEnabled = false;
                _revealAnswer.stateEnabled = false;
            }

            //llamo a la función Update de los bang
            _submitAnswer.Update(gameTime, mouseState);
            _revealAnswer.Update(gameTime, mouseState);
        }


        public static void Draw(SpriteBatch spriteBatch)
        {
            //llamo a la función Draw de los bang
            _submitAnswer.Draw(spriteBatch);
            _revealAnswer.Draw(spriteBatch);

            spriteBatch.DrawString(Main.fontDefault, evaluatedChordTypeAnswer, new Vector2(LayoutManager.marginLeft, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 19), _evaluatedChordTypeAnswerColor);
            spriteBatch.DrawString(Main.fontDefault, evaluatedChordInversionAnswer, new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 19), _evaluatedChordInversionAnswerColor);
            spriteBatch.DrawString(Main.fontDefault, chordDescriptionPart1, new Vector2(LayoutManager.marginLeft, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 20), Main.fontColorDefault);
            spriteBatch.DrawString(Main.fontDefault, chordDescriptionPart2, new Vector2(LayoutManager.marginLeft, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 21), Main.fontColorDefault);
        }
    }
}
