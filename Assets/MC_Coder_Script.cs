using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MCoder;
using TMPro;
using System;
using MCoder.Libary;

namespace MCoder.UI
{

    public class MC_Coder_Script : MonoBehaviour
    {
        public TMP_Text errorText;
        public Transform container;
        public NodeCodeLineElement elementPrefab;
        public int currentEventNumber = 0;

        public List<NodeCodeLineElement> Nodes = new List<NodeCodeLineElement>();

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

        public MC_BaseInstance mC_BaseInstance = new ExampleInstanceDamageIfClick();


        internal void MoveLine(int from, int postLine)
        {
            ReadInputs();
            Debug.Log("MoveLine " + from + " -> " + postLine);
            IMCoder_NodeElement myClass = mC_BaseInstance.nodesForEvents[currentEventNumber].logicnodes[from];

            IMCoder_NodeElement postClass = mC_BaseInstance.nodesForEvents[currentEventNumber].logicnodes[postLine];

            List<IMCoder_NodeElement> logicnodes = new List<IMCoder_NodeElement>();

        
           // if (postLine == -1) logicnodes.Add(myClass);

            int L = -1;
            foreach (IMCoder_NodeElement lgn in mC_BaseInstance.nodesForEvents[currentEventNumber].logicnodes)
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
            List<IMCoder_NodeElement> logicnodes = new List<IMCoder_NodeElement>();

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

        public void Render()
        {
           // ReadInputs();
            SEditor.FormBuilder.ClearAllChildren(container);

            int padding = 0;

           // Debug.Log("currentEventNumber: " + currentEventNumber);


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
                if (lgn.isType_IF()) padding += 1;
            }

            StartCoroutine(container.GetComponent<VerticalLayoutGroup>().ChangeUpdate());

            string _val = mC_BaseInstance.Validate();
            errorText.gameObject.SetActive(_val != null);
            if (_val != null)
            {
                errorText.text = _val;
            }
        }

       

        void Start()
        {
            Render();
        }

        // Update is called once per frame
        void Update()
        {

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