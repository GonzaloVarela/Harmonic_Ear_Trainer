using Microsoft.Xna.Framework; //para usar GameTime
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonicEarTrainer
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


        public static bool IsAnyOfTheseKeysPressedJustNow(params Keys[] keys) //recibe una cantidad cualquiera de teclas y devuelve true si alguna de ellas está presionada (la puede llamar una checkbox o bang en el update para saber si se presionó alguno de los keyboard shortcuts que tiene asignados, por ejemplo)
        {
            foreach (var key in keys)
            {
                if (IsKeyPressedJustNow(key)) return true; //si la función "IsKeyPressedJustNow" devuelve verdadero para alguna de las keys que haya asignado a la función "IsAnyOfTheseKeysPressedJustNow" se devuelve true.
            }
            return false; //si no se cumplió lo anterior para ninguna de las keys que le llegó a esta función, se va a llegar a esta línea de código y se va a devolver false.
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
