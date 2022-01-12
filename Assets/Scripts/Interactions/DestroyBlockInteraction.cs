using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlockInteraction : MonoBehaviour , IInteractable
{

    public bool CanInteract()
    {
        // Do not refuse interaction.
        return true;
    }

    public void Interact()
    {
    }
}
