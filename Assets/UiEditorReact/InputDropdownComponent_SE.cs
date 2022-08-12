using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SEditor
{
    public class InputDropdownComponent_SE : Component_SE<int>, IInputComponent
    {


        public TMPro.TMP_Dropdown inputBox;



        public string GetValueAsString()
        {
            return inputBox.value.ToString();
        }


        public void xSetOption()
        {

        }

        public override void Render()
        {
            base.Render();
            inputBox.value = val;
        }




        // Start is called before the first frame update
        void Start()
        {
            // inputBox.ClearOptions();
        }

    }

}