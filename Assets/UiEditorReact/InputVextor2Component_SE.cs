using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SEditor
{
    public class InputVextor2Component_SE : Component_SE<Vector3Int>, IInputComponent
    {


        public InputField inputBox_X;
        public InputField inputBox_Y;

        
        public string GetValueAsString()
        {
            return FormBuilder.ToStringNormal(val);
        }

        public override void Render()
        {
            base.Render();
            inputBox_X.text = val.x.ToString();
            inputBox_Y.text = val.y.ToString();
                
        }


        public void ChanX(string _val)
        {
            val = new Vector3Int(int.Parse(_val), val.y);
        }
        public void ChanY(string _val)
        {
            val = new Vector3Int( val.x, int.Parse(_val));
        }


        // Start is called before the first frame update
        void Start()
        {
            inputBox_X.onEndEdit.AddListener(ChanX);
            inputBox_Y.onEndEdit.AddListener(ChanY);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}