using SEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementTabRow_SE : MonoBehaviour
{

    public FormBuilder formBuilder;
    public string ind;



    public void Click()
    {

       // Debug.Log("ElementTabRow_SE click " + ind);
        formBuilder.TabClick(ind);
    }

    public void Init()
    {
        GetComponent<Button>().onClick.AddListener(Click);
    }
}
