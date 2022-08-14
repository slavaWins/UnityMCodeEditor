using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowTooltip : MonoBehaviour 
{
    public string title = "Какой-то текст";
    public string descr;

   
    public static void Create(GameObject to, string t, string d=null)
    {
        ShowTooltip ST = to.AddComponent<ShowTooltip>();
        ST.title = t;
        ST.descr = d;
    }
}
