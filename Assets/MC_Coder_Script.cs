using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MCoder;
using TMPro;
using SEditor;
using System;
using MCoder.Libary;
using static MCoder.MC_BaseInstance;

namespace MCoder.UI
{

    public class MC_Coder_Script : MonoBehaviour, ICallbackInputDropDown
    {

        [Header("Integrate Windows")]
        public MC_WindowVarible windowVarible;
        public MC_WindowsEvent windowsEvent;

        [Header("Integrate Other ")]
        public TMP_Text errorText;

        [Header("Inner")]
        public Transform container;


        [Header("Prefabs")]
        public NodeCodeLineElement elementPrefab; 
        public int currentEventNumber = 0;

        [Header("Others")]
        public InputDropdownComponent_SE selectBodyTypeForScript;

        public List<NodeCodeLineElement> Nodes = new List<NodeCodeLineElement>();

 public MC_BaseInstance mC_BaseInstance = new ExampleInstanceDamageIfClick();

        internal void EventCreate(MC_Base_Event plusEvent)
        {
            ReadInputs();
            MC_Base_Event _eventNewFromClass = (MC_Base_Event)Activator.CreateInstance(plusEvent.GetType());
            MC_NodeEventModule module = new MC_NodeEventModule(BodyTypeEnum.block);
            module.myEvent = _eventNewFromClass;

            mC_BaseInstance.nodesForEvents.Add(module);
        }


        internal void EventSelect(int Line)
        {
            ReadInputs();
            currentEventNumber = Line;
            Render();

        }

       


        internal void MoveLine(int from, int postLine)
        {
            ReadInputs();
            Debug.Log("MoveLine " + from + " -> " + postLine);
            MC_BaseNodeElement myClass = mC_BaseInstance.nodesForEvents[currentEventNumber].logicnodes[from];

            MC_BaseNodeElement postClass = mC_BaseInstance.nodesForEvents[currentEventNumber].logicnodes[postLine];

            List<MC_BaseNodeElement> logicnodes = new List<MC_BaseNodeElement>();


            // if (postLine == -1) logicnodes.Add(myClass);

            int L = -1;
            foreach (MC_BaseNodeElement lgn in mC_BaseInstance.nodesForEvents[currentEventNumber].logicnodes)
            {

                if (lgn == myClass) continue;

                L++;

                logicnodes.Add(lgn);

                if (lgn == postClass)
                {
                    Debug.Log("PPP");
                    logicnodes.Add(myClass);
                }
            }
            mC_BaseInstance.nodesForEvents[currentEventNumber].logicnodes.Clear();
            mC_BaseInstance.nodesForEvents[currentEventNumber].logicnodes = logicnodes;
            Render(); 

        }

        public void AddLine(MC_BaseNodeElement myClass, int postLine)
        {
            ReadInputs();
            List<MC_BaseNodeElement> logicnodes = new List<MC_BaseNodeElement>();

            if (postLine == -1) logicnodes.Add(myClass);

            int L = -1;
            foreach (MC_BaseNodeElement lgn in mC_BaseInstance.nodesForEvents[currentEventNumber].logicnodes)
            {
                L++;
                logicnodes.Add(lgn);
                if (L == postLine)
                {
                    logicnodes.Add(myClass);
                }
            }
            mC_BaseInstance.nodesForEvents[currentEventNumber].logicnodes.Clear();
            mC_BaseInstance.nodesForEvents[currentEventNumber].logicnodes = logicnodes;
            Render();


        }

        /*
        public void PublicAddClass(string indInLibary, int postLine)
        {
            foreach(MC_BaseNodeElement item in MC_BD_Nodes.GetList())
            {
                if (item.GetType().ToString() == indInLibary)
                {
                    AddLine(item, postLine);
                    ParseNode();
                }
            }
            return;
        }
        */

        public void SaveFromToNode()
        {

        }
        internal void ReadInputs()
        {
            for (int i = 0; i < container.childCount; i++)
            {
                GameObject go = container.GetChild(i).gameObject;
                if (!go.GetComponent<NodeCodeLineElement>()) continue;
                NodeCodeLineElement line = go.GetComponent<NodeCodeLineElement>();
                line.ReadInput();
            }
        }

        public NodeCodeLineElement GetNodeLineElementByLine(int lineNumber)
        {
            int J=-1;
            for (int i = 0; i < container.childCount; i++)
            {
                GameObject go = container.GetChild(i).gameObject;
                if (!go.GetComponent<NodeCodeLineElement>()) continue;
                J++;
                
                NodeCodeLineElement line = go.GetComponent<NodeCodeLineElement>();
                if (J == lineNumber)
                {
                    return line;
                }
            }
            return null;
        }
    
        public void RenderVaribles( )
        {
            windowVarible.ClearAll();

            int L = -1;
            foreach (MC_Argument lgn in mC_BaseInstance.nodesForEvents[currentEventNumber].myEvent.arguments)
            {
                L++;
                Debug.Log(lgn.name);
                windowVarible.AddVarible(L, lgn, MC_Value_LinkType._event, false);
            }

              L = -1;
            foreach (MC_Argument lgn in mC_BaseInstance.argumentsCustoms)
            {
                L++;
                windowVarible.AddVarible(L, lgn, MC_Value_LinkType._custom, true);
            }


              L = -1;
            foreach (MC_Argument lgn in mC_BaseInstance.argumentsInputs)
            {
                L++;
                windowVarible.AddVarible(L, lgn, MC_Value_LinkType._input, true);
            }
        }

        public void Render()
        {


           // Debug.Log("==Render");
            // ReadInputs();
            SEditor.FormBuilder.ClearAllChildren(container);

            int padding = 0;


            mC_BaseInstance.Init();

            // Debug.Log("currentEventNumber: " + currentEventNumber);

            int showwErrorInLine = -1;
            MC_Error error = mC_BaseInstance.Validate();
            if (error != null)
            {
                if (error.eventLin == currentEventNumber)
                {
                    showwErrorInLine = error.lineLogic;
                }
            }
            
                int L = -1;
            foreach (MC_BaseNodeElement lgn in mC_BaseInstance.nodesForEvents[currentEventNumber].logicnodes)
            {
                L++;

                NodeCodeLineElement go = Instantiate(elementPrefab.gameObject, container).GetComponent<NodeCodeLineElement>();
                go.lineNumber = L;
                go.nodeClass = lgn;
                go.callbackPanel = this;
               
                if (lgn.isType_END()) padding -= 1;
                go.SetPadding(padding);
                go.Render();

                if (L == showwErrorInLine)
                {
                    go.SetVisibleError(true);
                    go.SetVisibleErrorInArgument(true, error.agrumentNumber);

                }

                if (lgn.isType_IF()) padding += 1;

              
            }
            RenderVaribles();
            windowsEvent.HideAllError();
            CheckAndDrawError();

            StartCoroutine(container.GetComponent<VerticalLayoutGroup>().ChangeUpdate());
             
        }

        void CheckAndDrawError()
        {
            MC_Error error = mC_BaseInstance.Validate();
            errorText.gameObject.SetActive(error != null);
            if (error == null) return;

            //Debug.Log("=========iset errror");
            errorText.text = error.text;


           // Debug.Log("eventLin errror " + error.eventLin);
            windowsEvent.ShowErrorIn(error.eventLin);

            /*
            if (error.eventLin != currentEventNumber) return;

            Debug.Log("this current eventLin errror " + error.eventLin);

            NodeCodeLineElement nodeCodeLineElement = GetNodeLineElementByLine(error.lineLogic);
           
            if (nodeCodeLineElement == null) return; 
            Debug.Log("lineLogic isset element " + error.lineLogic);

            nodeCodeLineElement.SetVisibleError(true);
            */

        }

        public delegate void SetBodyType(string ind);


        void Start()
        {
            mC_BaseInstance.exampleBody = new ExampleBody();
            mC_BaseInstance.bodyType = (BodyTypeEnum.mob);
            //Добавляю инпут боди тайп всего скрипта
            List<TMP_Dropdown.OptionData> _opt = new List<TMP_Dropdown.OptionData>();
            foreach (string _name in Enum.GetNames(typeof(BodyTypeEnum)))
            {
                _opt.Add(new TMP_Dropdown.OptionData() { text = _name });
            }
            selectBodyTypeForScript.inputBox.options = _opt;
            selectBodyTypeForScript.callbackClass = this;
            selectBodyTypeForScript.SetValue((int)mC_BaseInstance.bodyType);


            Render();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetOptionFromSelect(int id, string ind, InputDropdownComponent_SE from)
        {
            if (from == selectBodyTypeForScript) {
                mC_BaseInstance.bodyType = (BodyTypeEnum)id;
                Render();
            }

        }
    }

    public static class LayoutHelper
    {
        public static IEnumerator ChangeUpdate(this VerticalLayoutGroup horizLayoutGroup)
        {

            //horizLayoutGroup.enabled = false;
            yield return new WaitForEndOfFrame();
            // horizLayoutGroup.enabled = true; 
            horizLayoutGroup.CalculateLayoutInputHorizontal();
            horizLayoutGroup.CalculateLayoutInputVertical();
            horizLayoutGroup.SetLayoutHorizontal();
            horizLayoutGroup.SetLayoutVertical();
            yield return null;
        }
    }
}