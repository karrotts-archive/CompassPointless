using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlockInteraction : MonoBehaviour , Interaction
{
    private GridController gridController;

    public void Interact()
    {
        Debug.Log("Interaction... buggy but works");
        //gridController = GameObject.FindGameObjectWithTag("GridController").GetComponent<GridController>();
        //gridController.RemoveGameObjectAtPosition(transform.position);
    }
}
