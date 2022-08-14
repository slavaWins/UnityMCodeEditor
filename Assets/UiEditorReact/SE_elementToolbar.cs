using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SEditor;
using UnityEngine.UI;
using System;

namespace SEditor
{

    public class SE_elementToolbar : MonoBehaviour
    {

        public GameObject _isActive;
        public Image _icon;

        public GameObject myWindow;
        public string winName;
        public Sprite winIcon;


       public void Render()
        {
            if (myWindow == null) return;
            _isActive.gameObject.SetActive(myWindow.activeSelf);

            if (winIcon != null) _icon.sprite = winIcon;
        }

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(Click);
        }

        private void Click()
        {
            if (myWindow == null) return;
            myWindow.SetActive(!myWindow.activeSelf);
            Render();
        }
    }

}