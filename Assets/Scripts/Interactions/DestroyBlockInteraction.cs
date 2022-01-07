using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlockInteraction : MonoBehaviour , IInteractable
{
    private GridController gridController;

    public bool CanInteract()
    {
        // Do not refuse interaction.
        return true;
    }

    public void Interact()
    {
        Debug.Log($"Removing game object at {transform.position}");
        gridController = GameObject.FindGameObjectWithTag("GridController").GetComponent<GridController>();
        gridController.RemoveGameObjectAtPosition(transform.position);
    }
}
