using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SEditor
{
    public class CheckboxComponent_SE : Component_SE<bool>, IInputComponent
    {


        public Image inpCheckbox;

        public Sprite onSprite;
        public Sprite offSprite;


        public string GetValueAsString()
        {
            if (val) return "true";
            return "false";
        }

        public override void Render()
        {
            base.Render();
            if (val)
            {
                inpCheckbox.sprite = onSprite;
            }
            else
            {
                inpCheckbox.sprite = offSprite;
            }
        }

        public void Click()
        {
            val = !val;
            Render();
        }

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(Click);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}