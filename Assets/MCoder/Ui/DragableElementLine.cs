using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MCoder;
using System;
using System.Linq;

using TMPro;
using UnityEngine.EventSystems;
using SEditor;

namespace MCoder.UI
{

    public class DragableElementLine <TargetDragClass>: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
         where TargetDragClass : class ,  new()
    {

        /// <summary> ��� ���� ����� ��, ������� ����� ��������� �� �������� </summary>
        [HideInInspector]
        public RectTransform myGhostDragibleCline;

        /// <summary> ������� ������ </summary>
        [HideInInspector]
        public TargetDragClass targetDropClass;


        private Canvas canvas;

 
        public virtual void OnCreateGhostDrag()
        {
            //Destroy(myGhostDragibleCline.GetComponent<XXX>());
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            canvas = PanelSeController.FindInParents<Canvas>(gameObject);

            if (canvas == null) return;


            myGhostDragibleCline = Instantiate(gameObject, canvas.transform).GetComponent<RectTransform>();
            
            myGhostDragibleCline.SetAsLastSibling();
            myGhostDragibleCline.sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta / 2;
            OnCreateGhostDrag();


            SetDraggedPosition(eventData);
        }



        internal virtual void OnMoveExitInTargetClass()
        {
            Debug.Log("� ���� � ������� ������");
        }

        internal virtual void OnMoveUpdateInTargetClass()
        {
            Debug.Log("� �������� ��� ������ �������");
            Debug.Log(targetDropClass);
        }


        private void SetDraggedPosition(PointerEventData data)
        {

            if (myGhostDragibleCline == null) return;


            Vector3 globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(myGhostDragibleCline, data.position, data.pressEventCamera, out globalMousePos))
            {
                myGhostDragibleCline.position = globalMousePos + new Vector3(myGhostDragibleCline.sizeDelta.x / 2, myGhostDragibleCline.sizeDelta.y, 0);
            }
        }


        public void OnDrag(PointerEventData eventData)
        {


            SetDraggedPosition(eventData);
            

            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                if (eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<TargetDragClass>()!=null) {
                    TargetDragClass _newTarget = eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<TargetDragClass>();
                   
                    if(_newTarget!= targetDropClass && targetDropClass!=null)
                    {
                        OnMoveExitInTargetClass();
                    }

                    targetDropClass = _newTarget;
                    OnMoveUpdateInTargetClass(); 
                    return;
                }
            }

            if (targetDropClass != null)
            {
                OnMoveExitInTargetClass();
                // Destroy(addHere);
                // myGhostDragibleCline.SetParent(canvas.transform);
                targetDropClass = null;
            }

        }

        public virtual void DragEndTRargetClass()
        {
            Debug.Log("�������� � ������ �����"); 
            Debug.Log(targetDropClass); 
        }

        public void OnEndDrag(PointerEventData eventData)
        {
           // Debug.Log("OnEndDrag");
            /*
            Debug.Log(eventData.lastPress);
            Debug.Log(eventData.pointerClick);
            Debug.Log(eventData.pointerDrag);
            Debug.Log(eventData.pointerCurrentRaycast);
            */
            Destroy(myGhostDragibleCline.gameObject);

            if (targetDropClass != null)
            {  
                DragEndTRargetClass();
            }
            /*
            if (addHere != null)
            {
                Destroy(addHere); 
            }*/
        }

 
    }
}