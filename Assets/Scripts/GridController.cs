using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public GameObject Marker;
    public GameObject Wall;

    // Contains Grid based items.
    public List<GridItem> GridItems;
    
    // Start is called before the first frame update
    void Start()
    {
        GridItems = new List<GridItem>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPos = GetGridPosFromMousePosition();
        bool canPlaceBlock = false;
        Marker.transform.position = currentPos;

        // check if space is available and if the player is not in the way. Render the marker as red if unavailable.
        if (!IsGridSpaceAvailable(currentPos) || Marker.GetComponent<MarkerCollision>().IsCollidingWithPlayer)
        {
            Marker.GetComponent<SpriteRenderer>().color = Color.red;
            canPlaceBlock = false;
        } 
        else 
        {
            Marker.GetComponent<SpriteRenderer>().color = Color.green;
            canPlaceBlock = true;
        }

        // Perform action if placing block is available
        if (Input.GetMouseButton(0) && canPlaceBlock)
        {
            GridItem item = new GridItem();
            item.Position = currentPos;
            item.gameObject = Instantiate(Wall, currentPos, Quaternion.identity);
            GridItems.Add(item);
        }

        // Alternative action
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GridItems.Count > 0)
            {
                int block = Random.Range(0, GridItems.Count);
                GridItems[block].gameObject.GetComponent<MoveTowardsPlayer>().Move = true;
                GridItems.RemoveAt(block);
            }
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
        foreach(GridItem item in GridItems)
        {
            if (item.Position == pos)
            {
                return false;
            }
        }
        return true;
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
