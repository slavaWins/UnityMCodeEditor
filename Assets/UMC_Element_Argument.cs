using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MCoder.Libary;
using SEditor;
using TMPro;
using UnityEngine.EventSystems;
using MCoder.UI;
using MCoder;
using System;

public class UMC_Element_Argument : MonoBehaviour
{

    public MC_Argument argument;
public     TMP_InputField inp;
    public NodeCodeLineElement classParent;
    internal MC_BaseNodeElement nodeClass;

    // Start is called before the first frame update
    void Start()
    {
        inp.onEndEdit.AddListener(classParent.OnEndEditArgumentText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Render()
    {
        transform.Find("_h1").GetComponent<TMP_Text>().text = argument.name;
        transform.Find("_type").GetComponent<TMP_Text>().text = argument.myType.ToString().Replace("_", "");

    }
}
