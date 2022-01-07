using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInteraction : MonoBehaviour, IInteractable
{
    public string Message;

    public bool CanInteract()
    {
        // Always have this as a interactable element.
        return true;
    }

    public void Interact()
    {
        // Send debug message.
        Debug.Log(Message);
    }
}
