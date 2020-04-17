using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System; //necesaria para random
using System.Diagnostics; //necesaria para debugging
using Microsoft.Xna.Framework.Audio; //para cargar WAVs.

namespace MusicGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game
    {


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //defino márgenes de la ventana que voy a tomar en cuenta cada vez que dibuje algo
        public static int marginHorizontal = 20;
        public static int marginVertical = 20;

        //defino separaciones de filas y columnas que voy a tomar en cuenta cuando dibuje texto
        public static int positionRowSeparation = 40;
        public static int positionColumnSeparation = 270;
        // public static int positionSmallRowSeparation = 30;
        // public static int positionSmallColumnSeparation = 10;



        public static Random randomize = new Random(); //creo una instancia de la Class "Random" que llamo "randomize", con la que voy a randomizar los números correspondientes a tipo de acorde, inversión del acorde y fundamental de acorde.

        public static SpriteFont font; //Creo una variable para luego cargar el font que voy a usar.

        public static Texture2D checkboxSelected;
        public static Texture2D checkboxUnselected;


        // en estos string voy a guardar los textos de descripción del acorde.
        string chordDescriptionPart1 = "";
        string chordDescriptionPart2 = "";

        //Creo las variables de tipo "SoundEffect" donde voy a cargar los archivos de sonido con las notas correspondientes.
        public static SoundEffect[] noteSoundFiles = new SoundEffect[128]; //Creo un array de SoundEffects de 128 elementos, para cargar los archivos de sonido correspondientes.

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

            //Cambio el tamaño de la ventana
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();


            Dictionaries.CreateDictionaries(); //llamo a la función "CreateDictionaries" (que está en la Class "Dictionaries", y que crea los diccionarios (pares de integers y strings) con los nombres e índices correspondientes de tipos de acorde y demás (


            IsMouseVisible = true; //pongo esto así puedo ver el mouse sobre la aplicación

            base.Initialize(); //esto llama a la función "LoadContent"


            AudioManager.Initialize(); //llamo a la función "Initialize" del Audio Manager, que es necesario hacerlo luego de cargar contenido, así puedo acceder al font (obs.: como AudioManager es static no es necesario inicializar una instancia)

            ChordListManager.Initialize(); //llamo a la función "Initialize" del Audio Manager, que es necesario hacerlo luego de cargar contenido, así puedo acceder al font (obs.: como AudioManager es static no es necesario inicializar una instancia)


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

            // Cargo las imágenes de los checkbox
            checkboxSelected = Content.Load<Texture2D>("Images/checkbox_selected");
            checkboxUnselected = Content.Load<Texture2D>("Images/checkbox_unselected");



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


            if (InputManager.IsKeyPressedJustNow(Keys.Left))
            {
                chordDescriptionPart1 = "";
                chordDescriptionPart2 = "";
                chord1.Generate(); //llamo a la función de chord1 "Generate"
                chord1.PlaySimultaneous(); //llamo a la función de chord1 "Play"
            }

            if (InputManager.IsKeyPressedJustNow(Keys.Right))
            {
                chord1.PlaySimultaneous(); //llamo a la función de chord1 "Play"
            }

            if (InputManager.IsKeyPressedJustNow(Keys.Up))
            {
                chord1.PlayArpeggio(); //llamo a la función de chord1 "PlayArpeggio"
            }

            if (InputManager.IsKeyPressedJustNow(Keys.Down))
            {
                //En la descripción de part1, para estas variables uso los diccionarios (que cargo de la class "Dictionaries", y como índices de los diccionarios uso valores de chord1.
                //Los casos del chordType y el chordInversion son más elaborados, porque para obtener los índices de los diccionarios como integers (que es como los preciso) debo convertir a integers los valores de las variables type e inversion, alojadas en chord1 (como uso enums esos valores son de tipo ChordType y ChordInversion, por eso debo convertirlos).
                chordDescriptionPart1 = $"The random chord generated was {Dictionaries.pitchClass[chord1.noteRootPitchClass]} {Dictionaries.chordType[(int)chord1.type].ToLower()} in {Dictionaries.chordInversion[(int)chord1.inversion].ToLower()}"; //(paso los nombres de los diccionarios chordType y chordInversion a lowercase)
                chordDescriptionPart2 = $"The MIDI pitches were (in order from low to high) {chord1.noteBassPitchMidi}, {chord1.noteTenorPitchMidi}, {chord1.noteAltoPitchMidi} and {chord1.noteSopranoPitchMidi} ({Dictionaries.pitchMidi[chord1.noteBassPitchMidi]}, {Dictionaries.pitchMidi[chord1.noteTenorPitchMidi]}, {Dictionaries.pitchMidi[chord1.noteAltoPitchMidi]} and {Dictionaries.pitchMidi[chord1.noteSopranoPitchMidi]}).";
            }

            if (InputManager.IsKeyPressedJustNow(Keys.Space))
            {
                AudioManager.AudioToggleWithShortcut(); //llamo a la función "AudioToggleWithShortcut" del AudioManager, que se va a encargar de habilitar o deshabilitar el audio (y mostrar el efecto) según corresponda).
            }


            chord1.UpdateArpeggio(gameTime); //en cada update corro a la función "UpdateArpeggio" del acorde.
            InputManager.Update(gameTime); //también el update del InputManager, que checkea estados de teclado y mouse (qué está o no está apretado)

            AudioManager.Update(InputManager.currentMouseState); //también a la función Update de AudioManager
            ChordListManager.Update(InputManager.currentMouseState);



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


            spriteBatch.DrawString(font, "Left arrow randomizes chord, Right arrow repeats chord.", new Vector2(marginHorizontal, marginVertical), Color.Black);
            spriteBatch.DrawString(font, "Up arrow arpeggiates chord.", new Vector2(marginHorizontal, marginVertical + positionRowSeparation), Color.Black);
            spriteBatch.DrawString(font, "Down arrow displays chord description.", new Vector2(marginHorizontal, marginVertical + positionRowSeparation * 2), Color.Black);
            spriteBatch.DrawString(font, chordDescriptionPart1, new Vector2(marginHorizontal, marginVertical + positionRowSeparation * 3), Color.Green);
            spriteBatch.DrawString(font, chordDescriptionPart2, new Vector2(marginHorizontal, marginVertical + positionRowSeparation * 4), Color.Green);


            AudioManager.Draw(spriteBatch); //llamo a la función "Draw" del AudioManager
            ChordListManager.Draw(spriteBatch);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
