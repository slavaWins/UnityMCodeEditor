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

    [Header("Integrate")]
    public MC_Value meValue;
    public MC_Argument argument; 
    public NodeCodeLineElement classParent;
    internal MC_BaseNodeElement nodeClass;

    [Header("Inner")]
    public TMP_InputField inp;


    [Header("Inner/varible")]
    public TMP_Text fromVaribleName;
    public TMP_Text fromVaribleType;

    // Start is called before the first frame update
    public void Init()
    {
        inp.onEndEdit.AddListener(classParent.OnEndEditArgumentText);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void Render()
    {
        transform.Find("_h1").GetComponent<TMP_Text>().text = argument.name;
        transform.Find("_type").GetComponent<TMP_Text>().text = argument.myType.ToString().Replace("_", "");


        transform.Find("_type").GetComponent<TMP_Text>().color = MC_Colors.GetColorByType(argument.myType);

        fromVaribleName.transform.parent.gameObject.SetActive((meValue.linkType != MC_Value_LinkType._none));

        if (meValue.linkType != MC_Value_LinkType._none)
        {
            
            fromVaribleName.text = meValue.linkType.ToString();
            fromVaribleType.text = argument.myType.ToString().Replace("_","");
            fromVaribleName.transform.parent.GetComponent<Image>().color = MC_Colors.GetColorByType(argument.myType);
        }

    }
}