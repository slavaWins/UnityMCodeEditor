using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SEditor
{
    public class CanvasEditor_SE : MonoBehaviour
    {
        public Transform container;
        public Button btnClose;


        public PanelSeController CreatePanel(string label="New Panel")
        {
            PanelSeController go = Instantiate(BaseDataSE.Get().panelSeController.gameObject).GetComponent<PanelSeController>();
            AddToContainer(go.transform);
            go.SetTitle(label);
            return go;
        }

        public void AddToContainer(Transform element)
        {
            element.transform.SetParent(container);
        }

       public void Close()
        {
            gameObject.SetActive(false);
        }

        void Start()
        {
            if (btnClose)
            {
                btnClose.onClick.AddListener(Close);
            }
        }
    }

}