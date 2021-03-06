﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;

namespace HarmonicEarTrainer
{
    /// <summary>
    /// 
    /// </summary>
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Random randomize = new Random(); //creo una instancia de la Class "Random" que llamo "randomize", con la que voy a randomizar los números correspondientes a tipo de acorde, inversión del acorde y fundamental de acorde.

        public static SpriteFont fontDefault; //Creo una variable para luego cargar el font que voy a usar por defecto.
        public static SpriteFont fontTitle; //Creo una variable para luego cargar el font para el título.

        // creo variables para los colores de fontDefault que voy a usar
        public static Color fontColorDefault { get; set; } = Color.Black;
        public static Color fontColorForCorrectAnswer { get; set; } = Color.Blue;
        public static Color fontColorForWrongAnswer { get; set; } = Color.Red;

        public static Texture2D background { get; set; }
        public static Texture2D checkboxSelected;
        public static Texture2D checkboxUnselected;
        public static Texture2D bangSelected;
        public static Texture2D bangUnselected;

        public static string gameName = "Harmonic Ear Trainer";

        //Creo las variables de tipo "SoundEffect" donde voy a cargar los archivos de sonido con las notas correspondientes.
        public static SoundEffect[] noteSoundFiles = new SoundEffect[128]; //Creo un array de SoundEffects de 128 elementos, para cargar los archivos de sonido correspondientes.

        public static Vector2 windowSize = new Vector2(1076, 980); // tamaño de la ventana

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            Window.Title = gameName; // cambio el título de la ventana
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
            graphics.PreferredBackBufferWidth = (int)windowSize.X; //el Vector2 trabaja con floats, así que acá preciso castear a Int
            graphics.PreferredBackBufferHeight = (int)windowSize.Y; //el Vector2 trabaja con floats, así que acá preciso castear a Int
            graphics.ApplyChanges();


            Dictionaries.CreateDictionaries(); //llamo a la función "CreateDictionaries" (que está en la Class "Dictionaries", y que crea los diccionarios (pares de integers y strings) con los nombres e índices correspondientes de tipos de acorde y demás (


            IsMouseVisible = true; //pongo esto así puedo ver el mouse sobre la aplicación

            base.Initialize(); //esto llama a la función "LoadContent"

            LayoutManager.Initialize();
            AudioManager.Initialize(); //llamo a la función "Initialize" del Audio Manager, que es necesario hacerlo luego de cargar contenido, así puedo acceder al fontDefault (obs.: como AudioManager es static no es necesario inicializar una instancia)
            ChordListManager.Initialize(); //llamo a la función "Initialize" del Audio Manager, que es necesario hacerlo luego de cargar contenido, así puedo acceder al fontDefault (obs.: como AudioManager es static no es necesario inicializar una instancia)
            ChordPlayingManager.Initialize();
            ChordAnsweringManager.Initialize();
            ScoreManager.Initialize();

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

            // Cargo el fontDefault a la variable "fontDefault"
            fontDefault = Content.Load<SpriteFont>("Fonts/Arial");
            fontTitle = Content.Load<SpriteFont>("Fonts/Calibri");

            // Cargo las imágenes
            background = Content.Load<Texture2D>("Images/background");
            checkboxSelected = Content.Load<Texture2D>("Images/checkbox_selected");
            checkboxUnselected = Content.Load<Texture2D>("Images/checkbox_unselected");
            bangSelected = Content.Load<Texture2D>("Images/bang_selected");
            bangUnselected = Content.Load<Texture2D>("Images/bang_unselected");



            //cargo los archivos de sonidos de notas a las variables correspondientes del array "noteSoundFiles" 
            //serían 128 archivos, a cada índice 0-127 le correspondiéndole la nota con ese mismo número de altura MIDI, solo que en realidad solo cargo sonidos para las notas de índices 48-95 (octavas 3-6, que son las que uso), y para el resto cargo un archivo de audio vacío (es importante cargar algo, porque he visto que a veces se intenta tocar la nota 0 si al iniciar la aplicación toco un acorde y muy rápidamente toco el botón de ver respuesta)
            for (int i = 0; i < noteSoundFiles.Length; i++)
            {
                if (i > 47 && i < 96)
                {
                    noteSoundFiles[i] = Content.Load<SoundEffect>($"Audio/note_{i}");
                }
                else
                {
                    noteSoundFiles[i] = Content.Load<SoundEffect>("Audio/note_null");
                }
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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

            InputManager.Update();

            AudioManager.Update(InputManager.mouseState); //también a la función Update de AudioManager
            ChordListManager.Update(gameTime, InputManager.mouseState);
            ChordPlayingManager.Update(gameTime, InputManager.mouseState);
            ChordAnsweringManager.Update(gameTime, InputManager.mouseState);
            ScoreManager.Update(gameTime, InputManager.mouseState);

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
            spriteBatch.Draw(background, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);

            LayoutManager.Draw(spriteBatch);
            AudioManager.Draw(spriteBatch);
            ChordListManager.Draw(spriteBatch);
            ChordPlayingManager.Draw(spriteBatch);
            ChordAnsweringManager.Draw(spriteBatch);
            ScoreManager.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
