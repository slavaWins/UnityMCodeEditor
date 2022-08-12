using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MCoder;



namespace MCoder.UI
{

    public class CoderMNodePanel : MonoBehaviour
    {
        public Transform container ;
        public  NodeCodeLineElement elementPrefab;
      
        public List<NodeCodeLineElement> Nodes = new List<NodeCodeLineElement>();

        public MC_BaseInstance mC_BaseInstance = new ExampleInstanceDamageIfClick();


        public void AddLine(MC_BaseNodeElement myClass, int postLine)
        {
            List<IMCoder_NodeElement> logicnodes = new List<IMCoder_NodeElement>();

            int L = 0;
            foreach (MC_BaseNodeElement lgn in mC_BaseInstance.nodesForEvents[0].logicnodes)
            {
                L++;
                logicnodes.Add(lgn);
                if (L == postLine)
                {
                    logicnodes.Add(myClass);
                }
            }
            mC_BaseInstance.nodesForEvents[0].logicnodes.Clear();
            mC_BaseInstance.nodesForEvents[0].logicnodes = logicnodes;
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


            NodeCodeLineElement go = null;
            int L = 0;
            foreach (MC_BaseNodeElement lgn in mC_BaseInstance.nodesForEvents[0].logicnodes)
            {
                L++;

                go = Instantiate(elementPrefab.gameObject, container).GetComponent<NodeCodeLineElement>();
                go.lineNumber = L;
                go.nodeClass = lgn;
                go.callbackPanel = this;
                if (lgn.isType_END()) padding -= 1;
                go.SetPadding(padding);
                go.Render();
                if (lgn.isType_IF()) padding += 1; 
            }

            if (go != null)
            {
                go.transform.SetParent(null);
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

}