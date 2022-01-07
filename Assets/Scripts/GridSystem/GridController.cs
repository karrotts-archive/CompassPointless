using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public GameObject Marker;

    public Color AvailableLocationColor;
    public Color UnavailableLocationColor;
    public Color InteractableLocationColor;

    public bool ShowGridPlacementMarker = true;

    // Contains Grid based items.
    public List<GridItem> GridItems;

    private bool buildEnabled = true;
    
    // Start is called before the first frame update
    void Start()
    {
        GridItems = new List<GridItem>();
    }

    // Update is called once per frame
    void Update()
    {
        RenderMarker();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            SetBuildEnabled(!buildEnabled);
        }
    }

    /// <summary>
    /// Converts the mouse position on screen to global block position
    /// </summary>
    /// <returns>Vector2 Grid Position</returns>
    public Vector2 GetGridPosFromMousePosition()
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
    public bool IsGridSpaceAvailable(Vector2 pos)
    {
        bool found = false;
        foreach(GridItem item in GridItems)
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
    public bool CanPlaceBlock()
    {
        Vector2 pos = GetGridPosFromMousePosition();
        bool canPlace = false;
        if (IsGridSpaceAvailable(pos)
            && !Marker.GetComponent<MarkerCollision>().IsCollidingWithPlayer
            && buildEnabled)
        {
            canPlace = true;
        }
        return canPlace;
    }

    /// <summary>
    /// Places a new game object in the grid and instantiates it in the game world.
    /// </summary>
    /// <param name="gameObject"></param>
    public void PlaceGameObjectInGrid(GameObject gameObject)
    {
        if (CanPlaceBlock())
        {
            GridItem gridItem = new GridItem();
            gridItem.Position = GetGridPosFromMousePosition();
            gridItem.gameObject = gameObject;
            GridItems.Add(gridItem);

            Instantiate(gameObject, GetGridPosFromMousePosition(), Quaternion.identity);
        }
    }

    /// <summary>
    /// Returns the game object at a specific position.
    /// </summary>
    /// <param name="position">Normalized position</param>
    public GameObject GetGameObjectAtPosition(Vector2 position)
    {
        foreach(GridItem item in GridItems)
        {
            if (position.Equals(item.Position))
            {
                return item.gameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// Enables or disables the build mode.
    /// </summary>
    /// <param name="enabled"></param>
    public void SetBuildEnabled(bool enabled)
    {
        ShowGridPlacementMarker = enabled;
        buildEnabled = enabled;
    }

    /// <summary>
    /// Removes and destroys game object from grid.
    /// </summary>
    /// <param name="position"></param>
    public void RemoveGameObjectAtPosition(Vector2 position)
    {
        foreach (GridItem item in GridItems)
        {
            if (position.Equals(item.Position))
            {
                GridItems.Remove(item);
            }
            Destroy(item.gameObject);
        }
    }

    /// <summary>
    /// Moves and renders marker on screen if enabled.
    /// </summary>
    private void RenderMarker()
    {
        if (ShowGridPlacementMarker)
        {
            Marker.SetActive(true);
            Marker.transform.position = GetGridPosFromMousePosition();

            if (CanPlaceBlock())
            {
                Marker.GetComponent<SpriteRenderer>().color = AvailableLocationColor;
            }
            else
            {
                Marker.GetComponent<SpriteRenderer>().color = UnavailableLocationColor;
            }
        } 
        else
        {
            Marker.SetActive(false);
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
