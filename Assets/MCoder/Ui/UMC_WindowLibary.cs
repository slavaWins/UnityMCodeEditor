using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MCoder;
using System;

namespace MCoder.UI
{
    public class UMC_WindowLibary : MonoBehaviour
    {
        public MC_Coder_Script coderMNodePanel;
        public Transform container;
        public LibaryElement element;
        


        internal void SetBodyType(BodyTypeEnum bodyType)
        {
            Rebder();

        }

        // Start is called before the first frame update
        public void Rebder()
        {
            SEditor.FormBuilder.ClearAllChildren(container);
            foreach (MC_BaseNodeElement item in MC_BD_Nodes.GetAllNodesList())
            {
                if (!item.IsSupportBodyType(coderMNodePanel.mC_BaseInstance.bodyType)) continue;

                LibaryElement go = Instantiate(element.gameObject, container).GetComponent<LibaryElement>();
                go.coderMNodePanel = coderMNodePanel;
                go.nodeClass = item;
                go.callbackPanel = this;
                go.Render();

            }
        }

        void Start()
        {
            Rebder();
        }

        // Update is called once per frame
        void Update()
        {

        }

      
    }

}