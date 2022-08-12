using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SEditor
{
    public interface IInputComponent
    {
        public void OnTabChange(string _tab);
        public void SetTab(string _tab);
        public void SetLabel(string val);
        public void Render();
        public void SetNameGo(string val);
        public void SetLaoytOrder(int val);
        public string GetValueAsString();

    }

    public class Component_SE<T> : MonoBehaviour
    {
        public string tab;
        public string label = "";
        public T val;



        public void OnTabChange(string _tab)
        {
            if (tab == null) return;
            gameObject.SetActive(_tab == tab);
        }

        public void SetTab(string _tab)
        {
            tab = _tab;
        }

        public void SetLaoytOrder(int val)
        {
            gameObject.GetComponent<RectTransform>().SetSiblingIndex(val);
        }

        public void SetNameGo(string val)
        {
            gameObject.name = val;
        }
        public void SetLabel(string val)
        {
            label = val;
            Render();
        }
        public virtual void Render()
        {
            Transform labelText = gameObject.transform.Find("Label");
            if (labelText)
            {
                labelText.GetComponent<Text>().text = label;
            }
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