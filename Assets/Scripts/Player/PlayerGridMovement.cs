using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGridMovement : MonoBehaviour
{
    /// <summary>
    /// Moves the player to a position in the game world.
    /// UPDATES THE GRID PLAYER ITEM
    /// </summary>
    public void MovePlayerToPosition(Vector2 worldPosition)
    {
        transform.position = worldPosition;

        //Move here
    }
}
