using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SEditor
{
    public interface ITakeValueFromSelector_SE
    {
        public void SelectValueFromSelector(string ind);
    }

    public class WindowSelectBigCanvas_SE : MonoBehaviour
    {
        public ITakeValueFromSelector_SE myInputCallback;
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

        public void Open(ITakeValueFromSelector_SE _callback)
        {
            Clear();
            myInputCallback = _callback;
            gameObject.SetActive(true);

        }

        public WindowSelectBigCanvas_SE AddElement(string ind, OptionInputBigSelect_SE data = null)
        {
            ElementWindowSelectBigCanvas element = Instantiate(templateElement.gameObject, container).GetComponent<ElementWindowSelectBigCanvas>();
           

            element.ind = ind;
            element.myCallbackCanvas = this;

            element.data  = data;
            element.Render();
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