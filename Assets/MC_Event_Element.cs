using MCoder.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MCoder;
using System;
using TMPro;
using UnityEngine.EventSystems;
using SEditor;
using MCoder.Libary;
using UnityEngine.UI;

public class MC_Event_Element : MonoBehaviour
{
    internal int nodeLine;
    internal MC_WindowsEvent callbackPanel;
    public TMP_Text h1;
    public TMP_Text small;
    internal MC_Base_Event eventClass;

    // Start is called before the first frame update
    public void Click()
    {
        callbackPanel.SelectEvent(nodeLine); 

    }
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Click);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Render()
    {
        if (eventClass == null) return;

        h1.text = eventClass.title;
        small.text = eventClass.descr;
    }
}
