using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MCoder;
using System;
using MCoder.Libary;

namespace MCoder.UI
{

    public class MC_Coder_Script : MonoBehaviour
    {
        public Transform container;
        public NodeCodeLineElement elementPrefab;
        public int currentEventNumber = 0;

        public List<NodeCodeLineElement> Nodes = new List<NodeCodeLineElement>();

        internal void EventCreate(MC_Base_Event plusEvent)
        {
            MC_Base_Event _eventNewFromClass = (MC_Base_Event)Activator.CreateInstance(plusEvent.GetType());
            MC_NodeEventModule module = new MC_NodeEventModule(BodyTypeEnum.block);
            module.myEvent = _eventNewFromClass;

            mC_BaseInstance.nodesForEvents.Add(module);
        }


        internal void EventSelect(int Line)
        {
            currentEventNumber = Line;
            Render();

        }

        public MC_BaseInstance mC_BaseInstance = new ExampleInstanceDamageIfClick();


        public void AddLine(MC_BaseNodeElement myClass, int postLine)
        {
            List<IMCoder_NodeElement> logicnodes = new List<IMCoder_NodeElement>();

            if (postLine == -1) logicnodes.Add(myClass);

            int L = 0;
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


        public void Render()
        {

            SEditor.FormBuilder.ClearAllChildren(container);

            int padding = 0;

            Debug.Log("currentEventNumber: " + currentEventNumber);


            int L = 0;
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