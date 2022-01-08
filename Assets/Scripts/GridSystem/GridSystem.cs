using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridSystem
{
    public static List<GridItem> GridRepository;

    private static GameObject marker;
    private static bool enabled = true;

    /// <summary>
    /// Initalizes the grid system.
    /// </summary>
    /// <param name="Marker"></param>
    public static void InitGridSystem(GameObject Marker)
    {
        marker = Marker;
        GridRepository = new List<GridItem>();
    }

    /// <summary>
    /// Converts the mouse position on screen to global block position
    /// </summary>
    /// <returns>Vector2 Grid Position</returns>
    public static Vector2 GetGridPosFromMousePosition()
    {
        Vector3 p = Input.mousePosition;
        Vector3 pos = Camera.main.ScreenToWorldPoint(p);
        pos.z = 0;

        Vector2 normalizedPos = new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
        return normalizedPos;
    }

    /// <summary>
    /// Checks to see if grid space is currently taken by another object
    /// </summary>
    /// <param name="pos">Position to check</param>
    /// <returns>Boolean value: True -> Space is not taken</returns>
    public static bool IsGridSpaceAvailable(Vector2 pos)
    {
        bool found = false;
        foreach(GridItem item in GridRepository)
        {
            if (item.Position.Equals(pos))
            {
                found = true;
            }
        }

        return !found;
    }

    /// <summary>
    /// Checks to see if space is free from other blocks or player
    /// </summary>
    public static bool CanPlaceBlock()
    {
        Vector2 pos = GetGridPosFromMousePosition();
        return (!marker.GetComponent<MarkerCollision>().IsCollidingWithPlayer
            && IsGridSpaceAvailable(pos)
            && enabled);
    }

    /// <summary>
    /// Places a new game object in the grid and instantiates it in the game world.
    /// 
    /// NOTE: GameObject must be instantiated first before placing in grid.
    /// </summary>
    /// <param name="gameObject"></param>
    public static void PlaceGameObjectInGrid(GameObject gameObject)
    {
        if (CanPlaceBlock())
        {
            GridItem gridItem = new GridItem();
            gridItem.Position = GetGridPosFromMousePosition();
            gridItem.gameObject = gameObject;
            GridRepository.Add(gridItem);
        }
    }

    /// <summary>
    /// Returns the game object at a specific position.
    /// </summary>
    /// <param name="position">Normalized position</param>
    public static GameObject GetGameObjectAtPosition(Vector2 position)
    {
        foreach(GridItem item in GridRepository)
        {
            if (position.Equals(item.Position))
            {
                return item.gameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// Finds and returns the closest game object at a position.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static GameObject GetClosestAtPosition(Vector2 position) 
    {
        GameObject closest = null;
        foreach(GridItem item in GridRepository)
        {
            closest = closest == null ? item.gameObject : closest;
            if (Vector2.Distance(item.Position, position) < Vector2.Distance(closest.transform.position, position))
            {
                closest = item.gameObject;
            }
        }
        return closest;
    }

    /// <summary>
    /// Enables or disables the grid system.
    /// </summary>
    /// <param name="enabled"></param>
    public static void SetEnabled(bool setEnabled)
    {
        enabled = setEnabled;
    }

    /// <summary>
    /// Removes and destroys game object from grid.
    /// 
    /// NOTE: GameObject must be destroyed before calling this method.
    /// </summary>
    /// <param name="position"></param>
    public static void RemoveGameObjectAtPosition(Vector2 position)
    {
        foreach (GridItem item in GridRepository)
        {
            if (position.Equals(item.Position))
            {
                GridRepository.Remove(item);
                break;
            }
        }
    }
}

/// <summary>
/// GridItem data structure
/// Used to store items to together in a list instead of dictionary for indexing.
/// </summary>
public class GridItem
{
    public Vector2 Position;
    public GameObject gameObject;
}

/// <summary>
/// Defines the type of a tile
/// </summary>
public enum TileType 
{
    NONE,
    MOVEMENT,
    ATTACK,
    PLAYER
}

/// <summary>
/// Stores data about the tile.
/// </summary>
public class GridTile
{
    public Vector2 Position;
    public TileType Type;
    public GameObject Marker;
}