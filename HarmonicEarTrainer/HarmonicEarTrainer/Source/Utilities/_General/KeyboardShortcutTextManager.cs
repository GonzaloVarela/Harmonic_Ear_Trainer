using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace HarmonicEarTrainer
{
    public static class KeyboardShortcutTextManager
    {
        public static string GetKeyboardShortcutText(Keys[] keyboardShortcuts) // esta función va a devolver el texto que debe ir entre los paréntesis rectos en un label de un objeto (por ejemplo bang o checkbox) que tenga keyboard shortcuts asignados.
        {
            string keyboardShortcutTextFull = ""; //string del texto completo que voy a agregar al label (entre los paréntesis rectos) para mostrar los keyboard shortcuts (en un principio está vacía)
            bool firstKeyboardShortcut = true; // cuando empiece a iterar la lista quiero que este bool sea true, así sé que no tengo que agregar el texto " or " (porque hay solo un shortcut). Eventualmente, si quiero que la label muestre más de un shortcut voy a modificar esta variable a false para todos los shortcutsque quiera mostrar posteriores al primero.

            foreach (var key in keyboardShortcuts) //itero entre todos los keyboard shortcuts que haya asignado cuando instancie el bang.
            {
                bool includeKeyboardShortcutTextForThisKeyboardShortcut = true; //por defecto voy a incluir el texto para cada key que haya agregado como keyboardShortcut, pero creo esta variable porque en algunos casos voy a querer no incluirlo (por ejemplo si tengo como shortcut la tecla "1" de ambos teclados numéricos, quiero que el label muestre solo un "1", y el otro lo ignore).
                string keyboardShortcutTextForThisKeyboardShortcut = ""; //string del texto correspondiente a este keyboard shortcut (por defecto está vacío). Este texto lo voy a agregar al string "keyboardShortcutTextFull" (que es el que la función va a devolver), siempre y cuando la variable "includeKeyboardShortcutTextForThisKeyboardShortcut" sea true.
                if (!firstKeyboardShortcut) keyboardShortcutTextForThisKeyboardShortcut += " or "; //para todos los shortcuts excepto el primero quiero antes del nombre del shortcut poner " or ".

                switch (key)
                {
                    //hay muchos keys que si los asigno como shortcuts quiero que se muestren tal cual están configurados en Monogame, pero otros requiero resolverlos de forma particular, por ejemplo porque quiero que se muestren distinto a como están asignados en el enum "Keys". Primero veo los casos que requieren resoluciones particulares.
                    //por ejemplo los keys del teclado alfanumérico (las que están arriba), que en el enum "Keys" se llaman "Dx", quiero que muestren solo su número, no el nombre que tienen asignado en el enum. Otros casos a resolver específicamente son los de las teclas correspondientes a símbolos como puntos, comas, adición, etc.
                    case Keys.D0: keyboardShortcutTextForThisKeyboardShortcut += "0"; break;
                    case Keys.D1: keyboardShortcutTextForThisKeyboardShortcut += "1"; break;
                    case Keys.D2: keyboardShortcutTextForThisKeyboardShortcut += "2"; break;
                    case Keys.D3: keyboardShortcutTextForThisKeyboardShortcut += "3"; break;
                    case Keys.D4: keyboardShortcutTextForThisKeyboardShortcut += "4"; break;
                    case Keys.D5: keyboardShortcutTextForThisKeyboardShortcut += "5"; break;
                    case Keys.D6: keyboardShortcutTextForThisKeyboardShortcut += "6"; break;
                    case Keys.D7: keyboardShortcutTextForThisKeyboardShortcut += "7"; break;
                    case Keys.D8: keyboardShortcutTextForThisKeyboardShortcut += "8"; break;
                    case Keys.D9: keyboardShortcutTextForThisKeyboardShortcut += "9"; break;
                    case Keys.OemMinus: keyboardShortcutTextForThisKeyboardShortcut += "-"; break;
                    case Keys.OemPeriod: keyboardShortcutTextForThisKeyboardShortcut += "."; break;

                    // por ejemplo los keys del numeric keypad (las que están a la derecha), que en el enum "Keys" se llaman "NumPadx", quiero que muestren solo su número, pero además quiero que lo muestren solo en caso de que entre los shortcuts que pasé no se incluya ya la tecla equivalente que está en el teclado alfanumérico (que en el enum figura como "Dx"). Por ejemplo, si la lista ya incluye a D1, no quiero que "NumPad1" genere ningún texto, por lo que para NumPad 1 no asigno un texto y declaro "includeKeyboardShortcutTextForThisKeyboardShortcut = false".
                    case Keys.NumPad0: if (!keyboardShortcuts.Contains(Keys.D0)) { keyboardShortcutTextForThisKeyboardShortcut += "0"; } else { includeKeyboardShortcutTextForThisKeyboardShortcut = false; } break;
                    case Keys.NumPad1: if (!keyboardShortcuts.Contains(Keys.D1)) { keyboardShortcutTextForThisKeyboardShortcut += "1"; } else { includeKeyboardShortcutTextForThisKeyboardShortcut = false; } break;
                    case Keys.NumPad2: if (!keyboardShortcuts.Contains(Keys.D2)) { keyboardShortcutTextForThisKeyboardShortcut += "2"; } else { includeKeyboardShortcutTextForThisKeyboardShortcut = false; } break;
                    case Keys.NumPad3: if (!keyboardShortcuts.Contains(Keys.D3)) { keyboardShortcutTextForThisKeyboardShortcut += "3"; } else { includeKeyboardShortcutTextForThisKeyboardShortcut = false; } break;
                    case Keys.NumPad4: if (!keyboardShortcuts.Contains(Keys.D4)) { keyboardShortcutTextForThisKeyboardShortcut += "4"; } else { includeKeyboardShortcutTextForThisKeyboardShortcut = false; } break;
                    case Keys.NumPad5: if (!keyboardShortcuts.Contains(Keys.D5)) { keyboardShortcutTextForThisKeyboardShortcut += "5"; } else { includeKeyboardShortcutTextForThisKeyboardShortcut = false; } break;
                    case Keys.NumPad6: if (!keyboardShortcuts.Contains(Keys.D6)) { keyboardShortcutTextForThisKeyboardShortcut += "6"; } else { includeKeyboardShortcutTextForThisKeyboardShortcut = false; } break;
                    case Keys.NumPad7: if (!keyboardShortcuts.Contains(Keys.D7)) { keyboardShortcutTextForThisKeyboardShortcut += "7"; } else { includeKeyboardShortcutTextForThisKeyboardShortcut = false; } break;
                    case Keys.NumPad8: if (!keyboardShortcuts.Contains(Keys.D8)) { keyboardShortcutTextForThisKeyboardShortcut += "8"; } else { includeKeyboardShortcutTextForThisKeyboardShortcut = false; } break;
                    case Keys.NumPad9: if (!keyboardShortcuts.Contains(Keys.D9)) { keyboardShortcutTextForThisKeyboardShortcut += "9"; } else { includeKeyboardShortcutTextForThisKeyboardShortcut = false; } break;
                    case Keys.Subtract: if (!keyboardShortcuts.Contains(Keys.OemMinus)) { keyboardShortcutTextForThisKeyboardShortcut += "-"; } else { includeKeyboardShortcutTextForThisKeyboardShortcut = false; } break;
                    case Keys.Decimal: if (!keyboardShortcuts.Contains(Keys.OemPeriod)) { keyboardShortcutTextForThisKeyboardShortcut += "."; } else { includeKeyboardShortcutTextForThisKeyboardShortcut = false; } break;

                    //para todos los keys que no haya resuelto como casos específicos, quiero que se imprima el texto correspondiente al nombre de la tecla en el enum "Keys", en minúscula.
                    default: keyboardShortcutTextForThisKeyboardShortcut += key.ToString().ToLower(); break;
                }

                if (includeKeyboardShortcutTextForThisKeyboardShortcut) // si decido incluir el texto para este keyboard shortcut (es decir, si no declaré a esta variable como false para este keyboard shortcut) 
                {
                    keyboardShortcutTextFull += keyboardShortcutTextForThisKeyboardShortcut; //agrego el string correspondiente a este keyboardshortcut al string que va a contener el texto correspondiente a todos los keyboard shortcuts.
                    firstKeyboardShortcut = false; //declaro esta variable falsa, así el string correspondiente al siguiente keyboard shortcut que haya incluido (si lo hay) incluye " or " al principio.
                }

            }
            return keyboardShortcutTextFull; //después de terminar el foreach (es decir, de procesar todos los keyboard shortcuts que haya declarado cuando instancie este bang) devuelvo el string completo resultante.
        }
    }
}
