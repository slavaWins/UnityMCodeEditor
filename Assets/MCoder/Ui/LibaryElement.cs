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

    public class LibaryElement : DragableElementLine<NodeCodeLineElement>
    {
        internal MC_Coder_Script coderMNodePanel;

        internal MC_BaseNodeElement nodeClass;
        internal UMC_WindowLibary callbackPanel;

        public GameObject addHerePrefab;
        GameObject addHere;

        public TMP_Text h1;
        public TMP_Text small;
        public TMP_Text iconText; 
        private Canvas canvas;


        // Start is called before the first frame update
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
            myGhostDragibleCline.sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta / 2;
        }



        bool OnMoveUpdateInAny_Check(GameObject go)
        {
            if (go.transform.parent.parent != coderMNodePanel.transform) return false;

            if (addHere == null) addHere = Instantiate(addHerePrefab);
            addHere.transform.SetParent(coderMNodePanel.container);
            addHere.transform.SetAsFirstSibling();
            return true;
        }

        //Превью в начеле контейнера
        internal override void OnMoveUpdateInAny(GameObject go)
        {
            OnMoveUpdateInAny_Check(go);

        }

        internal override void OnMoveUpdateInTargetClass()
        {
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
            base.DragEnd_TargetClass();
            NodeAddToCode(targetDropClass.lineNumber);

            if (addHere != null) Destroy(addHere);
        }

        public override void DragEnd_UnTargetClass(GameObject gameObject)
        {
            
            if (OnMoveUpdateInAny_Check(gameObject))
            {

                NodeAddToCode(-1);

                if (addHere != null) Destroy(addHere);
            }
        }

        internal override void OnDragStop(PointerEventData eventData = null)
        {
            base.OnDragStop();
            if (addHere != null) Destroy(addHere);
        }


        public void NodeAddToCode(int linePos)
        {
            //Debug.Log(coderMNodePanel);
           // Debug.Log(nodeClass);
           // Debug.Log(linePos);
            coderMNodePanel.AddLine(nodeClass, linePos);
        }
       


        internal void Render()
        {
            iconText.text = nodeClass.iconText;
            h1.text = nodeClass.name;
            small.text = nodeClass.descr; 
        }
    }
}