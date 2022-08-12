using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MCoder;
using TMPro;

namespace MCoder.UI
{
    public class NodeCodeLineElement : MonoBehaviour
    {

        public List<TMP_InputField> argumentsInputsList;
        public GameObject argumentInput;
        internal MC_BaseNodeElement nodeClass;
        internal CoderMNodePanel callbackPanel;
         

        public int lineNumber;
        public TMP_Text h1;
        public TMP_Text small;
        public TMP_Text iconText;


        internal void SetPadding(int val)
        {
           // Debug.Log(val);
            GetComponent<VerticalLayoutGroup>().padding.left = val*20;
        }

        internal void Render()
        {
            iconText.text = nodeClass.iconText;
            if (nodeClass.isType_IF()) iconText.text = "IF";
            if (nodeClass.isType_END()) iconText.text = "END";


            h1.text = nodeClass.name + " | " + nodeClass.descr;
            small.text = nodeClass.descr;

            int i = -1;
            foreach( MC_Argument arg in nodeClass.arguments)
            {
                i++;
                Debug.Log(arg.name);
                GameObject go =  Instantiate(argumentInput, transform);
                go.transform.Find("_h1").GetComponent<TMP_Text>().text = arg.name ;
                go.transform.Find("_type").GetComponent<TMP_Text>().text =  arg.myType.ToString().Replace("_","");

                TMP_InputField inp =  go.transform.Find("_inpt").GetComponent<TMP_InputField>();

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
    }
}