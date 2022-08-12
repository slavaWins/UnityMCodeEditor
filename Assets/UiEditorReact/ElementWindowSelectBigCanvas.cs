using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SEditor
{
    public class ElementWindowSelectBigCanvas : MonoBehaviour
    {
        public WindowSelectBigCanvas_SE myCallbackCanvas;
        public string ind;

        // Start is called before the first frame update
        void Click()
        {
            myCallbackCanvas.SetVal(ind);
        }

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(Click);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}