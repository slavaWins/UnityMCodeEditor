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

public class MC_WindowVarible : MonoBehaviour
{
    [Header("Integrate")]
    public MC_Coder_Script codeScript;


    [Header("Inner")]
    public Transform labelBy_Event;
    public Transform labelBy_Input;
    public Transform labelBy_Custom;
    public Transform labelBy_Saved;
    public Transform container;

    [Header("Btns")]
    public Button btnAddInput;
    public Button btnAddCustom;
    public Button btnAddSave;



    [Header("Prefabs")]
    public MC_VaribleElement prefabElement;

    public Dictionary<MC_Argument, MC_VaribleElement> dicList = new Dictionary<MC_Argument, MC_VaribleElement>();


    public void ClearAll()
    {
        foreach (var item in dicList)
        {
            Destroy(item.Value.gameObject);
        }
        dicList.Clear();
    }

    public void AddVarible(int localId, MC_Argument argument,  MC_Value_LinkType linkType, bool isCanEdit)
    {
        //SEditor.FormBuilder.ClearAllChildren(container);

        MC_VaribleElement go = Instantiate(prefabElement.gameObject, container).GetComponent<MC_VaribleElement>();
        go.myId = localId;
        go.argument = argument;
        go.myLinkType = linkType;
        
        go.callbackWindow = this;
        go.isCanEditType = isCanEdit;
        go.Render();
        dicList.Add(argument, go);

        if (linkType == MC_Value_LinkType._event) go.transform.SetSiblingIndex(labelBy_Event.GetSiblingIndex() + 1);
        if (linkType == MC_Value_LinkType._save) go.transform.SetSiblingIndex(labelBy_Saved.GetSiblingIndex() + 1);
        if (linkType == MC_Value_LinkType._custom) go.transform.SetSiblingIndex(labelBy_Custom.GetSiblingIndex() + 1);
        if (linkType == MC_Value_LinkType._input) go.transform.SetSiblingIndex(labelBy_Input.GetSiblingIndex() + 1);
        

        StartCoroutine(container.GetComponent<VerticalLayoutGroup>().ChangeUpdate());
    }

    
    void CreateSavedVarible()
    {
        
        MC_Argument _arg = new MC_Argument();
        _arg.name = "Save_" + codeScript.mC_BaseInstance.argumentsSave.Count;
        _arg.myType = MC_ArgumentTypeEnum._string;

        codeScript.mC_BaseInstance.argumentsSave.Add(_arg);
        codeScript.Render();
    }
    
    void CreateCutomVarible()
    {
        
        MC_Argument _arg = new MC_Argument();
        _arg.name = "Custom_" + codeScript.mC_BaseInstance.argumentsCustoms.Count;
        _arg.myType = MC_ArgumentTypeEnum._string;

        codeScript.mC_BaseInstance.argumentsCustoms.Add(_arg);
        codeScript.Render();
    }

    void CreateInputVarible()
    {
        MC_Argument _arg = new MC_Argument();
        _arg.name = "Custom_" + codeScript.mC_BaseInstance.argumentsInputs.Count;
        _arg.myType = MC_ArgumentTypeEnum._string;

        codeScript.mC_BaseInstance.argumentsInputs.Add(_arg);
        codeScript.Render();
    }

    void Start()
    {
        btnAddInput.onClick.AddListener(CreateInputVarible);
        btnAddCustom.onClick.AddListener(CreateCutomVarible);
        btnAddSave.onClick.AddListener(CreateSavedVarible);
    }

    // Update is called once per frame
    void Update()
    {

    }
}