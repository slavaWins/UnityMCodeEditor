using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SEditor
{
    public class inputBigSelect_SE : Component_SE<string>, IInputComponent, ITakeValueFromSelector_SE
    {


        public Button backFiledButton;

        [HideInInspector]
        public BaseDataSE baseDataSE;

        public Text myValVisible;
        public Image mySpriteVisible;

        public Dictionary<string, Sprite> options = new Dictionary<string, Sprite> ();

        public string GetValueAsString()
        {
            return val;
        }



        public virtual void SelectValueFromSelector(string ind)
        {
            val = ind;
            Render();
        }

        public override void Render()
        {
            base.Render();
            myValVisible.text = val;

            mySpriteVisible.sprite = null;
            if (val == null) return;
            if (options.ContainsKey(val))
            {
                mySpriteVisible.sprite = options[val];
            }

        }

        public virtual void Click()
        {
            baseDataSE.GetWindowSeletor().Open(this);

            foreach(var item in options)
            {
                baseDataSE.GetWindowSeletor().AddElement(item.Key, item.Value);

            }

        }

        bool isInit = false;
        public void Init()
        {
            if (isInit) return;             isInit = true;

            baseDataSE = BaseDataSE.Get();
            backFiledButton.onClick.AddListener(Click);

        }
        void Start()
        {
            Init();
        }

    }

}