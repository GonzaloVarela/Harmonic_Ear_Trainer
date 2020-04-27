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
    public class Bang
    {
        public bool stateEnabled { get; set; } //que el bang esté enabled o no es independiente de que esté selected. Si es true se va a mostrar con el color azul, sino con el color rojo
        public bool stateSelected { get; set; } = false; //si es true se muestra la imagen con el punto en el medio, si es false muestra la imagen sin el punto en el medio. Solo va a ser true por un instante, no es como el checkbox que se puede quedar true (por eso no uso esta variable en el constructor ni en los delegates).

        bool _stateHovering = false; //si el mouse está haciendo hovering quiero que cambie el color del bang.

        Vector2 _position;
        string _label; //texto que se muestra al lado del bang
        Vector2 _labelSize; // para almacenar el tamaño que ocupa el label (importante porque lo quiero hacer clickeable)
        int _category; // acá voy a asignar un int que corresponda a la "categoría" de la instancia de bang, para desde afuera poder si quiero trabajar con todos los bang de una categoría al mismo tiempo o cosas así. Me conviene hacer un enum BangCategory, y pasar como categoría del bang valores del enum casteados como int (así el bang es más independiente porque simplemente le entran ints como categoría, pero yo desde afuera del bang puedo pensar las categorías con nombres de acuerdo al proyecto).
        Color color { get; set; } //color de la imagen del bang y su label (cuando el bang esté enabled será azul cuando esté disabled será rojo).

        private double _selectionTimer = 0;    //timer para agendar cuando se debe de-seleccionar el botón (dejar de mostrar como presionado) una vez seleccionado

        int _imageSize = 20;
        int _spaceBetweenImageAndLabel = 10;

        public delegate void BangClickedEventHandler(Bang bang, int category, bool stateEnabled); //declaro el delegate que voy a usar para relacionar cada bang con uno o más methods (para darle funcionalidad). Los methods deberán recibir los paráetros especificados.
        public event BangClickedEventHandler BangClickedWithLeftButton; //declaro la variable (event, en este caso) que tiene como tipo el delegate, y que voy a poder llamar desde esta clase cuando se clickee el bang con botón izquierdo, pero va a apuntar a una o más funciones externas a esta clase,
        public event BangClickedEventHandler BangClickedWithRightButton; //declaro la variable (event, en este caso) que tiene como tipo el delegate, y que voy a poder llamar desde esta clase cuando se clickee el bang con botón derecho, pero va a apuntar a una o más funciones externas a esta clase,

        private Keys[] _keyboardShortcuts;

        public Bang(Vector2 position, string label, int category, bool initStateEnabled, params Keys[] keyboardShortcuts) //hago un constructor para cargar las variables, incluyendo la cantidad que se me ocurra de keyboard shortcuts
        {
            _position = position;
            _label = label;
            _category = category;
            stateEnabled = initStateEnabled;

            _keyboardShortcuts = keyboardShortcuts;

            //si incluyo keyboard shortcut(s), voy a agregar a la etiqueta un texto que los especifique
            if (_keyboardShortcuts.Length > 0)
            {
                _label += $" [{KeyboardShortcutTextManager.GetKeyboardShortcutText(_keyboardShortcuts)}]"; // la función "GetKeyboardShortcutText" va a devolver el texto que va entre los paréntesis rectos
            }

            _labelSize = Main.fontDefault.MeasureString(label);
        }

        public void Update(GameTime gameTime, MouseState mouseState)
        {
            //checkeo si se presionó cualquiera de los keyboard shortcuts, y en ese caso llamo a la misma función que llamo cuando clickeo el bang con el botón izquierdo del mouse.
            if (InputManager.IsAnyOfTheseKeysPressedJustNow(_keyboardShortcuts)) 
            {
                OnBangClickedWithLeftButton();
            }

            // checkeo si el mouse se encuentra entre los límites del bang (incluyendo imagen, label, y espacio entre imagen y label)
            if (mouseState.X >= _position.X && mouseState.X < _position.X + _imageSize + _spaceBetweenImageAndLabel + _labelSize.X && mouseState.Y >= _position.Y && mouseState.Y < _position.Y + _imageSize)
            {
                _stateHovering = true;

                //checkeo si el mouse recién se clickeó, y en ese caso llamo a la función correspondiente
                if (InputManager.IsLeftButtonPressedJustNow())
                {
                    OnBangClickedWithLeftButton();
                }

                if (InputManager.IsRightButtonPressedJustNow())
                {
                    OnBangClickedWithdRightButton();
                }
            }
            else
            {
                _stateHovering = false;
            }


            if (stateSelected)
            {

                _selectionTimer += gameTime.ElapsedGameTime.TotalMilliseconds; //al timer del de la selección (que funciona con milisegundos) le voy sumando el tiempo que pasa en cada frame (sin eso no sería un timer).
                if (_selectionTimer >= 200)
                {
                    stateSelected = false;
                    _selectionTimer = 0;
                }

            }
        }

        public virtual void OnBangClickedWithLeftButton() //al clickear en el la imagen quiero seleccionar/deseleccionar el bang.
        {
            if (stateEnabled) // checkeo si está enabled, el bang quiero que si no está enabled no se pueda seleccionar (a diferencia del checkbox)
            {
                BangClickedWithLeftButton?.Invoke(this, _category, stateEnabled);  //Si la variable no es null (o sea, si apunta a al menos una función, es decir que alguien se "suscribió a ese event) anuncio el event (la parte de "?.Invoke" es una mejor manera de checkear si lo de la izquierda es null que con un if statement). Qué es exactamente lo que sucede cuando esto ocurra será determinado por aquellas funciones a las que el event apunte (aquellos "Event handlers" que se hayan subscripto al event).
                stateSelected = true;
            }
        }

        public virtual void OnBangClickedWithdRightButton() //al clickear en el label quiero habilitar/deshabilitar el bang.
        {
            BangClickedWithRightButton?.Invoke(this, _category, stateEnabled);  //Si la variable no es null (o sea, si apunta a al menos una función, es decir que alguien se "suscribió a ese event) anuncio el event (la parte de "?.Invoke" es una mejor manera de checkear si lo de la izquierda es null que con un if statement). Qué es exactamente lo que sucede cuando esto ocurra será determinado por aquellas funciones a las que el event apunte (aquellos "Event handlers" que se hayan subscripto al event).
        }

        public void Draw(SpriteBatch spriteBatch) // a la función Draw le paso un SpriteBatch, así no tengo que hacer Begin a un nuevo SpriteBatch dentro de esta función, sino que puedo usar un SpriteBatch que ya haya comenzado.
        {
            //Cambio el color dependiendo del estado del bang
            if (_stateHovering) color = Color.Green;
            else if (stateEnabled) color = Color.Blue;
            else color = Color.Gray;

            //cambio la imagen dependiendo de si el bang está seleccionado o no
            if (stateSelected)
            {
                spriteBatch.Draw(Main.bangSelected, new Rectangle((int)_position.X, (int)_position.Y, _imageSize, _imageSize), color); //como los Vector2 trabajan con floats y acá necesito ints, casteo los valores como ints.
                spriteBatch.DrawString(Main.fontDefault, _label, new Vector2(_position.X + _imageSize + _spaceBetweenImageAndLabel, (int)_position.Y), color);
            }
            else
            {
                spriteBatch.Draw(Main.bangUnselected, new Rectangle((int)_position.X, (int)_position.Y, _imageSize, _imageSize), color);
                spriteBatch.DrawString(Main.fontDefault, _label, new Vector2(_position.X + _imageSize + _spaceBetweenImageAndLabel, (int)_position.Y), color);
            }

        }
    }
}
