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

    public class Checkbox
    {
        public bool stateEnabled { get; set; } //que el checkbox esté enabled o no es independiente de que esté selected. Si es true se va a mostrar con el color azul, sino con el color rojo
        public bool stateSelected { get; set; } //si es true se muestra la imagen con tick, si es false se muestra la imagen sin tick

        bool _stateHovering = false; //si el mouse está haciendo hovering quiero que cambie el color del button.

        Vector2 _position;
        string _label; //texto que se muestra al lado del checkbox
        Vector2 _labelSize; // para almacenar el tamaño que ocupa el label (importante porque lo quiero hacer clickeable)
        int _category; // acá voy a asignar un int que corresponda a la "categoría" de la instancia de checkbox, para desde afuera poder si quiero trabajar con todos los checkbox de una categoría al mismo tiempo o cosas así. Me conviene hacer un enum CheckboxCategory, y pasar como categoría del checkbox valores del enum casteados como int (así el checkbox es más independiente porque simplemente le entran ints como categoría, pero yo desde afuera del checkbox puedo pensar las categorías con nombres de acuerdo al proyecto).
        Color color { get; set; } //color de la imagen del checkbox y su label (cuando el checkbox esté enabled será azul cuando esté disabled será rojo).


        int _imageSize = 20;
        int _spaceBetweenImageAndLabel = 10;

        public delegate void CheckboxClickedEventHandler(Checkbox checkbox, int category, bool stateEnabled, bool stateSelected); //declaro el delegate que voy a usar para relacionar cada checkbox con uno o más methods (para darle funcionalidad). Los methods deberán recibir los paráetros especificados.
        public event CheckboxClickedEventHandler CheckboxClickedWithLeftButton; //declaro la variable (event, en este caso) que tiene como tipo el delegate, y que voy a poder llamar desde esta clase cuando se clickee el checkbox con botón izquierdo, pero va a apuntar a una o más funciones externas a esta clase,
        public event CheckboxClickedEventHandler CheckboxClickedWithRightButton; //declaro la variable (event, en este caso) que tiene como tipo el delegate, y que voy a poder llamar desde esta clase cuando se clickee el checkbox con botón derecho, pero va a apuntar a una o más funciones externas a esta clase,


        public Checkbox(Vector2 position, string label, int category, bool initStateEnabled, bool initStateSelected) //hago un constructor para cargar las variables
        {
            _position = position;
            _label = label;
            _labelSize = Main.fontDefault.MeasureString(label);
            _category = category;
            stateEnabled = initStateEnabled;
            stateSelected = initStateSelected;
        }


        public void Update(MouseState mouseState)
        {
            // checkeo si el mouse se encuentra entre los límites del button (incluyendo imagen, label, y espacio entre imagen y label)
            if (mouseState.X >= _position.X && mouseState.X < _position.X + _imageSize + _spaceBetweenImageAndLabel + _labelSize.X && mouseState.Y >= _position.Y && mouseState.Y < _position.Y + _imageSize)
            {
                _stateHovering = true;

                //checkeo si el mouse recién se clickeó, y en ese caso llamo a la función correspondiente
                if (InputManager.IsLeftButtonPressedJustNow() == true)
                {
                    OnCheckboxClickedWithLeftButton();
                }

                if (InputManager.IsRightButtonPressedJustNow() == true)
                {
                    OnCheckboxClickedWithRightButton();
                }
            }
            else
            {
                _stateHovering = false;
            }
        }

        public virtual void OnCheckboxClickedWithLeftButton() //al clickear en el la imagen quiero seleccionar/deseleccionar el checkbox.
        {
            if (CheckboxClickedWithLeftButton != null) CheckboxClickedWithLeftButton(this, _category, stateEnabled, stateSelected); //Si la variable no es null (o sea, si apunta a al menos una función, es decir que alguien se "suscribió a ese event) anuncio el event. Qué es exactamente lo que sucede cuando esto ocurra será determinado por aquellas funciones a las que el event apunte (aquellos "Event handlers" que se hayan subscripto al event).
            
            //Obs.: Acá no pongo que al clickear si el botón está seleccionado se deselecciona y viceversa, porque no siempre quiero que eso sea así. Por ejemplo, en el caso de un checkbox de tipo de acorde, si lo clickeo más de una vez seguida no quiero que se de-seleccione. 
        }

        public virtual void OnCheckboxClickedWithRightButton() //al clickear en el label quiero habilitar/deshabilitar el checkbox.
        {
            if (CheckboxClickedWithRightButton != null) CheckboxClickedWithRightButton(this, _category, stateEnabled, stateSelected); //Si la variable no es null (o sea, si apunta a al menos una función, es decir que alguien se "suscribió a ese event) anuncio el event. Qué es exactamente lo que sucede cuando esto ocurra será determinado por aquellas funciones a las que el event apunte (aquellos "Event handlers" que se hayan subscripto al event).
        }

        public void Draw(SpriteBatch spriteBatch) // a la función Draw le paso un SpriteBatch, así no tengo que hacer Begin a un nuevo SpriteBatch dentro de esta función, sino que puedo usar un SpriteBatch que ya haya comenzado.
        {
            //Cambio el color dependiendo del estado del checkbox
            if (_stateHovering == true) color = Color.Green;
            else if (stateEnabled == true) color = Color.Blue;
            else color = Color.Red;

            //cambio la imagen dependiendo de si el checkbox está seleccionado o no
            if (stateSelected == true)
            {
                spriteBatch.Draw(Main.checkboxSelected, new Rectangle((int)_position.X, (int)_position.Y, _imageSize, _imageSize), color); //como los Vector2 trabajan con floats y acá necesito ints, casteo los valores como ints.
                spriteBatch.DrawString(Main.fontDefault, _label, new Vector2(_position.X + _imageSize + _spaceBetweenImageAndLabel, (int)_position.Y), color);
            }
            else
            {
                spriteBatch.Draw(Main.checkboxUnselected, new Rectangle((int)_position.X, (int)_position.Y, _imageSize, _imageSize), color);
                spriteBatch.DrawString(Main.fontDefault, _label, new Vector2(_position.X + _imageSize + _spaceBetweenImageAndLabel, (int)_position.Y), color);
            }

        }
    }
}
