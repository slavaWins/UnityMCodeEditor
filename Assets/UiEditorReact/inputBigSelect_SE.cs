using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SEditor
{
    public class OptionInputBigSelect_SE
    {
        public string title;
        public string descr;
        public string tab;
        public Sprite icon;
    }

    public interface ICallbackInputBigSelect_SE
    {
        public void SelectValueFromSelector(string ind);
    }

    public class inputBigSelect_SE : Component_SE<string>, IInputComponent, ITakeValueFromSelector_SE
    {


        public ICallbackInputBigSelect_SE callbackClass;
        public Button backFiledButton;

        [HideInInspector]
        public BaseDataSE baseDataSE;

        public Text myValVisible;
        public Image mySpriteVisible;

        public Dictionary<string, OptionInputBigSelect_SE> options = new Dictionary<string, OptionInputBigSelect_SE> ();

        public void SetOptionByIndSprite(Dictionary<string, Sprite> dic)
        {
            options.Clear();
            foreach(var item in dic)
            {
                options.Add(item.Key, new OptionInputBigSelect_SE()
                {
                    title = item.Key,
                    icon = item.Value,
                });
            }
        }

        public string GetValueAsString()
        {
            return val;
        }



        public virtual void SelectValueFromSelector(string ind)
        {
            val = ind;
            Render();
            if (callbackClass != null)
            {
                callbackClass.SelectValueFromSelector(ind);
            }
        }

        public override void Render()
        {
            base.Render();
            myValVisible.text = val;

            mySpriteVisible.sprite = null;
            if (val == null) return;

            if (options.ContainsKey(val))
            {
                mySpriteVisible.sprite = options[val].icon;
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