using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame
{
    class LayoutManager // ACÁ PONER LOS TEXTOS QUE NO TIENEN VARIABLE, Y LOS GRID Y MARGINS , Y TAL VEZ INCLUSO EL TAMAÑO DE VENTANA
    {
        //defino márgenes de la ventana que voy a tomar en cuenta cada vez que dibuje algo
        public static int marginLeft = 20;
        public static int marginTop = 24;

        //defino separaciones de filas y columnas que voy a tomar en cuenta cuando dibuje texto
        public static int gridRowSeparation { get; private set; } = 40;
        public static int gridColumnSeparation { get; private set; } = 270;

        //voy a usar estas variables para calcular el origin del texto del título del juego (que quiero centrado)
        static Vector2 _titleSize;
        static Vector2 _centerOfWindow;
        static int _horizontalOriginOfTitle;

        public static void Initialize()
        {
            //calculo el origin del texto del título del juego(que quiero centrado)
            _titleSize = Main.fontTitle.MeasureString(Main.gameName.ToUpper()); //mido el tamaño del título del juego
            _centerOfWindow = Main.windowSize / 2; //consigo un Vector 2 con el punto central de la ventana (del que en realidad solo me interesa el eje de las X)
            _horizontalOriginOfTitle = (int)_centerOfWindow.X - (int)_titleSize.X / 2; //el origin horizontal del título
        }

        public static void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.DrawString(Main.fontTitle, Main.gameName.ToUpper(), new Vector2(_horizontalOriginOfTitle, 15), Main.fontColorDefault);
            spriteBatch.DrawString(Main.fontDefault, "Select Chord Type (right click to enable/disable):", new Vector2(marginLeft, marginTop + gridRowSeparation * 4), Main.fontColorDefault);
            spriteBatch.DrawString(Main.fontDefault, "Presets:", new Vector2(marginLeft, marginTop + gridRowSeparation * 12), Main.fontColorDefault);
            spriteBatch.DrawString(Main.fontDefault, "Select Chord Inversion:", new Vector2(marginLeft, marginTop + gridRowSeparation * 14), Main.fontColorDefault);
            spriteBatch.DrawString(Main.fontDefault, "Answer:", new Vector2(marginLeft, marginTop + gridRowSeparation * 17), Main.fontColorDefault);
            spriteBatch.DrawString(Main.fontDefault, "Options:", new Vector2(marginLeft + gridColumnSeparation * 2, marginTop + gridRowSeparation * 17), Main.fontColorDefault);
            spriteBatch.DrawString(Main.fontDefault, "v1.0 (revision: 2020.xx.xx)", new Vector2(marginLeft, marginTop + gridRowSeparation * 22), Main.fontColorDefault);
            spriteBatch.DrawString(Main.fontDefault, "Gonzalo Varela (gonzalo@gonzalovarela.com) (www.gonzalovarela.com)", new Vector2(marginLeft, marginTop + gridRowSeparation * 23), Main.fontColorDefault);

        }
    }
}
