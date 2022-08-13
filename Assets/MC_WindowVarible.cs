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
    public Transform labelBy_Varible;
    public Transform container;

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

    public void AddVarible(int localId, MC_Argument argument, bool isCanEdit)
    {
        //SEditor.FormBuilder.ClearAllChildren(container);

        MC_VaribleElement go = Instantiate(prefabElement.gameObject, container).GetComponent<MC_VaribleElement>();
        go.argument = argument;
        go.callbackWindow = this;
        go.Render();
        dicList.Add(argument, go);


        StartCoroutine(container.GetComponent<VerticalLayoutGroup>().ChangeUpdate());
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}