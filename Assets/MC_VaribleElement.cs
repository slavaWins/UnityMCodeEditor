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

public class MC_VaribleElement : DragableElementLine<UMC_Element_Argument>, ICallbackInputDropDown
{

    [HideInInspector]
    public MC_WindowVarible callbackWindow;

    [HideInInspector]
    public MC_Argument argument;

    [HideInInspector]
    public MC_Value_LinkType myLinkType;

    //Позиция перемнной от нуля. Как аргумент
    public int myId;

    [Header("Inner")]
    public TMP_Text title;
    public TMP_InputField _inpName;
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

       if(isCanEditType)CreateInputSelectType();

        title.gameObject.SetActive(!isCanEditType);
        _inpName.gameObject.SetActive(isCanEditType);

        if (isCanEditType)
        {
            _inpName.text = argument.name;
        }

        title.text = argument.name;
        _typeText.text = argument.myType.ToString();

        GetComponent<Image>().color = MC_Colors.GetColorByType(argument.myType);


    }


    void Start()
    {
        if (argument == null) return;
        ShowTooltip.Create(this.gameObject, "Переменная " + argument.name + " :" + _typeText.text, MC_DocHelp.GetInfoByLinkType(myLinkType).descr);

        _inpName.onEndEdit.AddListener(OnEditName);
    }

    private void OnEditName(string arg0)
    {
        if (!isCanEditType) return;

        argument.name = _inpName.text;
        argument.name = argument.name.Replace(" ", "");
        if (argument.name.Trim().Length == 0) argument.name = "Varible";
        

        callbackWindow.codeScript.Render();
    }

    void CreateInputSelectType()
    {
        List<TMP_Dropdown.OptionData> _opt = new List<TMP_Dropdown.OptionData>();
        foreach (string _name in Enum.GetNames(typeof(MC_ArgumentTypeEnum)))
        {
            _opt.Add(new TMP_Dropdown.OptionData() { text = _name });
        }
        inpSelectType.inputBox.options = _opt;
        inpSelectType.callbackClass = this;
        inpSelectType.SetValue((int)argument.myType);
    }

    public void SetOptionFromSelect(int id, string ind, InputDropdownComponent_SE from)
    {
        argument.myType = (MC_ArgumentTypeEnum)id;
        //Render();
        callbackWindow.codeScript.Render();

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
       // Debug.Log("enter");

        if (targetDropClass.argument == null) return;
        if (addHere == null)
        {
            addHere = Instantiate(addHerePrefab, targetDropClass.transform);
        }

        addHere.transform.SetParent(targetDropClass.transform);



        if (targetDropClass.argument.myType == argument.myType || (argument.myType == MC_ArgumentTypeEnum._any || targetDropClass.argument.myType == MC_ArgumentTypeEnum._any))
        {
            addHere.GetComponent<Image>().color = GetComponent<Image>().color;

            addHere.transform.Find("_text").GetComponent<TMP_Text>().text = "ПОДХОДИТ!"; 
        }
        else
        {
            addHere.GetComponent<Image>().color = new Color32(129, 20, 36, 255);
            addHere.transform.Find("_text").GetComponent<TMP_Text>().text = "НЕ ПОДХОДИТ ТИП";
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

        if (targetDropClass.argument.myType == argument.myType || (argument.myType == MC_ArgumentTypeEnum._any || targetDropClass.argument.myType == MC_ArgumentTypeEnum._any))
        {

        }
        else
        {
            return;
        }


        targetDropClass.meValue.linkType = myLinkType;
        targetDropClass.meValue.linkId = myId;
        //targetDropClass.Render();
        callbackWindow.codeScript.Render();
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
