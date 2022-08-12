using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MCoder.UI
{

    public class MC_WindowsEvent : MonoBehaviour
    {
        public MC_Coder_Script coderSctipt;
        public Transform container;
        public MC_Event_Element elementPrefab;



        public void AddNew()
        {
            coderSctipt.EventCreate();
        }

        public void SelectEvent(int N)
        {
            coderSctipt.EventSelect(2);
            for (int i = 0; i < container.childCount; i++)
            {
                container.GetChild(i).transform.Find("_active").gameObject.SetActive(i == N);
            }
        }

        public void Render()
        {
            SEditor.FormBuilder.ClearAllChildren(container);

            int L = 0;
            foreach (var lgn in coderSctipt.mC_BaseInstance.nodesForEvents)
            {

                L++;

                MC_Event_Element go = Instantiate(elementPrefab.gameObject, container).GetComponent<MC_Event_Element>();
                go.eventClass = lgn.myEvent;
                go.nodeLine = L;
                go.callbackPanel = this; 
                go.Render();

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
    }
}