using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
  

namespace SEditor {


    

    public enum InputDataTypeEnum
    {
        _bool,
        _string,
        _int,
        _float,
        _Vector3Int,
        _dropdown,
        _selectBig,
        Title,
    }


    public class RowElementType
    {
        public IInputComponent componentCreated;

        public string description;
        public string valueString="";
        public double valueDouble=0f;
        public Vector3Int valueVector3Int;
        public int valueInt=0;
        public bool valueBool=false;
        public string label = "xz";
        public string tab = "Main";
        public int LaoytOrder = 0;
        public InputDataTypeEnum mytype = InputDataTypeEnum._bool;
         
        public List<string> options;
        public Dictionary<string, Sprite> optionsSelectBig;

        public RowElementType SetLabel(string t)
        {
            label = t;
            return this;
        }
        public RowElementType SetDescription(string t)
        {
            description = t;
            return this;
        }
    }


    public class FormBuilder : MonoBehaviour
    {


        public TabRow_SE myTabRow; //Указать. Если нет сам создастся
        Transform btnRow;
        Button btnSave;

        public List<string> tabs = new List<string>() { "main" };


        public List<IInputComponent> myComponentsList = new List<IInputComponent>();
        public Transform conteiner;


        private BaseDataSE baseDataSE;

        [SerializeField]
        public Dictionary<string, RowElementType> template = new Dictionary<string, RowElementType>();

        public int lastLaoytOrder = 0;

        public RowElementType AddTemplateBool(string key, bool val, string tab, string title)
        {
            RowElementType _row = new RowElementType()
            {
                tab = tab,
                label = title,
                mytype = InputDataTypeEnum._bool,
                valueBool = val
            };
            AddTemplateElement(key, _row);
            return _row;
        }

        public RowElementType AddTemplateVector3Int(string key, Vector3Int val, string tab, string title)
        {

            RowElementType _row = new RowElementType()
            {
                tab = tab,
                label = title,
                mytype = InputDataTypeEnum._Vector3Int,
                valueVector3Int = val
            };
            AddTemplateElement(key, _row);
            return _row;
        }

        public RowElementType AddTemplateDouble(string key, double val, string tab, string title)
        {

            RowElementType _row = new RowElementType()
            {
                tab = tab,
                label = title,
                mytype = InputDataTypeEnum._float,
                valueDouble = val
            };
            AddTemplateElement(key, _row);
            return _row;
        }
        public RowElementType AddTemplateInt(string key, int val, string tab, string title)
        {
            RowElementType _row = new RowElementType()
            {
                tab = tab,
                label = title,
                mytype = InputDataTypeEnum._int,
                valueInt = val
            };
            AddTemplateElement(key, _row);
            return _row;
        }

        public RowElementType AddTemplateString(string key, string val, string tab, string title)
        {
            RowElementType _row = new RowElementType()
            {
                tab = tab,
                label = title,
                mytype = InputDataTypeEnum._string,
                valueString = val
            };
            AddTemplateElement(key, _row);
            return _row;
        }

        public void TabClick(string ind)
        {
            ///  Debug.Log("TabClick " + ind);



            foreach (IInputComponent item in myComponentsList)
            {
                item.OnTabChange(ind);
            }

            if (ind == null) return;
            if (myTabRow == null) return;
            for (int i = 0; i < myTabRow.transform.childCount; i++)
            {
                Transform _child = myTabRow.transform.GetChild(i);
                // Debug.Log(_child.name);
                _child.Find("_isActive").gameObject.SetActive(_child.name == ind);
            }
        }


        public void SetSaveButton(bool isShow, string label = "Save")
        {
            if (btnRow == null) return;
            btnRow.gameObject.SetActive(isShow);
            btnSave.transform.Find("_text").GetComponent<Text>().text = label;
            //Debug.Log("BTN CREATED");
            btnRow.GetComponent<RectTransform>().SetAsLastSibling();
        }

        public void AddTemplateElement(string key, RowElementType element)
        {
            if (element.LaoytOrder == 0)
            {
                lastLaoytOrder += 10;
                element.LaoytOrder = lastLaoytOrder;
            }
            template[key] = element;
        }
        public CheckboxComponent_SE AddCheckbox(Transform toContainer)
        {
            GameObject go = Instantiate(baseDataSE.checkboxComponent.gameObject, toContainer);
            CheckboxComponent_SE input = go.GetComponent<CheckboxComponent_SE>();

            return input;
        }
        public InputTextComponent_SE AddInputString(Transform toContainer)
        {
            GameObject go = Instantiate(baseDataSE.inputComponent.gameObject, toContainer);
            InputTextComponent_SE input = go.GetComponent<InputTextComponent_SE>();

            return input;
        }
        public static void ClearAllChildren(Transform conteiner)
        {
           
            for (int i = 0; i < conteiner.childCount; i++)
            {
                //if (conteiner.GetChild(i).gameObject.name == "btnSaveRow") continue;
                Destroy(conteiner.GetChild(i).gameObject);
            }
        }

        public void ClearAllChildren()
        {
            myComponentsList.Clear();
            for (int i = 0; i < conteiner.childCount; i++)
            {
                if (conteiner.GetChild(i).gameObject.name == "btnSaveRow") continue;
                Destroy(conteiner.GetChild(i).gameObject);
            }
        }

        public void TemplateRead()
        {



            foreach (var line in template)
            {

                string key = line.Key;

                if (line.Value.componentCreated == null) continue;

                if (line.Value.mytype == InputDataTypeEnum._bool)
                {
                    line.Value.valueBool = (line.Value.componentCreated.GetValueAsString() == "true");
                }

                if (line.Value.mytype == InputDataTypeEnum._string)
                {
                    line.Value.valueString = (line.Value.componentCreated.GetValueAsString());
                }

                if (line.Value.mytype == InputDataTypeEnum._int)
                {
                    line.Value.valueInt = line.Value.componentCreated.GetValueAsString().ToIntval();
                }

                if (line.Value.mytype == InputDataTypeEnum._Vector3Int)
                {
                    line.Value.valueVector3Int = ToVector2Int( line.Value.componentCreated.GetValueAsString());
                }

                if (line.Value.mytype == InputDataTypeEnum._float)
                {
                    //Debug.Log(line.Value.componentCreated.GetValueAsString().Replace(",", "."));
                    string str = line.Value.componentCreated.GetValueAsString().Replace(",", ".");

                    line.Value.valueDouble = float.Parse(str, CultureInfo.InvariantCulture);
                }

                if (line.Value.mytype == InputDataTypeEnum._dropdown)
                {
                    line.Value.valueInt = line.Value.componentCreated.GetValueAsString().ToIntval();
                }

                if (line.Value.mytype == InputDataTypeEnum._selectBig)
                {
                    line.Value.valueString = line.Value.componentCreated.GetValueAsString();
                }
            }
        }

        public static string ToStringNormal(Vector3Int val)
        { 
            return val.x + ";" + val.y;
        }

        public static Vector3Int ToVector2Int( string val)
        {
            string [] _p = val.Split(';');
            Vector3Int v = new Vector3Int(int.Parse(_p[0]), int.Parse(_p[1]));
             
            return v;
        }

        public void TemplateCreate()
        {
            if (myTabRow == null)
            {
                myTabRow = Instantiate(baseDataSE.tabRow_SE.gameObject, conteiner).GetComponent<TabRow_SE>();
            }

            string firstTab = null;
            foreach (var line in template)
            {
                if (line.Value.tab == null) continue;
                if (tabs.Contains(line.Value.tab)) continue; 
                tabs.Add(line.Value.tab);
                ElementTabRow_SE go = Instantiate(baseDataSE.elementTabRow_SE.gameObject, myTabRow.transform).GetComponent<ElementTabRow_SE>();
                go.ind = line.Value.tab;
                go.formBuilder = this;
                go.transform.Find("_text").GetComponent<Text>().text = go.ind;

                go.name = go.ind;
                go.Init();

                if (firstTab == null)
                {
                    firstTab = line.Value.tab;
                }

            }


            foreach (var line in template)
            {
                RowElementType elem = line.Value;
                string key = line.Key;

                  IInputComponent inputComponent = null;

                if (elem.mytype == InputDataTypeEnum._bool)
                {
                    CheckboxComponent_SE input = AddCheckbox(conteiner);
                    inputComponent = input;
                    input.val = elem.valueBool; 
                    
                }

                if (elem.mytype == InputDataTypeEnum._string)
                {
                    InputTextComponent_SE input = AddInputString(conteiner);
                    inputComponent = input;
                    input.val = elem.valueString;
                }

                if (elem.mytype == InputDataTypeEnum._int)
                {
                    InputTextComponent_SE input = AddInputString(conteiner);
                    inputComponent = input;
                    input.SetTypeToInt();
                    input.val = elem.valueInt.ToString(); 
                }

                if (elem.mytype == InputDataTypeEnum._float)
                {
                    InputTextComponent_SE input = AddInputString(conteiner);
                    inputComponent = input;
                    input.val = elem.valueDouble.ToString();
                    input.SetTypeToFloat();
                }

                if (elem.mytype == InputDataTypeEnum._Vector3Int)
                {
                    GameObject go = Instantiate(baseDataSE.inputVector2.gameObject, conteiner);
                    InputVextor2Component_SE input = go.GetComponent<InputVextor2Component_SE>();
                    inputComponent = input;
                    input.val = elem.valueVector3Int;
                }

                if (elem.mytype == InputDataTypeEnum._dropdown)
                {
                    GameObject go = Instantiate(baseDataSE.dropdownComponent.gameObject, conteiner);
                    InputDropdownComponent_SE input = go.GetComponent<InputDropdownComponent_SE>(); 
                    inputComponent = input;
                    input.val = elem.valueInt; 
                    input.inputBox.AddOptions(elem.options);
                }

                if (elem.mytype == InputDataTypeEnum._selectBig)
                {
                    GameObject go = Instantiate(baseDataSE.inputBigSelect.gameObject, conteiner);
                    inputBigSelect_SE input = go.GetComponent<inputBigSelect_SE>(); 
                    inputComponent = input;
                    input.val = elem.valueString; 
                    input.options=(elem.optionsSelectBig);
                }

                if (elem.mytype == InputDataTypeEnum.Title)
                {
                    GameObject go = Instantiate(baseDataSE.titleComponent, conteiner);
                    go.transform.Find("Label").GetComponent<Text>().text = elem.label;
                    go.GetComponent<RectTransform>().SetSiblingIndex(elem.LaoytOrder);
                }

                if (inputComponent == null) continue;
                inputComponent.SetTab(line.Value.tab);
                inputComponent.SetLabel(elem.label);
                inputComponent.Render();
                inputComponent.SetNameGo(line.Key);
                inputComponent.SetLaoytOrder(elem.LaoytOrder);
                elem.componentCreated = inputComponent;
                myComponentsList.Add(inputComponent);
            }

            TabClick(firstTab);
        }

        public virtual void ClickBtnSave()
        {

        }

        public virtual void Init()
        {

            if (myTabRow == null)
            {
                if (transform.Find("TabRow") != null)
                {
                    myTabRow = transform.Find("TabRow").GetComponent<TabRow_SE>();
                }
            }

            btnRow = conteiner.Find("btnSaveRow");
            if (btnRow!=null)
            {
                btnSave = btnRow.transform.Find("btnSave").GetComponent<Button>();
                btnSave.onClick.AddListener(ClickBtnSave);
            }

            baseDataSE = BaseDataSE.Get();
        }


        void Start()
        {

            Init();
        }

    }

}