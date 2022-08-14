using UnityEngine;
using UnityEngine.UI;
using MCoder;
using TMPro;
using System;
using System.IO;
using System.Collections.Generic;
using Json.Net;
using SEditor;

namespace MCoder.UI
{
    public class UMC_StorageScripts : MonoBehaviour
    {
        public Transform container;
        public UMC_File_Element elementPrefab;
        public MC_Coder_Script winCode;
        public Dictionary<string, MC_Save_Instance> listScripts;


        public static void SaveScript(string name,  MC_Save_Instance data)
        {
            Debug.Log("Save scr " + name);
            if (!Directory.Exists(GetFoolder())) Directory.CreateDirectory(GetFoolder());
            string j = JsonNet.Serialize(data);
            File.WriteAllText(GetFoolder()+"/"+name+".json", j);
        }

        public static Dictionary<string, MC_Save_Instance> GetAllScript()
        {
            Dictionary<string, MC_Save_Instance> list = new Dictionary<string, MC_Save_Instance>();
            if (!Directory.Exists(GetFoolder())) Directory.CreateDirectory(GetFoolder());
             

            foreach (var item in Directory.GetFiles(GetFoolder()))
            { 
                if(item.IndexOf(".meta")!=-1) continue;
                if(item.IndexOf(".json")==-1) continue;
               // Debug.Log(item);
                string f = File.ReadAllText(item);
               // Debug.Log(f);
                MC_Save_Instance data = JsonNet.Deserialize<MC_Save_Instance>(f);
                
                list.Add(Path.GetFileName(item), data);
            }

            return list;
        }

        public static string GetFoolder()
        {
            return Application.dataPath + "/winsual";
        }



        void Start()
        {
            Render();
        }
        private void OnEnable()
        {
            Render();
        }
        public void Render()
        {
            FormBuilder.ClearAllChildren(container);
            listScripts = GetAllScript();
            foreach (var item in listScripts)
            {
                UMC_File_Element element = Instantiate(elementPrefab.gameObject, container).GetComponent<UMC_File_Element>();
                element._name.text = item.Key;
                element.callbackClass = this;
                element.ind = item.Key;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        internal void ClickInd(string ind)
        {
            winCode.inpName.text = ind;
            winCode.mC_BaseInstance = new MC_BaseInstance(); 
            MCoderExport.SetData(winCode.mC_BaseInstance, listScripts[ind]);
            winCode.Render();
            gameObject.SetActive(false);
        }
    }
}