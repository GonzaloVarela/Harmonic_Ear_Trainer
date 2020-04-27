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
        
        //en este caso en que las keys del diccionario van a ser strings, no quiero copiar siempre el nombre del string cada vez que quiera llamar a ese ítem del diccionario (porque si lo copio mal una vez no me doy cuenta, y si lo quiero cambiar de un lado lo tengo que cambiar de todos, etc.), así que almaceno los keys del diccionario en constantes, y cada vez que quiera acceder a un item del diccionario llamaré a la constante que tenga ese string (porque si llamo mal a una constante sí me va a avisar que hay un problema)
        const string DictionaryKeyForRandomize = "Randomize"; //constante que referencia al key correspondiente al bang para randomizar acorde.
        const string DictionaryKeyForRepeat = "Repeat"; //constante que referencia al key correspondiente al bang para repetir acorde.
        const string DictionaryKeyForArpeggiate = "Arpeggiate"; //constante que referencia al key correspondiente al bang para arpegiar acorde.


        public static ChordVoicing chord1 = new ChordVoicing(); //creo una instancia de la clase Chord llamada "chord1"

        private static bool _alreadyRandomizedFirstChord = false; // solo quiero permitir que se repitan acordes una vez que haya randomizado un acorde

        public static Checkbox slowerArpeggiations = null; //la asigno en el initialize, así me aseguro de que para ese momento va a haber cargado lo necesario. La hago public porque quiero que el ChordVoicing pueda acceder a ella, para determinar la velocidad a la que hacer los arpegios.


        public static void Initialize()
        {
            //creo los bangs, asocio los delegates a las funciones correspondientes, y luego los agrego al diccionario (podría agregarlos al diccionario y luego asociar sus delegates, pero eso creo que sería menos eficiente, porque buscar al bang correspondiente adentro de un diccionario lleva más tiempo que accederlo directamente, me parece)
            Bang currentBang = null; //voy a usar una variable llamada "currentBang" como carretilla, para ir referenciando a los bangs que creo, asignándoles una función a su delegate, y agregándolos al diccionario.

            currentBang = new Bang(new Vector2(LayoutManager.marginLeft, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 2), "Randomize Chord", (int)BangCategory.Chord, true, Keys.Left);
            currentBang.BangClickedWithLeftButton += OnRandomizeBangClickedWithLeftButton;
            chordPlaying.Add(DictionaryKeyForRandomize, currentBang);
            
            //una vez que agregué ese bang al diccionario, voy a acceder a él por medio del diccionario, así que paso a usar la variable "currentBang" para hacer lo mismo con los otros bangs.
            currentBang = new Bang(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 2), "Repeat Chord", (int)BangCategory.Chord, false, Keys.Right);
            currentBang.BangClickedWithLeftButton += OnRepeatBangClickedWithLeftButton;
            chordPlaying.Add(DictionaryKeyForRepeat, currentBang);
            
            currentBang = new Bang(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 2, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 2), "Repeat as Arpeggio", (int)BangCategory.Chord, false, Keys.Up);
            currentBang.BangClickedWithLeftButton += OnArpeggiateBangClickedWithLeftButton;
            chordPlaying.Add(DictionaryKeyForArpeggiate, currentBang);


            slowerArpeggiations = new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 3, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 2), "Slower Arpeggiations", (int)CheckboxCategory.Option, true, false, Keys.OemMinus, Keys.Subtract);

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

            if (_alreadyRandomizedFirstChord == false) //solo quiero permitir que se repitan acordes una vez que haya randomizado un acorde
            {
                chordPlaying[DictionaryKeyForRepeat].stateEnabled = true;
                chordPlaying[DictionaryKeyForArpeggiate].stateEnabled = true;
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
