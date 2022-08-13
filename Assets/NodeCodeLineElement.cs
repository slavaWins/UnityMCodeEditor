using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MCoder;
using TMPro;

namespace MCoder.UI
{
    public class NodeCodeLineElement : DragableElementLine<NodeCodeLineElement>
    {

        public List<TMP_InputField> argumentsInputsList;
        public GameObject argumentInput;
        internal MC_BaseNodeElement nodeClass;
        internal MC_Coder_Script callbackPanel;
         

        public int lineNumber;
        public TMP_Text h1;
        public TMP_Text small;
        public TMP_Text iconText;


        [Header("Drag")]
        public GameObject addHerePrefab;
        GameObject addHere;


        internal void SetPadding(int val)
        {
           // Debug.Log(val);
            GetComponent<VerticalLayoutGroup>().padding.left = val*20;
        }

      
        internal void ReadInput()
        {
            if (nodeClass == null) return;
            if (nodeClass.arguments == null) return;
            if (nodeClass.arguments.Count == 0) return;

            int L = -1;
            foreach (var arg in nodeClass.arguments)
            {
                L++;
                if (nodeClass.values.Count-1 < L) nodeClass.values.Add(0 as object);
                Debug.Log(nodeClass.values[L]);
                Debug.Log(inputslist[L].text);
                Debug.Log((object)inputslist[L].text);

                nodeClass.values[L] = (object)inputslist[L].text;
            }
        }


        public List<TMP_InputField> inputslist = new List<TMP_InputField>();

        internal void Render()
        {
            inputslist.Clear();
            iconText.text = nodeClass.iconText;
            if (nodeClass.isType_IF()) iconText.text = "IF";
            if (nodeClass.isType_END()) iconText.text = "END";


            h1.text = nodeClass.name + " | " + nodeClass.descr;
            small.text = nodeClass.descr;

            int i = -1;
            foreach( MC_Argument arg in nodeClass.arguments)
            {
                i++;
                //Debug.Log(arg.name);
                GameObject go =  Instantiate(argumentInput, transform);
                go.transform.Find("_h1").GetComponent<TMP_Text>().text = arg.name ;
                go.transform.Find("_type").GetComponent<TMP_Text>().text =  arg.myType.ToString().Replace("_","");

                TMP_InputField inp =  go.transform.Find("_inpt").GetComponent<TMP_InputField>();

                inputslist.Add(inp);
                inp.text = "";
                if (nodeClass.values.Count-1 >= i)
                {
                    inp.text = nodeClass.values[i].ToString();
                }

                argumentsInputsList.Add(inp);

            }
        }


        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }










        public override void OnCreateGhostDrag()
        {
            base.OnCreateGhostDrag();
            Destroy(myGhostDragibleCline.GetComponent<LibaryElement>());
            Destroy(myGhostDragibleCline.GetComponent<NodeCodeLineElement>());
            myGhostDragibleCline.sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta / 3;
        }


        internal override void OnMoveUpdateInTargetClass()
        {
            if (targetDropClass == this) return;

            base.OnMoveUpdateInTargetClass();
            if (addHere == null)
            {
                addHere = Instantiate(addHerePrefab);
            }
            addHere.transform.SetParent(targetDropClass.transform.parent);
            addHere.transform.SetSiblingIndex(targetDropClass.transform.GetSiblingIndex() + 1);
        }


        internal override void OnMoveExitInTargetClass()
        {

            base.OnMoveExitInTargetClass();
            if (addHere != null)
            {
                Destroy(addHere);
            }
        }

        public override void DragEnd_TargetClass()
        {
            if (targetDropClass == this) return;

            base.DragEnd_TargetClass();
            callbackPanel.MoveLine(lineNumber, targetDropClass.lineNumber );
            

            if (addHere != null) Destroy(addHere);
        }

    

        internal override void OnDragStop()
        {
            base.OnDragStop();
            if (addHere != null) Destroy(addHere);
        }



    }
}