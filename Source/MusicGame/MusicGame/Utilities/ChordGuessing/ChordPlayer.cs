using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame.ChordGuessing
{
    static class ChordPlayer
    {
        private static List<Bang> chordPlaying = new List<Bang>();

        public static ChordVoicing chord1 = new ChordVoicing(); //genero una instancia de la clase Chord llamada "chord1"

        public static void Initialize()
        {
            chordPlaying.Add(new Bang(new Vector2(Main.marginLeft, Main.marginTop), "Randomize Chord [ left ]", (int)BangCategory.ChordPlaying, true));
            chordPlaying.Add(new Bang(new Vector2(Main.marginLeft + Main.gridColumnSeparation, Main.marginTop), "Repeat Chord [ right ]", (int)BangCategory.ChordPlaying, true));
            chordPlaying.Add(new Bang(new Vector2(Main.marginLeft + Main.gridColumnSeparation * 2, Main.marginTop), "Arpeggiate Chord [ up ]", (int)BangCategory.ChordPlaying, true));
            chordPlaying.Add(new Bang(new Vector2(Main.marginLeft + Main.gridColumnSeparation * 3, Main.marginTop), "Reveal Answer [ down ]", (int)BangCategory.ChordPlaying, true));

            chordPlaying[0].BangClickedLeft += OnChordPlaying0BangClickedLeft;
            chordPlaying[1].BangClickedLeft += OnChordPlaying1BangClickedLeft;
            chordPlaying[2].BangClickedLeft += OnChordPlaying2BangClickedLeft;

        }

        static void OnChordPlaying0BangClickedLeft(Bang bang, int category, bool stateEnabled)
        {
            Main.chordDescriptionPart1 = "";
            Main.chordDescriptionPart2 = "";
            chord1.Generate(); //llamo a la función de chord1 "Generate"
            chord1.PlaySimultaneous(); //llamo a la función de chord1 "Play"
        }
        static void OnChordPlaying1BangClickedLeft(Bang bang, int category, bool stateEnabled)
        {
            chord1.PlaySimultaneous(); //llamo a la función de chord1 "Play"
        }
        static void OnChordPlaying2BangClickedLeft(Bang bang, int category, bool stateEnabled)
        {
            chord1.PlayArpeggio(); //llamo a la función de chord1 "PlayArpeggio"
        }

        static void OnChordPlaying3BangClickedLeft(Bang bang, int category, bool stateEnabled)
        {
            //En la descripción de part1, para estas variables uso los diccionarios (que cargo de la class "Dictionaries", y como índices de los diccionarios uso valores de chord1.
            //Los casos del chordType y el chordInversion son más elaborados, porque para obtener los índices de los diccionarios como integers (que es como los preciso) debo convertir a integers los valores de las variables chordType e chordInversion, alojadas en chord1 (como uso enums esos valores son de tipo ChordType y ChordInversion, por eso debo convertirlos).
            Main.chordDescriptionPart1 = $"The random chord generated was {Dictionaries.pitchClass[chord1.noteRootPitchClass]} {Dictionaries.chordType[(int)chord1.type].ToLower()} in {Dictionaries.chordInversion[(int)chord1.inversion].ToLower()}"; //(paso los nombres de los diccionarios chordType y chordInversion a lowercase)
            Main.chordDescriptionPart2 = $"The MIDI pitches were (in order from low to high) {chord1.noteBassPitchMidi}, {chord1.noteTenorPitchMidi}, {chord1.noteAltoPitchMidi} and {chord1.noteSopranoPitchMidi} ({Dictionaries.pitchMidi[chord1.noteBassPitchMidi]}, {Dictionaries.pitchMidi[chord1.noteTenorPitchMidi]}, {Dictionaries.pitchMidi[chord1.noteAltoPitchMidi]} and {Dictionaries.pitchMidi[chord1.noteSopranoPitchMidi]}).";
        }


        public static void GenerateShortcut() //función que se llama si uso el shortcut para generar acorde (tecla izquierda)
        {
            chordPlaying[0].OnBangClickedLeft(); //Si uso el shortcut, quiero hacer lo mismo que haga si clickeara el bang con click izquierdo
        }
        public static void PlaySimultaneousShortcut() //función que se llama si uso el shortcut para tocar acorde (tecla derecha)
        {
            chordPlaying[1].OnBangClickedLeft();  //Si uso el shortcut, quiero hacer lo mismo que haga si clickeara el bang con click izquierdo
        }
        public static void PlayArpeggioShortcut() //función que se llama si uso el shortcut para arpegiar acorde (tecla arriba)
        {
            chordPlaying[2].OnBangClickedLeft(); //Si uso el shortcut, quiero hacer lo mismo que haga si clickeara el bang con click izquierdo
        }



        public static void Update(GameTime gameTime, MouseState mouseState)
        {
            //llamo a la función Update de los bang
            foreach (var bang in chordPlaying)
            {
                bang.Update(gameTime, mouseState);
            }


            chord1.UpdateArpeggio(gameTime); //en cada update corro a la función "UpdateArpeggio" del acorde.

        }


        public static void Draw(SpriteBatch spriteBatch)
        {
            //llamo a la función Update de los bang
            foreach (var bang in chordPlaying)
            {
                bang.Draw(spriteBatch);
            }

        }
    }
}
