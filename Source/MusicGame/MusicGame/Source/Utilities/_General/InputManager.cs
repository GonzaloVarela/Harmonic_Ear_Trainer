using Microsoft.Xna.Framework; //para usar GameTime
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame
{
    static class InputManager
    {

        public static KeyboardState keyboardState;
        public static KeyboardState keyboardStatePrevious;

        public static MouseState mouseState;
        public static MouseState mouseStatePrevious;

        public static bool IsKeyPressedJustNow(Keys key) //función que puedo llamar cuando quiera para determinar si una tecla recién se apretó (devuelve negativo si estaba apretado de antes)
        {
            return (keyboardState.IsKeyDown(key) && !keyboardStatePrevious.IsKeyDown(key));

        }

        public static bool IsLeftButtonPressedJustNow() //función que puedo llamar cuando quiera para determinar si el botón izquierdo recién se apretó (devuelve negativo si estaba apretado de antes)
        {
             return (mouseState.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton != ButtonState.Pressed);

        }
        public static bool IsRightButtonPressedJustNow() //función que puedo llamar cuando quiera para determinar si el botón derecho recién se apretó (devuelve negativo si estaba apretado de antes)
        {
            return (mouseState.RightButton == ButtonState.Pressed && mouseStatePrevious.RightButton != ButtonState.Pressed);

        }
        public static void Update() //no quiero tener que inicializar una instancia de Input para usar esta función, por eso la hago static.
        {
            //checkeo los estados de teclado y mouse.
            //en cada frame le doy a las variables del estado anterior los valor que hasta ahora tenían las variables de estado actual, y luego checkeo los inputs para actualizar los estados actuales

            keyboardStatePrevious = keyboardState; 
            keyboardState = Keyboard.GetState();


            mouseStatePrevious = mouseState;
            mouseState = Mouse.GetState();
        }
    }
}
