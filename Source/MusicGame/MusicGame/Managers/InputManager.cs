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

        public static KeyboardState currentKeyboardState;
        public static KeyboardState previousKeyboardState;

        public static MouseState currentMouseState;
        public static MouseState previousMouseState;

        public static bool IsKeyPressedJustNow(Keys key) //función que puedo llamar cuando quiera para determinar si una tecla recién se apretó (devuelve negativo si estaba apretado de antes)
        {
            return (currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key));

        }

        public static bool IsLeftButtonPressedJustNow() //función que puedo llamar cuando quiera para determinar si el botón izquierdo recién se apretó (devuelve negativo si estaba apretado de antes)
        {
             return (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton != ButtonState.Pressed);

        }
        public static bool IsRightButtonPressedJustNow() //función que puedo llamar cuando quiera para determinar si el botón derecho recién se apretó (devuelve negativo si estaba apretado de antes)
        {
            return (currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton != ButtonState.Pressed);

        }
        public static void Update(GameTime gameTime) //no quiero tener que inicializar una instancia de Input para usar esta función, por eso la hago static.
        {
            //checkeo los estados de teclado y mouse.
            //en cada frame le doy a las variables del estado anterior los valor que hasta ahora tenían las variables de estado actual, y luego checkeo los inputs para actualizar los estados actuales

            previousKeyboardState = currentKeyboardState; 
            currentKeyboardState = Keyboard.GetState();


            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }

    }
}
