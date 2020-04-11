using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System; //necesaria para random
using System.Diagnostics; //necesaria para debugging
using Microsoft.Xna.Framework.Audio; //para cargar WAVs.

namespace Prueba1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game
    {


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Random randomize = new Random(); //creo una instancia de la Class "Random" que llamo "randomize", con la que voy a randomizar los números correspondientes a tipo de acorde, inversión del acorde y fundamental de acorde.

        SpriteFont font; //Creo una variable para luegocargar el font que voy a usar.

        KeyboardState previousKeyboardState;

        // en estos string voy a guardar los textos de descripción del acorde.
        string chordDescriptionPart1 = "";
        string chordDescriptionPart2 = "";

        //Creo las variables de tipo "SoundEffect" donde voy a cargar los archivos de sonido con las notas correspondientes.
        public static SoundEffect[] noteSoundFiles = new SoundEffect[128]; //Creo un array de SoundEffects de 128 elementos, para cargar los archivos de sonido correspondientes.
        static float audioVolume = 1f; //Creo una variable "audioVolume", que inicializo en 1 pero que estaría bueno que el jugador pudiera determinar.
        static bool audioIsMuted = false; //creo una variable para guardar la información de si el audio está muteado o no.

        Chord chord1 = new Chord(); //genero una instancia de la clase Chord llamada "chord1"

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Dictionaries.CreateDictionaries(); //llamo a la función "CreateDictionaries" (que está en la Class "Dictionaries", y que crea los diccionarios (pares de integers y strings) con los nombres e índices correspondientes de tipos de acorde y demás (

            SoundEffect.MasterVolume = audioVolume; //hago que el volumen de los sonidos sea el que diga la variable "audioVolume" (por defect 1).

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // Cargo el font a la variable "font"
            font = Content.Load<SpriteFont>("Fonts/Arial");

            //cargo los archivos de sonidos de notas a las variables correspondientes del array "noteSoundFiles" (128 archivos, índices a cada índice 0-127 le corresponde la nota con ese mismo número de altura MIDI)
            for (int i = 0; i < noteSoundFiles.Length; i++)
            {
                noteSoundFiles[i] = Content.Load<SoundEffect>("Audio/note_" + (i));
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

            //hago dispose de los archivos de sonidos de notas de las variables del array "noteSoundFiles" (índices 0-23)
            for (int i = 0; i < noteSoundFiles.Length; i++)
            {
                noteSoundFiles[0].Dispose();
            }
            noteSoundFiles = null;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            if (IsPressedNow(Keys.Left))
            {
                chordDescriptionPart1 = "";
                chordDescriptionPart2 = "";
                chord1.Generate(); //llamo a la función de chord1 "Generate"
                chord1.PlaySimultaneous(); //llamo a la función de chord1 "Play"
            }

            if (IsPressedNow(Keys.Right))
            {
                chord1.PlaySimultaneous(); //llamo a la función de chord1 "Play"
            }

            if (IsPressedNow(Keys.Up))
            {
                chord1.PlayArpeggio(); //llamo a la función de chord1 "PlayArpeggio"
            }

            if (IsPressedNow(Keys.Down))
            {
                //En la descripción de part1, para estas variables uso los diccionarios (que cargo de la class "Dictionaries", y como índices de los diccionarios uso valores de chord1.
                //Los casos del chordType y el chordInversion son más elaborados, porque para obtener los índices de los diccionarios como integers (que es como los preciso) debo convertir a integers los valores de las variables type e inversion, alojadas en chord1 (como uso enums esos valores son de tipo ChordType y ChordInversion, por eso debo convertirlos).
                chordDescriptionPart1 = $"The random chord generated was {Dictionaries.pitchClass[chord1.noteRootPitchClass]} {Dictionaries.chordType[(int)chord1.type].ToLower()} in {Dictionaries.chordInversion[(int)chord1.inversion].ToLower()}"; //(paso los nombres de los diccionarios chordType y chordInversion a lowercase)
                chordDescriptionPart2 = $"The MIDI pitches were (in order from low to high) {chord1.noteBassPitchMidi}, {chord1.noteTenorPitchMidi}, {chord1.noteAltoPitchMidi} and {chord1.noteSopranoPitchMidi} ({Dictionaries.pitchMidi[chord1.noteBassPitchMidi]}, {Dictionaries.pitchMidi[chord1.noteTenorPitchMidi]}, {Dictionaries.pitchMidi[chord1.noteAltoPitchMidi]} and {Dictionaries.pitchMidi[chord1.noteSopranoPitchMidi]}).";
            }

            if (IsPressedNow(Keys.Space))
            {
                if (!audioIsMuted) // si el audio no está muteado, mutear
                {
                    SoundEffect.MasterVolume = 0f;
                    audioIsMuted = true;
                }
                else // si el audio está muteado, desmutear
                {
                    SoundEffect.MasterVolume = audioVolume;
                    audioIsMuted = false;
                }

            }


            previousKeyboardState = Keyboard.GetState();


            bool IsPressedNow(Keys key)
            {
                var keyboardState = Keyboard.GetState();

                //Checkea que se esté tocando la tecla y que no se estuviera tocando la tecla de antes, y solo en ese caso devuelve true.
                return keyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);

            }

            chord1.ArpeggioUpdate(gameTime); //en cada update corro a la función "ArpeggioUpdate" del acorde.

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();


            spriteBatch.DrawString(font, "Left arrow randomizes chord, Right arrow repeats chord.", new Vector2(0, 0), Color.Black);
            spriteBatch.DrawString(font, "Up arrow arpeggiates chord.", new Vector2(0, 50), Color.Black);
            spriteBatch.DrawString(font, "Down arrow displays Chord Description.", new Vector2(0, 100), Color.Black);
            spriteBatch.DrawString(font, "Space bar mutes audio.", new Vector2(0, 150), Color.Black);
            if (audioIsMuted) spriteBatch.DrawString(font, "THE AUDIO IS MUTED", new Vector2(0, 200), Color.Red);
            spriteBatch.DrawString(font, chordDescriptionPart1, new Vector2(0, 250), Color.Black);
            spriteBatch.DrawString(font, chordDescriptionPart2, new Vector2(0, 300), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
