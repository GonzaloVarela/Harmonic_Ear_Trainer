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
    static class ChordListManager
    {
        static int enabledChordTypes = Chord.isTypeEnabled.Length; //en esta variable voy a poner la cantidad de chordTypes habilitados (para asegurarme de no deshabilitarlos todos)
        public static Checkbox type0 = new Checkbox(new Vector2(Main.marginHorizontal, Main.marginVertical + Main.positionRowSeparation * 8), $"{Dictionaries.chordType[0]}", CheckboxCategory.ChordType, true, false); //inicializo una instancia de la class "Checkbox", que voy a usar para habilitar o deshabilitar el audio.
        public static Checkbox type1 = new Checkbox(new Vector2(Main.marginHorizontal, Main.marginVertical + Main.positionRowSeparation * 9), $"{Dictionaries.chordType[1]}", CheckboxCategory.ChordType, true, false);
        public static Checkbox type2 = new Checkbox(new Vector2(Main.marginHorizontal, Main.marginVertical + Main.positionRowSeparation * 10), $"{Dictionaries.chordType[2]}", CheckboxCategory.ChordType, true, false);
        public static Checkbox type3 = new Checkbox(new Vector2(Main.marginHorizontal, Main.marginVertical + Main.positionRowSeparation * 11), $"{Dictionaries.chordType[3]}", CheckboxCategory.ChordType, true, false);

        public static Checkbox type4 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation, Main.marginVertical + Main.positionRowSeparation * 8), $"{Dictionaries.chordType[4]}", CheckboxCategory.ChordType, true, false);
        public static Checkbox type5 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation, Main.marginVertical + Main.positionRowSeparation * 9), $"{Dictionaries.chordType[5]}", CheckboxCategory.ChordType, true, false);
        public static Checkbox type6 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation, Main.marginVertical + Main.positionRowSeparation * 10), $"{Dictionaries.chordType[6]}", CheckboxCategory.ChordType, true, false);
        public static Checkbox type7 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation, Main.marginVertical + Main.positionRowSeparation * 11), $"{Dictionaries.chordType[7]}", CheckboxCategory.ChordType, true, false);
        public static Checkbox type8 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation, Main.marginVertical + Main.positionRowSeparation * 12), $"{Dictionaries.chordType[8]}", CheckboxCategory.ChordType, true, false);
        public static Checkbox type9 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation, Main.marginVertical + Main.positionRowSeparation * 13), $"{Dictionaries.chordType[9]}", CheckboxCategory.ChordType, true, false);
        public static Checkbox type10 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation, Main.marginVertical + Main.positionRowSeparation * 14), $"{Dictionaries.chordType[10]}", CheckboxCategory.ChordType, true, false);

        public static Checkbox type11 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation * 2, Main.marginVertical + Main.positionRowSeparation * 8), $"{Dictionaries.chordType[11]}", CheckboxCategory.ChordType, true, false);
        public static Checkbox type12 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation * 2, Main.marginVertical + Main.positionRowSeparation * 9), $"{Dictionaries.chordType[12]}", CheckboxCategory.ChordType, true, false);
        public static Checkbox type13 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation * 2, Main.marginVertical + Main.positionRowSeparation * 10), $"{Dictionaries.chordType[13]}", CheckboxCategory.ChordType, true, false);
        public static Checkbox type14 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation * 2, Main.marginVertical + Main.positionRowSeparation * 11), $"{Dictionaries.chordType[14]}", CheckboxCategory.ChordType, true, false);
        public static Checkbox type15 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation * 2, Main.marginVertical + Main.positionRowSeparation * 12), $"{Dictionaries.chordType[15]}", CheckboxCategory.ChordType, true, false);
        public static Checkbox type16 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation * 2, Main.marginVertical + Main.positionRowSeparation * 13), $"{Dictionaries.chordType[16]}", CheckboxCategory.ChordType, true, false);
        public static Checkbox type17 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation * 2, Main.marginVertical + Main.positionRowSeparation * 14), $"{Dictionaries.chordType[17]}", CheckboxCategory.ChordType, true, false);

        public static Checkbox type18 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation * 3, Main.marginVertical + Main.positionRowSeparation * 8), $"{Dictionaries.chordType[18]}", CheckboxCategory.ChordType, true, false);
        public static Checkbox type19 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation * 3, Main.marginVertical + Main.positionRowSeparation * 9), $"{Dictionaries.chordType[19]}", CheckboxCategory.ChordType, true, false);
        public static Checkbox type20 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation * 3, Main.marginVertical + Main.positionRowSeparation * 10), $"{Dictionaries.chordType[20]}", CheckboxCategory.ChordType, true, false);
        public static Checkbox type21 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation * 3, Main.marginVertical + Main.positionRowSeparation * 11), $"{Dictionaries.chordType[21]}", CheckboxCategory.ChordType, true, false);
        public static Checkbox type22 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation * 3, Main.marginVertical + Main.positionRowSeparation * 12), $"{Dictionaries.chordType[22]}", CheckboxCategory.ChordType, true, false);


        public static Checkbox inversion0 = new Checkbox(new Vector2(Main.marginHorizontal, Main.marginVertical + Main.positionRowSeparation * 17), $"{Dictionaries.chordInversion[0]}", CheckboxCategory.ChordInversion, true, false);
        public static Checkbox inversion1 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation, Main.marginVertical + Main.positionRowSeparation * 17), $"{Dictionaries.chordInversion[1]}", CheckboxCategory.ChordInversion, true, false);
        public static Checkbox inversion2 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation * 2, Main.marginVertical + Main.positionRowSeparation * 17), $"{Dictionaries.chordInversion[2]}", CheckboxCategory.ChordInversion, true, false);
        public static Checkbox inversion3 = new Checkbox(new Vector2(Main.marginHorizontal + Main.positionColumnSeparation * 3, Main.marginVertical + Main.positionRowSeparation * 17), $"{Dictionaries.chordInversion[3]}", CheckboxCategory.ChordInversion, true, false);
        public static void Initialize()
        {
            //asocio todas las Checkboxes con las funciones correspondientes
            for (int i = 0; i < Chord.isTypeEnabled.Length; i++) Chord.isTypeEnabled[i] = true; //por ahora comienzo la instancia del acorde con todos los tipos de acorde habilitados

            for (int i = 0; i < Chord.isInversionEnabled.Length; i++) Chord.isInversionEnabled[i] = true; //por ahora voy a mantener siempre todas las inversiones habilitadas, porque funcionan de forma demasiado particular dependiendo del tipo de acorde, y hasta que desarrolle un buen sistema para implementarlas dan para problemas. Lo que sí podría hacer tal vez sin demasiado trabajo es que al seleccionar un tipo de acorde se muestren en rojo las inversiones que no aplican; HACER ESTO, Y TRABAJAR EL DESHABILITADO DE INVERSIONES DE FORMA PURAMENTE ESTÉTICA (QUE SE MUESTREN EN ROJO LAS QUE NO APLICAN), NO DESACTIVAR VERDADERAMENTE LAS INVERSIONES DE LA CLASS "CHORD" (INCLUSO DESHABILITAR ESA POSIBILIDAD, Y ELIMINAR LA VARIABLE "isInversionEnabled", PORQUE SINO ME PARECE QUE SE PODRÍAN DAR PROBLEMAS.
                                                                                                          //OBS.: Las tríadas aumentadas y tétradas disminuidas ignoran los settings de Inversions habilitadas, siempre se consideran en root position. Y los acordes con 9na y los sus siempre los hago en estado fundamental (aunque estos sí a lo mejor podrían tener inversiones), al menos por ahora.

            type0.CheckboxClickedRight += OnType0CheckboxClickedRight; //asocio la función "OnType0CheckboxClickedRight()" con la variable CheckboxClickedRight, cuyo tipo es un delegate.
            type1.CheckboxClickedRight += OnType1CheckboxClickedRight;
            type2.CheckboxClickedRight += OnType2CheckboxClickedRight;
            type3.CheckboxClickedRight += OnType3CheckboxClickedRight;
            type4.CheckboxClickedRight += OnType4CheckboxClickedRight;
            type5.CheckboxClickedRight += OnType5CheckboxClickedRight;
            type6.CheckboxClickedRight += OnType6CheckboxClickedRight;
            type7.CheckboxClickedRight += OnType7CheckboxClickedRight;
            type8.CheckboxClickedRight += OnType8CheckboxClickedRight;
            type9.CheckboxClickedRight += OnType9CheckboxClickedRight;
            type10.CheckboxClickedRight += OnType10CheckboxClickedRight;
            type11.CheckboxClickedRight += OnType11CheckboxClickedRight;
            type12.CheckboxClickedRight += OnType12CheckboxClickedRight;
            type13.CheckboxClickedRight += OnType13CheckboxClickedRight;
            type14.CheckboxClickedRight += OnType14CheckboxClickedRight;
            type15.CheckboxClickedRight += OnType15CheckboxClickedRight;
            type16.CheckboxClickedRight += OnType16CheckboxClickedRight;
            type17.CheckboxClickedRight += OnType17CheckboxClickedRight;
            type18.CheckboxClickedRight += OnType18CheckboxClickedRight;
            type19.CheckboxClickedRight += OnType19CheckboxClickedRight;
            type20.CheckboxClickedRight += OnType20CheckboxClickedRight;
            type21.CheckboxClickedRight += OnType21CheckboxClickedRight;
            type22.CheckboxClickedRight += OnType22CheckboxClickedRight;

            type0.CheckboxClickedLeft += OnTypeCheckboxClickedLeft; //asocio la función "OnTypeCheckboxClickedLeft()" con la variable CheckboxClickedLeft, cuyo tipo es un delegate.
            type1.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;
            type2.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;
            type3.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;
            type4.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;
            type5.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;
            type6.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;
            type7.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;
            type8.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;
            type9.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;
            type10.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;
            type11.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;
            type12.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;
            type13.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;
            type14.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;
            type15.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;
            type16.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;
            type17.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;
            type18.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;
            type19.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;
            type20.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;
            type21.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;
            type22.CheckboxClickedLeft += OnTypeCheckboxClickedLeft;


            inversion0.CheckboxClickedLeft += OnInversionCheckboxClickedLeft;
            inversion1.CheckboxClickedLeft += OnInversionCheckboxClickedLeft;
            inversion2.CheckboxClickedLeft += OnInversionCheckboxClickedLeft;
            inversion3.CheckboxClickedLeft += OnInversionCheckboxClickedLeft;
        }


        public static void OnType0CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled) //si hago click con botón derecho en la checkbox cuando la checkbox está habilitada y no es la única checkbox de type que está habilitada (o sea que hay más de un chord type habilitado) deshabilito el tipo de acorde y deshabilito el checkbox.
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[0] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else  //si hago click con botón derecho en la checkbox cuando la checkbox está deshabilitada, habilito el tipo de acorde y habilito el checkbox.
            {
                Chord.isTypeEnabled[0] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }
        public static void OnType1CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[1] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                Chord.isTypeEnabled[1] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }
        public static void OnType2CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[2] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                Chord.isTypeEnabled[2] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }
        public static void OnType3CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[3] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                Chord.isTypeEnabled[3] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }
        public static void OnType4CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[4] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[4] = true;
                    checkbox.stateEnabled = true;
                    enabledChordTypes++;
                }
            }
        }
        public static void OnType5CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[5] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                Chord.isTypeEnabled[5] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }
        public static void OnType6CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[6] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                Chord.isTypeEnabled[6] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }
        public static void OnType7CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[7] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                Chord.isTypeEnabled[7] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }
        public static void OnType8CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[8] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                Chord.isTypeEnabled[8] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }
        public static void OnType9CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[9] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                Chord.isTypeEnabled[9] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }
        public static void OnType10CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[10] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                Chord.isTypeEnabled[10] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }
        public static void OnType11CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[11] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                Chord.isTypeEnabled[11] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }
        public static void OnType12CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[12] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                Chord.isTypeEnabled[12] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }
        public static void OnType13CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[13] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                Chord.isTypeEnabled[13] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }
        public static void OnType14CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[14] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                Chord.isTypeEnabled[14] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }
        public static void OnType15CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[15] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                Chord.isTypeEnabled[15] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }
        public static void OnType16CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[16] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                Chord.isTypeEnabled[16] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }
        public static void OnType17CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[17] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                Chord.isTypeEnabled[17] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }
        public static void OnType18CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[18] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                Chord.isTypeEnabled[18] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }
        public static void OnType19CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[19] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                Chord.isTypeEnabled[19] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }
        public static void OnType20CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[20] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                Chord.isTypeEnabled[20] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }
        public static void OnType21CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[21] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                Chord.isTypeEnabled[21] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }
        public static void OnType22CheckboxClickedRight(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected)
        {
            if (stateEnabled)
            {
                if (enabledChordTypes > 1)
                {
                    Chord.isTypeEnabled[22] = false;
                    checkbox.stateEnabled = false;
                    enabledChordTypes--;
                }
            }
            else
            {
                Chord.isTypeEnabled[22] = true;
                checkbox.stateEnabled = true;
                enabledChordTypes++;
            }
        }

        public static void OnTypeCheckboxClickedLeft(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected) // cuando hago click izquierdo deshabilito todas las Checkboxes de type y luego habilito la que fue clickeada
        {
            type0.stateSelected = false;
            type1.stateSelected = false;
            type2.stateSelected = false;
            type3.stateSelected = false;
            type4.stateSelected = false;
            type5.stateSelected = false;
            type6.stateSelected = false;
            type7.stateSelected = false;
            type8.stateSelected = false;
            type9.stateSelected = false;
            type10.stateSelected = false;
            type11.stateSelected = false;
            type12.stateSelected = false;
            type13.stateSelected = false;
            type14.stateSelected = false;
            type15.stateSelected = false;
            type16.stateSelected = false;
            type17.stateSelected = false;
            type18.stateSelected = false;
            type19.stateSelected = false;
            type20.stateSelected = false;
            type21.stateSelected = false;
            type22.stateSelected = false;
            checkbox.stateSelected = true;
        }

        public static void OnInversionCheckboxClickedLeft(Checkbox checkbox, CheckboxCategory category, bool stateEnabled, bool stateSelected) // cuando hago click izquierdo deshabilito todas las Checkboxes de inversion y luego habilito la que fue clickeada
        {
            inversion0.stateSelected = false;
            inversion1.stateSelected = false;
            inversion2.stateSelected = false;
            inversion3.stateSelected = false;
            checkbox.stateSelected = true;
        }



        public static void Update(MouseState mouseState)
        {
            //llamo a la función Update de los checkboxes
            type0.Update(mouseState);
            type1.Update(mouseState);
            type2.Update(mouseState);
            type3.Update(mouseState);
            type4.Update(mouseState);
            type5.Update(mouseState);
            type6.Update(mouseState);
            type7.Update(mouseState);
            type8.Update(mouseState);
            type9.Update(mouseState);
            type10.Update(mouseState);
            type11.Update(mouseState);
            type12.Update(mouseState);
            type13.Update(mouseState);
            type14.Update(mouseState);
            type15.Update(mouseState);
            type16.Update(mouseState);
            type17.Update(mouseState);
            type18.Update(mouseState);
            type19.Update(mouseState);
            type20.Update(mouseState);
            type21.Update(mouseState);
            type22.Update(mouseState);

            inversion0.Update(mouseState);
            inversion1.Update(mouseState);
            inversion2.Update(mouseState);
            inversion3.Update(mouseState);
        }


        public static void Draw(SpriteBatch spriteBatch) // a la función Draw le paso un SpriteBatch, así no tengo que hacer Begin a un nuevo SpriteBatch dentro de esta función, sino que puedo usar un SpriteBatch que ya haya comenzado.
        {
            spriteBatch.DrawString(Main.font, "Chord Types (left click to select, right click to enable/desable):", new Vector2(Main.marginHorizontal, Main.marginVertical + Main.positionRowSeparation * 7), Color.Black);
            spriteBatch.DrawString(Main.font, "Chord Inversions (left click to select):", new Vector2(Main.marginHorizontal, Main.marginVertical + Main.positionRowSeparation * 16), Color.Black);

            //llamo a la función Draw de los checkboxes

            type0.Draw(spriteBatch);
            type1.Draw(spriteBatch);
            type2.Draw(spriteBatch);
            type3.Draw(spriteBatch);
            type4.Draw(spriteBatch);
            type5.Draw(spriteBatch);
            type6.Draw(spriteBatch);
            type7.Draw(spriteBatch);
            type8.Draw(spriteBatch);
            type9.Draw(spriteBatch);
            type10.Draw(spriteBatch);
            type11.Draw(spriteBatch);
            type12.Draw(spriteBatch);
            type13.Draw(spriteBatch);
            type14.Draw(spriteBatch);
            type15.Draw(spriteBatch);
            type16.Draw(spriteBatch);
            type17.Draw(spriteBatch);
            type18.Draw(spriteBatch);
            type19.Draw(spriteBatch);
            type20.Draw(spriteBatch);
            type21.Draw(spriteBatch);
            type22.Draw(spriteBatch);

            inversion0.Draw(spriteBatch);
            inversion1.Draw(spriteBatch);
            inversion2.Draw(spriteBatch);
            inversion3.Draw(spriteBatch);
        }
    }
}
