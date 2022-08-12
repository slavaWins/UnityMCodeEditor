using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace SEditor
{
    [RequireComponent(typeof(Image))]
    public class PanelSeController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        public bool dragOnSurfaces = true;


        [Header("Параметры")]
        public Sprite myIcon;
        public string title = "Новое окно";


        [Header("Внутрянка")]
        public Button btnClose;
        public TMP_Text titleWindow; 
        public Transform container; 
        public Image iconImage; 
        public Transform titleBar; 
        public Transform resizerGO; 

 
        
        public void SetTitle(string val)
        {
            titleWindow.text = val;
        }
        void Close()
        {
            gameObject.SetActive(false);
        }

        public virtual void Init()
        {
            SetTitle(title);

            if (myIcon == null)
            {
                myIcon = iconImage.sprite;
            }
            else
            {
                iconImage.sprite = myIcon;
            }


            if (btnClose)
            {
                btnClose.onClick.AddListener(Close);
            }
        }

        void Start()
        {
            Init();
        }



        private RectTransform m_DraggingPlane;
        private Vector3 _offsetDrag = Vector3.zero;
        private bool isResizeMode = false;

        public void OnBeginDrag(PointerEventData eventData)
        {
            var canvas = FindInParents<Canvas>(gameObject);

            if (canvas == null) return;


            if (eventData.pointerPressRaycast.gameObject == resizerGO.gameObject)
            {
                isResizeMode = true;
                SetResizePosition(eventData);
                return;
            }

            if (eventData.pointerPressRaycast.gameObject == titleBar.gameObject)
            {
                isResizeMode = false;
                
                m_DraggingPlane = transform as RectTransform; 

                SetDraggedPosition(eventData); 
                return;
            }

            Debug.Log(eventData.pointerPressRaycast.gameObject);
        }



        private void SetResizePosition(PointerEventData data)
        {
            if (!isResizeMode) return;

            var rt = transform.GetComponent<RectTransform>();

            Vector3 globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent.GetComponent<RectTransform>(), data.position, data.pressEventCamera, out globalMousePos))
            {
                Vector3 _res = globalMousePos - rt.position;
                Vector2 _resizeTo = new Vector2(Mathf.Abs(_res.x), Mathf.Abs(_res.y)) * 2;

                //Vector2 _resizeDelta = GetComponent<RectTransform>().sizeDelta  - new Vector2(Mathf.Abs(_res.x), Mathf.Abs(_res.y)) * 2;

                // rt.position -= new Vector3(_resizeDelta.x, -_resizeDelta.y,0)/2f;

                if (Mathf.Abs( _resizeTo.y) < 340) _resizeTo.y = 340;
                if (Mathf.Abs(_resizeTo.x) < 270) _resizeTo.x = 270;

                GetComponent<RectTransform>().sizeDelta = _resizeTo;
                 
            }
        }

        private void SetDraggedPosition(PointerEventData data)
        {

            if (m_DraggingPlane == null) return;

            transform.SetAsLastSibling();

            var rt = m_DraggingPlane.GetComponent<RectTransform>();
            Vector3 globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
            {
                if (_offsetDrag == Vector3.zero)
                {
                    _offsetDrag = rt.position - globalMousePos;
                }
               // Debug.Log(m_DraggingPlane);
                rt.position = globalMousePos + _offsetDrag;
            }
        }


        public void OnDrag(PointerEventData data)
        { 
                SetDraggedPosition(data);
            SetResizePosition(data);  
        }


        public void OnEndDrag(PointerEventData eventData)
        {
          //  Debug.Log("OnEndDrag");
              m_DraggingPlane = null;
              _offsetDrag = Vector3.zero;
            isResizeMode =false;
        }


     public  static  T FindInParents<T>(GameObject go) where T : Component
        {
            if (go == null) return null;
            var comp = go.GetComponent<T>();

            if (comp != null)
                return comp;

            Transform t = go.transform.parent;
            while (t != null && comp == null)
            {
                comp = t.gameObject.GetComponent<T>();
                t = t.parent;
            }
            return comp;
        }

    }

}