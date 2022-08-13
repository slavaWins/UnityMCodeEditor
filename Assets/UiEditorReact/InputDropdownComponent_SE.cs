using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SEditor
{
    public interface ICallbackInputDropDown
    {
        public void SetOptionFromSelect(int id, string ind, InputDropdownComponent_SE from);
    }

    public class InputDropdownComponent_SE : Component_SE<int>, IInputComponent
    {

        public ICallbackInputDropDown callbackClass;

        public TMPro.TMP_Dropdown inputBox;



        public void SetValue(string ind)
        {
            inputBox.value = GetIdByInd(ind);
        }

        public void SetValue(int id)
        {
            inputBox.value = id;
        }

        public string GetValueAsString()
        {
            return inputBox.value.ToString();
        }


        public int GetIdByInd(string valSelect)
        {
            for (int i = 0; i < inputBox.options.Count; i++)
            {

                if (inputBox.options[i].text == valSelect) return i;
            }
            return 0;
        }

        public string GetIndById(int valSelect)
        {
            for (int i = 0; i < inputBox.options.Count; i++)
            {

                if (valSelect == i) return inputBox.options[i].text;
            }
            return null;
        }

        public void OnSetOption(int valSelect)
        {
            if (callbackClass == null) return;

            string ind = GetIndById(valSelect);


            callbackClass.SetOptionFromSelect(valSelect, ind, this);

        }

        public override void Render()
        {
            base.Render();
            inputBox.value = val;
        }




        // Start is called before the first frame update
        void Start()
        {
            inputBox.onValueChanged.AddListener(OnSetOption);
            // inputBox.ClearOptions();
        }

      
    }

}