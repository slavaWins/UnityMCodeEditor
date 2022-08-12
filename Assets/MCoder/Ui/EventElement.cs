using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MCoder;
using System;
using TMPro;
using UnityEngine.EventSystems;
using SEditor;

namespace MCoder.UI
{

    public class EventElement : DragableElementLine<NodeCodeLineElement>
    {
        internal CoderMNodePanel coderMNodePanel;

        internal MC_BaseNodeElement nodeClass;
        internal NodeLibaryPanel callbackPanel;


        public TMP_Text h1;
        public TMP_Text small;

        private RectTransform m_DraggingPlane;
        private NodeCodeLineElement dragNodeCodeLineElement;
        private Canvas canvas;


        public override void OnCreateGhostDrag()
        {
            base.OnCreateGhostDrag();
            Destroy(myGhostDragibleCline.GetComponent<EventElement>());
            Debug.Log("OnCreateGhostDrag eeeee");
            //Destroy(myGhostDragibleCline.GetComponent<XXX>());
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


      

        

        public void NodeAddToCode(int linePos)
        {
            
            coderMNodePanel.AddLine(nodeClass, linePos);
        }
       

        internal void Render()
        { 
            h1.text = nodeClass.name;
            small.text = nodeClass.descr; 
        }
    }
}