using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame
{
    static class KeyboardShortcutManager
    {

        public static void Update()
        {
            if (InputManager.IsKeyPressedJustNow(Keys.Left))
            {
                ChordPlayingManager.GenerateShortcut();
            }

            if (InputManager.IsKeyPressedJustNow(Keys.Right))
            {
                ChordPlayingManager.PlaySimultaneousShortcut();
            }

            if (InputManager.IsKeyPressedJustNow(Keys.Up))
            {
                ChordPlayingManager.PlayArpeggioShortcut();
            }

            if (InputManager.IsKeyPressedJustNow(Keys.Down))
            {
                ChordAnsweringManager.RevealShortcut();
            }

            if (InputManager.IsKeyPressedJustNow(Keys.D1))
            {
                ChordListManager.Preset1Shortcut();
            }

            if (InputManager.IsKeyPressedJustNow(Keys.D2))
            {
                ChordListManager.Preset2Shortcut();
            }

            if (InputManager.IsKeyPressedJustNow(Keys.D3))
            {
                ChordListManager.Preset3Shortcut();
            }

            if (InputManager.IsKeyPressedJustNow(Keys.Enter))
            {
                ChordAnsweringManager.SubmitShortcut();
            }

            if (InputManager.IsKeyPressedJustNow(Keys.Space))
            {
                AudioManager.AllowAudioShortcut();
            }

            if (InputManager.IsKeyPressedJustNow(Keys.D0))
            {
                ChordListManager.OnlyRootPositionShortcut();
            }

        }
    }
}
