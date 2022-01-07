using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInteraction : MonoBehaviour, Interaction
{
    public string Message;

    public void Interact()
    {
        Debug.Log(Message);
    }
}
