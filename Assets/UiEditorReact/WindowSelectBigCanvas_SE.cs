using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SEditor
{
    public class WindowSelectBigCanvas_SE : MonoBehaviour
    {
        public inputBigSelect_SE myInputCallback;
        public ElementWindowSelectBigCanvas templateElement;
        public Transform container;

        public Button btnClose;


        public void Close()
        {
            Clear();
            gameObject.SetActive(false);
        }

        public void SetVal(string ind)
        {

            if (myInputCallback != null)
            {
                myInputCallback.SelectValueFromSelector( ind);
            }
            Close();
        }

        public void Open(inputBigSelect_SE _callback)
        {
            Clear();
            myInputCallback = _callback;
            gameObject.SetActive(true);

        }

        public WindowSelectBigCanvas_SE AddElement(string ind, Sprite icon = null)
        {
            ElementWindowSelectBigCanvas element = Instantiate(templateElement.gameObject, container).GetComponent<ElementWindowSelectBigCanvas>();
            
            element.transform.Find("_text").GetComponent<TMPro.TextMeshProUGUI>().text = ind;

            if (icon != null)
            {
                element.transform.Find("_icon").GetComponent<Image>().sprite = icon;
            }

            element.ind = ind;
            element.myCallbackCanvas = this;
            return this;
        }

        public void Clear()
        {
            for (int i = 0; i < container.childCount; i++)
            {
                Destroy(container.GetChild(i).gameObject);
            }
        }

        void Start()
        {
            btnClose.onClick.AddListener(Close);
        }

    }

}