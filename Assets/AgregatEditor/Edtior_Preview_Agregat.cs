using Agregat.Struct;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agregat.Struct {

    [SerializeField]
    public class AgregatUiElement
    { 
        public Vector2 pos = new Vector2Int(0, 0); 
        public Vector2 size = new Vector2Int(32, 32); 
    }
     
    public class AgregatIndicatorArgument : AgregatUiElement
    {
        public string imageBack;
        public string imageFront;
    }
     
    public class AgregatSlotArgument: AgregatUiElement
    { 
        public string icon;
    }
     
    public class AgregatSetting
    {
        public Vector2 size = new Vector2Int(320, 240);
        public List<AgregatSlotArgument> slots = new List<AgregatSlotArgument>();
        public List<AgregatIndicatorArgument> indicators = new List<AgregatIndicatorArgument>();
    
    }
}

namespace Agregat.Editor {
  

    public class Edtior_Preview_Agregat : MonoBehaviour
    {
     
        public Transform container;
        public Edtior_Slot_Agregat perfabSlot;
        public AgregatSetting agregatSetting;

        List<Edtior_Slot_Agregat> slotsGui = new List<Edtior_Slot_Agregat>();
        // Start is called before the first frame update
        void Start()
        {
            agregatSetting = new AgregatSetting();

            agregatSetting.slots.Add(new AgregatSlotArgument()
            {
                pos = new Vector2(32, 32),
            });

            agregatSetting.slots.Add(new AgregatSlotArgument()
            {
                pos = new Vector2(32, 32+64),
            });

            Render();
        }

        public void Clear()
        {
            foreach (var item in slotsGui)
            {
               Destroy(item.gameObject);
            }
            slotsGui.Clear();
        }


        public void Render()
        {
            Clear();

            int L = -1;
            foreach (var item in agregatSetting.slots)
            { 
                L++;
               // Debug.Log(L);
                Edtior_Slot_Agregat e = Instantiate(perfabSlot, container).GetComponent<Edtior_Slot_Agregat>();
                RectTransform rt = e.GetComponent<RectTransform>();
                e.GetComponent<RectTransform>().sizeDelta = item.size*2;
                e.GetComponent<RectTransform>().anchoredPosition  = new Vector3(0,0,0); 
                e.GetComponent<RectTransform>().anchorMin  = new Vector3(0.5f, 1,0); 
                e.GetComponent<RectTransform>().anchorMax  = new Vector3(0.5f,1,0); 

                e.GetComponent<RectTransform>().position  = new Vector3(0, 0,0); 
              
                e.GetComponent<RectTransform>().position = new Vector3(item.pos.x - rt.anchoredPosition.x + item.size.x, item.pos.y + Mathf.Abs( rt.anchoredPosition.y) + item.size.y, 0) ;
                e.slotId = L;
                e.callbackClass = this;
            }

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}