using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MusicGame
{
    static class AudioManager
    {

        static Checkbox _allowAudio = new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 2, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 18), "Audio [ space ]", (int)CheckboxCategory.Option, true, true); //inicializo una instancia de la class "Checkbox", que voy a usar para habilitar o deshabilitar el audio.

        public static float audioVolume = 1f; //Creo una variable "audioVolume", que inicializo en 1 pero que luego haré que el jugador pueda determinar con un slider.

        public static void Initialize()
        {
            SoundEffect.MasterVolume = audioVolume; //hago que el volumen de los sonidos sea el que diga la variable "audioVolume" (por defecto 1).
            //asocio la función "OnCheckboxClickedWithLeftButton()" con la variable las dos variables de clickear en una checkbox (la de clickear en la imagen y la de clickear en el label), que tienen como tipo un delegate.
            _allowAudio.CheckboxClickedWithLeftButton += OnCheckboxClickedWithLeftButton;
        }


        static void OnCheckboxClickedWithLeftButton(Checkbox checkbox, int category, bool stateEnabled, bool stateSelected)
        {
            if (stateSelected) // si hago click con botón izq en la checkbox cuando la checkbox está seleccionada, desactivo el audio, y deselecciono el checkbox.
            {
                SoundEffect.MasterVolume = 0f;
                checkbox.stateSelected = false;
            }
            else  // si hago click con botón izq en la checkbox cuando la checkbox está deseleccionada, activo el audio, y selecciono el checkbox.
            {
                SoundEffect.MasterVolume = audioVolume;
                checkbox.stateSelected = true;
            }
        }


        public static void AllowAudioShortcut() //función que se llama si uso el shortcut para habilitar o deshabilitar el audio (tecla Space)
        {
            _allowAudio.OnCheckboxClickedWithLeftButton(); //Si uso el shortcut, quiero hacer lo mismo que si clickeara ese checkbox con click izquierdo
        }


        public static void Update(MouseState mouseState)
        {
            _allowAudio.Update(mouseState); //llamo a la función Update de checkbox
        }


        public static void Draw(SpriteBatch spriteBatch) // a la función Draw le paso un SpriteBatch, así no tengo que hacer Begin a un nuevo SpriteBatch dentro de esta función, sino que puedo usar un SpriteBatch que ya haya comenzado.
        {
            _allowAudio.Draw(spriteBatch); //llamo a la función "Draw" del checkbox
        }
    }
}
