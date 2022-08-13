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

public class MC_VaribleElement : DragableElementLine<UMC_Element_Argument>
{

    [HideInInspector]
    public MC_WindowVarible callbackWindow;

    [HideInInspector]
    public MC_Argument argument;


    [Header("Inner")]
    public TMP_Text title;
    public TMP_Text _typeText;
    public InputDropdownComponent_SE inpSelectType;

    public bool isCanEditType = false;

    [Header("Drag")]
    public GameObject addHerePrefab;
    GameObject addHere;



    public void Render()
    {
        inpSelectType.gameObject.SetActive(isCanEditType);
        _typeText.gameObject.SetActive(!isCanEditType);

        title.text = argument.name;
        _typeText.text = argument.myType.ToString();
    }


    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
