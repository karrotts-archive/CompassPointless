using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public bool CanCurrentlyInteract = true;

    private Interaction[] Interactions;

    public void Interact()
    {
        Interactions = GetComponents<Interaction>();
        foreach (Interaction interaction in Interactions)
        {
            interaction.Interact();
        }
    }
}

public interface Interaction
{
    public void Interact();
}
