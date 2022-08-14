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
        public UMC_Element_Argument argumentInputPrefab;
        public MC_BaseNodeElement nodeClass;
        public MC_Coder_Script callbackPanel;
         

        public int lineNumber;
        public TMP_Text h1;
        public TMP_Text small;
        public TMP_Text iconText;
        public GameObject showError;


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
                if (nodeClass.values.Count-1 < L) nodeClass.values.Add(new MC_Value());
                /*
                Debug.Log(nodeClass.values[L]);
                Debug.Log(inputslist[L].text);
                Debug.Log((object)inputslist[L].text);
                */
                nodeClass.values[L].val = (object)inputslist[L].text;
            }
        }


        public List<TMP_InputField> inputslist = new List<TMP_InputField>();

        internal void SetVisibleError(bool val)
        {
            showError.gameObject.SetActive(val);
        }

        internal void SetVisibleErrorInArgument(bool val, int agrumentNumber)
        {
            
            showError.gameObject.SetActive(val);

            if (agrumentNumber == -1) return;
            if (inputslist.Count - 1 < agrumentNumber) return;
            if (inputslist[agrumentNumber] == null) return;
            inputslist[agrumentNumber].transform.parent.Find("_error").gameObject.SetActive(val);

        }

        public void OnEndEditArgumentText(string x)
        {
           // Debug.Log("OnEndEditArgumentText " + x );
            ReadInput();
            callbackPanel.Render();
        }

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

                if (nodeClass.values.Count - 1 < i)
                {
                    nodeClass.values.Add(new MC_Value(""));
                }

                    UMC_Element_Argument go =  Instantiate(argumentInputPrefab.gameObject, transform).GetComponent<UMC_Element_Argument>();
                go.argument = arg;
                go.classParent = this;
                go.nodeClass = nodeClass;
                go.meValue = nodeClass.values[i];
                go.Init();
                go.Render();

               

                TMP_InputField inp =  go.inp;

                inputslist.Add(inp);
                inp.text = "";
                if (nodeClass.values.Count-1 >= i)
                {
                    inp.text = nodeClass.values[i].val.ToString();
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