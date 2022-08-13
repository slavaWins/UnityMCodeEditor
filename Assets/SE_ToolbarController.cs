using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SEditor;
using UnityEngine.UI;

namespace SEditor
{

    public class SE_ToolbarController : MonoBehaviour
    {
        public HorizontalLayoutGroup horWindGro;
        public SE_elementToolbar elementPrefab;

         public List<SE_elementToolbar> toolbars = new List<SE_elementToolbar>();

        public  void Down(GameObject go)
        {
            go.SetActive(false);
            foreach (SE_elementToolbar toolbar in toolbars)
            {
                if (toolbar.myWindow == go)
                {
                    toolbar.Render();
                    return;
                }
            }
        }

        public void SetGridWindow()
        {
            horWindGro.enabled = !horWindGro.enabled;
        }

        public  void Close(GameObject go)
        {
            go.SetActive(false);
            foreach (SE_elementToolbar toolbar in toolbars)
            {
                if(toolbar.myWindow == go)
                {
                    toolbars.Remove(toolbar);
                    Destroy(toolbar.gameObject);
                    return;
                }
            }
        }

        public  void AddWindow(GameObject go, string winName,  Sprite winIcon = null)
        {
            SE_elementToolbar e = Instantiate(elementPrefab, transform).GetComponent<SE_elementToolbar>();
            e.myWindow = go;
            e.winIcon = winIcon;
            e.winName = winName;
            e.Render();
            toolbars.Add(e);
        }

        public static SE_ToolbarController Get()
        {
            SE_ToolbarController iam = FindObjectOfType<SE_ToolbarController>();
            if (iam != null) return iam;
            return null;
        }

       
    }
}