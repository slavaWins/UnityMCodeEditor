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

        /// <summary> Это клон этого ГО, который будет двигаться за курсором </summary>
        [HideInInspector]
        public RectTransform myGhostDragibleCline;

        /// <summary> Искомый объект </summary>
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



        internal virtual void OnDragStop(PointerEventData eventData=null)
        {

        }
        /// <summary> Впервые зашел на таргет класс </summary>
        internal virtual void OnMoveEnterInTargetClass()
        {

        }

        /// <summary> Вышел из  таргет класса </summary>
        internal virtual void OnMoveExitInTargetClass()
        {
         //   Debug.Log("Я ушел с нужного класса");
        }

        /// <summary>Тащем объект по какому-то другому объекту, который не таргет</summary>
        internal virtual void OnMoveUpdateInAny(GameObject go)
        {

        }
        internal virtual void OnMoveUpdateInTargetClass()
        {
         //  Debug.Log("Я пролетел над нужным классом");
          //  Debug.Log(targetDropClass);
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


        TargetDragClass SearchTargetGo(GameObject go)
        {
            if (go == null) return null;
            TargetDragClass _isset = null;

            _isset = go.transform.GetComponent<TargetDragClass>();
            if (_isset != null) return _isset;

            _isset = go.transform.parent.GetComponent<TargetDragClass>();
            if (_isset != null) return _isset;

            return null;
        }

        public void OnDrag(PointerEventData eventData)
        {


            SetDraggedPosition(eventData);


            TargetDragClass _targetSearch = SearchTargetGo(eventData.pointerCurrentRaycast.gameObject);
            if (_targetSearch != null)
            {

                TargetDragClass _newTarget = _targetSearch;

                if (_newTarget != targetDropClass && targetDropClass != null)
                {
                    OnMoveExitInTargetClass();
                     
                    
                }
              
          

                if (_newTarget != targetDropClass)
                {
                    targetDropClass = _newTarget;
                    OnMoveEnterInTargetClass();
                }
                targetDropClass = _newTarget;

                OnMoveUpdateInTargetClass();
                return;

            }

           

            if (targetDropClass != null)
            {
                OnMoveExitInTargetClass();
                // Destroy(addHere);
                // myGhostDragibleCline.SetParent(canvas.transform);
                targetDropClass = null;
            }

            OnMoveUpdateInAny(eventData.pointerCurrentRaycast.gameObject);

        }

        public virtual void DragEnd_UnTargetClass(GameObject gameObject)
        {

        }

        public virtual void DragEnd_TargetClass()
        {
           // Debug.Log("Дропнуто в нужный класс"); 
           // Debug.Log(targetDropClass); 
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
            OnDragStop(eventData);

            Destroy(myGhostDragibleCline.gameObject);

            if (targetDropClass != null)
            {  
                
                DragEnd_TargetClass();
            }
            else
            { 
                DragEnd_UnTargetClass(eventData.pointerCurrentRaycast.gameObject);
            }
            /*
            if (addHere != null)
            {
                Destroy(addHere); 
            }*/
        }

 
    }
}