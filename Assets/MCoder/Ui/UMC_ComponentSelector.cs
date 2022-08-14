using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MCoder;
using TMPro;
using SEditor;
using System;
using MCoder.Libary; 
using static MCoder.MC_BaseInstance;

namespace MCoder.UI
{

    public class UMC_ComponentSelector : MonoBehaviour, ICallbackInputBigSelect_SE
    {
        public InputTextComponent_SE inpPrefab;
        public inputBigSelect_SE inpSelect;

        [HideInInspector]
        public List<InputTextComponent_SE> listInp = new List<InputTextComponent_SE>();

        public string scriptInd;
        MC_Save_Instance save_Instance;

        void Clear()
        {
            foreach (InputTextComponent_SE s in listInp)
            {
                Destroy(s.gameObject);
            }

            listInp.Clear();
            listInp = new List<InputTextComponent_SE>();
        }

        void Render()
        {
            Clear();

            if (!UMC_StorageScripts.GetAllScript().ContainsKey(scriptInd)) return;
             save_Instance = UMC_StorageScripts.GetAllScript()[scriptInd];
            foreach(var item in save_Instance.argumentsInputs)
            {
                InputTextComponent_SE _inp = Instantiate(inpPrefab, transform).GetComponent<InputTextComponent_SE>();
                _inp.SetLabel(item.name);
                if (item.myType == MC_ArgumentTypeEnum._int) _inp.SetTypeToInt();
                listInp.Add(_inp); 
            }
        }


        void Start()
        {
            string _exampleSelectInd = null;
            foreach (var item in UMC_StorageScripts.GetAllScript())
            {
                _exampleSelectInd = item.Key;
                inpSelect.options.Add(item.Key, new OptionInputBigSelect_SE()
                {
                    title = item.Key,
                });

            }  
            inpSelect.callbackClass = this;

            SelectValueFromSelector(_exampleSelectInd);
        }

       

        public void SetData(string ind, List<object> values)
        {
            scriptInd = ind;
            Render();
            int L = -1;
            foreach(var item in values)
            {
                L++;
                if (listInp.Count - 1 < L) break;
                listInp[L].val = item.ToString();
                listInp[L].inputBox.text = item.ToString();
            }
        }

        public void SelectValueFromSelector(string ind)
        {
            scriptInd = ind;
            Render();
        }


        void Update()
        {

        }
    }
}