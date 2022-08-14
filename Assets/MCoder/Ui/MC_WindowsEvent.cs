using MCoder.Libary;
using SEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MCoder.UI
{

    public class MC_WindowsEvent : MonoBehaviour, ITakeValueFromSelector_SE
    {
        public MC_Coder_Script coderSctipt;
        public Transform container;
        public MC_Event_Element elementPrefab;



        public void AddNew()
        {
            BaseDataSE baseDataSE = BaseDataSE.Get();
            baseDataSE.GetWindowSeletor().Open(this);


            foreach (MC_Base_Event item in MC_BD_Nodes.GetEventsList())
            {
                if (coderSctipt.mC_BaseInstance.IssetEvent(item.GetEventInd())) continue;
                baseDataSE.GetWindowSeletor().AddElement(item.GetEventInd(), null);
            } 

             
        }

        public MC_Event_Element GetElementById(int N)
        {
           
            for (int i = 0; i < container.childCount; i++)
            {
                if (i == N) return container.GetChild(i).GetComponent<MC_Event_Element>();
            }
            return null;
        }

        public void SelectEvent(int N)
        {
            coderSctipt.EventSelect(N);
            for (int i = 0; i < container.childCount; i++)
            {
                container.GetChild(i).transform.Find("_active").gameObject.SetActive(i == N);
            }
        }

        public void Render()
        {
            SEditor.FormBuilder.ClearAllChildren(container);

            int L = -1;
            foreach (var lgn in coderSctipt.mC_BaseInstance.nodesForEvents)
            { 
                L++; 
                MC_Event_Element go = Instantiate(elementPrefab.gameObject, container).GetComponent<MC_Event_Element>();
                go.eventClass = lgn.myEvent;
                go.nodeLine = L;
                go.callbackPanel = this; 
                go.Render();
                go.transform.Find("_active").gameObject.SetActive(L==coderSctipt.currentEventNumber); 
            }


           

            StartCoroutine(container.GetComponent<VerticalLayoutGroup>().ChangeUpdate());
        }


        void Start()
        {
            Render();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SelectValueFromSelector(string ind)
        {

            foreach (MC_Base_Event item in MC_BD_Nodes.GetEventsList())
            {
                if (item.GetEventInd() != ind) continue;
                if (coderSctipt.mC_BaseInstance.IssetEvent(item.GetEventInd())) continue;
                coderSctipt.EventCreate(item);
                Render();
                return;
            }




        }

        internal void HideAllError()
        {
            for (int i = 0; i < container.childCount; i++)
            {
                container.GetChild(i).GetComponent<MC_Event_Element>().SetVisibleError(false);
            }
        }

        internal void ShowErrorIn(int eventLin)
        {
        
            MC_Event_Element e =  GetElementById(eventLin);
            if (e == null) return;
           e.SetVisibleError(true);
        }
    }
}