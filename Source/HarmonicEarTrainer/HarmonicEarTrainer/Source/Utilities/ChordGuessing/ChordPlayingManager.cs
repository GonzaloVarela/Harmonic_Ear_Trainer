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
    static class ChordPlayingManager
    {
        public static Dictionary<string, Bang> chordPlaying = new Dictionary<string, Bang>(); //Pongo todos los bangs de ChordPlaying en un dictionary, así puedo acceder por ellos por su "nombre" (string que asigno como key) pero también puedo iterar por ellos.

        public static ChordVoicing chord1 = new ChordVoicing(); //genero una instancia de la clase Chord llamada "chord1"

        private static bool _alreadyRandomizedFirstChord = false; // solo quiero permitir que se repitan acordes una vez que haya randomizado un acorde

        public static Checkbox slowerArpeggiations = null; //la asigno en el initialize, así me aseguro de que para ese momento va a haber cargado lo necesario. La hago public porque quiero que el ChordVoicing pueda acceder a ella, para determinar la velocidad a la que hacer los arpegios.

        public static void Initialize()
        {
            chordPlaying.Add("Randomize", new Bang(new Vector2(LayoutManager.marginLeft, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 2), "Randomize Chord [ left ]", (int)BangCategory.Chord, true));
            chordPlaying.Add("Repeat", new Bang(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 2), "Repeat Chord [ right ]", (int)BangCategory.Chord, false));
            chordPlaying.Add("Arpeggiate", new Bang(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 2, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 2), "Repeat as Arpeggio [ up ]", (int)BangCategory.Chord, false));
            slowerArpeggiations = new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 3, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 2), "Slower Arpeggiations", (int)CheckboxCategory.Option, true, false);

            chordPlaying["Randomize"].BangClickedWithLeftButton += OnRandomizeBangClickedWithLeftButton;
            chordPlaying["Repeat"].BangClickedWithLeftButton += OnRepeatBangClickedWithLeftButton;
            chordPlaying["Arpeggiate"].BangClickedWithLeftButton += OnArpeggiateBangClickedWithLeftButton;
            slowerArpeggiations.CheckboxClickedWithLeftButton += OnSlowerArpeggiationsCheckboxClickedWithLeftButton;
        }

        static void OnRandomizeBangClickedWithLeftButton(Bang bang, int category, bool stateEnabled)
        {
            ChordAnsweringManager.evaluatedChordTypeAnswer = "";
            ChordAnsweringManager.evaluatedChordInversionAnswer = "";
            ChordAnsweringManager.chordDescriptionPart1 = "";
            ChordAnsweringManager.chordDescriptionPart2 = "";
            ChordAnsweringManager.allowAnswer = true;
            chord1.GenerateRandomVoicing(); //llamo a la función de chord1 "GenerateRandomVoicing"
            chord1.PlaySimultaneous(); //llamo a la función de chord1 "Play"

            if (_alreadyRandomizedFirstChord == false)// solo quiero permitir que se repitan acordes una vez que haya randomizado un acorde
            {
                chordPlaying["Repeat"].stateEnabled = true;
                chordPlaying["Arpeggiate"].stateEnabled = true;
                _alreadyRandomizedFirstChord = true;
            }

        }
        static void OnRepeatBangClickedWithLeftButton(Bang bang, int category, bool stateEnabled)
        {
            chord1.PlaySimultaneous(); //llamo a la función de chord1 "Play"
        }
        static void OnArpeggiateBangClickedWithLeftButton(Bang bang, int category, bool stateEnabled)
        {
            chord1.PlayArpeggio(); //llamo a la función de chord1 "PlayArpeggio"
        }
        static void OnSlowerArpeggiationsCheckboxClickedWithLeftButton(Checkbox checkbox, int category, bool stateEnabled, bool stateSelected)
        {
            if (stateSelected) // acá solo cambio de estado el checkbox; el cambio de la velocidad se va a dar en la clase ChordVoicing, mirando si el checkbox está seleccionado.
            {
                checkbox.stateSelected = false;

            }
            else
            {
                checkbox.stateSelected = true;
            }
        }

        public static void GenerateShortcut() //función que se llama si uso el shortcut para generar acorde (tecla izquierda)
        {
            chordPlaying["Randomize"].OnBangClickedWithLeftButton(); //Si uso el shortcut, quiero hacer lo mismo que si clickeara ese bang con click izquierdo
        }
        public static void PlaySimultaneousShortcut() //función que se llama si uso el shortcut para tocar acorde (tecla derecha)
        {
            chordPlaying["Repeat"].OnBangClickedWithLeftButton();  //Si uso el shortcut, quiero hacer lo mismo que si clickeara esel bang con click izquierdo
        }
        public static void PlayArpeggioShortcut() //función que se llama si uso el shortcut para arpegiar acorde (tecla arriba)
        {
            chordPlaying["Arpeggiate"].OnBangClickedWithLeftButton(); //Si uso el shortcut, quiero hacer lo mismo que si clickeara ese bang con click izquierdo
        }



        public static void Update(GameTime gameTime, MouseState mouseState)
        {
            //llamo a la función Update de los bangs
            foreach (var bang in chordPlaying)
            {
                bang.Value.Update(gameTime, mouseState);
            }

            slowerArpeggiations.Update(mouseState);

            chord1.UpdateArpeggio(gameTime); //en cada update corro a la función "UpdateArpeggio" del acorde.

        }


        public static void Draw(SpriteBatch spriteBatch)
        {
            //llamo a la función Draw de los bangs
            foreach (var bang in chordPlaying)
            {
                bang.Value.Draw(spriteBatch);
            }

            slowerArpeggiations.Draw(spriteBatch);
        }
    }
}
