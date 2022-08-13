using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SEditor
{
    public class ElementWindowSelectBigCanvas : MonoBehaviour
    {
        public WindowSelectBigCanvas_SE myCallbackCanvas;
        public string ind;
        internal OptionInputBigSelect_SE data;

        [Header("Inner")]
        public TextMeshProUGUI _title;
        public TextMeshProUGUI _descr;
        public TextMeshProUGUI _tab;
        public Image _icon;

        // Start is called before the first frame update
        void Click()
        {
            myCallbackCanvas.SetVal(ind);
        }

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(Click);
        }


        void Update()
        {

        }

        internal void Render()
        {
         

            _title.text = ind;

            if (data == null) data = new OptionInputBigSelect_SE();
          

            if (data.title != null)
            {
                _title.text = ind + " " + data.title;
            }

               


            _descr.gameObject.SetActive(data.descr != null);
            _descr.text = data.descr ?? "";


            _tab.transform.parent.gameObject.SetActive(data.tab != null);
            _tab.text = data.descr ?? "";


            if (data.icon != null)
            {
              _icon.sprite = data.icon;
            }
        }
    }

}