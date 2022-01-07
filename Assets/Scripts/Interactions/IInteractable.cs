/// <summary>
/// Interface to allow for custom interaction based scripts
/// </summary>
public interface IInteractable
{
    /// <summary>
    /// Checks if the object is currently interactable.
    /// </summary>
    /// <returns>True if object can be interacted with.</returns>
    public bool CanInteract();

    /// <summary>
    /// Performs the interaction.
    /// 
    /// This may need to be changed to include a return 
    /// type such as a string message or bool if succeeded/failed
    /// </summary>
    public void Interact();
}
