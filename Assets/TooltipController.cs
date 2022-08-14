using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SEditor;
using TMPro;
using UnityEngine.EventSystems;
 

public class TooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{

    public GameObject goTooltip;
    public TMP_Text txtTitle;
    public TMP_Text txtDescr;

    public bool IsActive = true;

    Camera cam;
    Vector3 min, max;
    RectTransform rect;
    float offset = 10f;



    void Start()
    {
        cam = Camera.main;
        rect = goTooltip.GetComponent<RectTransform>();
        min = new Vector3(0, 0, 0);
        max = new Vector3(cam.pixelWidth, cam.pixelHeight, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    void MoveToMose()

    {
        cam = Camera.main;
        Vector3 position = new Vector3(Input.mousePosition.x + rect.rect.width / 2 + 40f, Input.mousePosition.y - (rect.rect.height / 2f), 0f);
        rect.position = position; // new Vector3(Mathf.Clamp(position.x, min.x + rect.rect.width / 2, max.x - rect.rect.width / 2), Mathf.Clamp(position.y, min.y + rect.rect.height / 2, max.y - rect.rect.height / 2), transform.position.z);

    }

    void Update()
    {


    }

    void Hide()
    {

        goTooltip.SetActive(false);
    }
    public void ShowThe(string _title, string _descr)
    {
        goTooltip.SetActive(true);

        txtTitle.text = _title;

        if (_descr == null)
        {
            txtDescr.gameObject.SetActive(false);
        }
        else
        {
            txtDescr.gameObject.SetActive(true);
            txtDescr.text = _descr;
        }
    }
    public static void Show(string _title, string _descr)
    {
        TooltipController TT = TooltipController.Get();

        TT.ShowThe(_title, _descr);
    }

    public static TooltipController Get()
    {
        TooltipController iam = FindObjectOfType<TooltipController>();
        if (iam != null) return iam;
        return null;
    }

    public void OnPointerMove(PointerEventData eventData)
    { 
        if (eventData.pointerEnter == null) return;

        ShowTooltip ST = eventData.pointerEnter.GetComponent<ShowTooltip>(); 


        if (ST == null)
        {
            
            ST = eventData.pointerEnter.transform.parent.GetComponent<ShowTooltip>();

        }
        if (ST == null)
        {
            Hide();
            return;
        } 
        Show(ST.title, ST.descr);
        MoveToMose();
    }
}