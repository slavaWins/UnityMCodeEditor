using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SEditor
{
    public class InputTextComponent_SE : Component_SE<string>, IInputComponent
    {


        public InputField inputBox;

        public void SetTypeToInt()
        {
            inputBox.contentType = InputField.ContentType.IntegerNumber;
        }
        public void SetTypeToFloat()
        {
            inputBox.contentType = InputField.ContentType.DecimalNumber;
        }

        public string GetValueAsString()
        {
            return val;
        }

        public override void Render()
        {
            base.Render();
            inputBox.text = val;
                
        }


        public void Chan(string _val)
        {
            val = _val;
        }


        // Start is called before the first frame update
        void Start()
        {
            inputBox.onEndEdit.AddListener(Chan);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}