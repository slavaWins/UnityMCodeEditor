using MCoder.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Agregat.Editor
{
    public class Edtior_Slot_Agregat : DragableElementLine<NodeCodeLineElement>
    {

        public Edtior_Preview_Agregat callbackClass;
        public int slotId;

        internal override void OnDragStop(PointerEventData eventData)
        {
            base.OnDragStop();
            callbackClass.agregatSetting.slots[slotId].pos = eventData.position;
            callbackClass.Render();
            Debug.Log(eventData.position);
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}