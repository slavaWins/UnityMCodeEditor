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

public class MC_VaribleElement : DragableElementLine<UMC_Element_Argument>
{

    [HideInInspector]
    public MC_WindowVarible callbackWindow;

    [HideInInspector]
    public MC_Argument argument;


    //Позиция перемнной от нуля. Как аргумент
    public int myId;

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

        GetComponent<Image>().color = MC_Colors.GetColorByType(argument.myType);
    }


    void Start()
    {
        
    }


    void Update()
    {
        
    }



    public override void OnCreateGhostDrag()
    {
        base.OnCreateGhostDrag();
        Destroy(myGhostDragibleCline.GetComponent<MC_VaribleElement>());
        myGhostDragibleCline.sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta / 2;
    }


    internal override void OnMoveUpdateInAny(GameObject go)
    {
        base.OnMoveUpdateInAny(go);
        //Debug.Log(go.GetComponent<UMC_Element_Argument>());
    }
    internal override void OnMoveEnterInTargetClass()
    {
        base.OnMoveEnterInTargetClass();
        Debug.Log("enter");

        if (targetDropClass.argument == null) return;
        if (addHere == null)
        {
            addHere = Instantiate(addHerePrefab, targetDropClass.transform);
        }

        addHere.transform.SetParent(targetDropClass.transform);



        if (targetDropClass.argument.myType != argument.myType)
        {
            addHere.GetComponent<Image>().color = new Color32(129,20,36,255);
            addHere.transform.Find("_text").GetComponent<TMP_Text>().text = "НЕ ПОДХОДИТ ТИП";
        }
        else
        { 
            addHere.GetComponent<Image>().color = GetComponent<Image>().color;

            addHere.transform.Find("_text").GetComponent<TMP_Text>().text = "ПОДХОДИТ!";
        }
    }


    internal override void OnMoveExitInTargetClass()
    {

        base.OnMoveExitInTargetClass();
        if (addHere != null)
        {
            Destroy(addHere);
        }
    }
     
    public override void DragEnd_TargetClass()
    {
        base.DragEnd_TargetClass();
        AddLinkToElementArgumentValue(targetDropClass);

        if (addHere != null) Destroy(addHere);
    }

    private void AddLinkToElementArgumentValue(UMC_Element_Argument targetDropClass)
    {
        if (targetDropClass.argument.myType != argument.myType) return;

        targetDropClass.meValue.linkType = MC_Value_LinkType._event;
        targetDropClass.meValue.linkId = myId;
        targetDropClass.Render();
    }

    public override void DragEnd_UnTargetClass(GameObject gameObject)
    { 
            if (addHere != null) Destroy(addHere); 
    }

    internal override void OnDragStop()
    {
        base.OnDragStop();
        if (addHere != null) Destroy(addHere);
    }

}
