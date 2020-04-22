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
        public static List<Checkbox> type = new List<Checkbox>(); //lista de las checkboxes para habilitar/deshabilitar y seleccionar/deseleccionar tipos de acordes
        public static List<Checkbox> inversion = new List<Checkbox>(); //lista de las checkboxes para habilitar/deshabilitar y seleccionar/deseleccionar inversiones de acordes

        public static int selectedType { get; set; } //variable donde voy a almacenar el número correspondiente al tipo de acorde que haya seleccionado el jugador
        public static int selectedInversion  { get; set; } //variable donde voy a almacenar el número correspondiente a la inversión de acorde que haya seleccionado el jugador

        public static void Initialize()
        {
            for (int i = 0; i < ChordVoicing.isTypeEnabled.Length; i++) ChordVoicing.isTypeEnabled[i] = true; //por ahora comienzo la instancia del acorde con todos los tipos de acorde habilitados

            for (int i = 0; i < ChordVoicing.isInversionEnabled.Length; i++) ChordVoicing.isInversionEnabled[i] = true; //por ahora voy a mantener siempre todas las inversiones habilitadas, porque funcionan de forma demasiado particular dependiendo del tipo de acorde, y hasta que desarrolle un buen sistema para implementarlas dan para problemas. Lo que sí podría hacer tal vez sin demasiado trabajo es que al seleccionar un tipo de acorde se muestren en rojo las inversiones que no aplican; HACER ESTO, Y TRABAJAR EL DESHABILITADO DE INVERSIONES DE FORMA PURAMENTE ESTÉTICA (QUE SE MUESTREN EN ROJO LAS QUE NO APLICAN), NO DESACTIVAR VERDADERAMENTE LAS INVERSIONES DE LA CLASS "CHORD" (INCLUSO DESHABILITAR ESA POSIBILIDAD, Y ELIMINAR LA VARIABLE "isInversionEnabled", PORQUE SINO ME PARECE QUE SE PODRÍAN DAR PROBLEMAS.
                                                                                                                        //OBS.: Las tríadas aumentadas y tétradas disminuidas ignoran los settings de Inversions habilitadas, siempre se consideran en root position. Y los acordes con 9na y los sus siempre los hago en estado fundamental (aunque estos sí a lo mejor podrían tener inversiones), al menos por ahora.
            type.Add(new Checkbox(new Vector2(Main.marginHorizontal, Main.marginVertical + Main.gridRowSeparation * 8), $"{Dictionaries.chordType[0]}", (int)CheckboxCategory.ChordType, true, false));
            type.Add(new Checkbox(new Vector2(Main.marginHorizontal, Main.marginVertical + Main.gridRowSeparation * 9), $"{Dictionaries.chordType[1]}", (int)CheckboxCategory.ChordType, true, false));
            type.Add(new Checkbox(new Vector2(Main.marginHorizontal, Main.marginVertical + Main.gridRowSeparation * 10), $"{Dictionaries.chordType[2]}", (int)CheckboxCategory.ChordType, true, false));
            type.Add(new Checkbox(new Vector2(Main.marginHorizontal, Main.marginVertical + Main.gridRowSeparation * 11), $"{Dictionaries.chordType[3]}", (int)CheckboxCategory.ChordType, true, false));

            type.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation, Main.marginVertical + Main.gridRowSeparation * 8), $"{Dictionaries.chordType[4]}", (int)CheckboxCategory.ChordType, true, false));
            type.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation, Main.marginVertical + Main.gridRowSeparation * 9), $"{Dictionaries.chordType[5]}", (int)CheckboxCategory.ChordType, true, false));
            type.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation, Main.marginVertical + Main.gridRowSeparation * 10), $"{Dictionaries.chordType[6]}", (int)CheckboxCategory.ChordType, true, false));
            type.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation, Main.marginVertical + Main.gridRowSeparation * 11), $"{Dictionaries.chordType[7]}", (int)CheckboxCategory.ChordType, true, false));
            type.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation, Main.marginVertical + Main.gridRowSeparation * 12), $"{Dictionaries.chordType[8]}", (int)CheckboxCategory.ChordType, true, false));
            type.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation, Main.marginVertical + Main.gridRowSeparation * 13), $"{Dictionaries.chordType[9]}", (int)CheckboxCategory.ChordType, true, false));
            type.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation, Main.marginVertical + Main.gridRowSeparation * 14), $"{Dictionaries.chordType[10]}", (int)CheckboxCategory.ChordType, true, false));

            type.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation * 2, Main.marginVertical + Main.gridRowSeparation * 8), $"{Dictionaries.chordType[11]}", (int)CheckboxCategory.ChordType, true, false));
            type.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation * 2, Main.marginVertical + Main.gridRowSeparation * 9), $"{Dictionaries.chordType[12]}", (int)CheckboxCategory.ChordType, true, false));
            type.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation * 2, Main.marginVertical + Main.gridRowSeparation * 10), $"{Dictionaries.chordType[13]}", (int)CheckboxCategory.ChordType, true, false));
            type.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation * 2, Main.marginVertical + Main.gridRowSeparation * 11), $"{Dictionaries.chordType[14]}", (int)CheckboxCategory.ChordType, true, false));
            type.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation * 2, Main.marginVertical + Main.gridRowSeparation * 12), $"{Dictionaries.chordType[15]}", (int)CheckboxCategory.ChordType, true, false));
            type.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation * 2, Main.marginVertical + Main.gridRowSeparation * 13), $"{Dictionaries.chordType[16]}", (int)CheckboxCategory.ChordType, true, false));
            type.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation * 2, Main.marginVertical + Main.gridRowSeparation * 14), $"{Dictionaries.chordType[17]}", (int)CheckboxCategory.ChordType, true, false));

            type.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation * 3, Main.marginVertical + Main.gridRowSeparation * 8), $"{Dictionaries.chordType[18]}", (int)CheckboxCategory.ChordType, true, false));
            type.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation * 3, Main.marginVertical + Main.gridRowSeparation * 9), $"{Dictionaries.chordType[19]}", (int)CheckboxCategory.ChordType, true, false));
            type.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation * 3, Main.marginVertical + Main.gridRowSeparation * 10), $"{Dictionaries.chordType[20]}", (int)CheckboxCategory.ChordType, true, false));
            type.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation * 3, Main.marginVertical + Main.gridRowSeparation * 11), $"{Dictionaries.chordType[21]}", (int)CheckboxCategory.ChordType, true, false));
            type.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation * 3, Main.marginVertical + Main.gridRowSeparation * 12), $"{Dictionaries.chordType[22]}", (int)CheckboxCategory.ChordType, true, false));

            inversion.Add(new Checkbox(new Vector2(Main.marginHorizontal, Main.marginVertical + Main.gridRowSeparation * 17), $"{Dictionaries.chordInversion[0]}", (int)CheckboxCategory.ChordInversion, true, false));
            inversion.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation, Main.marginVertical + Main.gridRowSeparation * 17), $"{Dictionaries.chordInversion[1]}", (int)CheckboxCategory.ChordInversion, true, false));
            inversion.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation * 2, Main.marginVertical + Main.gridRowSeparation * 17), $"{Dictionaries.chordInversion[2]}", (int)CheckboxCategory.ChordInversion, true, false));
            inversion.Add(new Checkbox(new Vector2(Main.marginHorizontal + Main.gridColumnSeparation * 3, Main.marginVertical + Main.gridRowSeparation * 17), $"{Dictionaries.chordInversion[3]}", (int)CheckboxCategory.ChordInversion, true, false));


            //asocio los delegates de los Checkboxes con las funciones correspondientes
            for (int checkboxIndexInTypeList = 0; checkboxIndexInTypeList < type.Count; checkboxIndexInTypeList++)
            {
                type[checkboxIndexInTypeList].CheckboxClickedRight += GetOnTypeCheckboxClickedRight(checkboxIndexInTypeList); //al delegate "CheckboxClickedRight" de cada uno de los checkboxes de la lista "type" le asigno lo que devuelva la función "GetOnTypeCheckboxClickedRight(checkboxIndexInTypeList)", siendo checkboxIndexInTypeList el número de la checkbox en la lista (que se corresponde con el número de type de acorde) (va a devolver un una lambda expression que use ese checkboxIndexInTypeList)
                type[checkboxIndexInTypeList].CheckboxClickedLeft += GetOnTypeCheckboxClickedLeft(checkboxIndexInTypeList); //ver comentario de asignación anterior, hago más o menos lo mismo.
            }

            for (int checkboxIndexInInversionList = 0; checkboxIndexInInversionList < inversion.Count; checkboxIndexInInversionList++)
            {
                inversion[checkboxIndexInInversionList].CheckboxClickedLeft += GetOnInversionCheckboxClickedLeft(checkboxIndexInInversionList); //ver comentario de asignación anterior, hago más o menos lo mismo.
            }
        }


        public static Checkbox.CheckboxClickedEventHandler GetOnTypeCheckboxClickedRight(int checkboxIndexInTypeList) // cada checkbox de la lista "type" va a tener asignada a su delegate "CheckboxClickedRight" la lambda expression que devuelva esta función, la cual va a incluir el scope, en el que la variable "checkboxIndexInTypeList" corresponde al índice del checkbox en la lista "type" (y eso corresponde a su vez con el número de tipo de acorde -que uso para mi enum y demás-) (o sea que a cada asignación va a corresponder un scope distinto, donde la variable checkboxIndexInTypeList tiene un valor distinto)
        {
            return (Checkbox checkbox, int category, bool stateEnabled, bool stateSelected) =>
            {
                if (stateEnabled) //si hago click con botón derecho en la checkbox cuando la checkbox está habilitada y no es la única checkbox de type que está habilitada (o sea que hay más de un chord type habilitado) deshabilito el tipo de acorde y deshabilito el checkbox.
                {
                    int enabledChordTypes = 0; //en esta variable voy a poner la cantidad de chordTypes habilitados (para asegurarme de no deshabilitarlos todos)
                    for (int i = 0; i < type.Count; i++) //itero entre las checkboxes de la lista, y cada vez que encuentro una checkbox habilitada sumo 1 a la variable enabledChordTypes
                    {
                        if (type[i].stateEnabled == true) enabledChordTypes++;
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

        public static Checkbox.CheckboxClickedEventHandler GetOnTypeCheckboxClickedLeft(int checkboxIndexInTypeList) //ver comentario de función anterior, hago más o menos lo mismo.
        {
            return (Checkbox checkbox, int category, bool stateEnabled, bool stateSelected) =>
            {
                for (int i = 0; i < type.Count; i++) // cuando hago click izquierdo deselecciono todas las Checkboxes de tipo y luego selecciono la que fue clickeada
                {
                    type[i].stateSelected = false;
                }
                type[checkboxIndexInTypeList].stateSelected = true;
                selectedInversion = checkboxIndexInTypeList;
            };
        }
        public static Checkbox.CheckboxClickedEventHandler GetOnInversionCheckboxClickedLeft(int checkboxIndexInInversionList) //ver comentario de función anterior, hago más o menos lo mismo.
        {
            return (Checkbox checkbox, int category, bool stateEnabled, bool stateSelected) =>
            {
                for (int i = 0; i < inversion.Count; i++) // cuando hago click izquierdo deselecciono todas las Checkboxes de inversión y luego selecciono la que fue clickeada
                {
                    inversion[i].stateSelected = false;
                }
                inversion[checkboxIndexInInversionList].stateSelected = true;
                selectedInversion = checkboxIndexInInversionList;
            };
        }

        public static void Update(MouseState mouseState)
        {
            //llamo a la función Update de los checkboxes
            for (int i = 0; i < type.Count; i++)
            {
                type[i].Update(mouseState);
            }

            for (int i = 0; i < inversion.Count; i++)
            {
                inversion[i].Update(mouseState);
            }
        }


        public static void Draw(SpriteBatch spriteBatch) // a la función Draw le paso un SpriteBatch, así no tengo que hacer Begin a un nuevo SpriteBatch dentro de esta función, sino que puedo usar un SpriteBatch que ya haya comenzado.
        {
            spriteBatch.DrawString(Main.font, "Chord Types (left click to select, right click to enable/disable):", new Vector2(Main.marginHorizontal, Main.marginVertical + Main.gridRowSeparation * 7), Main.fontColorGeneric);
            spriteBatch.DrawString(Main.font, "Chord Inversions (left click to select):", new Vector2(Main.marginHorizontal, Main.marginVertical + Main.gridRowSeparation * 16), Main.fontColorGeneric);

            //llamo a la función Draw de los checkboxes

            //llamo a la función Update de los checkboxes
            for (int i = 0; i < type.Count; i++)
            {
                type[i].Draw(spriteBatch);
            }

            for (int i = 0; i < inversion.Count; i++)
            {
                inversion[i].Draw(spriteBatch);
            }
        }
    }
}
