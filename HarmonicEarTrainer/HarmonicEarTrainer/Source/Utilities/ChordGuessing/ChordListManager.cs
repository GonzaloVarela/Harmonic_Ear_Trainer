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
    static class ChordListManager
    {
        static List<Checkbox> _chordType = new List<Checkbox>(); //lista de las checkboxes para habilitar/deshabilitar y seleccionar/deseleccionar tipos de acordes
        static List<Checkbox> _chordInversion = new List<Checkbox>(); //lista de las checkboxes para habilitar/deshabilitar y seleccionar/deseleccionar inversiones de acordes

        public static Dictionary<int, Bang> _chordTypePresets = new Dictionary<int, Bang>(); //diccionario de las checkboxes con bangs que habilitan o deshabilitan tipos de acorde (en el key voy a poner el número de preset, que va a empezar en 1 -por eso no uso lista o array- y corresponder con el keyboard shortcut)

        static Checkbox _onlyRootPosition = null; //creo esta variable pero no le asigno nada, porque quiero asignarla en el initialize, así me aseguro de que para ese momento se cargaron los assets necesarios

        public static int selectedType { get; set; } = 0; //variable donde almacenar el número correspondiente al tipo de acorde que tenga seleccionado el jugador (por defecto el primero)
        public static int selectedInversion { get; set; } = 0; //variable donde almaceno el número correspondiente a la inversión de acorde que tenga seleccionada el jugador (por defecto la primera)

        public static void Initialize()
        {
            for (int i = 0; i < ChordVoicing.isTypeEnabled.Length; i++) ChordVoicing.isTypeEnabled[i] = true; //por ahora comienzo la instancia del acorde con todos los tipos de acorde habilitados

            for (int i = 0; i < ChordVoicing.isInversionEnabled.Length; i++) ChordVoicing.isInversionEnabled[i] = true; //por ahora voy a mantener siempre todas las inversiones habilitadas, porque funcionan de forma demasiado particular dependiendo del tipo de acorde, y hasta que desarrolle un buen sistema para implementarlas dan para problemas. Lo que sí podría hacer tal vez sin demasiado trabajo es que al seleccionar un tipo de acorde se muestren en rojo las inversiones que no aplican; HACER ESTO, Y TRABAJAR EL DESHABILITADO DE INVERSIONES DE FORMA PURAMENTE ESTÉTICA (QUE SE MUESTREN EN ROJO LAS QUE NO APLICAN), NO DESACTIVAR VERDADERAMENTE LAS INVERSIONES DE LA CLASS "CHORD" (INCLUSO DESHABILITAR ESA POSIBILIDAD, Y ELIMINAR LA VARIABLE "isInversionEnabled", PORQUE SINO ME PARECE QUE SE PODRÍAN DAR PROBLEMAS.
                                                                                                                        //OBS.: Las tríadas aumentadas y tétradas disminuidas ignoran los settings de Inversions habilitadas, siempre se consideran en root position. Y los acordes con 9na y los sus siempre los hago en estado fundamental (aunque estos sí a lo mejor podrían tener inversiones), al menos por ahora.
          
            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 5), $"{Dictionaries.chordType[0]}", (int)CheckboxCategory.ChordType, true, true));
            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 6), $"{Dictionaries.chordType[1]}", (int)CheckboxCategory.ChordType, true, false));
            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 7), $"{Dictionaries.chordType[2]}", (int)CheckboxCategory.ChordType, true, false));
            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 8), $"{Dictionaries.chordType[3]}", (int)CheckboxCategory.ChordType, true, false));

            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 5), $"{Dictionaries.chordType[4]}", (int)CheckboxCategory.ChordType, true, false));
            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 6), $"{Dictionaries.chordType[5]}", (int)CheckboxCategory.ChordType, true, false));
            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 7), $"{Dictionaries.chordType[6]}", (int)CheckboxCategory.ChordType, true, false));
            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 8), $"{Dictionaries.chordType[7]}", (int)CheckboxCategory.ChordType, true, false));
            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 9), $"{Dictionaries.chordType[8]}", (int)CheckboxCategory.ChordType, true, false));
            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 10), $"{Dictionaries.chordType[9]}", (int)CheckboxCategory.ChordType, true, false));
            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 11), $"{Dictionaries.chordType[10]}", (int)CheckboxCategory.ChordType, true, false));

            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 2, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 5), $"{Dictionaries.chordType[11]}", (int)CheckboxCategory.ChordType, true, false));
            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 2, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 6), $"{Dictionaries.chordType[12]}", (int)CheckboxCategory.ChordType, true, false));
            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 2, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 7), $"{Dictionaries.chordType[13]}", (int)CheckboxCategory.ChordType, true, false));
            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 2, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 8), $"{Dictionaries.chordType[14]}", (int)CheckboxCategory.ChordType, true, false));
            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 2, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 9), $"{Dictionaries.chordType[15]}", (int)CheckboxCategory.ChordType, true, false));
            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 2, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 10), $"{Dictionaries.chordType[16]}", (int)CheckboxCategory.ChordType, true, false));
            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 2, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 11), $"{Dictionaries.chordType[17]}", (int)CheckboxCategory.ChordType, true, false));

            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 3, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 5), $"{Dictionaries.chordType[18]}", (int)CheckboxCategory.ChordType, true, false));
            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 3, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 6), $"{Dictionaries.chordType[19]}", (int)CheckboxCategory.ChordType, true, false));
            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 3, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 7), $"{Dictionaries.chordType[20]}", (int)CheckboxCategory.ChordType, true, false));
            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 3, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 8), $"{Dictionaries.chordType[21]}", (int)CheckboxCategory.ChordType, true, false));
            _chordType.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 3, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 9), $"{Dictionaries.chordType[22]}", (int)CheckboxCategory.ChordType, true, false));

            _chordInversion.Add(new Checkbox(new Vector2(LayoutManager.marginLeft, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 15), $"{Dictionaries.chordInversion[0]}", (int)CheckboxCategory.ChordInversion, true, true));
            _chordInversion.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 15), $"{Dictionaries.chordInversion[1]}", (int)CheckboxCategory.ChordInversion, true, false));
            _chordInversion.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 2, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 15), $"{Dictionaries.chordInversion[2]}", (int)CheckboxCategory.ChordInversion, true, false));
            _chordInversion.Add(new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 3, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 15), $"{Dictionaries.chordInversion[3]}", (int)CheckboxCategory.ChordInversion, true, false));

            _onlyRootPosition = new Checkbox(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 3, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 18), "Only Root Position", (int)CheckboxCategory.Option, true, false, Keys.D0, Keys.NumPad0);


            //asocio los delegates de los Checkboxes con las funciones correspondientes
            for (int checkboxIndexInTypeList = 0; checkboxIndexInTypeList < _chordType.Count; checkboxIndexInTypeList++)
            {
                _chordType[checkboxIndexInTypeList].CheckboxClickedWithLeftButton += GetOnChordTypeCheckboxClickedWithLeftButton(checkboxIndexInTypeList); //ver comentario de asignación anterior, hago más o menos lo mismo.
                _chordType[checkboxIndexInTypeList].CheckboxClickedWithRightButton += GetOnChordTypeCheckboxClickedWithRightButton(checkboxIndexInTypeList); //al delegate "CheckboxClickedRight" de cada uno de los checkboxes de la lista "chordType" le asigno lo que devuelva la función "GetOnChordTypeCheckboxClickedWithRightButton(checkboxIndexInTypeList)", siendo checkboxIndexInTypeList el número de la checkbox en la lista (que se corresponde con el número de chordType de acorde) (va a devolver un una lambda expression que use ese checkboxIndexInTypeList)
            }

            for (int checkboxIndexInInversionList = 0; checkboxIndexInInversionList < _chordInversion.Count; checkboxIndexInInversionList++)
            {
                _chordInversion[checkboxIndexInInversionList].CheckboxClickedWithLeftButton += GetOnChordInversionCheckboxClickedWithLeftButton(checkboxIndexInInversionList); //ver comentario de asignación anterior, hago más o menos lo mismo.
            }


            _onlyRootPosition.CheckboxClickedWithLeftButton += OnOnlyRootPositionCheckboxClickedWithLeftButton;

            //creo los bangs, asocio los delegates a las funciones correspondientes, y luego los agrego al diccionario (podría agregarlos al diccionario y luego asociar sus delegates, pero eso creo que sería menos eficiente, porque buscar al bang correspondiente adentro de un diccionario lleva más tiempo que accederlo directamente, me parece)
            Bang currentBang = null; //voy a usar una variable llamada "currentBang" como carretilla, para ir referenciando a los bangs que creo, asignándoles una función a su delegate, y agregándolos al diccionario.

            currentBang = new Bang(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 12), "Basic", (int)BangCategory.Chord, true, Keys.D1, Keys.NumPad1);
            currentBang.BangClickedWithLeftButton += OnChordTypePresets1BangClickedWithLeftButton;
            _chordTypePresets.Add(1, currentBang);

            //una vez que agregué ese bang al diccionario, voy a acceder a él por medio del diccionario, así que paso a usar la variable "currentBang" para hacer lo mismo con los otros bangs.
            currentBang = new Bang(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 2, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 12), "Advanced", (int)BangCategory.Chord, true, Keys.D2, Keys.NumPad2);
            currentBang.BangClickedWithLeftButton += OnChordTypePresets2BangClickedWithLeftButton;
            _chordTypePresets.Add(2, currentBang);

            currentBang = new Bang(new Vector2(LayoutManager.marginLeft + LayoutManager.gridColumnSeparation * 3, LayoutManager.marginTop + LayoutManager.gridRowSeparation * 12), "Expert", (int)BangCategory.Chord, true, Keys.D3, Keys.NumPad3);
            currentBang.BangClickedWithLeftButton += OnChordTypePresets3BangClickedWithLeftButton;
            _chordTypePresets.Add(3, currentBang);

        }

        static Checkbox.CheckboxClickedEventHandler GetOnChordTypeCheckboxClickedWithLeftButton(int checkboxIndexInTypeList) // cada checkbox de la lista "chordType" va a tener asignada a su delegate "CheckboxClickedWithLeftButton" la lambda expression que devuelva esta función, la cual va a incluir el scope, en el que la variable "checkboxIndexInTypeList" corresponde al índice del checkbox en la lista "chordType" (y eso corresponde a su vez con el número de tipo de acorde -que uso para mi enum y demás-) (o sea que a cada asignación va a corresponder un scope distinto, donde la variable checkboxIndexInTypeList tiene un valor distinto)
        {
            return (Checkbox checkbox, int category, bool stateEnabled, bool stateSelected) =>
            {
                foreach (var chkbox in _chordType)// cuando hago click izquierdo deselecciono todas las Checkboxes de tipo y luego selecciono la que fue clickeada
                {
                    chkbox.stateSelected = false;
                }
                _chordType[checkboxIndexInTypeList].stateSelected = true;
                selectedType = checkboxIndexInTypeList; //asigno a la variable "selectedType" el índice que tiene el checkbox que clickeé en la lista "chordType" (que corresponde con el número de inversión de acorde, que uso en el enum y demás).
            };
        }

        static Checkbox.CheckboxClickedEventHandler GetOnChordTypeCheckboxClickedWithRightButton(int checkboxIndexInTypeList) //ver comentario de función anterior, hago más o menos lo mismo.
        {
            return (Checkbox checkbox, int category, bool stateEnabled, bool stateSelected) =>
            {
                if (stateEnabled) //si hago click con botón derecho en la checkbox cuando la checkbox está habilitada y no es la única checkbox de chordType que está habilitada (o sea que hay más de un chord chordType habilitado) deshabilito el tipo de acorde y deshabilito el checkbox.
                {
                    int enabledChordTypes = 0; //en esta variable voy a poner la cantidad de chordTypes habilitados (para asegurarme de no deshabilitarlos todos)


                    foreach (var chkbox in _chordType) //itero entre las checkboxes de la lista, y cada vez que encuentro una checkbox habilitada sumo 1 a la variable enabledChordTypes
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

        static Checkbox.CheckboxClickedEventHandler GetOnChordInversionCheckboxClickedWithLeftButton(int checkboxIndexInInversionList) //ver comentario de función anterior, hago más o menos lo mismo.
        {
            return (Checkbox checkbox, int category, bool stateEnabled, bool stateSelected) =>
            {

                foreach (var chkbox in _chordInversion)// cuando hago click izquierdo deselecciono todas las Checkboxes de inversión y luego selecciono la que fue clickeada
                {
                    chkbox.stateSelected = false;
                }
                _chordInversion[checkboxIndexInInversionList].stateSelected = true;
                selectedInversion = checkboxIndexInInversionList; //asigno a la variable "selectedInversion" el índice que tiene el checkbox que clickeé en la lista "chordInversion" (que corresponde con el número de inversión de acorde, que uso en el enum y demás).
            };
        }

        static void OnChordTypePresets1BangClickedWithLeftButton(Bang bang, int category, bool stateEnabled)
        {
            for (int i = 0; i < _chordType.Count; i++) // activo todos los tipos de acorde cuyo índice sea menor a 4, y desactivo los otros
            {
                if (i < 4)
                {
                    _chordType[i].stateEnabled = true;
                    ChordVoicing.isTypeEnabled[i] = true;
                }
                else
                {
                    _chordType[i].stateEnabled = false;
                    ChordVoicing.isTypeEnabled[i] = false;
                }
            }
        }
        static void OnChordTypePresets2BangClickedWithLeftButton(Bang bang, int category, bool stateEnabled)
        {
            for (int i = 0; i < _chordType.Count; i++) // activo todos los tipos de acorde cuyo índice sea menor a 4, y desactivo los otros
            {
                if (i < 11)
                {
                    _chordType[i].stateEnabled = true;
                    ChordVoicing.isTypeEnabled[i] = true;
                }
                else
                {
                    _chordType[i].stateEnabled = false;
                    ChordVoicing.isTypeEnabled[i] = false;
                }
            }
        }
        static void OnChordTypePresets3BangClickedWithLeftButton(Bang bang, int category, bool stateEnabled)
        {
            for (int i = 0; i < _chordType.Count; i++) // activo todos los tipos de acorde
            {
                _chordType[i].stateEnabled = true;
                ChordVoicing.isTypeEnabled[i] = true;
            }
        }


        static void OnOnlyRootPositionCheckboxClickedWithLeftButton(Checkbox checkbox, int category, bool stateEnabled, bool stateSelected)
        {
            if (stateSelected) // si hago click con botón izq en la checkbox cuando la checkbox está seleccionada, activo las inversiones, y deselecciono el checkbox.
            {
                for (int i = 0; i < _chordInversion.Count; i++) // activo todas las inversiones
                {
                    _chordInversion[i].stateEnabled = true;
                    ChordVoicing.isInversionEnabled[i] = true;
                }
                checkbox.stateSelected = false;
                
            }
            else  // si hago click con botón izq en la checkbox cuando la checkbox está deseleccionada, desactivo las inversiones, y selecciono el checkbox.
            {
                for (int i = 0; i < _chordInversion.Count; i++) // activo todas las inversiones de acorde cuyo índice sea menor a 1, y desactivo las otras
                {
                    if (i < 1)
                    {
                        _chordInversion[i].stateEnabled = true;
                        ChordVoicing.isInversionEnabled[i] = true;
                    }
                    else
                    {
                        _chordInversion[i].stateEnabled = false;
                        ChordVoicing.isInversionEnabled[i] = false;
                    }
                }
                checkbox.stateSelected = true;
            }
        }

        public static void Update(GameTime gameTime, MouseState mouseState)
        {
            //llamo a la función Update de los checkboxes

            foreach (var checkbox in _chordType)
            {
                checkbox.Update(mouseState);
            }

            foreach (var checkbox in _chordInversion)
            {
                checkbox.Update(mouseState);
            }

            foreach (var bang in _chordTypePresets)
            {
                bang.Value.Update(gameTime, mouseState);
            }

            _onlyRootPosition.Update(mouseState);

        }


        public static void Draw(SpriteBatch spriteBatch) // a la función Draw le paso un SpriteBatch, así no tengo que hacer Begin a un nuevo SpriteBatch dentro de esta función, sino que puedo usar un SpriteBatch que ya haya comenzado.
        {
            //llamo a la función Draw de los checkboxes y bangs
            foreach (var checkbox in _chordType)
            {
                checkbox.Draw(spriteBatch);
            }

            foreach (var checkbox in _chordInversion)
            {
                checkbox.Draw(spriteBatch);
            }

            foreach (var bang in _chordTypePresets)
            {
                bang.Value.Draw(spriteBatch);
            }

            _onlyRootPosition.Draw(spriteBatch);

        }
    }
}
