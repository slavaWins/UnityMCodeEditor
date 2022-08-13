using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTrashStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //_DELETE_ME
        foreach (Transform child in GetComponentsInChildren<Transform>()) { 
            
        if(child.name== "_DELETE_ME")
            {
                Destroy(child.gameObject);
            }
        }
    }
 
}
