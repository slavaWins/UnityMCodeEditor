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

    public class LibaryElement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        internal CoderMNodePanel coderMNodePanel;

        internal MC_BaseNodeElement nodeClass;
        internal NodeLibaryPanel callbackPanel;

        public GameObject addHerePrefab;
        public GameObject addHere;

        public TMP_Text h1;
        public TMP_Text small;
        public TMP_Text iconText;
        private RectTransform m_DraggingPlane;
        private NodeCodeLineElement dragNodeCodeLineElement;
        private Canvas canvas;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            canvas = PanelSeController.FindInParents<Canvas>(gameObject);

            if (canvas == null) return;


            m_DraggingPlane = Instantiate(gameObject, canvas.transform).GetComponent<RectTransform>();
            Destroy(m_DraggingPlane.GetComponent<LibaryElement>());
            m_DraggingPlane.SetAsLastSibling();
            m_DraggingPlane.sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta / 2;

            SetDraggedPosition(eventData);
        }




        private void SetDraggedPosition(PointerEventData data)
        {

            if (m_DraggingPlane == null) return;


            Vector3 globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
            {
                m_DraggingPlane.position = globalMousePos + new Vector3(m_DraggingPlane.sizeDelta.x / 2, m_DraggingPlane.sizeDelta.y, 0);
            }
        }


        public void OnDrag(PointerEventData eventData)
        {


            SetDraggedPosition(eventData);
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                if (eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<NodeCodeLineElement>()) {
                    dragNodeCodeLineElement = eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<NodeCodeLineElement>();

                    if (addHere == null)
                    {
                        addHere = Instantiate(addHerePrefab);
                    }
                    addHere.transform.SetParent(dragNodeCodeLineElement.transform.parent);
                    addHere.transform.SetSiblingIndex(dragNodeCodeLineElement.transform.GetSiblingIndex() + 1);

                    return;
                }
            }

            if (dragNodeCodeLineElement != null)
            {
                Destroy(addHere);
                m_DraggingPlane.SetParent(canvas.transform);
                dragNodeCodeLineElement = null;
            }

        }

        public void NodeAddToCode(int linePos)
        {
            
            coderMNodePanel.AddLine(nodeClass, linePos);
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            /*
            Debug.Log(eventData.lastPress);
            Debug.Log(eventData.pointerClick);
            Debug.Log(eventData.pointerDrag);
            Debug.Log(eventData.pointerCurrentRaycast);
            */
            Destroy(m_DraggingPlane.gameObject);
            if (dragNodeCodeLineElement != null)
            { 
                Debug.Log(dragNodeCodeLineElement.lineNumber);
                NodeAddToCode(dragNodeCodeLineElement.lineNumber);
            }
            if (addHere != null)
            {
                Destroy(addHere); 
            }
        }


        internal void Render()
        {
            iconText.text = nodeClass.iconText;
            h1.text = nodeClass.name;
            small.text = nodeClass.descr; 
        }
    }
}