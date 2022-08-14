using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using MCoder;
using TMPro;
using System;
using System.IO;

namespace MCoder.UI
{
    [RequireComponent(typeof(Button))]
    public class UMC_File_Element : MonoBehaviour
    {
 


        public UMC_StorageScripts callbackClass;
        public TMP_Text _name;
        public TMP_Text _type;
        public string ind;

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(Click);
        }

        private void Click()
        {
            callbackClass.ClickInd(ind);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}