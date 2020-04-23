using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame
{
    static class ScoreManager
    {
        public static Dictionary<string, Bang> ChordAnswering = new Dictionary<string, Bang>(); //Pongo todos los bangs de ChordAnswering en un dictionary, así puedo acceder por ellos por su "nombre" (string que asigno como key) pero también puedo iterar por ellos.

        //variables para guardar el score
        public static int correctTypes = 0;
        public static int correctTypesPercentage = 0;
        public static int correctInversions = 0;
        public static int correctInversionsPercentage = 0;
        public static int totalAnswers = 0;
        static string _score; //para mostrar el texto de score

        private static Bang _reset = null; //no la referencio ahora sino en Initialize, así sé que para ese momento cargó todos los assets.



        public static void Initialize()
        {
            _reset = new Bang(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 3, LayoutManager.marginTop + LayoutManager.gridRowSeparation), "Reset Score", (int)BangCategory.Chord, true);

            _reset.BangClickedWithLeftButton += OnResetBangClickedWithLeftButton;

        }

        static void OnResetBangClickedWithLeftButton(Bang bang, int category, bool stateEnabled)
        {
            correctTypes = 0;
            correctTypesPercentage = 0;
            correctInversions = 0;
            correctInversionsPercentage = 0;
            totalAnswers = 0;
        }


        public static void Update(GameTime gameTime, MouseState mouseState)
        {
            _reset.Update(gameTime, mouseState);

            _score = $"CORRECT TYPES: {correctTypes} ({correctTypesPercentage}%) | CORRECT INVERSIONS: {correctInversions} ({correctInversionsPercentage}%) | TOTAL ANSWERS: {totalAnswers}";

        }


        public static void Draw(SpriteBatch spriteBatch)
        {
            _reset.Draw(spriteBatch);

            spriteBatch.DrawString(Main.fontDefault, _score, new Vector2(LayoutManager.marginLeft, LayoutManager.marginTop + LayoutManager.gridRowSeparation), Main.fontColorDefault);
        }
    }
}
