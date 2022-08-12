using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MCoder;

namespace MCoder.UI
{
    public class NodeLibaryPanel : MonoBehaviour
    {
        public MC_Coder_Script coderMNodePanel;
        public Transform container;
        public LibaryElement element;


        // Start is called before the first frame update
        void Start()
        {
            foreach (MC_BaseNodeElement item in MC_BD_Nodes.GetAllNodesList())
            {
                LibaryElement go = Instantiate(element.gameObject, container).GetComponent<LibaryElement>();
                go.coderMNodePanel = coderMNodePanel;
                go.nodeClass = item;
                go.callbackPanel = this;
                go.Render();

            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}