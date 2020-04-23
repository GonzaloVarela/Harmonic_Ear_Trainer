using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame.ChordGuessing
{
    static class ChordListManager
    {
        private static List<Checkbox> chordType = new List<Checkbox>(); //lista de las checkboxes para habilitar/deshabilitar y seleccionar/deseleccionar tipos de acordes
        private static List<Checkbox> chordInversion = new List<Checkbox>(); //lista de las checkboxes para habilitar/deshabilitar y seleccionar/deseleccionar inversiones de acordes

        public static int selectedType { get; set; } //variable donde voy a almacenar el número correspondiente al tipo de acorde que tenga seleccionado el jugador
        public static int selectedInversion  { get; set; } //variable donde voy a almacenar el número correspondiente a la inversión de acorde que tenga seleccionado el jugador

        public static void Initialize()
        {
            for (int i = 0; i < ChordVoicing.isTypeEnabled.Length; i++) ChordVoicing.isTypeEnabled[i] = true; //por ahora comienzo la instancia del acorde con todos los tipos de acorde habilitados

            for (int i = 0; i < ChordVoicing.isInversionEnabled.Length; i++) ChordVoicing.isInversionEnabled[i] = true; //por ahora voy a mantener siempre todas las inversiones habilitadas, porque funcionan de forma demasiado particular dependiendo del tipo de acorde, y hasta que desarrolle un buen sistema para implementarlas dan para problemas. Lo que sí podría hacer tal vez sin demasiado trabajo es que al seleccionar un tipo de acorde se muestren en rojo las inversiones que no aplican; HACER ESTO, Y TRABAJAR EL DESHABILITADO DE INVERSIONES DE FORMA PURAMENTE ESTÉTICA (QUE SE MUESTREN EN ROJO LAS QUE NO APLICAN), NO DESACTIVAR VERDADERAMENTE LAS INVERSIONES DE LA CLASS "CHORD" (INCLUSO DESHABILITAR ESA POSIBILIDAD, Y ELIMINAR LA VARIABLE "isInversionEnabled", PORQUE SINO ME PARECE QUE SE PODRÍAN DAR PROBLEMAS.
                                                                                                                        //OBS.: Las tríadas aumentadas y tétradas disminuidas ignoran los settings de Inversions habilitadas, siempre se consideran en root position. Y los acordes con 9na y los sus siempre los hago en estado fundamental (aunque estos sí a lo mejor podrían tener inversiones), al menos por ahora.
            chordType.Add(new Checkbox(new Vector2(Main.marginLeft, Main.marginTop + Main.gridRowSeparation * 8), $"{Dictionaries.chordType[0]}", (int)CheckboxCategory.ChordType, true, false));
            chordType.Add(new Checkbox(new Vector2(Main.marginLeft, Main.marginTop + Main.gridRowSeparation * 9), $"{Dictionaries.chordType[1]}", (int)CheckboxCategory.ChordType, true, false));
            chordType.Add(new Checkbox(new Vector2(Main.marginLeft, Main.marginTop + Main.gridRowSeparation * 10), $"{Dictionaries.chordType[2]}", (int)CheckboxCategory.ChordType, true, false));
            chordType.Add(new Checkbox(new Vector2(Main.marginLeft, Main.marginTop + Main.gridRowSeparation * 11), $"{Dictionaries.chordType[3]}", (int)CheckboxCategory.ChordType, true, false));

            chordType.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation, Main.marginTop + Main.gridRowSeparation * 8), $"{Dictionaries.chordType[4]}", (int)CheckboxCategory.ChordType, true, false));
            chordType.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation, Main.marginTop + Main.gridRowSeparation * 9), $"{Dictionaries.chordType[5]}", (int)CheckboxCategory.ChordType, true, false));
            chordType.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation, Main.marginTop + Main.gridRowSeparation * 10), $"{Dictionaries.chordType[6]}", (int)CheckboxCategory.ChordType, true, false));
            chordType.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation, Main.marginTop + Main.gridRowSeparation * 11), $"{Dictionaries.chordType[7]}", (int)CheckboxCategory.ChordType, true, false));
            chordType.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation, Main.marginTop + Main.gridRowSeparation * 12), $"{Dictionaries.chordType[8]}", (int)CheckboxCategory.ChordType, true, false));
            chordType.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation, Main.marginTop + Main.gridRowSeparation * 13), $"{Dictionaries.chordType[9]}", (int)CheckboxCategory.ChordType, true, false));
            chordType.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation, Main.marginTop + Main.gridRowSeparation * 14), $"{Dictionaries.chordType[10]}", (int)CheckboxCategory.ChordType, true, false));

            chordType.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation * 2, Main.marginTop + Main.gridRowSeparation * 8), $"{Dictionaries.chordType[11]}", (int)CheckboxCategory.ChordType, true, false));
            chordType.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation * 2, Main.marginTop + Main.gridRowSeparation * 9), $"{Dictionaries.chordType[12]}", (int)CheckboxCategory.ChordType, true, false));
            chordType.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation * 2, Main.marginTop + Main.gridRowSeparation * 10), $"{Dictionaries.chordType[13]}", (int)CheckboxCategory.ChordType, true, false));
            chordType.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation * 2, Main.marginTop + Main.gridRowSeparation * 11), $"{Dictionaries.chordType[14]}", (int)CheckboxCategory.ChordType, true, false));
            chordType.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation * 2, Main.marginTop + Main.gridRowSeparation * 12), $"{Dictionaries.chordType[15]}", (int)CheckboxCategory.ChordType, true, false));
            chordType.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation * 2, Main.marginTop + Main.gridRowSeparation * 13), $"{Dictionaries.chordType[16]}", (int)CheckboxCategory.ChordType, true, false));
            chordType.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation * 2, Main.marginTop + Main.gridRowSeparation * 14), $"{Dictionaries.chordType[17]}", (int)CheckboxCategory.ChordType, true, false));

            chordType.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation * 3, Main.marginTop + Main.gridRowSeparation * 8), $"{Dictionaries.chordType[18]}", (int)CheckboxCategory.ChordType, true, false));
            chordType.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation * 3, Main.marginTop + Main.gridRowSeparation * 9), $"{Dictionaries.chordType[19]}", (int)CheckboxCategory.ChordType, true, false));
            chordType.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation * 3, Main.marginTop + Main.gridRowSeparation * 10), $"{Dictionaries.chordType[20]}", (int)CheckboxCategory.ChordType, true, false));
            chordType.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation * 3, Main.marginTop + Main.gridRowSeparation * 11), $"{Dictionaries.chordType[21]}", (int)CheckboxCategory.ChordType, true, false));
            chordType.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation * 3, Main.marginTop + Main.gridRowSeparation * 12), $"{Dictionaries.chordType[22]}", (int)CheckboxCategory.ChordType, true, false));

            chordInversion.Add(new Checkbox(new Vector2(Main.marginLeft, Main.marginTop + Main.gridRowSeparation * 17), $"{Dictionaries.chordInversion[0]}", (int)CheckboxCategory.ChordInversion, true, false));
            chordInversion.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation, Main.marginTop + Main.gridRowSeparation * 17), $"{Dictionaries.chordInversion[1]}", (int)CheckboxCategory.ChordInversion, true, false));
            chordInversion.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation * 2, Main.marginTop + Main.gridRowSeparation * 17), $"{Dictionaries.chordInversion[2]}", (int)CheckboxCategory.ChordInversion, true, false));
            chordInversion.Add(new Checkbox(new Vector2(Main.marginLeft + Main.gridColumnSeparation * 3, Main.marginTop + Main.gridRowSeparation * 17), $"{Dictionaries.chordInversion[3]}", (int)CheckboxCategory.ChordInversion, true, false));


            //asocio los delegates de los Checkboxes con las funciones correspondientes
            for (int checkboxIndexInTypeList = 0; checkboxIndexInTypeList < chordType.Count; checkboxIndexInTypeList++)
            {
                chordType[checkboxIndexInTypeList].CheckboxClickedRight += GetOnTypeCheckboxClickedRight(checkboxIndexInTypeList); //al delegate "CheckboxClickedRight" de cada uno de los checkboxes de la lista "chordType" le asigno lo que devuelva la función "GetOnTypeCheckboxClickedRight(checkboxIndexInTypeList)", siendo checkboxIndexInTypeList el número de la checkbox en la lista (que se corresponde con el número de chordType de acorde) (va a devolver un una lambda expression que use ese checkboxIndexInTypeList)
                chordType[checkboxIndexInTypeList].CheckboxClickedLeft += GetOnTypeCheckboxClickedLeft(checkboxIndexInTypeList); //ver comentario de asignación anterior, hago más o menos lo mismo.
            }

            for (int checkboxIndexInInversionList = 0; checkboxIndexInInversionList < chordInversion.Count; checkboxIndexInInversionList++)
            {
                chordInversion[checkboxIndexInInversionList].CheckboxClickedLeft += GetOnInversionCheckboxClickedLeft(checkboxIndexInInversionList); //ver comentario de asignación anterior, hago más o menos lo mismo.
            }
        }


        static Checkbox.CheckboxClickedEventHandler GetOnTypeCheckboxClickedRight(int checkboxIndexInTypeList) // cada checkbox de la lista "chordType" va a tener asignada a su delegate "CheckboxClickedRight" la lambda expression que devuelva esta función, la cual va a incluir el scope, en el que la variable "checkboxIndexInTypeList" corresponde al índice del checkbox en la lista "chordType" (y eso corresponde a su vez con el número de tipo de acorde -que uso para mi enum y demás-) (o sea que a cada asignación va a corresponder un scope distinto, donde la variable checkboxIndexInTypeList tiene un valor distinto)
        {
            return (Checkbox checkbox, int category, bool stateEnabled, bool stateSelected) =>
            {
                if (stateEnabled) //si hago click con botón derecho en la checkbox cuando la checkbox está habilitada y no es la única checkbox de chordType que está habilitada (o sea que hay más de un chord chordType habilitado) deshabilito el tipo de acorde y deshabilito el checkbox.
                {
                    int enabledChordTypes = 0; //en esta variable voy a poner la cantidad de chordTypes habilitados (para asegurarme de no deshabilitarlos todos)


                    foreach (var chkbox in chordType) //itero entre las checkboxes de la lista, y cada vez que encuentro una checkbox habilitada sumo 1 a la variable enabledChordTypes
                    {
                        if (chkbox.stateEnabled == true) enabledChordTypes++;
                    }

                    if (enabledChordTypes > 1)
                    {
                        ChordVoicing.isTypeEnabled[checkboxIndexInTypeList] = false;
                        checkbox.stateEnabled = false;
                    }
                }
                else  //si hago click con botón derecho en la checkbox cuando la checkbox está deshabilitada, habilito el tipo de acorde y habilito el checkbox.
                {
                    ChordVoicing.isTypeEnabled[checkboxIndexInTypeList] = true;
                    checkbox.stateEnabled = true;
                }
            };
        }

        static Checkbox.CheckboxClickedEventHandler GetOnTypeCheckboxClickedLeft(int checkboxIndexInTypeList) //ver comentario de función anterior, hago más o menos lo mismo.
        {
            return (Checkbox checkbox, int category, bool stateEnabled, bool stateSelected) =>
            {
                foreach (var chkbox in chordType)// cuando hago click izquierdo deselecciono todas las Checkboxes de tipo y luego selecciono la que fue clickeada
                {
                    chkbox.stateSelected = false;
                }
                chordType[checkboxIndexInTypeList].stateSelected = true;
                selectedType = checkboxIndexInTypeList; //asigno a la variable "selectedType" el índice que tiene el checkbox que clickeé en la lista "chordType" (que corresponde con el número de inversión de acorde, que uso en el enum y demás).
            };
        }
        static Checkbox.CheckboxClickedEventHandler GetOnInversionCheckboxClickedLeft(int checkboxIndexInInversionList) //ver comentario de función anterior, hago más o menos lo mismo.
        {
            return (Checkbox checkbox, int category, bool stateEnabled, bool stateSelected) =>
            {

                foreach (var chkbox in chordInversion)// cuando hago click izquierdo deselecciono todas las Checkboxes de inversión y luego selecciono la que fue clickeada
                {
                    chkbox.stateSelected = false;
                }

                chordInversion[checkboxIndexInInversionList].stateSelected = true;
                selectedInversion = checkboxIndexInInversionList; //asigno a la variable "selectedInversion" el índice que tiene el checkbox que clickeé en la lista "chordInversion" (que corresponde con el número de inversión de acorde, que uso en el enum y demás).
            };
        }

        public static void Update(MouseState mouseState)
        {
            //llamo a la función Update de los checkboxes

            foreach (var checkbox in chordType)
            {
                checkbox.Update(mouseState);
            }

            foreach (var checkbox in chordInversion)
            {
                checkbox.Update(mouseState);
            }
        }


        public static void Draw(SpriteBatch spriteBatch) // a la función Draw le paso un SpriteBatch, así no tengo que hacer Begin a un nuevo SpriteBatch dentro de esta función, sino que puedo usar un SpriteBatch que ya haya comenzado.
        {
            spriteBatch.DrawString(Main.font, "Chord Types (left click to select, right click to enable/disable):", new Vector2(Main.marginLeft, Main.marginTop + Main.gridRowSeparation * 7), Main.fontColorDefault);
            spriteBatch.DrawString(Main.font, "Chord Inversions (left click to select):", new Vector2(Main.marginLeft, Main.marginTop + Main.gridRowSeparation * 16), Main.fontColorDefault);

            //llamo a la función Draw de los checkboxes

            foreach (var checkbox in chordType)
            {
                checkbox.Draw(spriteBatch);
            }

            foreach (var checkbox in chordInversion)
            {
                checkbox.Draw(spriteBatch);
            }
        }
    }
}
